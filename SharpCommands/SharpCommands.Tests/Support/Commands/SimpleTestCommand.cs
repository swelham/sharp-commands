using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Support.Commands
{
    internal class SimpleTestCommand : ICommand
    {
        public string[] Aliases
        {
            get
            {
                return null;
            }
        }

        public string Description
        {
            get
            {
                return "A simple command used for unit tests";
            }
        }

        public string Name
        {
            get
            {
                return "simple-cmd";
            }
        }

        public void Run()
        {
            Console.Write("simple-cmd#run");
        }
    }
}
