// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.common.Ownership;

public sealed record RecordOwnerCapture(
    IReadOnlyDictionary<Guid, string> OwnerKeys,
    IReadOnlyDictionary<Guid, SkippedOwnerCapture> SkippedRecords);
