﻿using SharpCommands.Flags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Text
{
    internal class HelpWriter
    {
        public void WriteHelpPage(CliApp cliApp)
        {
            var lines = new Dictionary<string, string>();
            lines.Add("name", cliApp.Name);
            lines.Add("usage", string.Format("{0} [global flags] command [command flags]", cliApp.Name));
            lines.Add("version", cliApp.Version);

            this.PrintDetails(lines);

            this.PrintCommands(cliApp.Commands);

            this.PrintFlags(new IFlag[] {
                new HelpFlag(cliApp),
                new VersionFlag(cliApp)
            }, "Global Flags:");
        }

        public void WriteHelpPage(ICommand cmd)
        {
            var lines = new Dictionary<string, string>();
            lines.Add("name", cmd.Name);

            var optionalCmd = string.Empty;

            if (cmd.Commands != null && cmd.Commands.Count() > 0)
            {
                optionalCmd = "command ";
            }

            lines.Add("usage", string.Format("{0} [{1}flags]", cmd.Name, optionalCmd));

            this.PrintDetails(lines);
            this.PrintCommands(cmd.Commands);
            this.PrintFlags(cmd.Flags);
        }

        private void PrintDetails(Dictionary<string, string> lines)
        {
            this.WriteSection("Details:", lines);
        }

        private void PrintCommands(List<ICommand> commands)
        {
            if (commands == null || commands.Count == 0)
            {
                return;
            }

            var lines = new Dictionary<string, string>();
            foreach (var command in commands)
            {
                var name = command.Name;

                if (command.Alias != null && command.Alias.Length > 0)
                {
                    name = string.Format("{0}, {1}", name, string.Join(", ", command.Alias));
                }

                lines.Add(name, command.Description);
            }

            Console.WriteLine(string.Empty);
            this.WriteSection("Commands:", lines);
        }

        private void PrintFlags(IFlag[] flags, string header = "Flags:")
        {
            var helpAliasTaken = false;
            var lines = new Dictionary<string, string>();

            if (flags != null)
            { 
                foreach (var flag in flags)
                {
                    if (flag.Alias == 'h' || flag.Name == "help")
                    {
                        helpAliasTaken = true;
                    }

                    lines.Add(string.Format("-{0}, --{1}", flag.Alias, flag.Name), flag.Description);
                }
            }

            if (!helpAliasTaken)
            {
                lines.Add("-h, --help", "Show help for this command");
            }

            Console.WriteLine(string.Empty);
            this.WriteSection(header, lines);
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
