using System.ComponentModel;
using System.Globalization;
using System.Collections.Generic;

namespace Foundation.Data.Types
{
    /// <summary>
    ///
    /// </summary>#
    /// <remarks>
    /// For conversion default is IntegerNumber(0), <see langword="null"/> is 0
    /// </remarks>
    [TypeConverter(typeof(Number.NumberConverter))]    
    public abstract partial class Number : TypeBase
    {
        #region Properties
        private static List<Type> _byPrec;
        #endregion
        static Number()
        {
            _converter = TypeDescriptor.GetConverter(typeof(Number));
            _byPrec = new List<Type>(){
                typeof(sbyte),
                typeof(byte),
                typeof(short),
                typeof(ushort),
                typeof(int),
                typeof(uint),
                typeof(long) ,  
                typeof(ulong),
                typeof(nint),
                typeof(nuint),
                typeof(float),
                typeof(double),
                typeof(decimal)
            };
        }
        public Number(string name)
            :base(name)
        {}
        public Number() {}

        #region Methods
        static private Number FactoryMethod(Object value)
        {
            if (_converter.CanConvertFrom(null, value.GetType()))
            {
                Object? o = _converter.ConvertFrom(null, null, value);
                if (o != null)
                    return (Number)o;
            }
            return new IntegerNumber(0);
        }
        static private T ToPrimitive<T>(Number n)
        {
            return (T)n.GetValue();
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            if (this.GetValue() == ((Number)obj).GetValue())
                return true;
            return false;
        }
        #endregion

        #region Converters
        public static implicit operator Number(sbyte i) => Number.FactoryMethod(i);
        public static implicit operator Number(byte i) => Number.FactoryMethod(i);
        public static implicit operator Number(short i) => Number.FactoryMethod(i);
        public static implicit operator Number(ushort i) => Number.FactoryMethod(i);
        public static implicit operator Number(int i) => Number.FactoryMethod(i);
        public static implicit operator Number(uint i) => Number.FactoryMethod(i);
        public static implicit operator Number(long i) => Number.FactoryMethod(i);
        public static implicit operator Number(ulong i) => Number.FactoryMethod(i);
        public static implicit operator Number(nint i) => Number.FactoryMethod(i);
        public static implicit operator Number(nuint i) => Number.FactoryMethod(i);
        public static implicit operator Number(float i) => Number.FactoryMethod(i);
        public static implicit operator Number(double i) => Number.FactoryMethod(i);
        public static implicit operator Number(decimal i) => Number.FactoryMethod(i);
        public static implicit operator byte(Number n) => Number.ToPrimitive<byte>(n); 
        public static implicit operator short(Number n) => Number.ToPrimitive<short>(n);
        public static implicit operator ushort(Number n) => Number.ToPrimitive<ushort>(n);
        public static implicit operator int(Number n) => Number.ToPrimitive<int>(n);
        public static implicit operator uint(Number n) => Number.ToPrimitive<uint>(n);
        public static implicit operator long(Number n) => Number.ToPrimitive<long>(n);
        public static implicit operator ulong(Number n) => Number.ToPrimitive<ulong>(n);
        public static implicit operator nint(Number n) => Number.ToPrimitive<nint>(n);
        public static implicit operator nuint(Number n) => Number.ToPrimitive<nuint>(n);
        public static implicit operator float(Number n) => Number.ToPrimitive<float>(n);
        public static implicit operator double(Number n) => Number.ToPrimitive<double>(n);
        public static implicit operator decimal(Number n) => Number.ToPrimitive<decimal>(n);
        #endregion

        #region Operators
        public static Number operator +(Number a) => a;
        public static Number operator -(Number a) => a * -1;
        public static Number operator ++(Number a) => a+1;
        public static Number operator --(Number a) => a -1;
        public static Number operator +(Number lhs, Number rhs)
        {
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                return 0;
            return Number.ToPrimitive<decimal>(lhs) + Number.ToPrimitive<decimal>(rhs);
        }
        public static Number operator -(Number lhs, Number rhs)
        {
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                return 0;
            return Number.ToPrimitive<decimal>(lhs) - Number.ToPrimitive<decimal>(rhs);
        }

        public static Number operator *(Number lhs, Number rhs)
        {
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                return 0;
            return Number.ToPrimitive<decimal>(lhs) * Number.ToPrimitive<decimal>(rhs);
        }

        public static Number operator /(Number lhs, Number rhs)
        {
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)|| rhs == 0)
                return 0;
            return Number.ToPrimitive<decimal>(lhs) / Number.ToPrimitive<decimal>(rhs);
        }
        public static bool operator ==(Number lhs, Number rhs)
        {
            if(ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                return false;
            return lhs.Equals(rhs);
        }
        public static bool operator !=(Number lhs, Number rhs)
        {
            if(ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                return false;
            return !lhs.Equals(rhs);
        }
        
        #endregion

        #region  SubTypes
        public class IntegerNumber : Number
        {
            public IntegerNumber(string name, int value)
            :base(name) {
                Value = value;
            }
            public IntegerNumber(){}
            public IntegerNumber(int value)
            :this("IntegerNumber", value)
            {}
            public IntegerNumber(string name)
            :base(name)
            {}
            public static implicit operator IntegerNumber(int i) => new IntegerNumber(i);
            public static implicit operator int(IntegerNumber i) => i.Value;
            public int Value {get;set;}
            public override object GetValue() => Value;
            public override void SetValue(object val) => Value = (int)val;
        }
        public class DecimalNumber : Number
        {
            public DecimalNumber(string name, decimal value)
            :base(name) {
                Value = value;
            }
            public DecimalNumber(){}
            public DecimalNumber(decimal value)
            :this("DecimalNumber", value)
            {}
            public DecimalNumber(string name)
            :base(name)
            {}
            public static implicit operator DecimalNumber(decimal i) => new DecimalNumber(i);
            public static implicit operator decimal(DecimalNumber i) => i.Value;
            public decimal Value {get;set;}
            public override object GetValue() => Value;
            public override void SetValue(object val) => Value = (decimal)val;
        }
        #endregion
        #region Converter
        public class NumberConverter : TypeConverter
        {
            static bool IsNum(Type? t) => t != null && _byPrec.Contains(t);
            public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
            {
                return IsNum(sourceType);
            }
            public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
            {
                return Number.FactoryMethod(value);
            }
            public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
            {
                return IsNum(destinationType);
            }
            public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
            {
                if (value==null)
                {
                    return null;
                } else if (destinationType == typeof(int)) {
                    return ((IntegerNumber)value).Value;
                } else if (destinationType == typeof(decimal)) {
                    return ((DecimalNumber)value).Value;
                } else {
                    return base.ConvertTo(context, culture,value,destinationType);
                }
            }
        }

        #endregion
    }
}