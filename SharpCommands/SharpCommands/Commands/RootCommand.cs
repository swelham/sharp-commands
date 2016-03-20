using SharpCommands.Flags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Commands
{
    internal class RootCommand : ICommand
    {
        private readonly CliApp _cliApp;

        public RootCommand(CliApp app)
        {
            _cliApp = app;
        }

        public string[] Aliases
        {
            get
            {
                return null;
            }
        }

        public string Description
        {
            get
            {
                return string.Empty;
            }
        }

        public IFlag[] Flags
        {
            get
            {
                return new IFlag[]
                {
                    new VersionFlag(),
                    new HelpFlag()
                };
            }
        }

        public string Name
        {
            get
            {
                return string.Empty;
            }
        }

        public void Run(RunContext context)
        {
            if (!context.HasArgs() || context.HasFlag<HelpFlag>())
            {
                this.WriteHelpPage();
            }
            else if (context.HasFlag<VersionFlag>())
            {
                Console.Write(_cliApp.Version);
            }
        }

        private void WriteHelpPage()
        {
            var details = new Dictionary<string, string>();
            details.Add("name", _cliApp.Name);
            details.Add("usage", string.Format("{0} [global options] command [command options]", _cliApp.Name));
            details.Add("version", _cliApp.Version);

            this.WriteSection("Details:", details);
            Console.WriteLine(string.Empty);

            if (_cliApp.Commands != null)
            {
                var commands = new Dictionary<string, string>();
                foreach (var command in _cliApp.Commands)
                {
                    var name = command.Name;

                    if (command.Aliases != null && command.Aliases.Length > 0)
                    {
                        name = string.Format("{0}, {1}", name, string.Join(", ", command.Aliases));
                    }

                    commands.Add(name, command.Description);
                }
                this.WriteSection("Commands:", commands);
                Console.WriteLine(string.Empty);
            }

            var globalOptions = new Dictionary<string, string>();
            globalOptions.Add("--help, -h", string.Format("Shows {0} help", _cliApp.Name));
            globalOptions.Add("--version, -v", string.Format("Shows current {0} version", _cliApp.Name));

            this.WriteSection("Global Options:", globalOptions);
        }

        private void WriteSection(string header, Dictionary<string, string> items)
        {
            if (items.Count == 0)
            {
                return;
            }

            Console.WriteLine(header);

            var valueStartIndex = items.Max(i => i.Key.Length) + 4;

            foreach (var item in items)
            {
                var builder = new StringBuilder("    ");
                builder.Append(item.Key);
                builder.Append(' ', valueStartIndex - item.Key.Length);
                builder.Append(item.Value);

                Console.WriteLine(builder.ToString());
            }
        }
    }
}
