using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Fixtures.Commands
{
    internal class NestedTestCommand : ICommand
    {
        public string[] Aliases
        {
            get
            {
                return new[] { "ntc" };
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
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                return "nested-cmd";
            }
        }

        public void Run(RunContext context)
        {
            throw new NotImplementedException();
        }
    }
}
