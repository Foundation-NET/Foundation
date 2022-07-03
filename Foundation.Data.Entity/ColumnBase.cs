namespace Foundation.Data.Entity
{
    public class DataColumn<T> : IColumn
    {
        public DataEntity _table;

        private ColumnNameUnique? _name;
        public string Name {
            get {
                if (_name == null)
                {
                    throw new Exception("Name must be declared");
                }
                return _name.Value;}
            set { 
                if (value == null)
                {
                    throw new Exception("Name may not be null");
                } else {
                    _name = new ColumnNameUnique(_table.EntityName, value);
                    _table._colmap.Add(value, this);
                }
            }
        }

        public string GetFullName()
        {
            return _table.EntityName + "." + Name;
        }
        public DataEntity GetTable()
        {
            return _table;
        }
        public void SetValue(Object o)
        {
            Value = (T)o;
        }

        private T? _value;
        public T? Value {
            get {
                return _value;}
            set {
                _value = value;
            }
        }

        public DataColumn(DataEntity entity, string name)
        {
            _table = entity;
            Name = name;
        }
        public DataColumn(DataEntity entity)
        {
            _table = entity;
        }
    }
}
