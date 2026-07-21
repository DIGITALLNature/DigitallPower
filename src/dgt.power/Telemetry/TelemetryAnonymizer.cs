// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.RegularExpressions;

namespace dgt.power.Telemetry;

/// <summary>
/// Strips personally identifiable and environment-specific information from exception messages and
/// stack traces before they are sent to telemetry. See <c>.memory/decision-error-telemetry-anonymization.md</c>
/// for the rationale and the scope/limitations of this best-effort anonymization.
/// </summary>
internal static partial class TelemetryAnonymizer
{
    private const string RedactedGuid = "00000000-0000-0000-0000-000000000000";
    private const string RedactedOrgUrl = "[dataverse-org-url-redacted]";
    private const string RedactedTenantUrl = "[entra-tenant-url-redacted]";

    private static readonly Regex s_guidRegex = GetGuidRegex();
    private static readonly Regex s_dataverseOrgUrlRegex = GetDataverseOrgUrlRegex();
    private static readonly Regex s_entraTenantUrlRegex = GetEntraTenantUrlRegex();

    // Matches well-known "home directory" roots that carry a local username, on any OS:
    // /Users/<name>/... (macOS), /home/<name>/... (Linux), /root/... (Linux root), C:\Users\<name>\... (Windows).
    // We deliberately don't try to anonymize *every* absolute path (e.g. /tmp/, /var/, arbitrary URLs) to avoid
    // mangling unrelated text - only paths that are likely to reveal a real person's username.
    private static readonly Regex s_unixHomePathRegex = GetUnixHomePathRegex();
    private static readonly Regex s_windowsHomePathRegex = GetWindowsHomePathRegex();

    /// <summary>
    /// Anonymizes a single-line message: GUIDs, Dataverse organization URLs, Entra tenant URLs and
    /// local home-directory paths are replaced with fixed placeholders.
    /// </summary>
    public static string Anonymize(string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }

        var result = s_guidRegex.Replace(input, RedactedGuid);
        result = s_dataverseOrgUrlRegex.Replace(result, RedactedOrgUrl);
        result = s_entraTenantUrlRegex.Replace(result, RedactedTenantUrl);
        result = s_unixHomePathRegex.Replace(result, m => StripToFileName(m.Value, '/'));
        result = s_windowsHomePathRegex.Replace(result, m => StripToFileName(m.Value, '\\'));

        return result;
    }

    /// <summary>
    /// Anonymizes a (potentially multi-line) stack trace. In addition to the generic <see cref="Anonymize"/>
    /// rules, every "at ... in &lt;path&gt;:line N" frame has its path shortened to just the file name,
    /// regardless of which root the path lives under (source paths don't necessarily carry a username,
    /// but they do reveal local folder structure / build machine layout).
    /// </summary>
    public static string AnonymizeStackTrace(string? stackTrace)
    {
        if (string.IsNullOrEmpty(stackTrace))
        {
            return string.Empty;
        }

        // Split on any line ending (\r\n, \r or \n), not just Environment.NewLine: the stack trace may
        // originate from a different platform than the one currently anonymizing it (e.g. a serialized
        // exception, or a dependency that always emits \n regardless of OS).
        var lines = LineSplitRegex().Split(stackTrace);
        for (var i = 0; i < lines.Length; i++)
        {
            // Example line: "   at dgt.power.Program.<>c.<<Main>$>b__0_1(Exception exception, ITypeResolver _) in /Users/Name/Source/Project/src/dgt.power/Program.cs:line 141"
            // We want to remove the path before the filename.
            var inIndex = lines[i].LastIndexOf(" in ", StringComparison.Ordinal);
            if (inIndex != -1)
            {
                var pathPart = lines[i][(inIndex + 4)..];
                var colonIndex = pathPart.LastIndexOf(':');
                if (colonIndex != -1)
                {
                    var filePart = pathPart[..colonIndex];
                    var linePart = pathPart[colonIndex..];
                    var lastSeparator = filePart.LastIndexOfAny(['/', '\\']);
                    if (lastSeparator != -1)
                    {
                        lines[i] = lines[i][..(inIndex + 4)] + filePart[(lastSeparator + 1)..] + linePart;
                    }
                }
            }
        }

        var result = string.Join(Environment.NewLine, lines);
        return Anonymize(result);
    }

    private static string StripToFileName(string path, char separator)
    {
        var lastSeparator = path.LastIndexOf(separator);
        return lastSeparator == -1 ? path : path[(lastSeparator + 1)..];
    }

    [GeneratedRegex("[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}")]
    private static partial Regex GetGuidRegex();

    // Matches https://<org>[.api].crm[N].dynamics.com(/path?query) - covers all regional CRM instance
    // suffixes (crm, crm2, crm3, ... crm11) as well as the plain "dynamics.com" apex used by some admin APIs.
    // The optional path/query is matched non-greedily and must not swallow trailing sentence punctuation
    // (e.g. the "." that ends "... systemusers.").
    [GeneratedRegex(@"https?://[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*\.(?:crm\d{0,2}\.)?dynamics\.com(?:/[^\s'""<>]*[^\s'""<>.,;:!?)])?", RegexOptions.IgnoreCase)]
    private static partial Regex GetDataverseOrgUrlRegex();

    // Matches https://<tenant>.onmicrosoft.com(/path?query) - Entra ID (Azure AD) tenant URLs, which also
    // identify the customer.
    [GeneratedRegex(@"https?://[A-Za-z0-9-]+\.onmicrosoft\.com(?:/[^\s'""<>]*[^\s'""<>.,;:!?)])?", RegexOptions.IgnoreCase)]
    private static partial Regex GetEntraTenantUrlRegex();

    [GeneratedRegex(@"(?<![:\w])/(?:Users|home|root)/[^\s'""<>:]+", RegexOptions.IgnoreCase)]
    private static partial Regex GetUnixHomePathRegex();

    [GeneratedRegex(@"[A-Za-z]:\\Users\\[^\s'""<>:]+", RegexOptions.IgnoreCase)]
    private static partial Regex GetWindowsHomePathRegex();

    [GeneratedRegex(@"\r\n|\r|\n")]
    private static partial Regex LineSplitRegex();
}
