using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converters.StringsToValueType.CustomConverters.Base
{
    public interface IConverter<T>
    {
        T Convert(string value);
    }
}
