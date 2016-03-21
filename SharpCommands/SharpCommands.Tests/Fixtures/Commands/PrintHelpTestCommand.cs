using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Fixtures.Commands
{
    internal class PrintHelpTestCommand : ICommand
    {
        public string Name
        {
            get
            {
                return "print-help-cmd";
            }
        }

        public string Alias
        {
            get
            {
                return "ph";
            }
        }

        public List<ICommand> Commands { get; set; }

        public string Description
        {
            get
            {
                return "This test commands prints the command help page";
            }
        }

        public IFlag[] Flags
        {
            get
            {
                return null;
            }
        }

        public void Run(RunContext context)
        {
            context.PrintHelp();
        }
    }
}
