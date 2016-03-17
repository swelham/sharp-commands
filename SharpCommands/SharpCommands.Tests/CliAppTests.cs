using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCommands.Tests.Support;

namespace SharpCommands.Tests
{
    [TestClass]
    public class CliAppTests
    {
        const string CLI_APP_NAME = "test_app";

        [TestMethod]
        public void Should_Have_Default_Values()
        {
            var app = new CliApp(CLI_APP_NAME);

            Assert.AreEqual(CLI_APP_NAME, app.Name);
            Assert.AreEqual("0.0.0.0", app.Version);
        }

        [TestMethod]
        public void Should_Output_Help_When_No_Command_Specified()
        {
            var app = new CliApp(CLI_APP_NAME);
            app.Version = "1.0.0.0";

            using (var console = new ConsoleOut())
            {
                app.Parse(new string[] { });

                Assert.AreEqual(Fixtures.NoCommandHelpScreen(), console.GetOuput());
            }
        }

        [TestMethod]
        public void Should_Output_Version_Number()
        {
            var options = new[] { "-v", "--version" };
            var version = "1.0.0.0";
            var app = new CliApp(CLI_APP_NAME);
            app.Version = version;

            foreach (var option in options)
            {
                using (var console = new ConsoleOut())
                {
                    app.Parse(new string[] { option });

                    Assert.AreEqual(version, console.GetOuput());
                }
            }
        }
    }
}
