using Sprache;
using System;
using System.Collections.Generic;
using System.Text;
using TriteMySqlParser.Commands;
using TriteMySqlParser.Parsers;
using TriteMySqlParser.Parsers.Actions;
using TriteMySqlParser.Types;

namespace TriteMySqlParser
{
    public class MySqlParser
    {
        internal static readonly Parser<ICommand> parseAny =
            Comments.AnyComment.Token()
            .Or(Tables.DropTableIfExists.Token())
            .Or(Tables.CreateTable.Token())
            .Or(Rows.InsertIntoTable.Token())
            .Or(Tables.LockTables.Token())
            .Or(Tables.UnlockTables.Token());

        internal static readonly Parser<IEnumerable<ICommand>> parseAll = parseAny.Many().End();

        public static DataPool ParseMySqlDump(string dump)
        {
            DataPoolGlobal.Pool.Reset();
            var commands = parseAll.Parse(dump);
            foreach (var command in commands)
            {
                command.Execute();
            }
            return DataPoolGlobal.Pool;
        }
    }
}
