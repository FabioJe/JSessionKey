using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace JSessionKey
{
    internal class SessionObject<T> : ISessionObject<T>
    {
        private const string SESSION_ID = "SID";
        private const ushort VERSION = 1;
        private readonly SessionKeyService<T> sessionKeyService;

        private readonly ConcurrentDictionary<string, string> _headerValues = new();

        public SessionObject(SessionKeyService<T> sessionKeyService)
        {
            this.sessionKeyService = sessionKeyService;
            _headerValues["SID"] = Guid.NewGuid().ToString("D");
        }

        public void SetHeaderValue(string key, string value) => _headerValues[key] = value;

        public string GetHeader(string header) => _headerValues[header];

        public ICollection<string> GetHeaderKeys() => _headerValues.Keys;

        public ushort SessionType { get; set; }

        public T? Payload { get; set; }

        public DateTime? ValidUntil { get; set; }


        public string BuildSessionKey()
        {
            var baseText = new BaseKeyBuilder<T>().SetVersion(VERSION).SetSessionType(SessionType).SetValidUntil(ValidUntil.GetValueOrDefault(DateTime.Now.AddHours(1))).SetHeaderValue(_headerValues).SetPayload(Payload).Build();          
            var signature = sessionKeyService.GetSignature(baseText);
            baseText = baseText + "." + signature;
            return baseText;
        }
    }
}
