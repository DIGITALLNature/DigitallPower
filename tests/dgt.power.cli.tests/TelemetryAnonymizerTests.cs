// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.Telemetry;

namespace dgt.power.cli.tests;

public class TelemetryAnonymizerTests
{
    [Test]
    public async Task Anonymize_WithGuid_ReplacesWithZeros()
    {
        const string input = "Error in record 12345678-1234-1234-1234-1234567890ab occurred.";
        var result = TelemetryAnonymizer.Anonymize(input);

        await Assert.That(result).IsEqualTo("Error in record 00000000-0000-0000-0000-000000000000 occurred.");
    }

    [Test]
    public async Task AnonymizeStackTrace_WithAbsolutePaths_StripsPaths()
    {
        const string stackTrace = @"   at dgt.power.Program.Main(String[] args) in /Users/jdoe/Source/dgtp/src/dgt.power/Program.cs:line 141
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)";

        var result = TelemetryAnonymizer.AnonymizeStackTrace(stackTrace);

        await Assert.That(result).Contains("at dgt.power.Program.Main(String[] args) in Program.cs:line 141");
        await Assert.That(result).DoesNotContain("/Users/jdoe/Source/dgtp/src/dgt.power/");
    }

    [Test]
    public async Task AnonymizeStackTrace_WithWindowsPaths_StripsPaths()
    {
        const string stackTrace = @"   at dgt.power.Program.Main(String[] args) in C:\Users\jdoe\Source\dgtp\src\dgt.power\Program.cs:line 141";

        var result = TelemetryAnonymizer.AnonymizeStackTrace(stackTrace);

        await Assert.That(result).Contains("at dgt.power.Program.Main(String[] args) in Program.cs:line 141");
        await Assert.That(result).DoesNotContain("C:\\Users\\jdoe\\Source\\dgtp\\src\\dgt.power\\");
    }

    [Test]
    public async Task AnonymizeStackTrace_WithMixedLineEndings_StripsAllFrames()
    {
        // Simulates a stack trace that mixes \n-only lines with the platform's Environment.NewLine,
        // e.g. because it was captured on a different OS or crossed a serialization boundary.
        const string stackTrace = "   at Foo.Bar() in /Users/jdoe/src/Foo.cs:line 10\n" +
                                   "   at Baz.Qux() in /Users/jdoe/src/Baz.cs:line 20\r\n" +
                                   "   at Program.Main() in /Users/jdoe/src/Program.cs:line 30";

        var result = TelemetryAnonymizer.AnonymizeStackTrace(stackTrace);

        await Assert.That(result).Contains("in Foo.cs:line 10");
        await Assert.That(result).Contains("in Baz.cs:line 20");
        await Assert.That(result).Contains("in Program.cs:line 30");
        await Assert.That(result).DoesNotContain("/Users/jdoe/");
    }

    [Test]
    public async Task Anonymize_WithUnixHomePath_StripsUsernameFromMessage()
    {
        const string input = "Could not find file '/Users/jdoe/secret/config.json'.";

        var result = TelemetryAnonymizer.Anonymize(input);

        await Assert.That(result).IsEqualTo("Could not find file 'config.json'.");
        await Assert.That(result).DoesNotContain("jdoe");
    }

    [Test]
    public async Task Anonymize_WithLinuxHomePath_StripsUsernameFromMessage()
    {
        const string input = "Access denied to /home/ciuser/workspace/output.xml";

        var result = TelemetryAnonymizer.Anonymize(input);

        await Assert.That(result).IsEqualTo("Access denied to output.xml");
        await Assert.That(result).DoesNotContain("ciuser");
    }

    [Test]
    public async Task Anonymize_WithWindowsHomePath_StripsUsernameFromMessage()
    {
        const string input = @"Could not find a part of the path 'C:\Users\jdoe\secret\config.json'.";

        var result = TelemetryAnonymizer.Anonymize(input);

        await Assert.That(result).IsEqualTo("Could not find a part of the path 'config.json'.");
        await Assert.That(result).DoesNotContain("jdoe");
    }

    [Test]
    public async Task Anonymize_WithDataverseOrgUrl_RedactsOrgUrl()
    {
        const string input = "The user is not enabled to access the environment https://contoso.crm4.dynamics.com/api/data/v9.2/systemusers.";

        var result = TelemetryAnonymizer.Anonymize(input);

        await Assert.That(result).IsEqualTo("The user is not enabled to access the environment [dataverse-org-url-redacted].");
        await Assert.That(result).DoesNotContain("contoso");
    }

    [Test]
    public async Task Anonymize_WithEntraTenantUrl_RedactsTenantUrl()
    {
        const string input = "Failed to authenticate against https://contoso.onmicrosoft.com/adfs/token.";

        var result = TelemetryAnonymizer.Anonymize(input);

        await Assert.That(result).IsEqualTo("Failed to authenticate against [entra-tenant-url-redacted].");
        await Assert.That(result).DoesNotContain("contoso");
    }

    [Test]
    public async Task Anonymize_WithUnrelatedUrl_LeavesUrlUntouched()
    {
        // Guards against the home-path regex over-matching generic URLs (e.g. nuget.org / github.com
        // links that regularly show up in version-check or network related exception messages).
        const string input = "Failed to reach https://api.nuget.org/v3/index.json";

        var result = TelemetryAnonymizer.Anonymize(input);

        await Assert.That(result).IsEqualTo(input);
    }
}
