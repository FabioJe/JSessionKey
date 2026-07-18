using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace JSessionKey
{
    internal class SessionKeyService<T> : ISessionKeyService<T>
    {
        private readonly byte[] secret;
        private readonly SignatureAlgorithm signatureAlgorithm;

        public SessionKeyService(string secret, SignatureAlgorithm signatureAlgorithm)
        {
            this.secret = Encoding.ASCII.GetBytes(secret);
            this.signatureAlgorithm = signatureAlgorithm;
        }

        public ISessionObject<T> CreateSession() => new SessionObject<T>(this);
        public ISessionKeyInfo<T> GetSessionKeyInfo(string key) => new SessionKeyInfo<T>(this, key);

        internal string GetSignature(string text)
        {
            var bytesToHash = Encoding.ASCII.GetBytes(text);
            if (signatureAlgorithm == SignatureAlgorithm.HMACSHA256)
            {               
                using var hmac = new HMACSHA256(secret);
                return Convert.ToHexString(hmac.ComputeHash(bytesToHash));
            }
            if (signatureAlgorithm == SignatureAlgorithm.HMACSHA384)
            {
                using var hmac = new HMACSHA384(secret);
                return Convert.ToHexString(hmac.ComputeHash(bytesToHash));
            }
            if (signatureAlgorithm == SignatureAlgorithm.HMACSHA512)
            {
                using var hmac = new HMACSHA512(secret);
                return Convert.ToHexString(hmac.ComputeHash(bytesToHash));
            }
            if (signatureAlgorithm == SignatureAlgorithm.SHA3_HMACSHA256)
            {
                using var hmac = new HMACSHA3_256(secret);
                return Convert.ToHexString(hmac.ComputeHash(bytesToHash));
            }
            if (signatureAlgorithm == SignatureAlgorithm.SHA3_HMACSHA384)
            {
                using var hmac = new HMACSHA3_384(secret);
                return Convert.ToHexString(hmac.ComputeHash(bytesToHash));
            }
            if (signatureAlgorithm == SignatureAlgorithm.SHA3_HMACSHA512)
            {
                using var hmac = new HMACSHA3_512(secret);
                return Convert.ToHexString(hmac.ComputeHash(bytesToHash));
            }
            throw new ArgumentException("Not supported SignatureAlgorithm.");
        }
    }
}
