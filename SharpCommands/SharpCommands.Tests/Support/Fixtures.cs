using SharpCommands.Tests.Support.Commands;
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
        public static ICommand[] TestCommands()
        {
            return new ICommand[]
            {
                new SimpleTestCommand(),
                new AliasTestCommand()
            };
        }

        public static string NoCommandHelpScreen()
        {
            return ReadFixureFile("Cli_Help_Screen.txt");
        }

        public static string NoCommandHelpScreenWithCommands()
        {
            return ReadFixureFile("Cli_Help_Screen_With_Commands.txt");
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
