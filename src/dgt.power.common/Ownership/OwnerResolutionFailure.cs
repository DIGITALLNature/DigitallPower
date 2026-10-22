// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.common.Ownership;

public enum OwnerResolutionFailure
{
    InvalidMapping,
    MissingKey,
    NotFound,
    Ambiguous,
    UnsupportedKind,
    MissingCaptureMapping,
    AmbiguousCaptureMapping
}
