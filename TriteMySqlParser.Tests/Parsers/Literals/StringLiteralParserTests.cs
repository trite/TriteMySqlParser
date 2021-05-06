using Sprache;
using TriteMySqlParser.Parsers;
using Xunit;

namespace TriteMySqlParser.Tests.Parsers
{
    public class StringLiteralParserTests
    {
        [Fact]
        public void StringLiteral_ShouldSucceedIfInputIsTextWrappedInValidQuotes()
        {
            Assert.Equal("test", Literals.StringLiteral.Parse("\"test\""));
            Assert.Equal("test", Literals.StringLiteral.Parse("'test'"));
            Assert.Equal("test\nwith more complex things, and 1 number + some other symbols",
                Literals.StringLiteral.Parse("\"test\nwith more complex things, and 1 number + some other symbols\""));
            Assert.Equal("test\nwith more complex things, and 1 number + some other symbols",
                Literals.StringLiteral.Parse("'test\nwith more complex things, and 1 number + some other symbols'"));
        }

        [Fact]
        public void StringLiteral_ShouldFailIfNotProperlyWrapped()
        {
            Assert.Throws<ParseException>(() => Literals.StringLiteral.Parse("'test;"));
            Assert.Throws<ParseException>(() => Literals.StringLiteral.Parse("'test\""));
            Assert.Throws<ParseException>(() => Literals.StringLiteral.Parse("\"test'"));
        }
    }
}
