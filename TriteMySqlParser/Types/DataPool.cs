using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TriteMySqlParser.Types
{
    // Is this lazy? Yes.
    // Bad design pracice? Totally.
    // Do I care? Not at the moment.
    public static class DataPoolGlobal
    {
        private static DataPool _pool = null;

        public static DataPool Pool
        {
            get
            {
                if (_pool is null)
                {
                    _pool = new DataPool();
                }
                return _pool;
            }
        }
    }

    public class DataPool
    {
        private Hashtable _value;
        private Hashtable _columnNameRef;

        private DataRow _currentRow;
        private string _currentTable;
        private IEnumerator _currentCol;

        public Hashtable Value
        {
            get { return _value; }
        }

        public string CurrentTable
        {
            get { return _currentTable; }
        }

        public DataPool()
        {
            _value = new Hashtable();
            _columnNameRef = new Hashtable();
        }

        public void Reset()
        {
            _value = new Hashtable();
            _columnNameRef = new Hashtable();
        }

        public void CreateTable(string tableName)
        {
            if (_value.ContainsKey(tableName))
            {
                throw new ArgumentException($"Attempted to add a table that already exists ({tableName})!");
            }

            _value.Add(tableName, new DataTable());
            _columnNameRef.Add(tableName, new List<string>());
            _currentTable = tableName;
        }

        public DataTable GetTable(string tableName)
        {
            return (DataTable)_value[tableName];
        }

        public void SetCurrentTable(string tableName)
        {
            _currentTable = tableName;
        }

        public List<string> GetColumnNameRefForTable(string tableName)
        {
            return (List<string>)_columnNameRef[tableName];
        }

        public void AddColumnToTable(string tableName, string colName, Type colType)
        {
            var table = GetTable(tableName);
            if (table.Columns.Contains(colName))
            {
                throw new ArgumentException($"Attempted to add a column that already exists in this table (table: {tableName} column: {colName})!");
            }
            table.Columns.Add(colName, colType);

            GetColumnNameRefForTable(tableName).Add(colName);
        }

        public void AddColumn(string colName, Type colType)
        {
            AddColumnToTable(_currentTable, colName, colType);
        }

        public void NewRow(string tableName)
        {
            _currentRow = GetTable(tableName).NewRow();
            _currentTable = tableName;
            var colRef = GetColumnNameRefForTable(_currentTable);
            _currentCol = colRef.GetEnumerator();
        }

        public void NewRowForCurrentTable()
        {
            // _currentRow = GetTable(_currentTable).NewRow();
            NewRow(_currentTable);
        }

        public void AddValueToCurrentRowByColumn<T>(string colName, T colVal)
        {
            _currentRow[colName] = colVal;
        }

        public string GetNextCol()
        {
            _currentCol.MoveNext();
            return _currentCol.Current as string;
        }

        public void AddNextValue<T>(T colVal)
        {
            var nextCol = GetNextCol();
            AddValueToCurrentRowByColumn(nextCol, colVal);
        }

        public void SaveCurrentRow()
        {
            GetTable(_currentTable).Rows.Add(_currentRow);
        }

        // Not sure yet if I care about following the "DROP TABLE IF EXISTS" statements, throwing this in for now though
        public void DropTableIfExists(string tableName)
        {
            if (_value.ContainsKey(tableName))
            {
                _value.Remove(tableName);
            }
        }
    }
}
