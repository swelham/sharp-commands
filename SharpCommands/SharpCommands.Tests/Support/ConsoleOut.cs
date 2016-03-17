using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Support
{
    public class ConsoleOut : IDisposable
    {
        private StringWriter _writer;
        private TextWriter _originalOut;

        public ConsoleOut()
        {
            _writer = new StringWriter();
            _originalOut = Console.Out;
            Console.SetOut(_writer);
        }

        public string GetOuput()
        {
            return _writer.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(_originalOut);
            _writer.Dispose();
        }
    }
}
