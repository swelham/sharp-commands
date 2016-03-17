using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands
{
    public class CliApp
    {
        public CliApp(string name)
        {
            this.Name = name;
            this.Version = "0.0.0.0";
        }

        public string Name { get; private set; }

        public string Version { get; set; }
    }
}
