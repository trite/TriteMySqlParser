using Sprache;
using System;
using System.Collections.Generic;
using System.Text;

namespace TriteMySqlParser.Parsers.Others
{
    public class Misc
    {
        public static readonly Parser<char> SemiColon = Parse.Char(';');
    }
}
