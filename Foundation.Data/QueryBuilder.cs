using System.Collections.Generic;
using Foundation.Data.Entity;
using Foundation.Data.Types;
using Foundation.Contracts;
using Foundation.Annotations;


namespace Foundation.Data
{
    public class QueryBuilder
    {
        private Contract Contract;
        public ColumnCollection _columns;
        public EntityCollection _entities;

        public Dictionary<DataEntity, List<PrimaryKey>> _primaryKeysForEntities;
        public Dictionary<DataEntity, List<ForeignKey>> _foreignKeysForEntities;
        public bool Built;

        public QueryBuilder()
        {
            Contract = new Contract(this);
            _columns = new ColumnCollection();
            _entities = new EntityCollection();
            _primaryKeysForEntities = new Dictionary<DataEntity, List<PrimaryKey>>();
            _foreignKeysForEntities = new Dictionary<DataEntity, List<ForeignKey>>();
            Built = false;
        }
        [ContractedMethod]
        public void AddColumns(ColumnCollection c)
        {
            Contract.New("AddColumns");
            Contract.Require(_columns);
            _columns = c;
        }
        [ContractedMethod]
        public void AddEntities(EntityCollection e) 
        {
            Contract.New("AddEntities");
            Contract.Require(_entities);
            _entities = e;
        }
        [ContractedMethod]
        public void Build()
        {
            // Chcek required instance variables
            Contract.New("Build");
            Contract.Require(_columns, (x) => ((ColumnCollection)x).Count() > 0);
            Contract.Require(_entities, (x) => ((EntityCollection)x).Count() > 0);
            Contract.Require(_primaryKeysForEntities);
            Contract.Require(_foreignKeysForEntities);
            // Get PrimaryKey from entities
            foreach (var entity in _entities)
            {
                Dictionary<int, PrimaryKey> primaryKeyListWithID = new Dictionary<int, PrimaryKey>(); 
                Dictionary<int, ForeignKey> foreignKeyListWithID = new Dictionary<int, ForeignKey>(); 
                foreach (var prop in entity.GetType().GetProperties())
                {
                    Attribute[] attrs = Attribute.GetCustomAttributes(prop);

                    foreach(Attribute attr in attrs)
                    {
                        
                        if (attr is PrimaryKeyAttribute)
                        {
                            int id = ((PrimaryKeyAttribute)attr).Id;
                            ITypedColumn? col = (ITypedColumn?)prop.GetValue(entity);
                            if (primaryKeyListWithID.ContainsKey(id) && col != null)
                            {
                                primaryKeyListWithID[id].Add(col);
                            } else if (!primaryKeyListWithID.ContainsKey(id) && col != null)
                            {
                                primaryKeyListWithID.Add(id, new PrimaryKey(id, col));
                            }
                        } else if (attr is ForeignKeyAttribute)
                        {
                            int id = ((ForeignKeyAttribute)attr).Id;
                            ITypedColumn? col = (ITypedColumn?)prop.GetValue(entity);
                            Type? ent  = ((ForeignKeyAttribute)attr).Table;
                            if(foreignKeyListWithID.ContainsKey(id) && col != null)
                            {
                                foreignKeyListWithID[id].Add(col);
                                foreignKeyListWithID[id].ForeignTable = ent;
                            } else if (!foreignKeyListWithID.ContainsKey(id) && col != null)
                            {
                                foreignKeyListWithID.Add(id, new ForeignKey(id, col));
                                foreignKeyListWithID[id].ForeignTable = ent;
                            }

                        }
                    }

                }
                List<PrimaryKey> pKeyList = new List<PrimaryKey>();
                List<ForeignKey> fKeyList = new List<ForeignKey>();
                foreach(var pKey in primaryKeyListWithID)
                {
                    pKeyList.Add(pKey.Value);
                }
                foreach(var fKey in foreignKeyListWithID)
                {
                    fKeyList.Add(fKey.Value);
                }
                _primaryKeysForEntities.Add(entity, pKeyList);
                _foreignKeysForEntities.Add(entity, fKeyList);

            }
            Built = true;
        }


        public class PrimaryKey
        {
            public int Id;
            public List<ITypedColumn> Columns;
            public PrimaryKey(int id, params ITypedColumn[] columns)
            {
                Columns = columns.ToList<ITypedColumn>();
            }
            public void Add(ITypedColumn col) => Columns.Add(col);
        }

        public class ForeignKey
        {
            public int Id;
            public Type? ForeignTable;
            public List<ITypedColumn> Columns;
            public ForeignKey(int id, params ITypedColumn[] columns)
            {
                Columns = columns.ToList<ITypedColumn>();
            }
            public void Add(ITypedColumn col) => Columns.Add(col);
        }
    }
}