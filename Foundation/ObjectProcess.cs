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

        public DataEntity? From {
            get {return _qb.From;}
            set {_qb.From = value;}
        }

        public QueryBuilder.RelationBuilder Relations {
            get {return _qb.Relations;}
        }
        public QueryBuilder.FilterBuilder Where {
            get {return _qb.Where;}
        }

        private DataResultRow? _currentRow;

        private ColumnCollection _columnCollection;
        private List<Tuple<DataEntity, ColumnBase>> _entityToColumnMap;

        public ObjectProcess()
        :base()
        {
            _DataSource = new DataSource();
            _qb = new QueryBuilder();
            _columnCollection = new ColumnCollection();
            _entityToColumnMap = new List<Tuple<DataEntity, ColumnBase>>();
        }

        public void Execute()
        {
            List<DataResultRow> queryResults = _DataSource.ExecuteQuery(_qb);
            OnStart();
            foreach(DataResultRow v in queryResults)
            {
                InRow(v);
            }

            OnEnd();
        }

        protected void AddColumn(DataEntity entity, ColumnBase column)
        {    _entityToColumnMap.Add(Tuple.Create(entity, column));     
            _columnCollection.Add(column);
        }

        private protected void InRow(DataResultRow d)        
        {
            _currentRow = d;
            foreach(var v in _columnCollection.SeclectAllColumns())
            {
                var entityColumnTuple = _entityToColumnMap.Find((x) => (x.Item2.Name == v.Name));
                if (entityColumnTuple != null)
                    _columnCollection.Update(entityColumnTuple.Item2, d.Get(entityColumnTuple.Item1, entityColumnTuple.Item2));
            }
            ProcessRow();
            
            SaveRow();
            foreach(var v in _columnCollection.SeclectAllColumns())
            {
                v.Set(new Object());
            }
            d = new DataResultRow();
        }

        private void SaveRow()
        {
            //Get changed columns, get primary key from data entity,
            //generate sql to update row based on primary key (do this in connection) for json backend?
        }

        #region Util functions for query / filter builder
        protected QueryBuilder.RelationBuilder.Relation.On On(IColumn left, Operators op, IColumn right)
        {
            return _qb.On(left, op, right);
        }

        protected QueryBuilder.FilterBuilder.Filter.Condition And(QueryBuilder.FilterBuilder.Filter.Condition left, QueryBuilder.FilterBuilder.Filter.Condition right)
        {
            return _qb.And(left, right);
        }
        protected QueryBuilder.FilterBuilder.Filter.Condition Or(QueryBuilder.FilterBuilder.Filter.Condition left, QueryBuilder.FilterBuilder.Filter.Condition right)
        {
            return _qb.Or(left, right);
        }
        protected QueryBuilder.FilterBuilder.Filter.Condition EqualTo(Object o)
        {
            return _qb.EqualTo(o);
        }
        protected QueryBuilder.FilterBuilder.Filter.Condition NotEqualTo(Object o)
        {
            return _qb.NotEqualTo(o);
        }
        protected QueryBuilder.FilterBuilder.Filter.Condition MoreThan(Object o)
        {
            return _qb.MoreThan(o);
        }
        protected QueryBuilder.FilterBuilder.Filter.Condition LessThan(Object o)
        {
            return _qb.LessThan(o);
        }
        #endregion

        public virtual void OnStart() {}
        public virtual void ProcessRow() {}
        public virtual void OnEnd() {}

    }
}