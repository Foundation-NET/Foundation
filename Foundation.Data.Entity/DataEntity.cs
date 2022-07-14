using Foundation.Data.Types;
using System.Reflection;

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
        public bool ContainsColumn(ITypedColumn col) => _internalColumnCollection.Contains(col);
        public ColumnCollection GetColumns() => _internalColumnCollection;
        public PrimaryKey GetPrimaryKey()
        {
            List<ITypedColumn> columns = new List<ITypedColumn>();
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (var prop in properties)
            {
                Attribute[] attrs = Attribute.GetCustomAttributes(prop);
                foreach(Attribute attr in attrs)
                {
                    if (attr is PrimaryKeyAttribute)
                    {
                        ITypedColumn? col = (ITypedColumn?)prop.GetValue(this);
                        if (col != null)
                            columns.Add(col);
                    }
                }
            }
            PrimaryKey pkey = new PrimaryKey(columns.ToArray());
            if (columns.Count > 1)
                pkey.Compound = true;
            return pkey;
        }
        public List<ForeignKey> GetForeignKeys()
        {
            Dictionary<int, ForeignKey> foreignKeyListWithID = new Dictionary<int, ForeignKey>(); 
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (var prop in properties)
            {
                Attribute[] attrs = Attribute.GetCustomAttributes(prop);
                foreach(Attribute attr in attrs)
                {
                    
                    if (attr is ForeignKeyAttribute)
                    {
                        int id = ((ForeignKeyAttribute)attr).Id;
                        ITypedColumn? col = (ITypedColumn?)prop.GetValue(this);
                        if (foreignKeyListWithID.ContainsKey(id) && col != null)
                        {
                            foreignKeyListWithID[id].Add(col);
                        } else if (!foreignKeyListWithID.ContainsKey(id) && col != null)
                        {
                            foreignKeyListWithID.Add(id, new ForeignKey(id, col));
                        }
                    }
                }
            }
            List<ForeignKey> fKeyList = new List<ForeignKey>();
            foreach(var fKey in foreignKeyListWithID)
            {
                fKeyList.Add(fKey.Value);
            }
            return fKeyList;
        }
        public class PrimaryKey
        {
            public bool Compound;
            public List<ITypedColumn> Columns;
            public PrimaryKey(params ITypedColumn[] columns)
            {
                Columns = columns.ToList<ITypedColumn>();
            }
            public void Add(ITypedColumn col) => Columns.Add(col);
        }

        public class ForeignKey
        {
            public int Id;
            public Type? ForeignTable;
            public List<ITypedColumn> Columns;
            public ForeignKey(int id, params ITypedColumn[] columns)
            {
                Columns = columns.ToList<ITypedColumn>();
            }
            public void Add(ITypedColumn col) => Columns.Add(col);
        }
    }
}