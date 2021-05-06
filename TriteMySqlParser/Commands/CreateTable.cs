using System;
using System.Collections.Generic;
using System.Text;
using TriteMySqlParser.Types;

namespace TriteMySqlParser.Commands
{
    public class CreateTable : ICommand
    {
        public string Name { get; set; }

        public IEnumerable<ICommand> ColumnCommands { get; set; }

        public CreateTable(string name)
        {
            Name = name;
            ColumnCommands = null;
        }

        public CreateTable(string name, IEnumerable<ICommand> columnCommands)
        {
            Name = name;
            ColumnCommands = columnCommands;
        }

        public string Execute()
        {
            var result = $"Added new table. Name: {Name}.\nColumns:\n";
            DataPoolGlobal.Pool.CreateTable(Name);
            foreach (var col in ColumnCommands)
            {
                result += " [" + col.Execute() + "] \n";
            }
            return result;
        }
    }
}
