using Xunit;
using Sprache;
using SpracheTutorialsAndPractice.JPSpracheIntro;

namespace SpracheTutorialsAndPractice.Tests.JPSpracheIntro
{
    // Sprache Part 1: Parsing Characters (Tests)
    public class Part01Tests
    {
        [Fact]
        public void MultiplyParse_ShouldParseAsterisk()
        {
            Assert.Equal('*', Part01.multiply.Parse("*"));
            Assert.Throws<ParseException>(() => Part01.multiply.Parse("/"));
        }

        [Fact]
        public void ParensParse_ShouldParseParenthesis()
        {
            Assert.Equal('(', Part01.parens.Parse("("));
            Assert.Equal(')', Part01.parens.Parse(")"));
            Assert.Throws<ParseException>(() => Part01.parens.Parse("["));
        }

        [Fact]
        public void PunctuationParse_ShouldFindPunctuation()
        {
            Assert.Equal(',', Part01.punctuation.Parse(","));
            Assert.Equal('.', Part01.punctuation.Parse("."));
            Assert.Equal(';', Part01.punctuation.Parse(";"));
            Assert.Throws<ParseException>(() => Part01.punctuation.Parse("a"));
        }

        [Fact]
        public void IgnoreCaseLetterAParse_ShouldParseBothCapitalizationsOfAToCorrectResult()
        {
            Assert.Equal('a', Part01.ignoreCaseLetterA.Parse("a"));
            Assert.Equal('A', Part01.ignoreCaseLetterA.Parse("A"));
            Assert.NotEqual('A', Part01.ignoreCaseLetterA.Parse("a"));
            Assert.NotEqual('a', Part01.ignoreCaseLetterA.Parse("A"));
            Assert.Throws<ParseException>(() => Part01.ignoreCaseLetterA.Parse("b"));
        }

        [Fact]
        public void WhitespaceParse_ShouldParseWhitespace()
        {
            Assert.Equal(' ', Part01.whitespace.Parse(" "));
            Assert.Equal('\t', Part01.whitespace.Parse("\t"));
            Assert.Throws<ParseException>(() => Part01.whitespace.Parse("a"));
        }

        [Fact]
        public void DigitParse_ShouldParseDigit()
        {
            Assert.Equal('7', Part01.digit.Parse("7"));
            Assert.Equal('5', Part01.digit.Parse("567"));
            Assert.Throws<ParseException>(() => Part01.digit.Parse("abc123"));
        }

        [Fact]
        public void NumericParse_ShouldParseNumeric()
        {
            Assert.Equal('1', Part01.numeric.Parse("1"));
            Assert.Equal('¼', Part01.numeric.Parse("¼"));
            Assert.Throws<ParseException>(() => Part01.numeric.Parse("abc123"));
        }

        [Fact]
        public void LetterParse_ShouldParseLetter()
        {
            Assert.Equal('x', Part01.letter.Parse("x"));
            Assert.Throws<ParseException>(() => Part01.letter.Parse("7"));
        }

        [Fact]
        public void LetterOrDigitParse_ShouldParseLetterOrDigit()
        {
            Assert.Equal('x', Part01.letterOrDigit.Parse("x"));
            Assert.Equal('4', Part01.letterOrDigit.Parse("4"));
            Assert.Throws<ParseException>(() => Part01.letterOrDigit.Parse("/"));
        }

        [Fact]
        public void LowerParse_ShouldParseLowercaseLetter()
        {
            Assert.Equal('x', Part01.lower.Parse("x"));
            Assert.Throws<ParseException>(() => Part01.lower.Parse("X"));
            Assert.Throws<ParseException>(() => Part01.lower.Parse("7"));
            Assert.Throws<ParseException>(() => Part01.lower.Parse("/"));
        }

        [Fact]
        public void UpperParse_ShouldParseUppercaseLetter()
        {
            Assert.Equal('X', Part01.upper.Parse("X"));
            Assert.Throws<ParseException>(() => Part01.upper.Parse("x"));
            Assert.Throws<ParseException>(() => Part01.upper.Parse("7"));
            Assert.Throws<ParseException>(() => Part01.upper.Parse("/"));
        }

        [Fact]
        public void AnyChar_ShouldParseAnyChar()
        {
            Assert.Equal('x', Part01.anyChar.Parse("x"));
            Assert.Equal('X', Part01.anyChar.Parse("X"));
            Assert.Equal('7', Part01.anyChar.Parse("7"));
            Assert.Equal('/', Part01.anyChar.Parse("/"));
            Assert.Equal('¼', Part01.anyChar.Parse("¼"));
        }
    }
}
