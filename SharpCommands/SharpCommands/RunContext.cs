using SharpCommands.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands
{
    public class RunContext
    {
        private readonly string[] _args;

        private ICommand _cmd;

        public RunContext(string[] args)
        {
            _args = args;
        }

        public RunContext(string[] args, ICommand command)
        {
            _args = args;
            _cmd = command;
        }

        public bool HasArgs()
        {
            return _args.Count() > 0;
        }

        public bool HasFlag<T>() where T : IFlag
        {
            if (_cmd == null)
            {
                throw new NullReferenceException("No instance of ICommand found in RunContext");
            }

            var flag = _cmd.Flags.OfType<T>().Single();

            return _args.Any(a => a.IsFlagMatch(flag));
        }

        public void Run(ICommand cmd)
        {
            _cmd = cmd;
            cmd.Run(this);
        }
    }
}
 