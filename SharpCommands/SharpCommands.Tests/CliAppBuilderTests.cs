using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCommands.Tests.Support;
using SharpCommands.Tests.Fixtures.Commands;
using System.Linq;

namespace SharpCommands.Tests
{
    [TestClass]
    public class CliAppBuilderTests
    {
        [TestMethod]
        public void Build_Should_Create_A_CliApp_With_Defaults()
        {
            var app = CliApp
                .Build(Constants.CLI_APP_NAME)
                .Create();

            Assert.AreEqual(Constants.CLI_APP_NAME, app.Name);
            Assert.AreEqual(Constants.CLI_APP_DEFAULT_VERSION, app.Version);
        }

        [TestMethod]
        public void Version_Should_Set_CliApp_Version()
        {
            var app = CliApp
                .Build(Constants.CLI_APP_NAME)
                .Version(Constants.CLI_APP_VERSION)
                .Create();

            Assert.AreEqual(Constants.CLI_APP_VERSION, app.Version);
        }

        [TestMethod]
        public void Command_Should_Add_Instance_Of_ICommand_To_CliApp()
        {
            var app = CliApp
                .Build(Constants.CLI_APP_NAME)
                .Command<SimpleTestCommand>()
                .Create();

            Assert.AreEqual(1, app.Commands.Count);
            Assert.IsInstanceOfType(app.Commands.Single(), typeof(SimpleTestCommand));
        }

        [TestMethod]
        public void Command_Should_Add_Nested_Instance_Of_ICommand_To_Command()
        {
            var app = CliApp
                .Build(Constants.CLI_APP_NAME)
                .Command<SimpleTestCommand>(b =>
                    b.Command<AliasTestCommand>(b2 =>
                        b2.Command<PrintHelpTestCommand>()
                          .Command<FlagsTestCommand>()
                    )
                )
                .Create();

            var simpleCommands = app.Commands.First().Commands;
            var aliasCommands = simpleCommands.Single().Commands;

            Assert.AreEqual(1, simpleCommands.Count);
            Assert.IsInstanceOfType(simpleCommands.Single(), typeof(AliasTestCommand));

            Assert.AreEqual(2, aliasCommands.Count);
            Assert.IsInstanceOfType(aliasCommands[0], typeof(PrintHelpTestCommand));
            Assert.IsInstanceOfType(aliasCommands[1], typeof(FlagsTestCommand));
        }

        [TestMethod]
        public void Parse_Should_Run_The_CliApp()
        {
            var args = new[] { "simple-cmd" };

            using (var console = new ConsoleOut())
            {
                CliApp
                    .Build(Constants.CLI_APP_NAME)
                    .Command<SimpleTestCommand>()
                    .Parse(args);

                Assert.AreEqual(SimpleTestCommand.RUN_OUTPUT, console.GetOuput());
            }
        }
    }
}
