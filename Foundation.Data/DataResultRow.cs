using Foundation.Data.Entity;

namespace Foundation.Data
{
    public class DataResultRow
    {
        private Dictionary<IColumn, Object> Results;

        public DataResultRow()
        {
            Results = new Dictionary<IColumn, Object>();
        }

        public Object Get(IColumn col)
        {
            return Results[col];
        }

        public void Set(IColumn col, Object value)
        {
            Results.Add(col, value);
        }
    }
}