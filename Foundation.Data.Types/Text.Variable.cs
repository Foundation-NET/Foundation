namespace Foundation.Data.Types
{ 
    public class StringColumn : TypedColumn<Text.StringText> {

        public static implicit operator Text(StringColumn c) => c.Value;
    }
    public class CharColumn : TypedColumn<Text.CharText> {

        public static implicit operator Text(CharColumn c) => c.Value;
    }
}