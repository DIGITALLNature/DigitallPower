// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.push.Model;

public class AssemblyException : Exception
{
    public AssemblyException()
    {
    }

    public AssemblyException(string message)
        : base(message)
    {
    }

    public AssemblyException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
