using Sprache;
using System;
using System.Collections.Generic;
using System.Text;
using TriteMySqlParser.Parsers;
using Xunit;

namespace TriteMySqlParser.Tests.Parsers
{
    public class CommentsParserTests
    {
        // TODO: Should test the individual components directly as well (Parsing SingleLine and MultiLine comments)
        [Fact]
        public void CommentParser_ShouldParseAnyComment()
        {
            Assert.Equal("!40101 SET character_set_client = utf8 ", Comments.AnyComment.Parse("/*!40101 SET character_set_client = utf8 */;").Name);
            Assert.Equal("Table structure for table `sometable01` ", Comments.AnyComment.Parse("-- Table structure for table `sometable01` \n").Name);
        }
    }
}
