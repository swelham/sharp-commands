using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Support
{
    internal class ArgsExpectation<T>
    {
        public ArgsExpectation(string[] args, T output)
        {
            this.Args = args;
            this.Output = output;
        }

        public readonly string[] Args;

        public readonly T Output;
    }
}
