using Sprache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TriteMySqlParser.Parsers.Keywords;
using TriteMySqlParser.Types;
using Xunit;

namespace TriteMySqlParser.Tests.Parsers.Keywords
{
    public class AddColumnCommandTests
    {
        [Fact]
        public void AddSingleColumn_ShouldWork()
        {
            var testing = AddColumnCommand.AddSingleColumn.Parse("`idsometable01` int(11) NOT NULL AUTO_INCREMENT");

            DataPoolGlobal.Pool.Reset(); // Other tests might mess with this, will need to keep in mind for the future
            var pool = DataPoolGlobal.Pool;
            pool.CreateTable("TestTable01");
            testing.Execute();


            var parseMultiple = AddColumnCommand.AddSingleColumn.Token().XDelimitedBy(Parse.Char(',').Token()).End();
            var testString = @"
                  `idsometable01` int(11) NOT NULL,
                  `sometable01col01` varchar(45) DEFAULT NULL AUTO_INCREMENT,
                  `sometable01col02` int(11) DEFAULT NULL,
                  `sometable01col03` datetime DEFAULT NULL,
                  `sometable01col04` double DEFAULT NULL
            ";

            var doesThisWork = parseMultiple.Parse(testString);
        }

        [Fact]
        public void AddColumns_ShouldWork()
        {
            var testString = @"
                `idsometable01` int(11) NOT NULL AUTO_INCREMENT,
                `sometable01col01` varchar(45) DEFAULT NULL,
                `sometable01col02` int(11) DEFAULT NULL,
                `sometable01col03` datetime DEFAULT NULL,
                `sometable01col04` double DEFAULT NULL,
                PRIMARY KEY (`idsometable01`),
                UNIQUE KEY `idsometable01_UNIQUE` (`idsometable01`)
            ";

            var result = AddColumnCommand.AddColumns.Parse(testString);

            DataPoolGlobal.Pool.Reset(); // Other tests might mess with this, will need to keep in mind for the future
            var pool = DataPoolGlobal.Pool;
            pool.CreateTable("TestTable02");

            foreach (var command in result)
            {
                command.Execute();
            }

            var expectedValues = new List<string>()
            {
                "idsometable01",
                "sometable01col01",
                "sometable01col02",
                "sometable01col03",
                "sometable01col04"
            };

            var enumerator = expectedValues.GetEnumerator();

            foreach (DataColumn col in pool.GetTable(pool.CurrentTable).Columns)
            {
                enumerator.MoveNext();
                var current = enumerator.Current;

                Assert.Equal(current, col.ColumnName);
            }
        }

        [Fact]
        public void AddColumn_ShouldParseBothKnownBehaviors()
        {
            Assert.Equal("idsometable01", ColumnProperties.PrimaryKey.Parse("PRIMARY KEY (`idsometable01`)").Name);
            Assert.Equal("idsometable01_UNIQUE", ColumnProperties.UniqueKey.Parse("UNIQUE KEY `idsometable01_UNIQUE` (`idsometable01`)").Name);

            var testString = @"
                PRIMARY KEY (`idsometable01`),
                UNIQUE KEY `idsometable01_UNIQUE` (`idsometable01`)
            ";

            var result = ColumnProperties.AllBehavioralProperties.Parse(testString);

            List<string> expectedResultNames = new List<string>() { "idsometable01", "idsometable01_UNIQUE" };
            var enumerator = expectedResultNames.GetEnumerator();

            foreach (var item in result)
            {
                enumerator.MoveNext();
                var current = enumerator.Current;

                Assert.Equal(current, item.Name);
            }
        }
    }
}
