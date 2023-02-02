namespace dgt.power.common.Extensions;

public static class ExceptionExtensions
{
    public static Exception RootException(this Exception exception)
    {
        var rootException = exception;
        while (rootException.InnerException != null)
        {
            rootException = rootException.InnerException;
        }

        return rootException;
    }

    public static string RootMessage(this Exception exception)
    {
        var rootException = exception;
        while (rootException.InnerException != null)
        {
            rootException = rootException.InnerException;
        }

        return rootException.Message;
    }
}
