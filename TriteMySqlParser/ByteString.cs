namespace TriteMySqlParser
{
    public class BitString
    {
        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value.ToLower(); }
        }

        public BitString()
        {

        }

        public BitString(string value)
        {
            _value = value.ToLower();
        }

        public override string ToString()
        {
            return $"b'{_value}'";
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as BitString);
        }

        public bool Equals(BitString hex)
        {
            return this.Value == hex.Value;
        }
    }
}
