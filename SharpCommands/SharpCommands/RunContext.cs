using SharpCommands.Text;
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
            var flag = this.GetCommandFlag<T>();
            string flagValue = string.Empty;

            for (int i = 0; i < _args.Length; i++)
            {
                var arg = _args[i];

                if (arg.IsFlagMatch(flag) && _args.Length - 1 > i)
                {
                    if (arg.IsChainedFlag())
                    {
                        var aliasIndex = arg.IndexOf(flag.Alias);
                        flagValue = _args[i + aliasIndex];
                    }
                    else
                    {
                        flagValue = _args[i + 1];
                    }

                    break;
                }
            }

            if (flagValue.IsFlag())
            {
                return string.Empty;
            }

            return flagValue;
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
    }
}
 