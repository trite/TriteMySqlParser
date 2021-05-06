using Sprache;
using System;
using System.Collections.Generic;
using System.Text;
using TriteMySqlParser.Parsers.Actions;
using Xunit;

namespace TriteMySqlParser.Tests.Parsers.Actions
{
    public class RowsTests
    {
        [Fact]
        public void InitialTesting()
        {
            var testString = "(1,'something001',42,'2020-10-21 09:43:00',123.45)";
            var result = Rows.RowOfLiterals.Parse(testString);
        }

        [Fact]
        public void InsertIntoTableOpen_ShouldParseOpeningOfInsert()
        {
            var testString = "INSERT INTO `sometable01` VALUES";
            Assert.Equal("sometable01", Rows.InsertIntoTableOpen.Parse(testString));
        }

        [Fact]
        public void InsertIntoTable_ShouldParseStuffRight()
        {
            var testString =
                "INSERT INTO `sometable01` VALUES (1,'something001',42,'2020-10-21 09:43:00',123.45),(2,'something002',27,'2020-10-21 09:44:00',485.584);";
            var result = Rows.InsertIntoTable.Parse(testString);
        }
    }
}
