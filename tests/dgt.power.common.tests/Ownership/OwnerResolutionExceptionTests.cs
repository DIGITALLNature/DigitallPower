// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common.Ownership;
using Microsoft.Xrm.Sdk;

namespace dgt.power.common.tests.Ownership;

public class OwnerResolutionExceptionTests
{
    [Test]
    public async Task DefaultConstructor_ReportsInvalidMapping()
    {
        var exception = new OwnerResolutionException();

        await Assert.That(exception.Failure).IsEqualTo(OwnerResolutionFailure.InvalidMapping);
        await Assert.That(exception.Message).IsEqualTo("Owner resolution failed.");
    }

    [Test]
    public async Task MessageConstructor_PreservesMessage()
    {
        var exception = new OwnerResolutionException("Invalid owner selector.");

        await Assert.That(exception.Failure).IsEqualTo(OwnerResolutionFailure.InvalidMapping);
        await Assert.That(exception.Message).IsEqualTo("Invalid owner selector.");
    }

    [Test]
    public async Task InnerExceptionConstructor_PreservesCause()
    {
        var cause = new InvalidOperationException("Parsing failed.");
        var exception = new OwnerResolutionException("Invalid owner selector.", cause);

        await Assert.That(exception.Failure).IsEqualTo(OwnerResolutionFailure.InvalidMapping);
        await Assert.That(exception.Message).IsEqualTo("Invalid owner selector.");
        await Assert.That(exception.InnerException).IsSameReferenceAs(cause);
    }

    [Test]
    public async Task ContextConstructor_PreservesResolutionDiagnostics()
    {
        var capturedOwner = new EntityReference("team", Guid.NewGuid());
        var cause = new InvalidOperationException("Lookup failed.");
        var exception = new OwnerResolutionException(
            OwnerResolutionFailure.AmbiguousCaptureMapping,
            "Multiple owner keys.",
            logicalKey: "salesOwner",
            selector: "Sales-*",
            capturedOwner: capturedOwner,
            innerException: cause);

        await Assert.That(exception.Failure).IsEqualTo(OwnerResolutionFailure.AmbiguousCaptureMapping);
        await Assert.That(exception.Message).IsEqualTo("Multiple owner keys.");
        await Assert.That(exception.LogicalKey).IsEqualTo("salesOwner");
        await Assert.That(exception.Selector).IsEqualTo("Sales-*");
        await Assert.That(exception.CapturedOwner).IsSameReferenceAs(capturedOwner);
        await Assert.That(exception.InnerException).IsSameReferenceAs(cause);
    }
}
