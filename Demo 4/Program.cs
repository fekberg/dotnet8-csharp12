global using static System.Console;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

#region Setup
var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

#region Rate Limiter & Exception Handler
app.UseRateLimiter();

app.UseExceptionHandler(options => { });
#endregion

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
