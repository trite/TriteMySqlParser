using System;
using System.Collections.Generic;
using System.Text;
using TriteMySqlParser.Types;
using Xunit;

namespace TriteMySqlParser.Tests.Types
{
    public class DataPoolTests
    {
        [Fact]
        public void FiguringThisOut()
        {
            var pool = DataPoolGlobal.Pool;
            pool.CreateTable("TestTable01");
            pool.AddColumn("TestCol01", typeof(int));
            pool.AddColumn("TestCol02", typeof(string));
            pool.NewRowForCurrentTable();
            pool.AddNextValue<int>(123);
            pool.AddNextValue<string>("blah");
            pool.SaveCurrentRow();

            pool.CreateTable("TestTable02");
            pool.AddColumn("MoreTestCols01", typeof(int));
            pool.AddColumn("MoreTestCols02", typeof(string));
            pool.AddColumn("MoreTestCols03", typeof(string));
            pool.NewRowForCurrentTable();
            pool.AddNextValue<int>(234);
            pool.AddNextValue<string>("foo");
            pool.AddNextValue<string>("bar");
            pool.SaveCurrentRow();

            var testing = pool.GetTable("TestTable01");
        }
    }
}
