using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Flags
{
    internal class HelpFlag : IFlag
    {
        private readonly CliApp _cliApp;

        public HelpFlag(CliApp cliApp)
        {
            _cliApp = cliApp;
        }

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
                return string.Format("Shows {0} help", _cliApp.Name);
            }
        }
    }
}
