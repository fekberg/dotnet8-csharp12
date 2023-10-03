global using static System.Console;

using CSharp10;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

#region Setup
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddRateLimiter(limiter => {
    limiter.AddFixedWindowLimiter("goAway", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 1;
        options.QueueLimit = 1;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

var app = builder.Build();

app.UseRateLimiter();

app.UseExceptionHandler(options => { });
#endregion

app.MapGet("/", (HttpContext context, 
    [FromHeader]string accept, string input = "anonymous") => {
        if(input == "filip")
        {
            throw new NotImplementedException();
        }

        return DateTimeOffset.UtcNow.Ticks;
})
    .RequireRateLimiting("goAway");

app.Run();
