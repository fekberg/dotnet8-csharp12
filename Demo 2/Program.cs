using Microsoft.Extensions.Time.Testing;

var provider = new FakeTimeProvider()
{
    AutoAdvanceAmount = TimeSpan.FromHours(24)
};

OrderService service = new(provider);

Console.WriteLine(service.CountOrders());

Console.WriteLine(service.CountOrders());

class OrderService(TimeProvider timeProvider)
{
    public int CountOrders()
    {
        var utcNow = timeProvider.GetUtcNow();

        Console.WriteLine(utcNow);

        return utcNow.DayOfYear;
    }
}

class CustomTimeProvider : TimeProvider
{
    public override DateTimeOffset GetUtcNow()
    {
        return DateTimeOffset.Now; // Don't do this...
    }
}