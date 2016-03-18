using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Support
{
    internal class TestCommand : ICommand
    {
        public string Description
        {
            get
            {
                return "The test command used for unit tests";
            }
        }

        public string Name
        {
            get
            {
                return "test-cmd";
            }
        }
    }
}
