using Sprache;
using TriteMySqlParser.Types.Literals;

namespace TriteMySqlParser.Parsers
{
    public partial class Literals
    {
        public static readonly Parser<IOption<string>> BitLiteralPrefixBQuote = Parse.String("b'").Or(Parse.String("B'")).Text().Optional();

        public static readonly Parser<IOption<string>> BitLiteralPrefixZeroB = Parse.String("0b").Text().Optional();

        public static readonly Parser<IOption<string>> BitLiteralSuffixQuote = Parse.Char('\'').Once().Text().Optional();

        public static readonly Parser<string> BitLiteralContent = Parse.Chars("01").AtLeastOnce().Text();

        public static readonly Parser<BitString> BitLiteral =
            from bQuote in BitLiteralPrefixBQuote
            from zeroB in BitLiteralPrefixZeroB
            from content in BitLiteralContent
            from endQuote in BitLiteralSuffixQuote
            where (bQuote.IsDefined == endQuote.IsDefined) && (bQuote.IsDefined != zeroB.IsDefined) // Quotes should match, quote vs 0 should not
            select new BitString(content);
    }
}
