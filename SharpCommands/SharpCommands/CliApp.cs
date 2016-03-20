using SharpCommands.Commands;
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

        public ICommand[] Commands { get; set; }

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
                var cmd = this.Commands.SingleOrDefault(c =>
                    c.Name == args[0] ||
                    (c.Aliases != null && c.Aliases.Any(a => a == args[0])));

                if (cmd == null)
                {
                    Console.Write(string.Format("The command '{0}' was not found", args[0]));
                    return;
                }

                context.Run(cmd);
            }
        }
    }
}
