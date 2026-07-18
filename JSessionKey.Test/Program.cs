using JSessionKey;
using System.Diagnostics;
using System.Text;

namespace JSessionKey.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var payload = new SessionInfo { TestValue = "Hallo World!" };
            var sessionService = new SessionKeyBuilder().SetAlgorithm(SignatureAlgorithm.SHA3_HMACSHA256).SetSecret("opdsaopdwijdwqoihhewqo2dfay2!1xwhqigrz1e9u12eohghd2091901erhkjADKJHKJa").Build<SessionInfo>();

            var session = sessionService.CreateSession();
            session.SetHeaderValue("Value-1", "Test value 1");
            session.SetHeaderValue("Value-2", "Test value 2");
            session.Payload = payload; 
            session.ValidUntil = DateTimeOffset.UtcNow.AddMinutes(10);
            session.SessionType = 1;
            var key = session.BuildSessionKey();
            var info = sessionService.GetSessionKeyInfo(key);
            Console.WriteLine(info.IsValid());

            var info2 = sessionService.GetSessionKeyInfo(key + "A");
            Console.WriteLine(info2.IsValid());




        }
    }
}