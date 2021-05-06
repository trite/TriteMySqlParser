using Sprache;
using System;
using System.Collections.Generic;
using System.Text;

namespace TriteMySqlParser.Parsers.Keywords
{
    public class TableActions
    {
        public static readonly Parser<string> Table = Parse.String("TABLE").Text().Token();

        public static readonly Parser<string> Tables = Parse.String("TABLES").Text().Token();

        public static readonly Parser<string> Drop = Parse.String("DROP").Text().Token();

        public static readonly Parser<string> Create = Parse.String("CREATE").Text().Token();

        public static readonly Parser<string> If = Parse.String("IF").Text().Token();
        
        public static readonly Parser<string> Exists = Parse.String("EXISTS").Text().Token();

        public static readonly Parser<string> Lock = Parse.String("LOCK").Text().Token();

        public static readonly Parser<string> Write = Parse.String("WRITE").Text().Token();

        public static readonly Parser<string> Into = Parse.String("INTO").Text().Token();

        public static readonly Parser<string> Values = Parse.String("VALUES").Text().Token();

        public static readonly Parser<string> Insert = Parse.String("INSERT").Text().Token();

        public static readonly Parser<string> Unlock = Parse.String("UNLOCK").Text().Token();
    }
}
