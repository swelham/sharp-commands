using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCommands.Tests.Support;

namespace SharpCommands.Tests
{
    [TestClass]
    public class CliAppTests
    {
        const string CLI_APP_NAME = "test_app";
        const string CLI_APP_VERSION = "1.0.0.0";

        [TestMethod]
        public void Should_Have_Default_Values()
        {
            var app = new CliApp(CLI_APP_NAME);

            Assert.AreEqual(CLI_APP_NAME, app.Name);
            Assert.AreEqual("0.0.0.0", app.Version);
        }

        [TestMethod]
        public void Should_Output_Cli_Help_When_No_Command_Specified()
        {
            var app = new CliApp(CLI_APP_NAME);
            app.Version = CLI_APP_VERSION;

            using (var console = new ConsoleOut())
            {
                app.Parse(new string[] { });

                Assert.AreEqual(Fixtures.NoCommandHelpScreen(), console.GetOuput());
            }
        }

        [TestMethod]
        public void Should_Output_Cli_Help_With_Commands_When_No_Command_Specified()
        {
            var app = new CliApp(CLI_APP_NAME);
            app.Version = CLI_APP_VERSION;
            app.Commands = Fixtures.TestCommands();

            using (var console = new ConsoleOut())
            {
                app.Parse(new string[] { });

                Assert.AreEqual(Fixtures.NoCommandHelpScreenWithCommands(), console.GetOuput());
            }
        }

        [TestMethod]
        public void Should_Output_Cli_Help_For_Option()
        {
            var options = new[] { "-h", "--help" };
            var expected = Fixtures.NoCommandHelpScreen();
            var app = new CliApp(CLI_APP_NAME);
            app.Version = CLI_APP_VERSION;

            foreach (var option in options)
            {
                using (var console = new ConsoleOut())
                {
                    app.Parse(new string[] { option });

                    Assert.AreEqual(expected, console.GetOuput());
                }
            }
        }

        [TestMethod]
        public void Should_Output_Version_Number()
        {
            var options = new[] { "-v", "--version" };
            var app = new CliApp(CLI_APP_NAME);
            app.Version = CLI_APP_VERSION;

            foreach (var option in options)
            {
                using (var console = new ConsoleOut())
                {
                    app.Parse(new string[] { option });

                    Assert.AreEqual(CLI_APP_VERSION, console.GetOuput());
                }
            }
        }
    }
}
