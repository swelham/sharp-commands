﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCommands.Tests.Fixtures.Commands
{
    internal class SimpleTestCommand : ICommand
    {
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
                return "A simple command used for unit tests";
            }
        }

        public IFlag[] Flags
        {
            get
            {
                throw new NotImplementedException();
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
            Console.Write("simple-cmd#run");
        }
    }
}