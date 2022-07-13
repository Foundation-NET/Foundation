using System.Collections.Generic;

namespace Foundation.Data
{
    public class DataSource 
    {
        private IConnection _Connection;
        public DataSource()
        {
            _Connection = new tmpconn(); 
        }

        public void EnsureConnection(string connstring)
        {
            _Connection.Open(connstring);
        }

        public List<DataResultSet> ExecuteQuery(QueryBuilder query)
        {
            return _Connection.ExecuteQuery(query);
        }

        public void Close()
        {
            _Connection.Close();
        }

        public class tmpconn : IConnection
        {
            public void Open(string s)
            {
                throw new NotImplementedException();
            }
            public List<DataResultSet> ExecuteQuery(QueryBuilder s)
            {
                throw new NotImplementedException();
            }
            public void Close()
            {
                throw new NotImplementedException();
            }
        }
    }
}
