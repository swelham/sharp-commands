using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands
{
    public class CliAppBuilder
    {
        private CliApp _cliApp;

        public CliAppBuilder(CliApp cliApp)
        {
            _cliApp = cliApp;
        }

        public CliAppBuilder Version(string version)
        {
            _cliApp.Version = version;
            return this;
        }

        /*
            TODO: this method has some duplication with CliCommandBuilder.Command<T>
        */
        public CliAppBuilder Command<T>() where T : ICommand
        {
            if (_cliApp.Commands == null)
            {
                _cliApp.Commands = new List<ICommand>();
            }

            _cliApp.Commands.Add(Activator.CreateInstance<T>());

            return this;
        }

        /*
            TODO: this method has some duplication with CliCommandBuilder.Command<T>(Action<CliCommandBuilder> action)
        */
        public CliAppBuilder Command<T>(Action<CliCommandBuilder> action) where T : ICommand
        {
            this.Command<T>();

            var cmdBuilder = new CliCommandBuilder(_cliApp.Commands.Last());

            action.Invoke(cmdBuilder);

            return this;
        }

        public CliApp Create()
        {
            return _cliApp;
        }

        public void Parse(string[] args)
        {
            _cliApp.Parse(args);
        }
    }
}
