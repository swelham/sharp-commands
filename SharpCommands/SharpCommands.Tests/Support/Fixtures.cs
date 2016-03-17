using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Support
{
    internal static class Fixtures
    {
        public static string NoCommandHelpScreen()
        {
            return ReadFixureFile("No_Command_Help_Screen.txt");
        }

        private static string ReadFixureFile(string name)
        {
            using (var stream = File.OpenText(string.Concat("..\\..\\Fixtures\\", name)))
            {
                return stream.ReadToEnd();
            }
        }
    }
}
