using Converters.StringsToValueType.CustomConverters.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converters.StringsToValueType.CustomConverters
{
    public sealed class FractionalNumericValueConverter<T> : BaseConverter<T>
    {
        public override object InnerConvert(string value)
        {
            var newType = typeof(T);

            if (typeof(T) == typeof(Single))
            {
                return Single.Parse(value);
            }

            if(typeof(T) == typeof(double))
            {
                return Double.Parse(value);
            }

            return default(T);
        }
    }
}
