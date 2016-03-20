using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Flags
{
    internal class VersionFlag : IFlag
    {
        public string Name
        {
            get
            {
                return "version";
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
