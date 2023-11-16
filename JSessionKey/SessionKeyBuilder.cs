using System;

namespace JSessionKey
{
    public class SessionKeyBuilder
    {

        public SignatureAlgorithm Algorithm { get; set; } = SignatureAlgorithm.HMACSHA256;
        public string Secret { get; set; } = string.Empty;

        public SessionKeyBuilder SetAlgorithm(SignatureAlgorithm algorithm) { this.Algorithm = algorithm; return this; }
        public SessionKeyBuilder SetSecret(string secret) { this.Secret = secret; return this; }

        public ISessionKeyService<T> Build<T>()
        {
            return new SessionKeyService<T>(Secret, Algorithm);
        }


    }
}
