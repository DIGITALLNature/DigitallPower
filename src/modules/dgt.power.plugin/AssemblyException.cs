// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin;

/// <summary>Thrown when a local assembly's declared registration attributes cannot be resolved (e.g. an unknown data provider event value).</summary>
[Serializable]
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
