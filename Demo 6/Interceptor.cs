using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Interceptors;

public static class Interceptor
{
    [DebuggerHidden]
    [InterceptsLocation
        (@"Y:\Code\dotnet8-csharp12\Demo 6\Logger.cs", 7, 9)]
    public static void DebugLog(this Logger logger, string message)
    {
        Console.WriteLine(message);
    }
}