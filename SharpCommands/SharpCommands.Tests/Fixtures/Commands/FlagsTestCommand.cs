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
        public string[] Aliases
        {
            get
            {
                throw new NotImplementedException();
            }
        }

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
                    new ValidFlag()
                };
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Run(RunContext context)
        {
            throw new NotImplementedException();
        }
    }
}
