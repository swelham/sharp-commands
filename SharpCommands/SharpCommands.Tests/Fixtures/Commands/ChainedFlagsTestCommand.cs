using SharpCommands.Tests.Fixtures.Flags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Fixtures.Commands
{
    internal class ChainedFlagsTestCommand : ICommand
    {
        public string Name
        {
            get
            {
                return "chained-flags-cmd";
            }
        }

        public string Alias
        {
            get
            {
                return "cf";
            }
        }

        public List<ICommand> Commands { get; set; }

        public string Description
        {
            get
            {
                return "A test command used for testing chained flags";
            }
        }

        public IFlag[] Flags
        {
            get
            {
                return new IFlag[]
                {
                    new TestFlag(),
                    new ValidFlag(),
                    new OverrideHelpFlag()
                };
            }
        }

        public void Run(RunContext context)
        {
            throw new NotImplementedException();
        }
    }
}
