using System;
using System.Collections.Generic;
using System.Text;
using TriteMySqlParser.Types;

namespace TriteMySqlParser.Commands
{
    public class InsertIntoTable : ICommand
    {
        public string Name { get; set; }

        public string TableName { get; set; }

        public IEnumerable<ICommand> RowsToInsert { get; set; }

        public InsertIntoTable(string name, string tableName, IEnumerable<ICommand> rowsToInsert)
        {
            Name = name;
            TableName = tableName;
            RowsToInsert = rowsToInsert;
        }

        public string Execute()
        {
            var result = $"Inserting rows for Table: {TableName} (\n\t";
            DataPoolGlobal.Pool.SetCurrentTable(TableName);
            foreach (var row in RowsToInsert)
            {
                result += row.Execute() + "\n\t";
            }
            return result.Trim('\t') + ")";
        }
    }
}
