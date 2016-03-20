﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCommands.Tests.Fixtures.Flags;
using SharpCommands.Tests.Fixtures.Commands;
using SharpCommands.Tests.Support;

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
        public void HasFlag_Should_Return_True_For_Matched_Flag()
        {
            var argsList = new[] {
                new[] { "-v" },
                new[] { "--valid" }
            };
            var flag = new ValidFlag();

            foreach (var args in argsList)
            {
                var context = new RunContext(args);

                Assert.IsTrue(context.HasFlag(flag));
            }
        }

        [TestMethod]
        public void HasFlag_Should_Return_False_For_Unmatched_Flag()
        {
            var argsList = new[] {
                new[] { "-h" },
                new[] { "--help" }
            };
            var flag = new ValidFlag();

            foreach (var args in argsList)
            {
                var context = new RunContext(args);

                Assert.IsFalse(context.HasFlag(flag));
            }
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

        //[TestMethod]
        //public void Run_Should_Throw_NullReferenceException_With_No_Command()
        //{
        //    var args = new string[] { };
        //    var context = new RunContext(args);

        //    context
        //}
    }
}
