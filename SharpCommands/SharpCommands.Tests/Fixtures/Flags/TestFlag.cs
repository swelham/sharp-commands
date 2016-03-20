using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Fixtures.Flags
{
    internal class TestFlag : IFlag
    {
        public string Name
        {
            get
            {
                return "test";
            }
        }

        public char Alias
        {
            get
            {
                return 't';
            }
        }

        public string Description
        {
            get
            {
                return "This is test flag";
            }
        }
    }
}
