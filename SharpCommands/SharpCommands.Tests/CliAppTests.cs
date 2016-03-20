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
        public void Should_Output_Cli_Help_For_Flags()
        {
            var flags = new[] { "-h", "--help" };
            var expected = TestFixtures.NoCommandHelpScreen();
            var app = new CliApp(CLI_APP_NAME);
            app.Version = CLI_APP_VERSION;

            foreach (var flag in flags)
            {
                using (var console = new ConsoleOut())
                {
                    app.Parse(new string[] { flag });

                    Assert.AreEqual(expected, console.GetOuput());
                }
            }
        }

        [TestMethod]
        public void Should_Output_Version_Number()
        {
            var flags = new[] { "-v", "--version" };
            var app = new CliApp(CLI_APP_NAME);
            app.Version = CLI_APP_VERSION;

            foreach (var flag in flags)
            {
                using (var console = new ConsoleOut())
                {
                    app.Parse(new string[] { flag });

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
            var expectations = new[] {
                new ArgsExpectation<CommandNotFoundExpectation>(
                    new[] { "does_not_exist" },
                    new CommandNotFoundExpectation("does_not_exist")),

                new ArgsExpectation<CommandNotFoundExpectation>(
                    new[] { "nested-cmd", "nested_does_not_exist" },
                    new CommandNotFoundExpectation("nested_does_not_exist"))
            };

            var app = new CliApp(CLI_APP_NAME);
            app.Commands = new List<ICommand>
            {
                new NestedTestCommand
                {
                    Commands = new List<ICommand>
                    {
                        new SimpleTestCommand()
                    }
                }
            };

            foreach (var expectation in expectations)
            {
                using (var console = new ConsoleOut())
                {
                    app.Parse(expectation.Args);

                    Assert.AreEqual(expectation.Output.ToString(), console.GetOuput());
                }
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

        [TestMethod]
        public void Should_Output_Command_Help()
        {
            var expect = TestFixtures.CommandHelpScreen();
            var argsList = new[]
            {
                new[] { "flags-cmd", "-h" },
                new[] { "fc", "-h" }
            };

            var app = new CliApp(CLI_APP_NAME);
            app.Version = CLI_APP_VERSION;
            app.Commands = new List<ICommand>
            {
                new FlagsTestCommand()
            };

            foreach (var args in argsList)
            {
                using (var console = new ConsoleOut())
                {
                    app.Parse(args);

                    Assert.AreEqual(expect, console.GetOuput());
                }
            }
        }
    }
}
