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

Console.ReadLine();
#endregion

#region Polymorphic serialization
var users = new User[] {
    new User() { Username = "Filip" },
    new InactiveUser(DateTimeOffset.UtcNow) { Username = "Sofie" },
    new DisabledUser(DateTimeOffset.UtcNow) { Username = "Mila" },
    new DisabledUser(DateTimeOffset.UtcNow) { Username = "Elise" }
};

var json = JsonSerializer.Serialize(users);

var jsonAsString = """
[
    {
        "$discriminator": "user",
        "Username": "Filip",
        "PhoneNumbers": [
            "12345"
        ]
    },
    {
        "$discriminator": "inactive",
        "InactiveSince": "2023-10-02T11:53:24.2010948+00:00",
        "Username": "Sofie",
        "PhoneNumbers": [
            "6789"
        ]
    },
    {
        "$discriminator": "disabled",
        "DisabledSince": "2023-10-02T11:53:24.2011285+00:00",
        "Username": "Mila",
        "PhoneNumbers": []
    },
    {
        "$discriminator": "disabled",
        "DisabledSince": "2023-10-02T11:53:24.2011561+00:00",
        "Username": "Elise",
        "PhoneNumbers": []
    }
]    
""";

var usersFromJson = JsonSerializer.Deserialize<User[]>(jsonAsString);

Console.ReadLine();

#endregion














[JsonDerivedType(typeof(User), typeDiscriminator: "user")]
[JsonDerivedType(typeof(InactiveUser), typeDiscriminator: "inactive")]
[JsonDerivedType(typeof(DisabledUser), typeDiscriminator: "disabled")]
[JsonPolymorphic(TypeDiscriminatorPropertyName = "$discriminator")]
record User()
{
    public string Username { get; init; } = "Anonymous";

    [JsonObjectCreationHandling(JsonObjectCreationHandling.Populate)]
    public List<string> PhoneNumbers { get; } = new();
}

record DisabledUser(DateTimeOffset DisabledSince)
    : User();

record InactiveUser(DateTimeOffset InactiveSince) 
    : User();

record Stock(string Ticker, 
    string Identifier, 
    DateTimeOffset TradeDate);