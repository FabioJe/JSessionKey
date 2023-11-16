using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static JSessionKey.Base64Coder;
using static JSessionKey.DateTimeConverter;

namespace JSessionKey
{
    internal class SessionKeyInfo<T> : ISessionKeyInfo<T>
    {
        private readonly SessionKeyService<T> sessionService;

        private readonly BaseKeyBuilder<T> baseKeyBuilder;

        public SessionKeyInfo(SessionKeyService<T> sessionService, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"\"{nameof(key)}\"  must not be NULL or a space character.", nameof(key));
            }
            this.sessionService = sessionService;
            Key = key;
            var substrVals = Key.Split('.', StringSplitOptions.None);
            if (substrVals.Length != 6)
            {
                throw new ArgumentException("The key does not consist of 6 data blocks.");
            }
            baseKeyBuilder = new BaseKeyBuilder<T>();
            KeyVersion = ushort.Parse(substrVals[0]);           
            SessionType = ushort.Parse(substrVals[1]);
            baseKeyBuilder.SetVersion(KeyVersion).SetSessionType(SessionType);
            ValidUntil = FromKeyTimeString(substrVals[2]);
            baseKeyBuilder.SetValidUntilAsString(substrVals[2]);
            HeaderValue = substrVals[3];
            PayloadValue = substrVals[4];
            baseKeyBuilder.SetHeaderValueAsBase64(HeaderValue).SetPayloadAsBase64Text(PayloadValue);
            Signature = substrVals[5];
        }

        public ushort SessionType { get; }
        public ushort KeyVersion { get; }
        public string HeaderValue { get; }

        public ConcurrentDictionary<string, string> GetHeaderObject()
        {
            var val = JsonSerializer.Deserialize<ConcurrentDictionary<string, string>>(FromBase64String(HeaderValue));
            if (val is null) return new ConcurrentDictionary<string, string>();
            return val;
        }

        public string PayloadValue { get; }

        public T? GetPayloadObject()
        {
            if (string.IsNullOrWhiteSpace(PayloadValue)) return default;
            return JsonSerializer.Deserialize<T>(FromBase64String(PayloadValue));
        }

        public string Signature { get; }
        public DateTime ValidUntil { get; }
        public string Key { get; }

        public string GetBaseKey() => baseKeyBuilder.Build();

        public bool IsValid()
        {
            if (ValidUntil < DateTime.UtcNow) return false;
            var calcSig = sessionService.GetSignature(GetBaseKey());
            return calcSig == Signature;
        }

        public bool IsValid(ushort sessiontype)
        {
            if (sessiontype !=  SessionType) return false;
            return IsValid();
        }
    }
}
