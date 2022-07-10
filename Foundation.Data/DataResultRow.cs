using Foundation.Data.Entity;
using Foundation.Data.Types;

namespace Foundation.Data
{
    public class DataResultRow
    {
        private Dictionary<Tuple<DataEntity, string>, Object> results;

        public DataResultRow()
        {
            results = new Dictionary<Tuple<DataEntity, string>, object>();
        }

        public Object Get(DataEntity entity, ColumnBase col)
        {
            return results[Tuple.Create(entity, col.Name)];
        }

        public void Set(DataEntity entity, ColumnBase col, Object value)
        {
            results.Add(Tuple.Create(entity, col.Name), value);
        }
    }
}