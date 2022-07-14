using System.Collections.Generic;
using Foundation.Data.Entity;

namespace Foundation.Data
{
    public interface IConnection
    {
        void Open(string connectionString);
        List<DataResultSet> ExecuteSelectQuery(QueryBuilder qb);
        void UpdateRow(DataEntity d, bool InnerTransaction = true);
        void UpdateRows(List<DataEntity> d, bool InnerTransaction = true);
        void InsertRow(DataEntity d, bool InnerTransaction = true);
        void InsertRows(DataEntity d, bool InnerTransaction = true);
        void CreateStructure();
        void Close();

    }
}
