using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands
{
    public interface ICommand
    {
        string Name { get; }

        string Description { get; }

        string[] Aliases { get; }

        IFlag[] Flags { get; }

        List<ICommand> Commands { get; set; }

        void Run(RunContext context);
    }
}
