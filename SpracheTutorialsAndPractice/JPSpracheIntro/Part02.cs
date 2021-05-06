using Sprache;
using System.Collections.Generic;

namespace SpracheTutorialsAndPractice.JPSpracheIntro
{
    // Sprache Part 2: Parsing Strings
    public class Part02
    {
        public static readonly Parser<IEnumerable<char>> keywordReturnAsCharEnumerable = Parse.String("return");

        public static readonly Parser<string> keywordReturnAsString = Parse.String("return").Text();
    }
}
