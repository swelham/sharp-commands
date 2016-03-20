using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Flags
{
    internal class HelpFlag : IFlag
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
    }
}
