namespace Foundation.Data.Types
{ 
    public class IntegerColumn : TypedColumn<Number.IntegerNumber> {

        public static implicit operator Number(IntegerColumn c) => c.Value;
    }
    public class DecimalColumn : TypedColumn<Number.DecimalNumber> {

        public static implicit operator Number(DecimalColumn c) => c.Value;
    }
}