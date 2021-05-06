using Sprache;
using System;
using System.Collections.Generic;
using System.Text;
using TriteMySqlParser.Commands;
using TriteMySqlParser.Parsers.Others;

namespace TriteMySqlParser.Parsers.Keywords
{
    public class ColumnProperties
    {
        public static readonly Parser<string> Not = Parse.String("NOT").Text().Token();

        public static readonly Parser<string> Null = Parse.String("NULL").Text().Token();

        public static readonly Parser<string> Default = Parse.String("DEFAULT").Text().Token();

        public static readonly Parser<string> AutoIncrement = Parse.String("AUTO_INCREMENT").Text().Token();

        public static readonly Parser<string> AnyInlineProperty =
            Not
            .Or(Null)
            .Or(Default)
            .Or(AutoIncrement);

        public static readonly Parser<IEnumerable<string>> AllInlineProperties = AnyInlineProperty.Many();

        internal static readonly Parser<string> Primary = Parse.String("PRIMARY").Text().Token();

        internal static readonly Parser<string> Unique = Parse.String("UNIQUE").Text().Token();

        internal static readonly Parser<string> Key = Parse.String("KEY").Text().Token();

        internal static readonly Parser<string> ColumnBehaviorNameOpenSequence = Parse.String("(`").Text();

        internal static readonly Parser<string> ColumnBehaviorNameCloseSequence = Parse.String("`)").Text();

        internal static readonly Parser<string> ColumnBehaviorColumnNameContent =
    Parse.AnyChar.Except(ColumnBehaviorNameCloseSequence).Many().Text();

        internal static readonly Parser<string> ColumnBehaviorColumnName =
            from openSequence in ColumnBehaviorNameOpenSequence
            from content in ColumnBehaviorColumnNameContent
            from closeSequence in ColumnBehaviorNameCloseSequence
            select content;

        public static readonly Parser<ICommand> PrimaryKey =
            from primary in Primary
            from key in Key
            from colName in ColumnBehaviorColumnName
            select new Ignore(colName);

        internal static readonly Parser<char> ColumnBehaviorKeyQuote = Parse.Char('`');

        internal static readonly Parser<string> ColumnBehaviorKeyContent =
            Parse.AnyChar.Except(ColumnBehaviorKeyQuote).Many().Text();

        internal static readonly Parser<string> ColumnBehaviorKey =
            from openQuote in ColumnBehaviorKeyQuote
            from content in ColumnBehaviorKeyContent
            from closeQuote in ColumnBehaviorKeyQuote
            select content;

        public static readonly Parser<ICommand> UniqueKey =
            from unique in Unique
            from key in Key
            from behaviorKey in Identifiers.Identifier.Token()
            from colName in ColumnBehaviorColumnName
            select new Ignore(behaviorKey);

        public static readonly Parser<ICommand> AnyBehavioralProperty =
            PrimaryKey
            .Or(UniqueKey);

        public static readonly Parser<IEnumerable<ICommand>> AllBehavioralProperties = AnyBehavioralProperty.DelimitedBy(Parse.Char(',').Token());

    }
}
