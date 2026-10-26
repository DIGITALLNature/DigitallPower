// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Spectre.Console;

namespace dgt.power.common.Storage;

/// <summary>
/// Warns once when an invocation actually selects unencrypted persistent storage.
/// </summary>
public sealed class StorageSecurityNotice(IAnsiConsole console)
{
    private int _warningShown;

    /// <summary>
    /// Writes the unencrypted-storage warning the first time an unencrypted backend is selected.
    /// </summary>
    public void WarnIfUnencryptedStorageIsUsed()
    {
        if (Interlocked.Exchange(ref _warningShown, 1) == 0)
        {
            console.MarkupLine(
                "[yellow]WARNING: Unencrypted storage is in use because DGTP_ALLOW_UNENCRYPTED_STORAGE is enabled.[/]");
        }
    }
}
