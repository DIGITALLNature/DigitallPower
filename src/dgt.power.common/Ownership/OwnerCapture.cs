// Copyright (c) DIGITALL Nature. All rights reserved

using Microsoft.Xrm.Sdk;

namespace dgt.power.common.Ownership;

public sealed record OwnerCapture(EntityReference Principal, string LogicalKey);
