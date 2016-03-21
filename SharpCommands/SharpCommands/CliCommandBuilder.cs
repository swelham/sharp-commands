using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands
{
    public class CliCommandBuilder
    {
        private ICommand _command;

        public CliCommandBuilder(ICommand command)
        {
            _command = command;
        }

        public virtual CliCommandBuilder Command<T>() where T : ICommand
        {
            if (_command.Commands == null)
            {
                _command.Commands = new List<ICommand>();
            }

            _command.Commands.Add(Activator.CreateInstance<T>());

            return this;
        }

        public virtual CliCommandBuilder Command<T>(Action<CliCommandBuilder> action) where T : ICommand
        {
            this.Command<T>();

            var cmdBuilder = new CliCommandBuilder(_command.Commands.Last());

            action.Invoke(cmdBuilder);

            return this;
        }
    }
}
