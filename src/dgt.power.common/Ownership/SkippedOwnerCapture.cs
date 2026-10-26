// Copyright (c) DIGITALL Nature. All rights reserved

using Microsoft.Xrm.Sdk;

namespace dgt.power.common.Ownership;

public sealed record SkippedOwnerCapture(EntityReference Principal, OwnerCaptureSkipReason Reason, string? DisplayName = null);
