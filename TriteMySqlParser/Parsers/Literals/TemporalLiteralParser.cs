using Sprache;
using System;

namespace TriteMySqlParser.Parsers
{
    public partial class Literals
    {
        public static readonly Parser<char> TemporalLiteralQuote = Parse.Char('\'');

        public static readonly Parser<string> TemporalLiteralCharacters = Parse.Digit.Or(Parse.Chars("-: ")).AtLeastOnce().Text();

        public static readonly Parser<DateTime> TemporalLiteral =
            from openQuote in TemporalLiteralQuote
            from characters in TemporalLiteralCharacters
            from endQuote in TemporalLiteralQuote
            select DateTime.Parse(characters);
    }
}
