﻿using SharpCommands.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands
{
    public class RunContext
    {
        private readonly string[] _args;

        private ICommand _cmd;

        public RunContext(string[] args)
        {
            _args = args;
        }

        internal RunContext(string[] args, ICommand command)
        {
            _args = args;
            _cmd = command;
        }

        public bool HasArgs()
        {
            return _args.Count() > 0;
        }

        public bool HasFlag<T>() where T : IFlag
        {
            if (_cmd == null)
            {
                throw new NullReferenceException("No instance of ICommand found in RunContext");
            }

            var flag = this.GetCommandFlag<T>();

            return _args.Any(a => a.IsFlagMatch(flag));
        }

        public string FlagValue<T>() where T : IFlag
        {
            return this.FlagValue<T, string>();
        }

        public TResult FlagValue<T, TResult>() where T : IFlag
        {
            var flag = this.GetCommandFlag<T>();
            var values = this.FindFlagValues(flag);

            if (values.Length == 0)
            {
                return default(TResult);
            }
            else if (values.Length > 1)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Flag value expected to be a singular value but contained {0} values",
                        values.Length));
            }

            return this.ConvertType<TResult>(values.Single());
        }

        public string[] FlagArrayValue<T>() where T : IFlag
        {
            return this.FlagArrayValue<T, string>();
        }

        public TResult[] FlagArrayValue<T, TResult>() where T : IFlag
        {
            var flag = this.GetCommandFlag<T>();
            var values = this.FindFlagValues(flag);

            return values
                .Select(v => this.ConvertType<TResult>(v))
                .ToArray();
        }

        public bool TryGetFlagValue<T>(out string value) where T : IFlag
        {
            try
            {
                value = this.FlagValue<T>();
            }
            catch (Exception)
            {
                value = null;
                return false;
            }

            return true;
        }

        public bool TryGetFlagValue<T, TResult>(out TResult value) where T : IFlag
        {
            try
            {
                value = this.FlagValue<T, TResult>();
            }
            catch (Exception)
            {
                value = default(TResult);
                return false;
            }

            return true;
        }

        public void PrintHelp()
        {
            var helpWriter = new HelpWriter();
            helpWriter.WriteHelpPage(_cmd);
        }

        public void Run(ICommand cmd)
        {
            _cmd = cmd;

            if (!this.ValidateFlags())
            {
                return;
            }

            cmd.Run(this);
        }

        private T GetCommandFlag<T>() where T : IFlag
        {
            return _cmd.Flags.OfType<T>().Single();
        }

        private T ConvertType<T>(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        private bool ValidateFlags()
        {
            foreach (var arg in _args)
            {
                if (!arg.IsFlag())
                {
                    continue;
                }

                if (_cmd.Flags == null || !_cmd.Flags.Any(f => arg.IsFlagMatch(f)))
                {
                    Console.WriteLine("Error: unknown flag '{0}'", arg);
                    return false;
                }
            }

            return true;
        }

        private string[] FindFlagValues(IFlag flag)
        {
            var values = new List<string>();

            for (int flagIndex = 0; flagIndex < _args.Length; flagIndex++)
            {
                var arg = _args[flagIndex];

                if (arg.IsFlagMatch(flag) && _args.Length - 1 > flagIndex)
                {
                    if (arg.IsChainedFlag())
                    {
                        var aliasIndex = arg.IndexOf(flag.Alias);
                        var value = _args[flagIndex + aliasIndex];
    
                        if (value.IsFlag())
                        {
                            break;
                        }

                        values.Add(value);
                    }
                    else
                    {
                        for (int valueIndex = flagIndex + 1; valueIndex < _args.Length; valueIndex++)
                        {
                            var value = _args[valueIndex];

                            if (value.IsFlag())
                            {
                                break;
                            }

                            values.Add(value);
                        }
                    }

                    break;
                }
            }

            return values.ToArray();
        }
    }
}
 