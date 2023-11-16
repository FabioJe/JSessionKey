using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSessionKey
{
    internal static class Base64Coder
    {
        private static readonly char[] padding = { '=' };

        internal static string ToBase64String(string value)
        {
            return Convert.ToBase64String(GetBytes(value)).TrimEnd(padding).Replace('+', '-').Replace('/', '_');
        }

        private static byte[] GetBytes(string value) => Encoding.UTF8.GetBytes(value);

        internal static string FromBase64String(string base64String)
        {
            string incoming = base64String.Replace('_', '/').Replace('-', '+');
            switch (base64String.Length % 4)
            {
                case 2: incoming += "=="; break;
                case 3: incoming += "="; break;
            }
            byte[] bytes = Convert.FromBase64String(incoming);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
