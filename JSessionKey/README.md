# JSessionKey

JSessionKey is a system similar to JWT tokens that provides authentication tokens in distributed systems. 

#### Requires .NET 8 or higher

### How to use?

```csharp
using JSessionKey;

var payload = new SessionInfo { TestValue = "Hallo World!" };
var sessionService = new SessionKeyBuilder().SetAlgorithm(SignatureAlgorithm.SHA3_HMACSHA256).SetSecret("opdsaopdwijdwqoihhewqo2dfay2!1xwhqigrz1e9u12eohghd2091901erhkjADKJHKJa").Build<SessionInfo>();

var session = sessionService.CreateSession();
session.SetHeaderValue("Value-1", "Test value 1");
session.SetHeaderValue("Value-2", "Test value 2");
session.Payload = payload; 
session.ValidUntil = DateTimeOffset.UtcNow.AddMinutes(10);
session.SessionType = 1;

//Create key
var key = session.BuildSessionKey();

//Valid key
var info = sessionService.GetSessionKeyInfo(key);
Console.WriteLine(info.IsValid());
```