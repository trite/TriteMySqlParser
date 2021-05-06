using System;
using System.Collections.Generic;
using System.Text;

namespace TriteMySqlParser.Commands
{
    public class Ignore : ICommand
    {
        public string Name { get; set; }

        public Ignore(string name)
        {
            Name = name;
        }

        public string Execute()
        {
            // Do nothing, commands of this type are not intended to actually do anything
            return $"Ignored - {Name}";
        }
    }
}
