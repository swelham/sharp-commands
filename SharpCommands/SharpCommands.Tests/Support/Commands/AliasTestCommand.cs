using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Support.Commands
{
    internal class AliasTestCommand : ICommand
    {
        public string[] Aliases
        {
            get
            {
                return new[] { "ac" };
            }
        }

        public string Description
        {
            get
            {
                return "A aliased command used for unit tests";
            }
        }

        public string Name
        {
            get
            {
                return "alias-cmd";
            }
        }

        public void Run()
        {
            Console.Write("alias-cmd#run");
        }
    }
}
