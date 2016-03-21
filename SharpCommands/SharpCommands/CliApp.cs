using SharpCommands.Commands;
using SharpCommands.Flags;
using SharpCommands.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands
{
    public class CliApp
    {
        private readonly ICommand _rootCommand;

        public CliApp(string name)
        {
            _rootCommand = new RootCommand(this);

            this.Name = name;
            this.Version = "0.0.0.0";
        }

        public string Name { get; private set; }

        public string Version { get; set; }

        public List<ICommand> Commands { get; set; }

        public void Parse(params string[] args)
        {
            var context = new RunContext(args);

            if (!context.HasArgs() || args.First().IsFlag())
            {
                context.Run(_rootCommand);
                return;
            }

            if (this.Commands != null)
            {
                var commands = this.Commands;

                for (int i = 0; i < args.Length; i++)
                {
                    var cmd = this.MatchCommand(args[i], commands);

                    if (cmd == null)
                    {
                        Console.Write(string.Format("The command '{0}' was not found{1}", args[i], Environment.NewLine));
                        return;
                    }

                    if (args.Length - 1 > i)
                    {
                        var nextArg = args[i + 1];

                        if (!nextArg.IsFlag())
                        {
                            commands = cmd.Commands;
                            continue;
                        }

                        var helpFlag = new HelpFlag(this);
                        if (nextArg.IsFlagMatch(helpFlag))
                        {
                            if (cmd.Flags == null || !cmd.Flags.Any(f => f.Name == helpFlag.Name || f.Alias == helpFlag.Alias))
                            {
                                var helpWriter = new HelpWriter();
                                helpWriter.WriteHelpPage(cmd);
                                return;
                            }
                        }
                    }

                    context.Run(cmd);
                    return;
                }
            }
        }

        public static CliAppBuilder Build(string name)
        {
            return new CliAppBuilder(new CliApp(name));
        }

        private ICommand MatchCommand(string cmdStr, IEnumerable<ICommand> commands)
        {
            if (commands == null || commands.Count() == 0)
            {
                return null;
            }

            return commands.SingleOrDefault(c =>
                    c.Name == cmdStr ||
                    (c.Aliases != null && c.Aliases.Any(a => a == cmdStr)));
        }
    }
}
