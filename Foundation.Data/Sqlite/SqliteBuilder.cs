using Foundation.Data;
using Foundation.Contracts;
using Foundation.Annotations;

namespace Foundation.Data.Sqlite
{
    public class SqliteBuilder : IConnection
    {
        private Contract Contract;
        QueryBuilder? queryBuilder;

        public SqliteBuilder()
        {
            queryBuilder = null; 
        }

        [ContractedMethod]
        public void Open(string connectionString)
        {
            Contract.New("Open", runOnce: true);
        }

        [ContractedMethod]
        public void Close()
        {
            Contract.New("Close", runOnce: true);
        }

        [ContractedMethod]
        public List<DataResultSet> ExecuteQuery(QueryBuilder qb)
        {
            Contract.New("ExecuteQuery", runOnce:false);
            queryBuilder = qb;


            return new List<DataResultSet>();
        }

    }
}