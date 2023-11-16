using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JSessionKey
{
    internal class BaseKeyBuilder<T>
    {
        private ushort version = 1;
        private ushort sessiontype = 0;
        private string validUntil = "";
        private string headerValue = "";
        private string payloadValue = "";

        public BaseKeyBuilder<T> SetPayload(T? payload)
        {
            if (payload == null) payloadValue = "";
            else payloadValue = Base64Coder.ToBase64String(JsonSerializer.Serialize(payload));
            return this;
        }
        public BaseKeyBuilder<T> SetPayloadAsBase64Text(string payload)
        {
            payloadValue = payload;
            return this;
        }

        public BaseKeyBuilder<T> SetVersion(ushort version)
        {
            this.version = version;
            return this;
        }

        public BaseKeyBuilder<T> SetSessionType(ushort sessiontype)
        {
            this.sessiontype = sessiontype;
            return this;
        }
        public BaseKeyBuilder<T> SetValidUntil(DateTime validUntil)
        {
            this.validUntil = DateTimeConverter.GetKeyTimeString(validUntil);
            return this;
        }
        public BaseKeyBuilder<T> SetValidUntilAsString(string validUntil)
        {
            this.validUntil = validUntil;
            return this;
        }

        public BaseKeyBuilder<T> SetHeaderValue(ConcurrentDictionary<string, string> header)
        {
            headerValue = Base64Coder.ToBase64String(JsonSerializer.Serialize(header));
            return this;
        }

        public BaseKeyBuilder<T> SetHeaderValueAsBase64(string header)
        {
            headerValue = header;
            return this;
        }

        public string Build()
        {
            if (string.IsNullOrWhiteSpace(validUntil))
                SetValidUntil(DateTime.Now.AddHours(1));
            if (string.IsNullOrWhiteSpace(headerValue))
                SetHeaderValue(new ConcurrentDictionary<string, string>());
            return $"{version}.{sessiontype}.{validUntil}.{headerValue}.{payloadValue}";
        }





    }
}
