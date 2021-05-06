using Sprache;
using TriteMySqlParser.Types.Literals;

namespace TriteMySqlParser.Parsers
{
    public partial class Literals
    {
        public static readonly Parser<NullValue> NullLiteral = Parse.IgnoreCase("null").Return(new NullValue());
    }
}
