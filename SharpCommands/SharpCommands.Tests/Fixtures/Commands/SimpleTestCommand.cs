﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Fixtures.Commands
{
    internal class SimpleTestCommand : ICommand
    {
        public const string RUN_OUTPUT = "simple-cmd#run";

        public string Alias
        {
            get
            {
                return null;
            }
        }

        public List<ICommand> Commands { get; set; }

        public string Description
        {
            get
            {
                return "A simple command used for unit tests";
            }
        }

        public IFlag[] Flags
        {
            get
            {
                return null;
            }
        }

        public string Name
        {
            get
            {
                return "simple-cmd";
            }
        }

        public void Run(RunContext context)
        {
            Console.Write(RUN_OUTPUT);
        }
    }
}
