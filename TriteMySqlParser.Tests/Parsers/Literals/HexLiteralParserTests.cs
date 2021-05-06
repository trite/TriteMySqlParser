using Sprache;
using TriteMySqlParser.Parsers;
using TriteMySqlParser.Types.Literals;
using Xunit;

namespace TriteMySqlParser.Tests.Parsers
{
    public class HexLiteralParserTests
    {
        [Fact]
        public void HexLiteral_ShouldDoThingsAndWhateverJustKeepMakingCrappyNamesThenIDontCare()
        {
            var testValidHex = new HexString("01af");

            Assert.Equal(testValidHex, Literals.HexLiteral.Parse("X'01AF'"));
            Assert.Equal(testValidHex, Literals.HexLiteral.Parse("X'01af'"));
            Assert.Equal(testValidHex, Literals.HexLiteral.Parse("x'01AF'"));
            Assert.Equal(testValidHex, Literals.HexLiteral.Parse("x'01af'"));
            Assert.Equal(testValidHex, Literals.HexLiteral.Parse("0x01AF"));
            Assert.Equal(testValidHex, Literals.HexLiteral.Parse("0x01af"));

            var test = Literals.HexLiteral.Parse("0xFGHI");

            Assert.Throws<ParseException>(() => Literals.HexLiteral.Parse("abcd"));
            Assert.Throws<ParseException>(() => Literals.HexLiteral.Parse("1234"));
            Assert.Throws<ParseException>(() => Literals.HexLiteral.Parse("X'GHIJ'"));
            Assert.Throws<ParseException>(() => Literals.HexLiteral.Parse("x'GHIJ'"));
            Assert.Throws<ParseException>(() => Literals.HexLiteral.Parse("X'ghij'"));
            Assert.Throws<ParseException>(() => Literals.HexLiteral.Parse("x'ghij'"));
            Assert.Throws<ParseException>(() => Literals.HexLiteral.Parse("0xGHIJ"));
            Assert.Throws<ParseException>(() => Literals.HexLiteral.Parse("0xghij"));

            // These 2 tests will fail because the parser doesn't know it needs to continue to the end
            // But shouldn't be an issue as the next parser up will fail to find the next character otherwise
            // TODO: make sure this is actually the case
            //Assert.Throws<ParseException>(() => Literals.HexLiteral.Parse("0xFGHI"));
            //Assert.Throws<ParseException>(() => Literals.HexLiteral.Parse("0xfghi"));
        }
    }
}
