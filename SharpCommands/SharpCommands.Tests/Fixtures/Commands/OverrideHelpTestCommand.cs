using SharpCommands.Tests.Fixtures.Flags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Fixtures.Commands
{
    internal class OverrideHelpTestCommand : ICommand
    {
        public const string RUN_OUTPUT = "override-help-cmd#run";


        public string[] Aliases
        {
            get
            {
                return new[] { "oh" };
            }
        }

        public List<ICommand> Commands { get; set; }

        public string Description
        {
            get
            {
                return "This command overrides the help flag";
            }
        }

        public IFlag[] Flags
        {
            get
            {
                return new[]
                {
                    new OverrideHelpFlag()
                };
            }
        }

        public string Name
        {
            get
            {
                return "override-help-cmd";
            }
        }

        public void Run(RunContext context)
        {
            Console.Write(RUN_OUTPUT);
        }
    }
}
