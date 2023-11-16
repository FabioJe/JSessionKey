using System;
using System.Collections.Concurrent;

namespace JSessionKey
{
    public interface ISessionKeyInfo<T>
    {
        string HeaderValue { get; }
        string Key { get; }
        ushort KeyVersion { get; }
        string PayloadValue { get; }
        ushort SessionType { get; }
        string Signature { get; }
        DateTime ValidUntil { get; }

        string GetBaseKey();
        ConcurrentDictionary<string, string> GetHeaderObject();
        T? GetPayloadObject();
        bool IsValid();
        bool IsValid(ushort sessiontype);
    }
}