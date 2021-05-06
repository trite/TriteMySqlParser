using Sprache;
using System;
using TriteMySqlParser.Parsers.Others;
using TriteMySqlParser.Types;

namespace TriteMySqlParser.Parsers.Keywords
{
    public class ColInfo
    {
        public string ColName { get; set; }
        public Type ColType { get; set; }

        public ColInfo(string name, Type type)
        {
            ColName = name;
            ColType = type;
        }
    }

    //public class ColumnDescriptions
    //{
    //    public static readonly Parser<string> SingleColumnDescription()
    //    {
    //        string identifierContent = null;
    //        Type colTypeValue = null;
    //        var tupledResults =
    //            from identifier in Identifiers.Identifier
    //            from colType in TableTypes.AnyType
    //            from ignoreProperties in TableProperties.AllProperties
    //            // select (identifier, colType);
    //            select new ColInfo(identifier, colType);
    //        tupledResults.
    //    }
    //}
}
