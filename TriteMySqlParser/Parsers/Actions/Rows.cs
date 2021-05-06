using Sprache;
using System;
using System.Collections.Generic;
using System.Text;
using TriteMySqlParser.Commands;
using TriteMySqlParser.Parsers.Keywords;
using TriteMySqlParser.Parsers.Others;

namespace TriteMySqlParser.Parsers.Actions
{
    public class Rows
    {
        public static readonly Parser<string> AcceptedLiterals =
            Literals.NumericLiteral
            .Or(Literals.StringLiteral);


        public static readonly Parser<ICommand> RowOfLiterals =
            from openQuote in Parse.Char('(')
            from values in AcceptedLiterals.DelimitedBy(Parse.Char(',').Token())
            from closeQuote in Parse.Char(')')
            select new AddRow("Row", values);

        public static readonly Parser<string> InsertIntoTableOpen =
            from ignoreInsert in TableActions.Insert
            from ignoreInto in TableActions.Into
            from identifier in Identifiers.Identifier.Token()
            from ignoreValues in TableActions.Values
            select identifier;

        public static readonly Parser<ICommand> InsertIntoTable =
            from tableName in InsertIntoTableOpen
            from rows in RowOfLiterals.DelimitedBy(Parse.Char(',').Token())
            from ignoreSemiColon in Parse.Char(';').Token()
            select new InsertIntoTable("Insert", tableName, rows);
    }
}
