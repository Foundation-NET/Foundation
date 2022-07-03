namespace Foundation.Data.Entity
{
    public interface IColumn
    {
        string GetFullName();
        DataEntity GetTable();
        void SetValue(Object o);
    }
}