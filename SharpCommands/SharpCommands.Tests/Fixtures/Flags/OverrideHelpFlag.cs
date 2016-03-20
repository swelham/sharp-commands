using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Fixtures.Flags
{
    class OverrideHelpFlag : IFlag
    {
        public string Name
        {
            get
            {
                return "help";
            }
        }

        public char Alias
        {
            get
            {
                return 'h';
            }
        }

        public string Description
        {
            get
            {
                return "This flag overrides the help flag";
            }
        }
    }
}
