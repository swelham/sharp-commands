using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Fixtures.Commands
{
    internal class AliasTestCommand : ICommand
    {
        public const string RUN_OUTPUT = "alias-cmd#run";

        public string[] Aliases
        {
            get
            {
                return new[] { "ac" };
            }
        }

        public List<ICommand> Commands { get; set; }

        public string Description
        {
            get
            {
                return "A aliased command used for unit tests";
            }
        }

        public IFlag[] Flags
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                return "alias-cmd";
            }
        }

        public void Run(RunContext context)
        {
            Console.Write(RUN_OUTPUT);
        }
    }
}
