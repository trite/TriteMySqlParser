using Sprache;
using System;
using System.Collections.Generic;

namespace TriteMySqlParser.Parsers.Keywords
{
    public class TableTypes
    {
        // TODO: This only covers a small subset of type cases, will almost certainly need to expand this later.
        
        internal static readonly Parser<int> TypeLength =
            from openParen in Parse.Char('(')
            from content in Parse.Digit.AtLeastOnce().Text()
            from closeParen in Parse.Char(')')
            select int.Parse(content);

        internal static Parser<Type> TypeReducer<T>(Parser<T> identifierParser, Type returnType)
        {
            var result =
                from identifier in identifierParser
                from optionalLength in TypeLength.Optional()
                select returnType;
            return result;
        }

        
        internal static readonly Parser<string> IntKeyword = Parse.String("int").Text();

        public static readonly Parser<Type> IntType = TypeReducer<string>(IntKeyword, typeof(int));
            //from identifier in IntKeyword
            //from optionalLength in TypeLength.Optional()
            //select typeof(int);

        internal static readonly Parser<string> VarCharKeyword = Parse.String("varchar").Text();

        public static readonly Parser<Type> StringType = TypeReducer<string>(VarCharKeyword, typeof(string));
            //from identifier in VarCharKeyword
            //from optionalLength in TypeLength.Optional()
            //select typeof(string);

        internal static readonly Parser<string> DateTimeKeyword = Parse.String("datetime").Text();

        // Returning DateTimes as strings for now, we'll see if this is good or bad eventually.
        // The logic here being that calling DateTime.Parse() on every DateTime field when few will
        // be used is quite expensive and for little benefit. Let the user Parse ones they care about.
        public static readonly Parser<Type> DateTimeType = TypeReducer<string>(DateTimeKeyword, typeof(string));

        internal static readonly Parser<string> DoubleKeyword = Parse.String("double").Text();

        public static readonly Parser<Type> DoubleType = TypeReducer<string>(DoubleKeyword, typeof(double));

        public static readonly Parser<Type> AnyType =
            IntType
            .Or(DoubleType)
            .Or(StringType)
            .Or(DateTimeType);

        public static readonly Parser<IEnumerable<Type>> AllTypes = AnyType.Many();
    }
}
