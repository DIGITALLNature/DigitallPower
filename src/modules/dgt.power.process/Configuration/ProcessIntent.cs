// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.process.Configuration;

public sealed record ProcessIntent(Guid ProcessId, string? Name, bool? Active, string? Owner);
