using Microsoft.Extensions.Time.Testing;

#region TimeProvider
var provider = new FakeTimeProvider()
{
    AutoAdvanceAmount = TimeSpan.FromHours(24)
};
#endregion

OrderService service = new();

var order = new Order(DateTimeOffset.UtcNow.AddDays(-10));

Console.WriteLine(service.HasPaymentExpired(order));

Console.WriteLine(service.HasPaymentExpired(order));

class OrderService
{
    public bool HasPaymentExpired(Order order)
    {
        return (DateTimeOffset.UtcNow - order.PaymentReservedOn).TotalDays > 30;
    }

}

class CustomTimeProvider : TimeProvider
{
    public override DateTimeOffset GetUtcNow()
    {
        return DateTimeOffset.Now; // Don't do this...
    }
}

record Order(DateTimeOffset PaymentReservedOn);