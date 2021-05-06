using System;
using System.Collections.Generic;
using System.Text;
using TriteMySqlParser.Types;

namespace TriteMySqlParser.Commands
{
    public class AddColumn : ICommand
    {
        public string Name { get; set; }
        public string ColName { get; set; }

        public Type ColType { get; set; }

        public AddColumn(string name, Type type)
        {
            ColName = name;
            Name = name;
            ColType = type;
        }

        public string Execute()
        {
            DataPoolGlobal.Pool.AddColumn(ColName, ColType);
            return $"Added Column: {{ColName: {ColName}, ColType: {ColType}}} for Table: {DataPoolGlobal.Pool.CurrentTable}";
        }
    }
}
