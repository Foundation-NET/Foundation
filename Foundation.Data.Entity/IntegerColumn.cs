namespace Foundation.Data.Entity
{
    public class IntegerColumn : DataColumn<int>
    {
        public IntegerColumn(DataEntity entity)
        :base(entity) {}
        public IntegerColumn(DataEntity entity, string name)
        :base(entity, name) {}
    }
}