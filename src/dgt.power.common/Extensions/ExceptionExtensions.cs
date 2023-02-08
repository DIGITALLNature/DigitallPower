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

    public static bool IsDerivedFrom<TException>(this Exception exception)
        where TException : Exception
    {
        return exception.GetType().IsAssignableTo(typeof(TException))
               || (exception.InnerException?.IsDerivedFrom<TException>() ?? false);
    }

    public static TException? GetInnerException<TException>(this Exception exception) where TException : Exception
    {
        return exception.GetType().IsAssignableTo(typeof(TException))
            ? (TException)exception
            : exception.InnerException?.GetInnerException<TException>() ?? null;
    }
}
