// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Model;
using dgt.power.codegeneration.Templates;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Services.Contracts;

public interface IMetadataService
{
    /// <summary>
    ///     Retrieves request metadata (actions, custom APIs, built-in SDK messages) for the given names.
    ///     Auto-detects type per entry: custom API → CustomAPI table, classic action → Workflow XAML, built-in → name only.
    /// </summary>
    Task<IReadOnlyList<WfAction>> RetrieveRequestsAsync(IReadOnlyCollection<string> requestNames, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns SDK message name constants. Includes hardcoded defaults (Create, Update, etc.)
    ///     plus any additional names from the provided requests collection.
    /// </summary>
    Task<IReadOnlyList<(string Name, string Message)>> RetrieveSdkMessageNamesAsync(IReadOnlyCollection<string> requestNames, CancellationToken cancellationToken = default);
    Task<SortedDictionary<string, List<Option>>> RetrieveOptionSetsAsync(IReadOnlyCollection<string> globalOptionSets, CancellationToken cancellationToken = default);

    Task<EntityMetadata> RetrieveEntityMetadataAsync(string entity, EntityFilters filter = EntityFilters.Default, CancellationToken cancellationToken = default);
    Task<int> RetrieveOrganizationLanguageAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns the UI language (LCID) of the currently connected user, as configured in their personal
    ///     <c>usersettings</c>. Dataverse resolves translatable out-of-box record text (e.g. system form
    ///     <c>name</c>) based on this session language, not on any per-request parameter - so it can differ
    ///     from the language configured for code generation. Used to warn about this known limitation.
    /// </summary>
    Task<int> RetrieveConnectionUserLanguageAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Tuple<string, string, Guid, string>>> RetrieveBusinessProcessFlowsAsync(IReadOnlyCollection<string> businessProcessFlows, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Tuple<string, string, List<Guid>>>> RetrieveBusinessProcessFlowStagesAsync(Guid processId, CancellationToken cancellationToken = default);

    Task<Dictionary<string, FormDetail>> RetrieveFormsDetailsFromSolutionsAsync(
        string entityLogicalName, string[] configSolutions, SortedSet<BpfControlDetail>? bpfControls, CancellationToken cancellationToken = default);
    Task<Dictionary<string, FormDetail>> RetrieveFormsDetailsAsync(string entityLogicalName, SortedSet<BpfControlDetail>? bpfControls, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<BpfControlDetail>> RetrieveBusinessProcessFlowControlsForMainEntityAsync(
        IReadOnlyCollection<string> businessProcessFlows, string entityName, CancellationToken cancellationToken = default);

    Task PopulateEntitiesAndSolutionsAsync(CodeGenerationConfigBase config, CancellationToken cancellationToken = default);
}
