using System;
using System.Reflection;

namespace Foundation.Data.Entity
{
    public partial class DataEntity
    {

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
        internal Dictionary<string, Object> _colmap;

        public DataEntity()
        {
            _colmap = new Dictionary<string, Object>();
            EntityName = string.Empty;
        }
    }
}