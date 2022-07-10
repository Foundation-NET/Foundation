using System.ComponentModel;
using System.Globalization;
using System.Collections.Generic;

namespace Foundation.Data.Types
{
    [TypeConverter(typeof(Text.TextConverter))]    
    public abstract partial class Text : TypeBase
    {
        #region Properties
        #endregion
        static Text()
        {
            _converter = TypeDescriptor.GetConverter(typeof(Text));
        }
        public Text(string name)
            :base(name)
        {}
        public Text() {}

        #region Methods
        static private Text FactoryMethod(Object value)
        {
            if (_converter.CanConvertFrom(null, value.GetType()))
            {
                Object? o = _converter.ConvertFrom(null, null, value);
                if (o != null)
                    return (Text)o;
            }
            return new StringText("");
        }
        static private T ToPrimitive<T>(Text n)
        {
            return (T)n.GetValue();
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            if (this.GetValue() == ((Text)obj).GetValue())
                return true;
            return false;
        }
        #endregion

        #region Converters
 
        public static implicit operator Text(string i) => Text.FactoryMethod(i);
        public static implicit operator string(Text n) => Text.ToPrimitive<string>(n);
        public static implicit operator Text(char i) => Text.FactoryMethod(i);
        public static implicit operator char(Text n) => Text.ToPrimitive<char>(n); 

        #endregion

        #region Operators
        public static Text operator +(Text a) => a;
        public static Text operator +(Text lhs, Text rhs)
        {
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                return "";
            return Text.ToPrimitive<string>(lhs) + Text.ToPrimitive<string>(rhs);
        }
       
        #endregion

        #region  SubTypes
        public class StringText : Text
        {
            public StringText(string name, string value)
            :base(name) {
                Value = value;
            }
            public StringText(){}
            public StringText(string value)
            :base("StringText")
            {
                Value = value;
            }
            public static implicit operator StringText(string i) => new StringText(i);
            public static implicit operator string(StringText i) => i.Value;
            public string Value {get;set;}
            public override object GetValue() => Value;
            public override void SetValue(object val) => Value = (string)val;
        }
        public class CharText : Text
        {
            public CharText(string name, char value)
            :base(name) {
                Value = value;
            }
            public CharText(){}
            public CharText(string name)
            :base(name)
            {}
            public CharText(char c)
            :base("CharText")
            {
                Value = c;
            }
            public static implicit operator CharText(char i) => new CharText(i);
            public static implicit operator char(CharText i) => i.Value;
            public char Value {get;set;}
            public override object GetValue() => Value;
            public override void SetValue(object val) => Value = (char)val;
        }
        
        #endregion
        #region Converter
        public class TextConverter : TypeConverter
        {
            static bool IsText(Type? t) => t != null && (t == typeof(string) || t == typeof(char));
            public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
            {
                return IsText(sourceType);
            }
            public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
            {
                return Text.FactoryMethod(value);
            }
            public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
            {
                return IsText(destinationType);
            }
            public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
            {
                if (value==null)
                {
                    return null;
                } else if (destinationType == typeof(string)) {
                    return ((StringText)value).Value;
                } else if (destinationType == typeof(char)) {
                    return ((CharText)value).Value;
                } else {
                    return base.ConvertTo(context, culture,value,destinationType);
                }
            }
        }

        #endregion
    }
}