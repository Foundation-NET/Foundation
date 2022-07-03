using System.Collections.Generic;

namespace Foundation.Data
{
    public interface IConnection
    {
        void Open(string connectionString);
        List<DataResultRow> ExecuteQuery(QueryBuilder qb);
        void Close();

    }
}
