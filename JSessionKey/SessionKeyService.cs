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
            if (signatureAlgorithm == SignatureAlgorithm.HMACSHA256)
            {               
                using var hmac = new HMACSHA256(secret);
                return Convert.ToHexString(hmac.ComputeHash(Encoding.ASCII.GetBytes(text)));
            }
            if (signatureAlgorithm == SignatureAlgorithm.HMACSHA384)
            {
                using var hmac = new HMACSHA384(secret);
                return Convert.ToHexString(hmac.ComputeHash(Encoding.ASCII.GetBytes(text)));
            }
            if (signatureAlgorithm == SignatureAlgorithm.HMACSHA512)
            {
                using var hmac = new HMACSHA512(secret);
                return Convert.ToHexString(hmac.ComputeHash(Encoding.ASCII.GetBytes(text)));
            }
            throw new ArgumentException("Not supported SignatureAlgorithm.");
        }
    }
}
