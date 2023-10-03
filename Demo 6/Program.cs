#region Collection expressions

using Interceptors;

// Collection literal
byte[] payload = [0x1, 0xf1, 0xaa, 0xf2];
byte[] checksum = [0xff, 0xab];

byte[] result = [..payload, ..checksum];

#endregion

#region Interceptors
var logger = new Logger();

logger.Log("Hello world!");
#endregion

#region Primary constructors

class User(string username)
{
    public string Username { get; } = username;
}

class OrderController(IUserRepository repository)
{
    public (Guid, decimal) GetTotalFor(Guid orderId) =>
        (orderId, repository.Sum(orderId));
}

interface IUserRepository
{
    public decimal Sum(Guid orderId) => 0m;
}

#endregion