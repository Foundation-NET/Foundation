using Foundation.Data.Entity;
using Foundation.Data.Types;

namespace Foundation.Data
{
    public class DataResultSet
    {
        private Dictionary<Tuple<DataEntity, string>, Object> results;

        public DataResultSet()
        {
            results = new Dictionary<Tuple<DataEntity, string>, object>();
        }

        public Object Get(DataEntity entity, ITypedColumn col)
        {
            return results[Tuple.Create(entity, col.Name)];
        }

        public void Set(DataEntity entity, ITypedColumn col, Object value)
        {
            results.Add(Tuple.Create(entity, col.Name), value);
        }
    }
}