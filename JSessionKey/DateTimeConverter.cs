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

        public static string GetKeyTimeString(DateTime dateTime) => dateTime.ToUniversalTime().ToString(TIME_FORMAT);

        public static DateTime FromKeyTimeString(string keyTimeString)
        {
            var dt = DateTime.ParseExact(keyTimeString, TIME_FORMAT, CultureInfo.InvariantCulture);
            return DateTime.SpecifyKind(dt, DateTimeKind.Utc);
        }
    }
}
