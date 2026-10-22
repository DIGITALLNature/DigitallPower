// Copyright (c) DIGITALL Nature. All rights reserved

using Microsoft.Xrm.Sdk;

namespace dgt.power.common.Ownership;

public sealed class OwnerResolutionException : Exception
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

    public OwnerResolutionException(
        OwnerResolutionFailure failure,
        string message,
        string? logicalKey = null,
        string? selector = null,
        EntityReference? capturedOwner = null,
        Exception? innerException = null)
        : base(message, innerException)
    {
        Failure = failure;
        LogicalKey = logicalKey;
        Selector = selector;
        CapturedOwner = capturedOwner;
    }

    public OwnerResolutionFailure Failure { get; }

    public string? LogicalKey { get; }

    public string? Selector { get; }

    public EntityReference? CapturedOwner { get; }
}
