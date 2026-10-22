// Copyright (c) DIGITALL Nature. All rights reserved

using Microsoft.Xrm.Sdk;

namespace dgt.power.common.Ownership;

public sealed class OwnerResolutionException(
    OwnerResolutionFailure failure,
    string message,
    string? logicalKey = null,
    string? selector = null,
    EntityReference? capturedOwner = null,
    Exception? innerException = null) : Exception(message, innerException)
{
    public OwnerResolutionException()
        : this(OwnerResolutionFailure.InvalidMapping, "Owner resolution failed.")
    {
    }

    public OwnerResolutionException(string message)
        : this(OwnerResolutionFailure.InvalidMapping, message)
    {
    }

    public OwnerResolutionException(string message, Exception innerException)
        : this(OwnerResolutionFailure.InvalidMapping, message, innerException: innerException)
    {
    }

    public OwnerResolutionFailure Failure { get; } = failure;

    public string? LogicalKey { get; } = logicalKey;

    public string? Selector { get; } = selector;

    public EntityReference? CapturedOwner { get; } = capturedOwner;
}
