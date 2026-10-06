// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;

namespace dgt.power.common.Extensions;

public static class ExceptionExtensions
{
    public static Exception RootException(this Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);
        var rootException = exception;
        while (rootException.InnerException != null)
        {
            rootException = rootException.InnerException;
        }

        return rootException;
    }

    public static string RootMessage(this Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);
        var rootException = exception;
        while (rootException.InnerException != null)
        {
            rootException = rootException.InnerException;
        }

        return rootException.Message;
    }

    public static string DiagnosticMessage(this Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);
        var messages = new List<string>();
        for (Exception? current = exception; current is not null; current = current.InnerException)
        {
            messages.Add(current.Message);
            if (current is not FaultException<OrganizationServiceFault> serviceException)
            {
                continue;
            }

            for (var fault = serviceException.Detail; fault is not null; fault = fault.InnerFault)
            {
                var code = unchecked((uint)fault.ErrorCode).ToString("X8", CultureInfo.InvariantCulture);
                messages.Add($"Dataverse fault 0x{code}: {fault.Message}");
            }
        }

        return string.Join(Environment.NewLine, messages.Distinct(StringComparer.Ordinal));
    }

    public static bool IsDerivedFrom<TException>(this Exception exception)
        where TException : Exception
    {
        ArgumentNullException.ThrowIfNull(exception);
        return exception.GetType().IsAssignableTo(typeof(TException))
               || (exception.InnerException?.IsDerivedFrom<TException>() ?? false);
    }

    public static TException? GetInnerException<TException>(this Exception exception) where TException : Exception
    {
        ArgumentNullException.ThrowIfNull(exception);
        return exception.GetType().IsAssignableTo(typeof(TException))
            ? (TException)exception
            : exception.InnerException?.GetInnerException<TException>() ?? null;
    }
}
