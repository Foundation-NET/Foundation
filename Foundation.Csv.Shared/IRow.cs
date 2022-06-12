namespace Foundation.Csv.Shared
{
    public interface IRow
    {
        Object? GetColByName(string key);
        Object? GetColByIndex(int index);
        void SetColByName(string key, Object obj);
        void SetColByIndex(int index, Object obj);
        void CreateColumn(string colname, Type type);
    }
}