namespace Foundation.Data.Types
{
    public class Null : TypeBase
    {
        
        public override object? GetValue() => null;
        public override void SetValue(object val) {}
        public object? Value {
            get {return null;}
        }

        public Null(string name)
            :base(name)
        {}
        public Null() {}

        public static implicit operator Null(Number n) => new Null();
        public static implicit operator Null(Text n) => new Null();
        public static implicit operator Number?(Null n) => null;
        public static implicit operator Text?(Null n) => null;
        public static bool operator ==(Null lhs, Null rhs)
        {
            if((ReferenceEquals(lhs, null) && rhs.GetType() == typeof(Null)) || ReferenceEquals(rhs, null)  && lhs.GetType() == typeof(Null)) 
                return true;
            return lhs.Equals(rhs);
        }
        public static bool operator !=(Null lhs, Null rhs)
        {
            if((ReferenceEquals(lhs, null) && rhs.GetType() == typeof(Null)) || ReferenceEquals(rhs, null)  && lhs.GetType() == typeof(Null)) 
                return false;
            return !lhs.Equals(rhs);
        }
    }
}