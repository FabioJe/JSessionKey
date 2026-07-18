using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSessionKey
{
    internal static class DateTimeConverter
    {

        private const string TIME_FORMAT = "yyyyMMddHHmm";

        public static string GetKeyTimeString(DateTimeOffset dateTime) => dateTime.ToUniversalTime().ToString(TIME_FORMAT);

        public static DateTimeOffset FromKeyTimeString(string keyTimeString)
        {
            var dt = DateTimeOffset.ParseExact(keyTimeString, TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
            return dt;
        }
    }
}
