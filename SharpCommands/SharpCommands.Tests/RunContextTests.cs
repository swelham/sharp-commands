using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCommands.Tests.Fixtures.Flags;
using SharpCommands.Tests.Fixtures.Commands;
using SharpCommands.Tests.Support;
using SharpCommands.Tests.Fixtures;

namespace SharpCommands.Tests
{
    [TestClass]
    public class RunContextTests
    {
        [TestMethod]
        public void HasArgs_Should_Return_False_For_No_Input_Args()
        {
            var args = new string[] { };
            var context = new RunContext(args);

            Assert.IsFalse(context.HasArgs());
        }

        [TestMethod]
        public void HasArgs_Should_Return_True_With_Input_Args()
        {
            var args = new string[] { "cmd" };
            var context = new RunContext(args);

            Assert.IsTrue(context.HasArgs());
        }

        [TestMethod]
        public void HasFlag_T_Should_Return_True_For_Matched_Flag()
        {
            var argsList = new[] {
                new[] { "-v" },
                new[] { "--valid" }
            };
            var flag = new ValidFlag();
            var cmd = new FlagsTestCommand();

            foreach (var args in argsList)
            {
                var context = new RunContext(args, cmd);

                Assert.IsTrue(context.HasFlag<ValidFlag>());
            }
        }

        [TestMethod]
        public void HasFlag_T_Should_Return_False_For_Unmatched_Flag()
        {
            var argsList = new[] {
                new[] { "-h" },
                new[] { "--help" }
            };
            var flag = new ValidFlag();
            var cmd = new FlagsTestCommand();

            foreach (var args in argsList)
            {
                var context = new RunContext(args, cmd);

                Assert.IsFalse(context.HasFlag<ValidFlag>());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void HasFlag_T_Should_Throw_NullReferenceException_With_No_Command()
        {
            var args = new string[] { };
            var flag = new ValidFlag();
            var context = new RunContext(args);

            context.HasFlag<ValidFlag>();
        }

        [TestMethod]
        public void HasFlag_T_Should_Return_True_For_Matched_Chained_Flag()
        {
            var args = new[] { "-vt" };
            var cmd = new ChainedFlagsTestCommand();

            var context = new RunContext(args, cmd);

            Assert.IsTrue(context.HasFlag<ValidFlag>());
            Assert.IsTrue(context.HasFlag<TestFlag>());
        }

        [TestMethod]
        public void HasFlag_T_Should_Return_False_For_Unmatched_Chained_Flag()
        {
            var args = new[] { "-vt" };
            var cmd = new ChainedFlagsTestCommand();

            var context = new RunContext(args, cmd);

            Assert.IsFalse(context.HasFlag<OverrideHelpFlag>());
        }

        [TestMethod]
        public void Run_Should_Execute_The_Command_Run_Method()
        {
            var cmd = new SimpleTestCommand();
            var args = new[] { "simple-cmd" };
            var context = new RunContext(args);

            using (var console = new ConsoleOut())
            {
                context.Run(cmd);

                Assert.AreEqual("simple-cmd#run", console.GetOuput());
            }
        }

        [TestMethod]
        public void Run_Should_Print_Flag_Not_Found_For_Invalid_Flag()
        {
            var expect = string.Concat("Error: unknown flag '-i'", Environment.NewLine);
            var cmd = new FlagsTestCommand();
            var args = new[] { "flags-cmd", "-i" };
            var context = new RunContext(args);

            using (var console = new ConsoleOut())
            {
                context.Run(cmd);
                Assert.AreEqual(expect, console.GetOuput());
            }
        }

        [TestMethod]
        public void FlagValue_Should_Return_The_Flag_Value()
        {
            var cmd = new FlagsTestCommand();
            var args = new[] { "flags-cmd", "-t", "flag_value" };
            var context = new RunContext(args, cmd);

            var flagValue = context.FlagValue<TestFlag>();

            Assert.AreEqual("flag_value", flagValue);
        }

        [TestMethod]
        public void FlagValue_Should_Not_Return_A_Flag_Value()
        {
            var cmd = new FlagsTestCommand();
            var argsList = new[] {
                new[] { "flags-cmd", "-t" },
                new[] { "flags-cmd", "-t", "-v"}
            };

            foreach (var args in argsList)
            {
                var context = new RunContext(args, cmd);

                var flagValue = context.FlagValue<TestFlag>();

                Assert.AreEqual(null, flagValue);
            }
        }

        [TestMethod]
        public void FlagValue_Should_Return_A_Flag_Value_For_Chained_Flag()
        {
            var cmd = new FlagsTestCommand();
            var args = new[] { "flags-cmd", "-tv", "test_flag_value", "valid_flag_value" };
            var context = new RunContext(args, cmd);

            Assert.AreEqual("test_flag_value", context.FlagValue<TestFlag>());
            Assert.AreEqual("valid_flag_value", context.FlagValue<ValidFlag>());
        }

        [TestMethod]
        public void PrintHelp_Should_Print_Active_Command_Help_Screen()
        {
            var cmd = new PrintHelpTestCommand();
            var args = new[] { "print-help-cmd", };
            var context = new RunContext(args, cmd);

            using (var console = new ConsoleOut())
            {
                context.PrintHelp();
                Assert.AreEqual(TestFixtures.PrintHelpCommandHelpScreen(), console.GetOuput());
            }
        }

        [TestMethod]
        public void FlagValue_Should_Return_Typed_Default_Value_For_Empty_Flag_Value()
        {
            var cmd = new FlagsTestCommand();
            var args = new[] { "flags-cmd", "-t" };
            var context = new RunContext(args, cmd);

            var result = context.FlagValue<TestFlag, int>();

            Assert.AreEqual(default(int), result);
        }

        [TestMethod]
        public void FlagValue_Should_Return_A_Typed_Flag_Value()
        {
            var cmd = new FlagsTestCommand();
            var args = new[] { "flags-cmd", "-t", "12345" };
            var context = new RunContext(args, cmd);

            var result = context.FlagValue<TestFlag, int>();

            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(12345, result);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void FlagValue_Should_Throw_FormatException_For_Invalid_Flag_Value()
        {
            var cmd = new FlagsTestCommand();
            var args = new[] { "flags-cmd", "-t", "abc" };
            var context = new RunContext(args, cmd);

            context.FlagValue<TestFlag, int>();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FlagValue_Should_Throw_InvalidOperationException_For_Multiple_Values_With_Non_Array_Return_Type()
        {
            var cmd = new FlagsTestCommand();
            var args = new[] { "flags-cmd", "-t", "1", "2", "3" };
            var context = new RunContext(args, cmd);

            context.FlagValue<TestFlag>();
        }
    }
}
