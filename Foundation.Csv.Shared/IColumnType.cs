namespace Foundation.Csv.Shared
{
    public interface IColumnType<T>
    {
        string? GetValue();
        void SetValue(string val);
        T? GetPrimitive();
        void SetPrimitive(T val);
    }
}