using Sprache;
using TriteMySqlParser.Commands;

namespace TriteMySqlParser.Parsers
{
    public class Comments
    {
        internal static readonly Parser<string> MultiLineQuoteOpen = Parse.String("/*").Text();

        internal static readonly Parser<string> MultiLineQuoteClose = Parse.String("*/;").Text();

        internal static readonly Parser<string> MultiLineCommentContent = Parse.AnyChar.Except(MultiLineQuoteClose).Many().Text().Token();

        public static readonly Parser<ICommand> MultiLineComment =
            from openQuote in MultiLineQuoteOpen
            // from ws1 in Parse.WhiteSpace.Many().Optional()
            from comment in MultiLineCommentContent
            // from ws2 in Parse.WhiteSpace.Many().Optional()
            from closeQuote in MultiLineQuoteClose
            select new Ignore(comment);

        internal static readonly Parser<string> SingleLineCommentMarker = Parse.String("--").Text();

        internal static readonly Parser<string> NewLine = Parse.String("\n").Or(Parse.String("\r\n")).Text();

        internal static readonly Parser<string> SingleLineCommentContent = Parse.AnyChar.Except(NewLine).Many().Text();

        public static readonly Parser<ICommand> SingleLineComment =
            from open in SingleLineCommentMarker
            from ws in Parse.WhiteSpace.Except(NewLine).Many()
            from comment in SingleLineCommentContent
            from end in NewLine
            select new Ignore(comment);

        public static readonly Parser<ICommand> AnyComment = MultiLineComment.Or(SingleLineComment);
    }
}
