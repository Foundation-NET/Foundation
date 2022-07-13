using System.Collections.Generic;

namespace Foundation.Data
{
    public interface IConnection
    {
        void Open(string connectionString);
        List<DataResultSet> ExecuteQuery(QueryBuilder qb);
        void Close();

    }
}
