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

        public static bool IsFlagMatch(this string str, IFlag flag)
        {
            var name = string.Concat("--", flag.Name);
            var alias = string.Concat("-", flag.Alias);

            return str == name || str == alias;
        }
    }
}
