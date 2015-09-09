using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converters.StringsToValueType.CustomConverters.Base
{
    /// <summary>
    /// Inherit this class and overide the InnerConvert to create a custom string to type converter.
    /// </summary>
    /// <typeparam name="T">Type of value type</typeparam>
    public abstract class BaseConverter <T> : IConverter<T>
    {
        /// <summary>
        /// Convert a given string to provided value type
        /// </summary>
        /// <param name="value">string value</param>
        /// <returns>Returns the converted value</returns>
        public T Convert(string value)
        {
            try
            {
                return (T)this.InnerConvert(value);
            }
            catch (Exception exception)
            {
                throw new InvalidCastException(string.Format("Unable to cast the {0} to type {1}", value, typeof(T)), exception);
            }
        }

        /// <summary>
        /// Override this method in child class to provide actual implementation.
        /// </summary>
        /// <param name="value">string value</param>
        /// <returns>return a object as converted value.</returns>
        public abstract object InnerConvert(string value);
    }
}
