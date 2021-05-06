namespace TriteMySqlParser.Types.Literals
{
    public class HexString
    {
        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value.ToLower(); }
        }

        public HexString()
        {

        }

        public HexString(string value)
        {
            _value = value.ToLower();
        }

        public override string ToString()
        {
            return $"x'{_value}'";
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as HexString);
        }

        public bool Equals(HexString hex)
        {
            return this.Value == hex.Value;
        }
    }
}
