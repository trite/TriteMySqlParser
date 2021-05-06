using System;
using System.Collections.Generic;
using System.Text;
using TriteMySqlParser.Types;

namespace TriteMySqlParser.Commands
{
    public class AddRow : ICommand
    {
        public string Name { get; set; }

        public IEnumerable<ICommand> ValuesToAdd { get; set; }

        //public AddRow(string name, IEnumerable<ICommand> valuesToAdd)
        //{
        //    Name = name;
        //    ValuesToAdd = valuesToAdd;
        //}

        public AddRow(string name, IEnumerable<string> valuesToAdd)
        {
            Name = name;
            if (valuesToAdd is IEnumerable<ICommand>)
            {
                ValuesToAdd = valuesToAdd as IEnumerable<ICommand>;
            }
            else
            {
                var valueCommands = new List<ICommand>();
                foreach (var val in valuesToAdd)
                {
                    var cmd = new AddValue<string>(name, val);
                    valueCommands.Add(cmd);
                }
                ValuesToAdd = valueCommands;
            }
        }

        public string Execute()
        {
            string result = "Added row: [";
            DataPoolGlobal.Pool.NewRowForCurrentTable();
            foreach (var val in ValuesToAdd)
            {
                result += val.Execute() + ", ";
            }
            DataPoolGlobal.Pool.SaveCurrentRow();
            return result.Trim(' ').Trim(',') + "]";
        }
    }
}
