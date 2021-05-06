using Sprache;

namespace TriteMySqlParser.Parsers
{
    public partial class Literals
    {
        // TODO: MySql documentation says this can be a + sign... But is that likely when we're reading mysql dump files?
        public static readonly Parser<IOption<string>> NumericLiteralSign = Parse.Char('-').Once().Text().Optional();

        public static readonly Parser<string> NumericLiteralWholeDigits = Parse.Digit.AtLeastOnce().Text();

        public static readonly Parser<IOption<string>> NumericLiteralDecimalPoint = Parse.Char('.').Once().Text().Optional();

        public static readonly Parser<IOption<string>> NumericLiteralFractionalDigits = Parse.Digit.AtLeastOnce().Text().Optional();

        public static readonly Parser<IOption<string>> NumericLiteralExponentMarker = Parse.Char('E').Once().Text().Optional();

        public static readonly Parser<IOption<string>> NumericLiteralExponentDigits = Parse.Digit.AtLeastOnce().Text().Optional();

        public static readonly Parser<string> NumericLiteral =
            from sign in NumericLiteralSign
            from whole in NumericLiteralWholeDigits
            from dot in NumericLiteralDecimalPoint
            from fract in NumericLiteralFractionalDigits
            from mark in NumericLiteralExponentMarker
            from exp in NumericLiteralExponentDigits
            where (dot.IsDefined == fract.IsDefined) && (mark.IsDefined == exp.IsDefined) // expect a number if a decimal point or exponent marker was given
            select sign.GetOrDefault() + whole + dot.GetOrDefault() + fract.GetOrDefault() + mark.GetOrDefault() + exp.GetOrDefault();
    }
}
