using Sprache;

namespace TriteMySqlParser.Parsers.Others
{
    public class Identifiers
    {
        internal static readonly Parser<char> IdentifierQuote = Parse.Char('`');

        internal static readonly Parser<string> IdentifierContent = Parse.AnyChar.Except(IdentifierQuote).Many().Text();

        public static readonly Parser<string> Identifier =
            from openQuote in IdentifierQuote
            from content in IdentifierContent
            from closeQuote in IdentifierQuote
            select content;
    }
}
