using Sprache;
using System;
using System.Collections.Generic;
using System.Text;
using TriteMySqlParser.Commands;
using TriteMySqlParser.Parsers.Others;

namespace TriteMySqlParser.Parsers.Keywords
{
    public class AddColumnCommand
    {
        //public static readonly Parser<AddColumn> AddSingleColumn()
        //{
        //    var command =
        //        from identifier in Identifiers.Identifier
        //        from colType in TableTypes.AnyType
        //        from ignoreProperties in TableProperties.AllProperties
        //        select new AddColumn(identifier, colType);

        //}

        //public static readonly Parser<string> AllowedProperties =
        //    TableProperties.Not
        //    .Or(TableProperties.Null)
        //    .Or(TableProperties.Default)
        //    .Or(TableProperties.AutoIncrementSimple);

        //public static readonly Parser<IEnumerable<string>> AllAllowedProperties =
        //    AllowedProperties.Many();

        public static readonly Parser<ICommand> AddSingleColumn =
            from identifier in Identifiers.Identifier.Token()
            from colType in TableTypes.AnyType.Token()
                //from ignoreProperties in TableProperties.AllProperties
            from ignoreProperties in ColumnProperties.AllInlineProperties
            select new AddColumn(identifier, colType);

        public static readonly Parser<ICommand> AddColumnOrIgnoreBehavioralInfo =
            AddSingleColumn
            .Or(ColumnProperties.AnyBehavioralProperty);

        public static readonly Parser<IEnumerable<ICommand>> AddColumns =
            AddColumnOrIgnoreBehavioralInfo.DelimitedBy(Parse.Char(',').Token());
            //from columns in AddSingleColumn.Token().XDelimitedBy(Parse.Char(',').Token())
            //from comma in Parse.Char(',').Token()
            //from behaviors in ColumnProperties.AllBehavioralProperties
            //select columns;

    }
}
