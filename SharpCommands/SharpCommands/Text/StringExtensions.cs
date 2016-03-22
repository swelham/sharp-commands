using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Text
{
    internal static class StringExtensions
    {
        public static bool IsFlag(this string str)
        {
            return str.StartsWith("-");
        }

        public static bool IsFlagMatch(this string str, IFlag flag)
        {
            if (!str.IsFlag())
            {
                return false;
            }

            if (str.StartsWith("--"))
            {
                return str == string.Concat("--", flag.Name); ;
            }
            else
            {
                var alias = string.Concat("-", flag.Alias);

                for (int i = 1; i < str.Length; i++)
                {
                    if (str[i] == flag.Alias)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsChainedFlag(this string str)
        {
            return str.IsFlag() && !str.StartsWith("--") && str.Length > 2;
        }
    }
}
