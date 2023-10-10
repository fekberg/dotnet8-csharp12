using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;

namespace CSharp10;

public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> logger;

    public ExceptionHandler(ILogger<ExceptionHandler> logger)
    {
        this.logger = logger;
    }
   
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, CancellationToken cancellationToken)
    {





        WriteLine("Where is this from?");

        logger.LogError(exception, "Caught an unexcepted error");

        await httpContext.Response.WriteAsJsonAsync(new { 
            title = "error",
            message = exception.Message,
            url = httpContext.Request.GetDisplayUrl()
        });

        return true;
    }
}
