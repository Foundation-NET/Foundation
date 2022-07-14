using System.Collections.Generic;

namespace Foundation.Data
{
    public class DataSource 
    {
        private IConnection? _Connection;
        public void EnsureConnection(string connstring)
        {
            _Connection.Open(connstring);
        }

        public List<DataResultSet> ExecuteQuery(QueryBuilder query)
        {
            return _Connection.ExecuteSelectQuery(query);
        }

        public void Close()
        {
            _Connection.Close();
        }
    }
}
