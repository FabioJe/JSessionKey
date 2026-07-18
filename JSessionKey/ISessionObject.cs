using System;
using System.Collections.Generic;

namespace JSessionKey
{
    public interface ISessionObject<T>
    {
        T? Payload { get; set; }
        ushort SessionType { get; set; }
        DateTimeOffset? ValidUntil { get; set; }

        string BuildSessionKey();
        string GetHeader(string header);
        ICollection<string> GetHeaderKeys();
        void SetHeaderValue(string key, string value);
    }
}