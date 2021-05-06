using System;
using System.Collections.Generic;
using System.Text;
using TriteMySqlParser.Types;

namespace TriteMySqlParser.Commands
{
    public class AddValue<T> : ICommand
    {
        public string Name { get; set; }

        public T Value { get; set; }

        public AddValue(string name, T value)
        {
            Name = name;
            Value = value;
        }

        public string Execute()
        {
            DataPoolGlobal.Pool.AddNextValue<T>(Value);
            return $"Added value: {Value} of Type: {Value.GetType()}";
        }
    }
}
