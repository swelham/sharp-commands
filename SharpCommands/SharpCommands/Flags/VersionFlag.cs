using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Flags
{
    internal class VersionFlag : IFlag
    {
        private readonly CliApp _cliApp;

        public VersionFlag(CliApp cliApp)
        {
            _cliApp = cliApp;
        }

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

        public string Description
        {
            get
            {
                return string.Format("Shows current {0} version", _cliApp.Name);
            }
        }
    }
}
