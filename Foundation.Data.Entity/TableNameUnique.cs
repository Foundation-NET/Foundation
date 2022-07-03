using System.Collections.Generic;
using System;

namespace Foundation.Data.Entity
{
    public class TableNameUnique
    {
        static private List<string> _nameRegistry;
        public string Value;

        static TableNameUnique()
        {
            _nameRegistry = new List<string>();
        }
        public TableNameUnique(string tbl)
        {
            if(_nameRegistry.Contains(tbl))
            {
                throw new Exception("Table Name / Column name must not duplicate");
            } else {
                _nameRegistry.Add(tbl);
                Value = tbl;
            }
        }
    }
}