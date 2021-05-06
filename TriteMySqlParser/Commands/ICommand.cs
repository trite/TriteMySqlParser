using System;
using System.Collections.Generic;
using System.Text;

namespace TriteMySqlParser.Commands
{
    public interface ICommand
    {
        string Name { get; set; }
        string Execute();
    }
}
