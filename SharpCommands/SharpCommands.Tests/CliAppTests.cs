using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCommands.Tests.Support;
using SharpCommands.Tests.Fixtures;
using SharpCommands.Tests.Fixtures.Commands;
using System.Collections.Generic;

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

                Assert.AreEqual(TestFixtures.NoCommandHelpScreen(), console.GetOuput());
            }
        }

        [TestMethod]
        public void Should_Output_Cli_Help_With_Commands_When_No_Command_Specified()
        {
            var app = new CliApp(CLI_APP_NAME);
            app.Version = CLI_APP_VERSION;
            app.Commands = new List<ICommand>
            {
                new SimpleTestCommand(),
                new AliasTestCommand()
            };

            using (var console = new ConsoleOut())
            {
                app.Parse(new string[] { });

                Assert.AreEqual(TestFixtures.NoCommandHelpScreenWithCommands(), console.GetOuput());
            }
        }

        [TestMethod]
        public void Should_Output_Cli_Help_For_Option()
        {
            var options = new[] { "-h", "--help" };
            var expected = TestFixtures.NoCommandHelpScreen();
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

        [TestMethod]
        public void Should_Run_Command()
        {
            var args = new[] { "simple-cmd" };
            var app = new CliApp(CLI_APP_NAME);
            app.Commands = new List<ICommand>
            {
                new SimpleTestCommand()
            };

            using (var console = new ConsoleOut())
            {
                app.Parse(args);

                Assert.AreEqual(SimpleTestCommand.RUN_OUTPUT, console.GetOuput());
            }
        }

        [TestMethod]
        public void Should_Run_Command_By_Alias()
        {
            var args = new[] { "ac" };
            var app = new CliApp(CLI_APP_NAME);
            app.Commands = new List<ICommand>
            {
                new AliasTestCommand()
            };

            using (var console = new ConsoleOut())
            {
                app.Parse(args);

                Assert.AreEqual(AliasTestCommand.RUN_OUTPUT, console.GetOuput());
            }
        }

        [TestMethod]
        public void Should_Print_Command_Not_Found()
        {
            var args = new[] { "does_not_exist" };
            var app = new CliApp(CLI_APP_NAME);
            app.Commands = new List<ICommand>
            {
                new SimpleTestCommand()
            };

            using (var console = new ConsoleOut())
            {
                app.Parse(args);

                Assert.AreEqual("The command 'does_not_exist' was not found", console.GetOuput());
            }
        }

        [TestMethod]
        public void Should_Run_Nested_Command()
        {
            var argsList = new[] {
                new[] { "nested-cmd", "alias-cmd" },
                new[] { "ntc", "ac" }
            };
            var app = new CliApp(CLI_APP_NAME);
            app.Commands = new List<ICommand>
            {
                new NestedTestCommand
                {
                    Commands = new List<ICommand>
                    {
                        new AliasTestCommand()
                    }
                }
            };

            foreach (var args in argsList)
            {
                using (var console = new ConsoleOut())
                {
                    app.Parse(args);

                    Assert.AreEqual(AliasTestCommand.RUN_OUTPUT, console.GetOuput());
                }
            }
        }

        [TestMethod]
        public void Should_Run_Command_With_Flag()
        {
            var argsList = new[] {
                new[] { "flags-cmd", "-v" },
                new[] { "flags-cmd", "--valid" }
            };
            var app = new CliApp(CLI_APP_NAME);
            app.Commands = new List<ICommand>
            {
                new FlagsTestCommand()
            };

            foreach (var args in argsList)
            {
                using (var console = new ConsoleOut())
                {
                    app.Parse(args);

                    Assert.AreEqual(FlagsTestCommand.RUN_OUTPUT, console.GetOuput());
                }
            }
        }
    }
}
