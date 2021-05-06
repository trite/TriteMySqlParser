using Xunit;
using Sprache;
using SpracheTutorialsAndPractice.JPSpracheIntro;

namespace SpracheTutorialsAndPractice.Tests.JPSpracheIntro
{
    // Sprache Part 2: Parsing Strings (Tests)
    public class Part02Tests
    {
        [Fact]
        public void KeywordReturnAsCharEnumerable_ShouldBlah()
        {
            Assert.Equal("return".ToCharArray(), Part02.keywordReturnAsCharEnumerable.Parse("return"));
        }

        [Fact]
        public void KeywordReturnAsString_ShouldBlah()
        {
            Assert.Equal("return", Part02.keywordReturnAsString.Parse("return"));
        }

        [Fact]
        public void SomethingRandom()
        {
            // var testing = Parse.

            // var testing = Parse.
            // Assert.Equal("tes\nting", Parse.String("tes\nting").Text().Parse("tes\nting"));
        }
    }
}
