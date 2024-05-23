global using static System.Console;

using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAntiforgery();

#region Keyed Services

builder.Services.AddTransient<ICache, InMemoryCache>();

builder.Services.AddKeyedTransient<ICache, DistributedCache>("distributed");
builder.Services.AddKeyedTransient<ICache, InMemoryCache>("memory");

#endregion

#region Exception Handler

builder.Services.AddExceptionHandler<ExceptionHandler>();

#endregion

#region Rate Limiter

builder.Services.AddRateLimiter(limiter => {
    limiter.AddFixedWindowLimiter("goAway", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 1;
        options.QueueLimit = 1;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

#endregion

#region Hosted Services

builder.Services.AddHostedService<StockMonitorService>();

#endregion








var app = builder.Build();

app.UseRateLimiter();

app.UseExceptionHandler(options => { });

app.UseAntiforgery();




app.MapGet("/", (
    HttpContext context,

    [FromKeyedServices("distributed")]ICache cache,

    [FromHeader]string accept, 
    string input = "anonymous") => 
{
        if(input != "anonymous") throw new NotImplementedException();

        return DateTimeOffset.UtcNow.Ticks;
}).RequireRateLimiting("goAway");










app.MapGet("/short-circuit", () => "I'm executed immediately!")
    .ShortCircuit();












app.MapGet("/upload", (HttpContext context, IAntiforgery antiforgery) =>
{
    var token = antiforgery.GetAndStoreTokens(context);

    return Results.Content($"""
    <html>
    <body>
        <form method="post" action="upload" enctype="multipart/form-data">
            <input name="{token.FormFieldName}" type="hidden" value="{token.RequestToken}"/>
            <input type="file" name="data" />
            <input type="submit" text="submit" />
        </form>
    </body>
    """, "text/html");
});


app.MapPost("/upload", async (
    IFormFile data, 
    HttpContext context, 
    IAntiforgery antiforgery) => 
{
    await antiforgery.ValidateRequestAsync(context);
});

app.Run();