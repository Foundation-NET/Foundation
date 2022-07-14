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

        public Dictionary<DataEntity, DataEntity.PrimaryKey> _primaryKeysForEntities;
        public Dictionary<DataEntity, List<DataEntity.ForeignKey>> _foreignKeysForEntities;
        public bool Built;

        public QueryBuilder()
        {
            Contract = new Contract(this);
            _columns = new ColumnCollection();
            _entities = new EntityCollection();
            _primaryKeysForEntities = new Dictionary<DataEntity, DataEntity.PrimaryKey>();
            _foreignKeysForEntities = new Dictionary<DataEntity, List<DataEntity.ForeignKey>>();
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
                _primaryKeysForEntities.Add(entity, entity.GetPrimaryKey());
                _foreignKeysForEntities.Add(entity, entity.GetForeignKeys());
            }
            Built = true;
        }
    }
}