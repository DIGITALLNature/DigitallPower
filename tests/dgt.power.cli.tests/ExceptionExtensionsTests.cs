// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.ServiceModel;
using CommonExceptionExtensions = dgt.power.common.Extensions.ExceptionExtensions;
using dgt.power.common.Extensions;
using Microsoft.Xrm.Sdk;
using Spectre.Console;
using Spectre.Console.Testing;

namespace dgt.power.cli.tests;

public class ExceptionExtensionsTests
{
    [Test]
    public async Task DiagnosticMessage_NestedExceptions_PreservesOperationContext()
    {
        var exception = new InvalidOperationException("Registering provider Source failed", new ArgumentException("Invalid schema"));
        await Assert.That(exception.DiagnosticMessage()).IsEqualTo($"Registering provider Source failed{Environment.NewLine}Invalid schema");
    }

    [Test]
    public async Task DiagnosticMessage_RepeatedMessages_PrintsMessageOnce()
    {
        var exception = new InvalidOperationException("Failure", new InvalidOperationException("Failure"));
        await Assert.That(exception.DiagnosticMessage()).IsEqualTo("Failure");
    }

    [Test]
    public async Task DiagnosticMessage_ServiceFault_IncludesCodesAndInnerFaultMessagesOnly()
    {
        var fault = new OrganizationServiceFault
        {
            ErrorCode = unchecked((int)0x8004F036),
            Message = "An unexpected error occurred",
            TraceText = "trace-data-not-for-console",
            InnerFault = new OrganizationServiceFault { ErrorCode = unchecked((int)0x80040203), Message = "Invalid data-source table" },
            ErrorDetails = { ["Configuration"] = "configuration-data-not-for-console" }
        };
        var exception = new InvalidOperationException("Registering provider Source failed", new FaultException<OrganizationServiceFault>(fault, fault.Message));
        await Assert.That(exception.DiagnosticMessage()).IsEqualTo(string.Join(Environment.NewLine,
            "Registering provider Source failed",
            "An unexpected error occurred",
            "Dataverse fault 0x8004F036: An unexpected error occurred",
            "Dataverse fault 0x80040203: Invalid data-source table"));
    }

    [Test]
    public async Task DiagnosticMessage_MarkupInFault_RenderedAsLiteralText()
    {
        using var console = new TestConsole();
        var fault = new OrganizationServiceFault { ErrorCode = 1, Message = "Invalid [source]" };
        var exception = new FaultException<OrganizationServiceFault>(fault, fault.Message);
        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]{exception.DiagnosticMessage()}[/]");
        await Assert.That(console.Output).Contains("Dataverse fault 0x00000001: Invalid [source]");
    }

    [Test]
    public async Task DiagnosticMessage_NullException_Throws()
    {
        await Assert.That(() => CommonExceptionExtensions.DiagnosticMessage(null!)).ThrowsExactly<ArgumentNullException>();
    }
}
