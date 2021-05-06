using Xunit;
using TriteMySqlParser.Parsers;
using Sprache;
using TriteMySqlParser.Types.Literals;

namespace TriteMySqlParser.Tests.Parsers
{
    public class NullLiteralParserTests
    {
        [Fact]
        public void NullLiteral_ShouldWorkOnAllCapitalizationsOfNull()
        {
            Assert.Equal(new NullValue(), Literals.NullLiteral.Parse("null"));
            Assert.Equal(new NullValue(), Literals.NullLiteral.Parse("NULL"));
            Assert.Equal(new NullValue(), Literals.NullLiteral.Parse("NuLl"));
            Assert.Equal(new NullValue(), Literals.NullLiteral.Parse("nUlL"));
        }

        [Fact]
        public void NullLiteral_ShouldFailIfNotNull()
        {
            Assert.Throws<ParseException>(() => Literals.NullLiteral.Parse("knoll"));
            Assert.Throws<ParseException>(() => Literals.NullLiteral.Parse("KNOLL"));
            Assert.Throws<ParseException>(() => Literals.NullLiteral.Parse("12345"));
            Assert.Throws<ParseException>(() => Literals.NullLiteral.Parse("@*$()"));
        }
    }
}
