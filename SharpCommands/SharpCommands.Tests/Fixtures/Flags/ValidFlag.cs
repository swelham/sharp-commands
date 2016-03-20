using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Fixtures.Flags
{
    internal class ValidFlag : IFlag
    {
        public string Name
        {
            get
            {
                return "valid";
            }
        }

        public char Alias
        {
            get
            {
                return 'v';
            }
        }
    }
}
