namespace Foundation.Data.Types
{ 
    public class NullColumn : TypedColumn<Null> {

        public static implicit operator Null(NullColumn c) => c.Value;
    }
}