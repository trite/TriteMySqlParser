using Sprache;
using System;
using TriteMySqlParser.Parsers;
using Xunit;

namespace TriteMySqlParser.Tests.Parsers
{
    public class TemporalLiteralParserTests
    {
        [Fact]
        public void TemporalLiteral_ShouldDoWhateverItIsSupposedToSeriouslyStopBeingSoLazyWithTheseNames()
        {
            var x = "2020-10-21 09:43:00";
            var y = "'2020-10-21 09:43:00'";
            Assert.Equal(DateTime.Parse(x), Literals.TemporalLiteral.Parse(y));
        }
    }
}
