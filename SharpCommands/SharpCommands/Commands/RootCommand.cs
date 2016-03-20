using SharpCommands.Flags;
using SharpCommands.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Commands
{
    internal class RootCommand : ICommand
    {
        private readonly CliApp _cliApp;

        public RootCommand(CliApp app)
        {
            _cliApp = app;
        }

        public string[] Aliases
        {
            get
            {
                return null;
            }
        }

        public List<ICommand> Commands { get; set; }

        public string Description
        {
            get
            {
                return string.Empty;
            }
        }

        public IFlag[] Flags
        {
            get
            {
                return new IFlag[]
                {
                    new VersionFlag(_cliApp),
                    new HelpFlag(_cliApp)
                };
            }
        }

        public string Name
        {
            get
            {
                return string.Empty;
            }
        }

        public void Run(RunContext context)
        {
            if (!context.HasArgs() || context.HasFlag<HelpFlag>())
            {
                var helpWriter = new HelpWriter();
                helpWriter.WriteHelpPage(_cliApp);
            }
            else if (context.HasFlag<VersionFlag>())
            {
                Console.Write(_cliApp.Version);
            }
        }
    }
}
