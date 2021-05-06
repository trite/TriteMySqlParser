using Sprache;
using System;
using System.Collections.Generic;
using System.Text;

namespace TriteMySqlParser.Parsers.Keywords
{
    public class TableProperties
    {
        public static readonly Parser<string> Not = Parse.String("NOT").Text().Token();

        public static readonly Parser<string> Null = Parse.String("NULL").Text().Token();

        public static readonly Parser<string> Default = Parse.String("DEFAULT").Text().Token();

        public static readonly Parser<string> Primary = Parse.String("PRIMARY").Text().Token();

        public static readonly Parser<string> Unique = Parse.String("UNIQUE").Text().Token();

        public static readonly Parser<string> Key = Parse.String("KEY").Text().Token();

        public static readonly Parser<string> AutoIncrementSimple = Parse.String("AUTO_INCREMENT").Text().Token();

        internal static Parser<string> PropertyReducer(string PropertyName)
        {
            var identifierParser = Parse.String(PropertyName).Text();
            var result =
                from identifier in identifierParser
                from optionalValueDelimiter in Parse.Char('=').Optional()
                from optionalValue in Parse.AnyChar.Except(Parse.Char(' ')).Many().Text().Optional()
                select identifier;
            return result;
        }

        //public static readonly Parser<string> AutoIncrement = Parse.String("AUTO_INCREMENT").Text().Token();

        public static readonly Parser<string> AutoIncrement = PropertyReducer("AUTO_INCREMENT").Token();

        public static readonly Parser<string> Engine = PropertyReducer("ENGINE").Token();

        public static readonly Parser<string> Charset = PropertyReducer("CHARSET").Token();

        public static readonly Parser<string> AnyProperty =
            Not
            .Or(Null)
            .Or(Default)
            .Or(Primary)
            .Or(Unique)
            .Or(Key)
            .Or(AutoIncrement)
            .Or(Engine)
            .Or(Charset);

        public static readonly Parser<IEnumerable<string>> AllProperties = AnyProperty.Many();
    }
}
