// Copyright (c) DIGITALL Nature. All rights reserved

using Microsoft.Xrm.Sdk;

namespace dgt.power.teamtemplates.Logic;

public sealed record TeamTemplatePlan(
    IReadOnlyList<Entity> Creates,
    IReadOnlyList<Entity> Updates,
    IReadOnlyList<Guid> Unchanged,
    IReadOnlyList<Guid> Deletes);
