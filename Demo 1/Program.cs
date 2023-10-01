using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;


#region System.Net.Http.Json extensions for IAsyncEnumerable
const string url = "https://ps-async.fekberg.com/api/stocks/MSFT";
using var client = new HttpClient();
IAsyncEnumerable<Stock> stocks = 
    client.GetFromJsonAsAsyncEnumerable<Stock>(url, new JsonSerializerOptions(JsonSerializerDefaults.Web) { TypeInfoResolver =  new DefaultJsonTypeInfoResolver()})!;

await foreach (Stock stock in stocks)
{
    Console.WriteLine($"Stock: '{stock.Ticker}'");
}
#endregion

#region Polymorphic serialization
var users = new IUser[] {
    new User("Filip"),
    new InactiveUser("Sofie", DateTimeOffset.UtcNow),
    new DisabledUser("Mila", DateTimeOffset.UtcNow),
    new DisabledUser("Elise", DateTimeOffset.UtcNow)
};

var json = JsonSerializer.Serialize(users);

var jsonAsString = """
[{"$discriminator":"user","Username":"Filip","RegistrationInformation":{"RegisteredAt":"2020-09-29T12:08:43.8403576+00:00"}},{"$discriminator":"inactive","InactiveSince":"2020-09-29T12:08:43.8403585+00:00","Username":"Sofie","RegistrationInformation":{"RegisteredAt":"2020-09-29T12:08:43.8403953+00:00"}},{"$discriminator":"disabled","DisabledSince":"2023-09-29T12:08:43.8403957+00:00","Username":"Mila","RegistrationInformation":{"RegisteredAt":"2023-09-29T12:08:43.8404261+00:00"}},{"$discriminator":"disabled","DisabledSince":"2023-09-29T12:08:43.8404264+00:00","Username":"Elise","RegistrationInformation":{"RegisteredAt":"2023-09-29T12:08:43.8404265+00:00"}}]
""";

var usersFromJson = JsonSerializer.Deserialize<IUser[]>(jsonAsString);

Console.ReadLine();

#endregion

[JsonDerivedType(typeof(User), typeDiscriminator: "user")]
[JsonDerivedType(typeof(InactiveUser), typeDiscriminator: "inactive")]
[JsonDerivedType(typeof(DisabledUser), typeDiscriminator: "disabled")]
[JsonPolymorphic(
    TypeDiscriminatorPropertyName = "$discriminator",
    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType)]

interface IUser
{
    public string Username { get; init; }
}

[JsonObjectCreationHandling(JsonObjectCreationHandling.Populate)]
record RegistrationInformation
{
    [JsonObjectCreationHandling(JsonObjectCreationHandling.Populate)]
    public DateTimeOffset RegisteredAt { get; }
        = DateTimeOffset.UtcNow;
}

record User(string Username) : IUser
{
    [JsonObjectCreationHandling(JsonObjectCreationHandling.Populate)]
    public RegistrationInformation RegistrationInformation { get; } = new();
}

record DisabledUser(string Username, DateTimeOffset DisabledSince)
    : User(Username);

record InactiveUser(string Username, DateTimeOffset InactiveSince) 
    : User(Username);

record Stock(string Ticker, 
    string Identifier, 
    DateTimeOffset TradeDate);