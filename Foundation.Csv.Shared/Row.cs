using System.Collections.Generic;

namespace Foundation.Csv.Shared
{
    public class Row : IRow, ICloneable
    {
        private Dictionary<string, Object?> _columnstore;
        private Dictionary<string, Type> _typemap;

        public Row()
        {
            _columnstore = new Dictionary<string, Object?>();
            _typemap = new Dictionary<string, Type>();
        }
        public Object? GetColByName(string key)
        {
            return _columnstore[key];
        }
        public Object? GetColByIndex(int index)
        {
            return _columnstore.ElementAt(index).Value;
        }
        public void SetColByName(string key, Object val)
        {
            if (val.GetType() != _typemap[key])
                throw new Exception("Type Mismatch");
            _columnstore[key] = val;
        }
        public void SetColByIndex(int index, Object val)
        {
            var key = _columnstore.ElementAt(index).Key;
            if (val.GetType() != _typemap[key])
                throw new Exception("Type Mismatch");
            _columnstore[key] = val;
        }
        public void CreateColumn(string colname, Type type)
        {
            _columnstore.Add(colname, null);
            _typemap.Add(colname, type);
        }

        public Object Clone()
        {
            return new Row{
                _columnstore = new Dictionary<string, object?>(this._columnstore),
                _typemap = new Dictionary<string, Type>(this._typemap)
            };
        }
    }
}