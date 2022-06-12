namespace Foundation.Csv.Shared
{
    public class GenericColumn<T> : IColumnType<T> where T : IConvertible
    {
        private T? _primitive;
        private string? Value;
        public string? GetValue()
        {
            return Value;
        }

        public void SetValue(string val)
        {
            Value = val;
            _primitive = (T)Convert.ChangeType(val, typeof(T));
        }

        public T? GetPrimitive()
        {
            return _primitive;
        }
        public void SetPrimitive(T val)
        {
            _primitive = val;
            Value = val.ToString();
        }
    }
}