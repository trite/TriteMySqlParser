using Sprache;

namespace SpracheTutorialsAndPractice.JPSpracheIntro
{
    // Sprache Part 1: Parsing Characters
    public class Part01
    {
        public static readonly Parser<char> multiply = Parse.Char('*');

        public static readonly Parser<char> parens = Parse.Chars("()");

        public static readonly Parser<char> punctuation = Parse.Char(char.IsPunctuation, "punctuation");

        public static readonly Parser<char> ignoreCaseLetterA = Parse.IgnoreCase('a');

        public static readonly Parser<char> whitespace = Parse.WhiteSpace;

        public static readonly Parser<char> digit = Parse.Digit;

        public static readonly Parser<char> numeric = Parse.Numeric;

        public static readonly Parser<char> letter = Parse.Letter;

        public static readonly Parser<char> letterOrDigit = Parse.LetterOrDigit;

        public static readonly Parser<char> lower = Parse.Lower;

        public static readonly Parser<char> upper = Parse.Upper;

        public static readonly Parser<char> anyChar = Parse.AnyChar;
    }
}
