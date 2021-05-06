using Sprache;
using System;

namespace TriteMySqlParser
{
    public class PutteringAround
    {
        public static readonly Parser<char> StringLiteralQuote = Parse.Chars("\"'");

        public static readonly Parser<string> StringLiteralContent = Parse.AnyChar.Except(StringLiteralQuote).Many().Text();

        public static readonly Parser<string> StringLiteral =
            from openQuote in StringLiteralQuote
            from content in StringLiteralContent
            from endQuote in StringLiteralQuote
            select content;


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


        public static readonly Parser<char> TemporalLiteralQuote = Parse.Char('\'');
        
        public static readonly Parser<string> TemporalLiteralCharacters = Parse.Digit.Or(Parse.Chars("-: ")).AtLeastOnce().Text();

        public static readonly Parser<DateTime> TemporalLiteral =
            from openQuote in TemporalLiteralQuote
            from characters in TemporalLiteralCharacters
            from endQuote in TemporalLiteralQuote
            select DateTime.Parse(characters);


        public static readonly Parser<IOption<string>> HexLiteralPrefixXQuote = Parse.String("x'").Or(Parse.String("X'")).Text().Optional();

        public static readonly Parser<IOption<string>> HexListeralPrefixZeroX = Parse.String("0x").Text().Optional();

        public static readonly Parser<IOption<string>> HexLiteralSuffixQuote = Parse.Char('\'').Once().Text().Optional();

        public static readonly Parser<string> HexLiteralContent = Parse.Chars("abcdefABCDEF0123456789").AtLeastOnce().Text();

        public static readonly Parser<HexString> HexLiteral =
            from xQuote in HexLiteralPrefixXQuote
            from zeroX in HexListeralPrefixZeroX
            from content in HexLiteralContent
            from endQuote in HexLiteralSuffixQuote
            where (xQuote.IsDefined == endQuote.IsDefined) && (xQuote.IsDefined != zeroX.IsDefined) // Quotes should match, quote vs 0 should not
            select new HexString(content);

        //public static readonly Parser<HexString> HexLiteralMaybeDontDoThisWay =
        //    from zero in HexLiteralPrefixZero
        //    from lowerX in HexLiteralPrefixLowerX
        //    from eitherX in HexLiteralPrefixEitherX
        //    from openQuote in HexLiteralPrefixQuote
        //    from content in HexLiteralContent
        //    from endQuote in HexLiteralPrefixQuote
        //    where (eitherX.IsDefined == openQuote.IsDefined) && // x' prefix must have x and ' appear together or not at all
        //          (openQuote.IsDefined == endQuote.IsDefined) && // if using x' prefix then a closing quote should appear
        //          (zero.IsDefined == lowerX.IsDefined) // if using 0x prefix the 0 and x should appear together or not at all
        //    select new HexString(content);


        public static readonly Parser<IOption<string>> BitLiteralPrefixBQuote = Parse.String("b'").Or(Parse.String("B'")).Text().Optional();

        public static readonly Parser<IOption<string>> BitLiteralPrefixZeroB = Parse.String("0b").Text().Optional();

        public static readonly Parser<IOption<string>> BitLiteralSuffixQuote = Parse.Char('\'').Once().Text().Optional();

        public static readonly Parser<string> BitLiteralContent = Parse.Chars("01").AtLeastOnce().Text();

        public static readonly Parser<BitString> BitLiteral =
            from bQuote in BitLiteralPrefixBQuote
            from zeroB in BitLiteralPrefixZeroB
            from content in BitLiteralContent
            from endQuote in BitLiteralSuffixQuote
            where (bQuote.IsDefined == endQuote.IsDefined) && (bQuote.IsDefined != zeroB.IsDefined) // Quotes should match, quote vs 0 should not
            select new BitString(content);


        // Need to parse nulls as well
        public static readonly Parser<NullValue> NullLiteral = Parse.IgnoreCase("null").Return(new NullValue());
    }
}
