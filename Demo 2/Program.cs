﻿using Microsoft.Extensions.Time.Testing;


#region FakeTimeProvider

var provider = new FakeTimeProvider()
{
    AutoAdvanceAmount = TimeSpan.FromHours(24)
};

#endregion

#region Demo

OrderService service = new();

var order = new Order(DateTimeOffset.UtcNow.AddDays(-10));

Console.WriteLine(service.HasPaymentExpired(order));

Console.WriteLine(service.HasPaymentExpired(order));

#endregion

record Order(DateTimeOffset PaymentReservedOn);

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
