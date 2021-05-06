using Sprache;
using System;
using TriteMySqlParser.Parsers;
using Xunit;

namespace TriteMySqlParser.Tests.Parsers
{
    public class NumericLiteralParserTests
    {
        [Fact]
        public void NumericLiteral_ShouldDoNumericLiteralThingsCmonSeriouslyFixTheseFreakingNames()
        {
            // var test = DateTime.Parse("2020-10-21 09:43:00");

            //var test = PutteringAround.NumericLiteralSign.Parse(".");
            //var test2 = PutteringAround.NumericLiteralSign.Parse("-");

            // PutteringAround.NumericLiteral.Parse("123.abc");

            Assert.Equal("123", Literals.NumericLiteral.Parse("123"));
            Assert.Equal("-123", Literals.NumericLiteral.Parse("-123"));
            Assert.Equal("123.45", Literals.NumericLiteral.Parse("123.45"));
            Assert.Equal("-123.45", Literals.NumericLiteral.Parse("-123.45"));
            Assert.Equal("1E23", Literals.NumericLiteral.Parse("1E23"));
            Assert.Equal("-1E23", Literals.NumericLiteral.Parse("-1E23"));
            Assert.Equal("1.23E45", Literals.NumericLiteral.Parse("1.23E45"));
            Assert.Equal("-1.23E45", Literals.NumericLiteral.Parse("-1.23E45"));

            Assert.Throws<ParseException>(() => Literals.NumericLiteral.Parse("abc"));
            Assert.Throws<ParseException>(() => Literals.NumericLiteral.Parse("-abc"));
            Assert.Throws<ParseException>(() => Literals.NumericLiteral.Parse("123."));
            Assert.Throws<ParseException>(() => Literals.NumericLiteral.Parse("-123."));
            Assert.Throws<ParseException>(() => Literals.NumericLiteral.Parse("123.abc"));
            Assert.Throws<ParseException>(() => Literals.NumericLiteral.Parse("-123.abc"));
            Assert.Throws<ParseException>(() => Literals.NumericLiteral.Parse("abc.123"));
            Assert.Throws<ParseException>(() => Literals.NumericLiteral.Parse("-abc.123"));
            Assert.Throws<ParseException>(() => Literals.NumericLiteral.Parse("123Eabc"));
            Assert.Throws<ParseException>(() => Literals.NumericLiteral.Parse("-123Eabc"));
            Assert.Throws<ParseException>(() => Literals.NumericLiteral.Parse("abcE123"));
            Assert.Throws<ParseException>(() => Literals.NumericLiteral.Parse("-abcE123"));
            Assert.Throws<ParseException>(() => Literals.NumericLiteral.Parse("123.456Eabc"));
            Assert.Throws<ParseException>(() => Literals.NumericLiteral.Parse("-123.456Eabc"));
            Assert.Throws<ParseException>(() => Literals.NumericLiteral.Parse("abc.123E456"));
            Assert.Throws<ParseException>(() => Literals.NumericLiteral.Parse("-abc.123E456"));
        }
    }
}
