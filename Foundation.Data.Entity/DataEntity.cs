using System;
using System.Reflection;
using Foundation.Data.Types;

namespace Foundation.Data.Entity
{
    public partial class DataEntity
    {
        private ColumnCollection _internalColumnCollection;
        private List<string> _columnNames;
        private TableNameUnique? _name;
        public string EntityName {
            get {
                if (_name == null)
                {
                    throw new Exception("Name must be declared");
                }
                return _name.Value;
                }
            set { 
                if (value == null)
                {
                    throw new Exception("Name may not be null");
                } else {
                    _name = new TableNameUnique(value);
                }
            }
        }
        public DataEntity()
        {
            EntityName = string.Empty;
            _internalColumnCollection = new ColumnCollection();
            _columnNames = new List<string>();
        }
        protected void AddColumn(ITypedColumn col) 
        {
            if (_columnNames.Contains(col.Name))
            {
                _columnNames.Add(col.Name);
                _internalColumnCollection.Add(col);
            }
            throw new Exception("Cannot have duplicate column name in an entity");
        }
    }
}