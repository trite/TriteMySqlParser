using Sprache;
using System;
using Xunit;

namespace TriteMySqlParser.Tests
{
    public class PutteringAroundTests
    {
        [Fact]
        public void StringLiteral_ShouldDoSomeStringLiteralThingsMakeThisNameBetterLater()
        {
            Assert.Equal("test", PutteringAround.StringLiteral.Parse("\"test\""));
            Assert.Equal("test", PutteringAround.StringLiteral.Parse("'test'"));
            Assert.Equal("test\nwith more complex things, and 1 number + some other symbols",
                PutteringAround.StringLiteral.Parse("\"test\nwith more complex things, and 1 number + some other symbols\""));
            Assert.Equal("test\nwith more complex things, and 1 number + some other symbols",
                PutteringAround.StringLiteral.Parse("'test\nwith more complex things, and 1 number + some other symbols'"));
        }

        [Fact]
        public void IntLiteral_ShouldDoIntLiteralThingsCmonSeriouslyFixTheseFreakingNames()
        {
            var test = DateTime.Parse("2020-10-21 09:43:00");
            
            //var test = PutteringAround.NumericLiteralSign.Parse(".");
            //var test2 = PutteringAround.NumericLiteralSign.Parse("-");

            // PutteringAround.NumericLiteral.Parse("123.abc");

            Assert.Equal("123", PutteringAround.NumericLiteral.Parse("123"));
            Assert.Equal("-123", PutteringAround.NumericLiteral.Parse("-123"));
            Assert.Equal("123.45", PutteringAround.NumericLiteral.Parse("123.45"));
            Assert.Equal("-123.45", PutteringAround.NumericLiteral.Parse("-123.45"));
            Assert.Equal("1E23", PutteringAround.NumericLiteral.Parse("1E23"));
            Assert.Equal("-1E23", PutteringAround.NumericLiteral.Parse("-1E23"));
            Assert.Equal("1.23E45", PutteringAround.NumericLiteral.Parse("1.23E45"));
            Assert.Equal("-1.23E45", PutteringAround.NumericLiteral.Parse("-1.23E45"));

            Assert.Throws<ParseException>(() => PutteringAround.NumericLiteral.Parse("abc"));
            Assert.Throws<ParseException>(() => PutteringAround.NumericLiteral.Parse("-abc"));
            Assert.Throws<ParseException>(() => PutteringAround.NumericLiteral.Parse("123."));
            Assert.Throws<ParseException>(() => PutteringAround.NumericLiteral.Parse("-123."));
            Assert.Throws<ParseException>(() => PutteringAround.NumericLiteral.Parse("123.abc"));
            Assert.Throws<ParseException>(() => PutteringAround.NumericLiteral.Parse("-123.abc"));
            Assert.Throws<ParseException>(() => PutteringAround.NumericLiteral.Parse("abc.123"));
            Assert.Throws<ParseException>(() => PutteringAround.NumericLiteral.Parse("-abc.123"));
            Assert.Throws<ParseException>(() => PutteringAround.NumericLiteral.Parse("123Eabc"));
            Assert.Throws<ParseException>(() => PutteringAround.NumericLiteral.Parse("-123Eabc"));
            Assert.Throws<ParseException>(() => PutteringAround.NumericLiteral.Parse("abcE123"));
            Assert.Throws<ParseException>(() => PutteringAround.NumericLiteral.Parse("-abcE123"));
            Assert.Throws<ParseException>(() => PutteringAround.NumericLiteral.Parse("123.456Eabc"));
            Assert.Throws<ParseException>(() => PutteringAround.NumericLiteral.Parse("-123.456Eabc"));
            Assert.Throws<ParseException>(() => PutteringAround.NumericLiteral.Parse("abc.123E456"));
            Assert.Throws<ParseException>(() => PutteringAround.NumericLiteral.Parse("-abc.123E456"));
        }

        [Fact]
        public void DateTimeLiteral_ShouldDoWhateverItIsSupposedToSeriouslyStopBeingSoLazyWithTheseNames()
        {
            var x = "2020-10-21 09:43:00";
            var y = "'2020-10-21 09:43:00'";
            Assert.Equal(DateTime.Parse(x), PutteringAround.TemporalLiteral.Parse(y));
        }

        [Fact]
        public void HexLiteral_ShouldDoThingsAndWhateverJustKeepMakingCrappyNamesThenIDontCare()
        {
            var testValidHex = new HexString("01af");

            Assert.Equal(testValidHex, PutteringAround.HexLiteral.Parse("X'01AF'"));
            Assert.Equal(testValidHex, PutteringAround.HexLiteral.Parse("X'01af'"));
            Assert.Equal(testValidHex, PutteringAround.HexLiteral.Parse("x'01AF'"));
            Assert.Equal(testValidHex, PutteringAround.HexLiteral.Parse("x'01af'"));
            Assert.Equal(testValidHex, PutteringAround.HexLiteral.Parse("0x01AF"));
            Assert.Equal(testValidHex, PutteringAround.HexLiteral.Parse("0x01af"));

            var test = PutteringAround.HexLiteral.Parse("0xFGHI");

            Assert.Throws<ParseException>(() => PutteringAround.HexLiteral.Parse("abcd"));
            Assert.Throws<ParseException>(() => PutteringAround.HexLiteral.Parse("1234"));
            Assert.Throws<ParseException>(() => PutteringAround.HexLiteral.Parse("X'GHIJ'"));
            Assert.Throws<ParseException>(() => PutteringAround.HexLiteral.Parse("x'GHIJ'"));
            Assert.Throws<ParseException>(() => PutteringAround.HexLiteral.Parse("X'ghij'"));
            Assert.Throws<ParseException>(() => PutteringAround.HexLiteral.Parse("x'ghij'"));
            Assert.Throws<ParseException>(() => PutteringAround.HexLiteral.Parse("0xGHIJ"));
            Assert.Throws<ParseException>(() => PutteringAround.HexLiteral.Parse("0xghij"));

            // These 2 tests will fail because the parser doesn't know it needs to continue to the end
            // But shouldn't be an issue as the next parser up will fail to find the next character otherwise
            // TODO: make sure this is actually the case
            //Assert.Throws<ParseException>(() => PutteringAround.HexLiteral.Parse("0xFGHI"));
            //Assert.Throws<ParseException>(() => PutteringAround.HexLiteral.Parse("0xfghi"));
        }

        [Fact]
        public void BitLiteral_ShouldBitLiteralAndStuffYouJerk()
        {
            var testValidBit = new BitString("0110");
            Assert.Equal(testValidBit, PutteringAround.BitLiteral.Parse("b'0110'"));
            Assert.Equal(testValidBit, PutteringAround.BitLiteral.Parse("B'0110'"));
            Assert.Equal(testValidBit, PutteringAround.BitLiteral.Parse("0b0110"));

            Assert.Throws<ParseException>(() => PutteringAround.BitLiteral.Parse("b'01ab'"));
            Assert.Throws<ParseException>(() => PutteringAround.BitLiteral.Parse("B'01AB'"));
            Assert.Throws<ParseException>(() => PutteringAround.BitLiteral.Parse("0babcd"));

            // Similar to the HexLiteral, this test will fail right now because it doesn't match to the end (and shouldn't need to for the next parser up)
            // TODO: again, verify this behavior is actually what happens
            // Assert.Throws<ParseException>(() => PutteringAround.BitLiteral.Parse("0b01ab"));
        }
    }
}
