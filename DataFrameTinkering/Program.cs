using System;
using System.Data;
using Microsoft.Data.Analysis;

namespace DataFrameTinkering
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: this should work fine I think
            var test = new DataTable();
            
            test.Columns.Add("index", typeof(int));
            test.Columns.Add("name", typeof(string));
            test.Columns.Add("message", typeof(string));
            test.Columns.Add("timestamp", typeof(DateTime));

            test.Rows.Add(0, "Paul", "Foo", DateTime.Now);
            test.Rows.Add(0, "Pouru", "Bar", DateTime.Now);
            test.Rows.Add(0, "Trite", "Baz", DateTime.Now);
            test.Rows.Add(0, "Pbrundog", "Bang", DateTime.Now);

            //  var testDR = new DataRow("blah", DateTime.Now);
            //  Add data incrementally using the example here:
            //  https://docs.microsoft.com/en-us/dotnet/api/system.data.datarow?view=net-5.0

            Console.WriteLine("Hello World!");
        }

    }
}
