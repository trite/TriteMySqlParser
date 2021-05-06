using Sprache;
using TriteMySqlParser.Types.Literals;

namespace TriteMySqlParser.Parsers
{
    public partial class Literals
    {
        public static readonly Parser<IOption<string>> HexLiteralPrefixXQuote = Parse.String("x'").Or(Parse.String("X'")).Text().Optional();

        public static readonly Parser<IOption<string>> HexListeralPrefixZeroX = Parse.String("0x").Text().Optional();

        public static readonly Parser<IOption<string>> HexLiteralSuffixQuote = Parse.Char('\'').Once().Text().Optional();

        public static readonly Parser<string> HexLiteralContent = Parse.Chars("abcdefABCDEF0123456789").AtLeastOnce().Text();

        public static readonly Parser<HexString> HexLiteral =
            from xQuote in HexLiteralPrefixXQuote
            from zeroX in HexListeralPrefixZeroX
            from content in HexLiteralContent
            from endQuote in HexLiteralSuffixQuote
            where (xQuote.IsDefined == endQuote.IsDefined) && (xQuote.IsDefined != zeroX.IsDefined) // Quotes should match, quote vs 0 should not
            select new HexString(content);
    }
}
