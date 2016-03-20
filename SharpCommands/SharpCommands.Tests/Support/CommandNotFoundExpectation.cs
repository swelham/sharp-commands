using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Support
{
    internal class CommandNotFoundExpectation
    {
        private readonly string _cmdName;

        public CommandNotFoundExpectation(string cmdName)
        {
            _cmdName = cmdName;
        }

        public override string ToString()
        {
            return string.Format("The command '{0}' was not found{1}", _cmdName, Environment.NewLine);
        }
    }
}
