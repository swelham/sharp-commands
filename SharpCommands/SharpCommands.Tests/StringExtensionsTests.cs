using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCommands.Text;

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
    }
}
