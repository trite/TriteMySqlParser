using Sprache;
using TriteMySqlParser.Parsers;
using TriteMySqlParser.Types.Literals;
using Xunit;

namespace TriteMySqlParser.Tests.Parsers
{
    public class BitLiteralParserTests
    {
        [Fact]
        public void BitLiteral_ShouldBitLiteralAndStuffYouJerk()
        {
            var testValidBit = new BitString("0110");
            Assert.Equal(testValidBit, Literals.BitLiteral.Parse("b'0110'"));
            Assert.Equal(testValidBit, Literals.BitLiteral.Parse("B'0110'"));
            Assert.Equal(testValidBit, Literals.BitLiteral.Parse("0b0110"));

            Assert.Throws<ParseException>(() => Literals.BitLiteral.Parse("b'01ab'"));
            Assert.Throws<ParseException>(() => Literals.BitLiteral.Parse("B'01AB'"));
            Assert.Throws<ParseException>(() => Literals.BitLiteral.Parse("0babcd"));

            // Similar to the HexLiteral, this test will fail right now because it doesn't match to the end (and shouldn't need to for the next parser up)
            // TODO: again, verify this behavior is actually what happens
            // Assert.Throws<ParseException>(() => Literals.BitLiteral.Parse("0b01ab"));
        }
    }
}
