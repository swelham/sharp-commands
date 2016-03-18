using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands
{
    public class CliApp
    {
        public CliApp(string name)
        {
            this.Name = name;
            this.Version = "0.0.0.0";
        }

        public string Name { get; private set; }

        public string Version { get; set; }

        public ICommand[] Commands { get; set; }

        public void Parse(params string[] args)
        {
            if (args == null || args.Count() == 0)
            {
                this.WriteHelpPage();
                return;
            }
                       
            if (args[0] == "-v" || args[0] == "--version")
            {
                Console.Write(this.Version);
            }
            else if (args[0] == "-h" || args[0] == "--help")
            {
                this.WriteHelpPage();
            }
        }

        private void WriteHelpPage()
        {
            var details = new Dictionary<string, string>();
            details.Add("name", this.Name);
            details.Add("usage", string.Format("{0} [global options] command [command options]", this.Name));
            details.Add("version", this.Version);

            this.WriteSection("Details:", details);
            Console.WriteLine(string.Empty);

            if (this.Commands != null)
            {
                var commands = new Dictionary<string, string>();
                foreach (var command in this.Commands)
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
            globalOptions.Add("--help, -h", string.Format("Shows {0} help", this.Name));
            globalOptions.Add("--version, -v", string.Format("Shows current {0} version", this.Name));

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
