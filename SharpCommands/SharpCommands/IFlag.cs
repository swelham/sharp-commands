using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands
{
    public interface IFlag
    {
        string Name { get; }

        char Alias { get; }
    }
}
