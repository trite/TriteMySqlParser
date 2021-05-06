using Sprache;
using TriteMySqlParser.Commands;
using TriteMySqlParser.Parsers.Keywords;
using TriteMySqlParser.Parsers.Others;

namespace TriteMySqlParser.Parsers.Actions
{
    public class Tables
    {
        public static readonly Parser<ICommand> DropTableIfExists =
            from ignoreDrop in TableActions.Drop
            from ignoreTable in TableActions.Table
            from ignoreIf in TableActions.If
            from ignoreExists in TableActions.Exists
            from content in Identifiers.Identifier
            from ignoreSemicolon in Misc.SemiColon
            select new Ignore(content);

        public static readonly Parser<string> CreateTableOpen =
            from create in TableActions.Create
            from table in TableActions.Table
            from identifier in Identifiers.Identifier
            from openQuote in Parse.Char('(').Token()
            select identifier;

        public static readonly Parser<ICommand> CreateTable =
            from tableOpen in CreateTableOpen
            from columns in AddColumnCommand.AddColumns
            from closeQuote in Parse.Char(')').Token()
            from ignoreTillSemiColon in Parse.AnyChar.Except(Parse.Char(';')).Many()
            from semiColon in Parse.Char(';').Token()
            select new CreateTable(tableOpen, columns);

        public static readonly Parser<ICommand> LockTables =
            from ignoreLock in TableActions.Lock
            from ignoreTables in TableActions.Tables
            from identifier in Identifiers.Identifier
            from ignoreWrite in TableActions.Write
            from ignoreSemiColon in Parse.Char(';').Token()
            select new Ignore(identifier);

        public static readonly Parser<ICommand> UnlockTables =
            from ignoreUnlock in TableActions.Unlock
            from ignoreTables in TableActions.Tables
            from ignoreSemiColon in Parse.Char(';').Token()
            select new Ignore("Unlock Tables");
    }
}
