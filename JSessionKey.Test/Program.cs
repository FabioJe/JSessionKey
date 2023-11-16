using JSessionKey;
using System.Diagnostics;
using System.Text;

namespace JSessionKey.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string key1 = "1.1.202311161303.eyJNYWlsIjoiaW5mb0B0ZXN0LmRlIiwiTWFpbDIiOiJpbmZvQHRlc3QuZGUifQ.eyJUZXN0VmFsdWUiOiJcdTAwRkNvcGtzYW9wZHNvcCJ9.2A3E08C085FCFC076FC5A9311A57A2FE9EE3F974B366EF624793098D288541D09513EEC8C715B01541E5A149C42678F2";
            string key2 = "1.1.202311161311.eyJNYWlsMiI6ImluZm9AdGVzdC5kZSIsIlNJRCI6IjIwZDMxMTYyLTA2YmQtNGVjMy1hMTg4LTVlMmFiZDJiYzE2YyIsIk1haWwiOiJpbmZvQHRlc3QuZGUifQ.eyJUZXN0VmFsdWUiOiJcdTAwRkNvcGtzYW9wZHNvcCJ9.4BECBAC0DBA94869789EC166E59440149BF10199EB682D6B2C616561E9AAE6D2A6E072F2C188EB77086C141139C64267";
            var payload = new SessionInfo { TestValue = "üopksaopdsop" };
            var sessionService = new SessionKeyBuilder().SetAlgorithm(SignatureAlgorithm.HMACSHA384).SetSecret("opdsaopdwijdwqoihhewqo2dfay2!1xwhqigrz1e9u12eohghd2091901erhkjADKJHKJa").Build<SessionInfo>();

            var session = sessionService.CreateSession();
            session.SetHeaderValue("Mail", "info@test.de");
            session.SetHeaderValue("Mail2", "info@test.de");
            session.Payload = payload;
            session.ValidUntil = DateTime.Now.AddDays(7);
            session.SessionType = 1;
            var key = session.BuildSessionKey();

            Console.WriteLine(key);
            Console.WriteLine("Byte: " + Encoding.ASCII.GetBytes(key).Length);
            var info = sessionService.GetSessionKeyInfo(key1);
            Console.WriteLine(info.IsValid());

            var info2 = sessionService.GetSessionKeyInfo(key2);
            Console.WriteLine(info2.IsValid());




        }
    }
}