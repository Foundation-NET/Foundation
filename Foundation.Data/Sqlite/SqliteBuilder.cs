using Foundation.Data;
using Foundation.Contracts;
using Foundation.Annotations;
using Foundation.Data.Entity;
using Foundation.Data.Types;
using System.Data.SQLite;

namespace Foundation.Data.Sqlite
{
    public class SqliteBuilder : IConnection
    {
        private Contract Contract;
        QueryBuilder? queryBuilder;
        private SQLiteConnection? _conn;

        public SqliteBuilder()
        {
            Contract = new Contract(this);
            queryBuilder = null; 
        }

        [ContractedMethod]
        public void Open(string connectionString)
        {
            Contract.New("Open", runOnce: true);
            _conn = new SQLiteConnection(connectionString);
            Contract.Require(_conn);
            _conn.Open();
        }

        [ContractedMethod]
        public void Close()
        {
            Contract.New("Close", runOnce: true);
            Contract.Require(_conn);
            _conn.Close();
        }

        [ContractedMethod]
        public List<DataResultSet> ExecuteSelectQuery(QueryBuilder qb)
        {
            Contract.New("ExecuteQuery", runOnce:false);
            queryBuilder = qb;


            return new List<DataResultSet>();
        }
        public void CreateStructure()
        {
            Contract.New("CreateStructure");
            Contract.Require(_conn);
            Contract.Require(EntityRegister.GetDataEntities(), (x) => ((EntityCollection)x).Count() > 0);
            foreach(var v in EntityRegister.GetDataEntities())
            {
                //     string sql = @"
                // CREATE TABLE cars(id INTEGER PRIMARY KEY,
                // name TEXT, price INT)";

                // check if table exists? TSQL if Exists Do -->

                var pkeylist = v.GetPrimaryKeys();
                var fkeylist = v.GetForeignKeys();

                // get itypedcol PrimaryKey (check how swlite handles compund primary keys)


                string sql = "CREATE TABLE [" + v.EntityName + "](";

                
                ColumnCollection collection = v.GetColumns();
                foreach (ITypedColumn col in collection)
                {
                    // mark pkey
                    string colName = col.Name;
                    SQLiteTypes colType = GetColumnType(col); 
                    // append column to sql
                }
                //append foreign keys
                // append to sql run

            }
            // run SQL
        }
        public void UpdateRow(DataEntity d, bool InnerTransaction = true) {}
        public void UpdateRows(List<DataEntity> d, bool InnerTransaction = true) {}
        public void InsertRow(DataEntity d, bool InnerTransaction = true) {}
        public void InsertRows(DataEntity d, bool InnerTransaction = true) {}

        

        [ContractedMethod]
        private SQLiteTypes GetColumnType(ITypedColumn col)
        {
            Contract.New("GetColumnType");
            if(col.GetType() == typeof(IntegerColumn))
            {
                return SQLiteTypes.Integer;
            } else if (col.GetType() == typeof(DecimalColumn))
            {
                return SQLiteTypes.Real;
            } else if (col.GetType() == typeof(StringColumn))
            {
                return SQLiteTypes.Text;
            } else if (col.GetType() == typeof(CharColumn))
            {
                return SQLiteTypes.Text;
            } else if (col.GetType() == typeof(NullColumn))
            {
                return SQLiteTypes.Null;
            } else {
                return SQLiteTypes.Null;
            }
        }
        private enum SQLiteTypes
        {
            Null,
            Integer,
            Real,
            Text,
            Blob
        }

        internal class SQLTableDetails
        {
            string Name;
            bool PrimaryKey;
            SQLiteTypes Types;
        }

    }
}