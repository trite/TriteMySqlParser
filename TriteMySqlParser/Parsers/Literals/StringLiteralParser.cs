using Sprache;

namespace TriteMySqlParser.Parsers
{
    public partial class Literals
    {
        internal static readonly Parser<char> StringLiteralQuote = Parse.Chars("\"'");

        internal static readonly Parser<IOption<string>> StringLiteralSingleQuote = Parse.Char('\'').Once().Text().Optional();

        internal static readonly Parser<IOption<string>> StringLiteralDoubleQuote = Parse.Char('"').Once().Text().Optional();

        internal static readonly Parser<string> StringLiteralContent = Parse.AnyChar.Except(StringLiteralQuote).Many().Text();

        public static readonly Parser<string> StringLiteral =
            from openSingle in StringLiteralSingleQuote
            from openDouble in StringLiteralDoubleQuote
            from content in StringLiteralContent
            from endSingle in StringLiteralSingleQuote
            from endDouble in StringLiteralDoubleQuote
            where (openSingle.IsDefined == endSingle.IsDefined) && // opening/ending single quote usage should match
                  (openDouble.IsDefined == endDouble.IsDefined) && // opening/ending double   "     "      "     "
                  (openSingle.IsDefined != openDouble.IsDefined)   // only single OR double quotes should be found
            select content;
    }
}
