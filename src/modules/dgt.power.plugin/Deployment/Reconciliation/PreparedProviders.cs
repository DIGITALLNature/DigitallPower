// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record PreparedProviders(
    IReadOnlyList<DataProviderChange> Changes,
    ProviderSolutionChanges? Solution);
