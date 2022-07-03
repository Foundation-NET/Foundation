using System.Collections.Generic;
using System;

namespace Foundation.Data.Entity
{
    public class ColumnNameUnique
    {
        static private List<Tuple<string,string>> _nameRegistry;
        public string Value;

        static ColumnNameUnique()
        {
            _nameRegistry = new List<Tuple<string, string>>();
        }
        public ColumnNameUnique(string tbl, string col)
        {
            var T = Tuple.Create<string, string>(tbl, col);
            if(_nameRegistry.Contains(T))
            {
                throw new Exception("Table Name / Column name must not duplicate");
            } else {
                _nameRegistry.Add(T);
                Value = col;
            }
        }
    }
}