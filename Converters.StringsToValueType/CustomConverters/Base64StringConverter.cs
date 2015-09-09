using Converters.StringsToValueType.CustomConverters.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converters.StringsToValueType.CustomConverters
{
    public class Base64StringConverter : BaseConverter<byte[]>
    {
        public override object InnerConvert(string value)
        {
            return System.Convert.FromBase64String(value);
        }
    }
}
