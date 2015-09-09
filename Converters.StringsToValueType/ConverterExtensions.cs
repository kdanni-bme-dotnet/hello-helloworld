using Converters.StringsToValueType.CustomConverters.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converters.StringsToValueType
{
    /// <summary>
    /// Extension methos of string providing conversions to primitve types and specials
    /// cases like GUID/ENUM and Byte[] of Base64 string.
    /// </summary>
    public static class ConverterExtensions
    {
        /// <summary>
        /// Method can be used to convert primitive types. It supports only converting IConvertible structs.
        /// </summary>
        /// <typeparam name="T">type for cast</typeparam>
        /// <param name="value">string representation of type value</param>
        /// <returns>casted value of given type</returns>
        public static T To<T>(this string value) // where T: struct
        {
            var newType = typeof(T);

            // Handle special cases first, supports Enum, DateTime, Guid etc.
            // Tester-Doer for special cases.
            object result;

            if (TryConvertTo<T>(value, out result))
            {
                return (T)result;
            }

            // 
            if (string.IsNullOrWhiteSpace(value) || value.ToLower().Equals("null"))
            {
                return default(T);
            }

            if (newType.IsGenericType && newType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                var underlyingType = new NullableConverter(newType).UnderlyingType;

                return (T)Convert.ChangeType(value, underlyingType);
            }

            if (typeof(IConvertible).IsAssignableFrom(newType))
            {
                return (T)Convert.ChangeType(value, newType);
            }

            throw new InvalidCastException(string.Format("Unable to cast the {0} to type {1}", value, newType));
        }

        /// <summary>
        /// Method can be used to convert primitive types. It supports only converting IConvertible structs.
        /// </summary>
        /// <typeparam name="T">type for cast</typeparam>
        /// <param name="value">string representation of type value</param>
        /// <returns>casted value of given type</returns>
        public static T To<T>(this string value, IConverter<T> converter)
        {
            T result = default(T);

            if(converter != null)
            {
                result = converter.Convert(value);
            }

            if(result.Equals(default(T)))
            {
                result = value.To<T>();
            }

            return result;
        }

        /// <summary>
        /// Try converting by casting to special types struct like Enum, Guid and DateTime.
        /// </summary>
        /// <typeparam name="T">type for cast</typeparam>
        /// <param name="value">string representation of type value</param>
        /// <param name="result">casted value of given type</param>
        /// <returns>true, if cast is successful otherwise false.</returns>
        private static bool TryConvertTo<T>(this string value, out object result)
        {
            var newType = typeof(T);

            if (newType == typeof(Guid))
            {
                return TryGuid(value, out result);
            }

            if (newType == typeof(DateTime))
            {
                return TryDateTime(value, out result);
            }

            if (newType.IsEnum)
            {
                result = Enum.Parse(newType, value, true);
                return true;
            }

            result = default(T);

            return false;
        }

        /// <summary>
        /// Try to parse the string to Guid.
        /// </summary>
        /// <typeparam name="T">type for cast</typeparam>
        /// <param name="value">string representation of type value</param>
        /// <param name="result">casted value of given type</param>
        /// <returns>true, if cast is successful otherwise false.</returns>
        private static bool TryGuid(string value, out object result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = Guid.Empty;
                return true;
            }
            else
            {
                result = new Guid(value);
                return true;
            }
        }

        /// <summary>
        /// Try to parse the string to DateTime.
        /// </summary>
        /// <typeparam name="T">type for cast</typeparam>
        /// <param name="value">string representation of type value</param>
        /// <param name="result">casted value of given type</param>
        /// <returns>true, if cast is successful otherwise false.</returns>

        private static bool TryDateTime(string value, out object result)
        {
            DateTime obj;

            var flag = DateTime.TryParse(value, out obj);
            result = obj;

            return flag;
        }
    }
}
