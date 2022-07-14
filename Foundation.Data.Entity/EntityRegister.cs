using Foundation.Contracts;
using Foundation.Annotations;

namespace Foundation.Data.Entity
{
    public static class EntityRegister
    {
        private static EntityCollection Collection;
        private static Contract Contract;

        static EntityRegister()
        {
            Collection = new EntityCollection();
            Contract = new Contract(Contract.Type.Static);
        }

        [ContractedMethod]
        public static void RegisterEntity(DataEntity e)
        {
            Contract.New("RegisterEntity", runOnce:false);
            Contract.Require(Collection);
            Contract.Require(e);
            Collection.Add(e);
        }

        public static EntityCollection GetDataEntities()
        {
            Contract.New("GetDataEntities");
            Contract.Require(Collection);
            return Collection;
        }
    }
}