using Sprache;
using System;
using System.Collections.Generic;
using System.Text;
using TriteMySqlParser.Parsers.Keywords;
using Xunit;

namespace TriteMySqlParser.Tests.Parsers.Keywords
{
    public class TablePropertiesTests
    {
        [Fact]
        public void Not_ShouldParseNot()
        {
            Assert.Equal("NOT", TableProperties.Not.Parse("NOT"));
        }

        // TODO: the rest of the simple ones

        [Fact]
        public void AutoIncrement_ShouldParseVariationsOfAutoIncrement()
        {
            Assert.Equal("AUTO_INCREMENT", TableProperties.AutoIncrement.Parse("AUTO_INCREMENT"));
            Assert.Equal("AUTO_INCREMENT", TableProperties.AutoIncrement.Parse("AUTO_INCREMENT=3"));
        }

        [Fact]
        public void AnyProperty_ShouldParseAnyKnownProperty()
        {
            Assert.Equal("NOT", TableProperties.AnyProperty.Parse("NOT"));
            Assert.Equal("NULL", TableProperties.AnyProperty.Parse("NULL"));
            Assert.Equal("DEFAULT", TableProperties.AnyProperty.Parse("DEFAULT"));
            Assert.Equal("PRIMARY", TableProperties.AnyProperty.Parse("PRIMARY"));
            Assert.Equal("UNIQUE", TableProperties.AnyProperty.Parse("UNIQUE"));
            Assert.Equal("KEY", TableProperties.AnyProperty.Parse("KEY"));

            Assert.Equal("AUTO_INCREMENT", TableProperties.AnyProperty.Parse("AUTO_INCREMENT"));
            Assert.Equal("AUTO_INCREMENT", TableProperties.AnyProperty.Parse("AUTO_INCREMENT=3"));

            Assert.Equal("ENGINE", TableProperties.AnyProperty.Parse("ENGINE"));
            Assert.Equal("ENGINE", TableProperties.AnyProperty.Parse("ENGINE=InnoDB"));

            Assert.Equal("CHARSET", TableProperties.AnyProperty.Parse("CHARSET"));
            Assert.Equal("CHARSET", TableProperties.AnyProperty.Parse("CHARSET=latin1"));
        }

        [Fact]
        public void AllProperties_ShouldParseASeriesOfProperties()
        {
            // var result = testParser.Parse("ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1");
            var expectedResult = new List<string>() { "ENGINE", "AUTO_INCREMENT", "DEFAULT", "CHARSET" };
            var toParse = "ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1";
            Assert.Equal(expectedResult, TableProperties.AllProperties.Parse(toParse));
        }
    }
}
