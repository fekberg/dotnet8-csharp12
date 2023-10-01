using System.Text.Json;
using System.Text.Json.Serialization;

CustomerInfo customer =
    JsonSerializer.Deserialize<CustomerInfo>("""{"Name":"John Doe","Company":{"Name":"Contoso"}}""")!;

Console.WriteLine(JsonSerializer.Serialize(customer));

class CompanyInfo
{
    public string Name { get; set; }
    public string? PhoneNumber { get; set; }
}

[JsonObjectCreationHandling(JsonObjectCreationHandling.Populate)]
class CustomerInfo
{
    // Both of these properties are read-only.
    public string Name { get; } = "Anonymous";
    public CompanyInfo Company { get; } = new() { Name = "N/A", PhoneNumber = "N/A" };
}