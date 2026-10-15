// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common;

/// <summary>
/// The only place that prints the start and end line of a command run.
/// </summary>
public interface ICommandStatusWriter
{
    void WriteStart(string commandName);

    void WriteEnd(string commandName, bool success, TimeSpan duration);
}
