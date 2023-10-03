namespace Interceptors;

public class Logger
{
    public void Log(string message)
    {
        LogInternal(message);
    }

    private void LogInternal(string message)
        => throw new NotImplementedException();
}
