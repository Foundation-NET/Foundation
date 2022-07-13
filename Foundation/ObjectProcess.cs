using System.Collections.Generic;
using System.Reflection;
using System;
using Foundation.Data;
using Foundation.Data.Entity;
using Foundation.Data.Types;

namespace Foundation
{
    public partial class ObjectProcess : ObjectBase
    {
        private DataSource _DataSource;
        private QueryBuilder _qb;
        private EntityCollection _entityCollection;
        private DataResultSet? _currentRow;
        private ColumnCollection _columnCollection;
        private List<Tuple<DataEntity, ITypedColumn>> _entityToColumnMap;
        public ObjectProcess()
        :base()
        {
            _DataSource = new DataSource();
            _qb = new QueryBuilder();
            _columnCollection = new ColumnCollection();
            _entityCollection = new EntityCollection();
            _entityToColumnMap = new List<Tuple<DataEntity, ITypedColumn>>();
        }

        public void Execute()
        {
            _qb.AddEntities(_entityCollection);
            _qb.AddColumns(_columnCollection);
            _qb.Build();
            List<DataResultSet> queryResults = _DataSource.ExecuteQuery(_qb);
            OnStart();
            foreach(DataResultSet v in queryResults)
            {
                InRow(v);
            }

            OnEnd();
        }
        protected void AddEntity(DataEntity entity)
        {
            _entityCollection.Add(entity);
        }
        protected void AddColumn(ITypedColumn column)
        {
            DataEntity? e = null;
            foreach(DataEntity table in _entityCollection)
            {
                if(table.ContainsColumn(column))
                {
                    e = table;
                    break;
                }                    
            }
            if (e==null)
            {
                throw new Exception("Column not found in EntityCollection");
            }
            _entityToColumnMap.Add(Tuple.Create(e, column));     
            _columnCollection.Add(column);
        }

        private protected void InRow(DataResultSet d)        
        {
            _currentRow = d;
            foreach(ITypedColumn v in _columnCollection)
            {
                var entityColumnTuple = _entityToColumnMap.Find((x) => (x.Item2.Name == v.Name));
                if (entityColumnTuple != null)
                    v.Set(d.Get(entityColumnTuple.Item1, entityColumnTuple.Item2));
            }
            ProcessRow();
            
            SaveRow();
            foreach(var v in _columnCollection)
            {
                v.Set(new Object());
            }
            _currentRow = new DataResultSet();
        }

        private void SaveRow()
        {
            List<ITypedColumn> altered = new List<ITypedColumn>();
            foreach(ITypedColumn v in _columnCollection)
            {
                if (v.Changed)
                    altered.Add(v);
            }
            
        }

       


        public virtual void OnStart() {}
        public virtual void ProcessRow() {}
        public virtual void OnEnd() {}

    }
}