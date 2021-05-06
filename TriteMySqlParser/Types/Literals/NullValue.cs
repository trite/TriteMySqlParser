using System;
using System.Collections.Generic;
using System.Text;

namespace TriteMySqlParser.Types.Literals
{
    public class NullValue
    {
        public override bool Equals(object obj)
        {
            return this.Equals(obj as NullValue);
        }

        public bool Equals(NullValue val)
        {
            return (val is NullValue) || (val is null);
        }
    }
}
