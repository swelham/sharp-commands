using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpCommands.Tests
{
    [TestClass]
    public class CliAppTests
    {
        const string CLI_APP_NAME = "TestApp";

        [TestMethod]
        public void Should_Have_Default_Values()
        {
            var app = new CliApp(CLI_APP_NAME);

            Assert.AreEqual(app.Name, CLI_APP_NAME);
            Assert.AreEqual(app.Version, "0.0.0.0");
        }
    }
}
