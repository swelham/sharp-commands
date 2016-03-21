﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCommands.Text;
using SharpCommands.Tests.Fixtures.Flags;

namespace SharpCommands.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void IsFlag_Should_Return_True_For_Valid_Flag()
        {
            var flags = new[] { "-v", "--valid" };

            foreach (var flag in flags)
            {
                Assert.IsTrue(flag.IsFlag());
            }
        }

        [TestMethod]
        public void IsFlag_Should_Return_False_For_Invalid_Flag()
        {
            Assert.IsFalse("invalid".IsFlag());
        }

        [TestMethod]
        public void IsFlagMatch_Should_Return_True_For_Matched_Flag()
        {
            var flag = new TestFlag();

            Assert.IsTrue("-t".IsFlagMatch(flag));
            Assert.IsTrue("--test".IsFlagMatch(flag));
        }

        [TestMethod]
        public void IsFlagMatch_Should_Return_False_For_Unmatched_Flag()
        {
            var flag = new TestFlag();

            Assert.IsFalse("-i".IsFlagMatch(flag));
            Assert.IsFalse("--invalid".IsFlagMatch(flag));
        }
    }
}
