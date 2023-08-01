// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
