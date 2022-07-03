namespace Foundation.Data.Entity
{
    public class StringColumn : DataColumn<string>
    {
        public StringColumn(DataEntity entity)
        :base(entity) {}
        public StringColumn(DataEntity entity, string name)
        :base(entity, name) {}
    }
}