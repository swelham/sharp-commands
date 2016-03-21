using SharpCommands.Tests.Fixtures.Flags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Fixtures.Commands
{
    internal class FlagsTestCommand : ICommand
    {
        public const string RUN_OUTPUT = "flags-cmd#run";

        public string Alias
        {
            get
            {
                return "fc";
            }
        }

        public List<ICommand> Commands { get; set; }

        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IFlag[] Flags
        {
            get
            {
                return new IFlag[]
                {
                    new TestFlag(),
                    new ValidFlag()
                };
            }
        }

        public string Name
        {
            get
            {
                return "flags-cmd";
            }
        }

        public void Run(RunContext context)
        {
            if (context.HasFlag<ValidFlag>())
            {
                Console.Write(RUN_OUTPUT);
            }
            else
            {
                Console.Write("No_Flags");
            }
        }
    }
}
