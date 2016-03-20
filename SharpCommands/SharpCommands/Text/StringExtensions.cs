using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Text
{
    public static class StringExtensions
    {
        public static bool IsFlag(this string str)
        {
            return str.StartsWith("-");
        }
    }
}
