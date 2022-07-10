using System.ComponentModel;

namespace Foundation.Data.Types
{
    public abstract class TypeBase
    {
        protected static TypeConverter _converter;
        public string Name {get;set;}
        public TypeBase(string name)
        {
            Name = name;
        }
        public TypeBase()
        {}

        public abstract object GetValue();
        public abstract void SetValue(object val);
    }
}
