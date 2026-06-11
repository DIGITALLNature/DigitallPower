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
    IReadOnlyList<WfAction> RetrieveRequests(IReadOnlyCollection<string> requestNames);

    /// <summary>
    ///     Returns SDK message name constants. Includes hardcoded defaults (Create, Update, etc.)
    ///     plus any additional names from the provided requests collection.
    /// </summary>
    IReadOnlyList<(string Name, string Message)> RetrieveSdkMessageNames(IReadOnlyCollection<string> requestNames);

    SortedDictionary<string, List<Option>> RetrieveOptionSets(IReadOnlyCollection<string> globalOptionSets);
    EntityMetadata RetrieveEntityMetadata(string entity, EntityFilters filter = EntityFilters.Default);
    int RetrieveOrganizationLanguage();
    IReadOnlyList<Tuple<string, string, Guid, string>> RetrieveBusinessProcessFlows(IReadOnlyCollection<string> businessProcessFlows);
    IReadOnlyList<Tuple<string, string, List<Guid>>> RetrieveBusinessProcessFlowStages(Guid processId);

    Dictionary<string, FormDetail> RetrieveFormsDetailsFromSolutions(string entityLogicalName, string[] configSolutions, SortedSet<BpfControlDetail>? bpfControls);

    Dictionary<string, FormDetail> RetrieveFormsDetails(string entityLogicalName, SortedSet<BpfControlDetail>? bpfControls);
    IReadOnlyList<BpfControlDetail> RetrieveBusinessProcessFlowControlsForMainEntity(IReadOnlyCollection<string> businessProcessFlows, string entityName);
    void PopulateEntitiesAndSolutions(CodeGenerationConfigBase config);

    #region Legacy V1 support
    [Obsolete("Use RetrieveRequests instead. Will be removed in a future major version.")]
    IEnumerable<WfAction> RetrieveActions(CodeGenerationConfig config);
    [Obsolete("Use RetrieveRequests instead. Will be removed in a future major version.")]
    IEnumerable<WfAction> RetrieveCustomApis(CodeGenerationConfig config);
    [Obsolete("Use RetrieveSdkMessageNames(IReadOnlyCollection<string>) instead.")]
    IReadOnlyList<(string Name, string Message)> RetrieveSdkMessageNames(CodeGenerationConfig config);
    [Obsolete("Use RetrieveOptionSets(IReadOnlyCollection<string>) instead.")]
    SortedDictionary<string, List<Option>> RetrieveOptionSets(CodeGenerationConfig config);
    [Obsolete("Use PopulateEntitiesAndSolutions(CodeGenerationConfigBase) instead.")]
    void PopulateEntitiesAndSolutions(CodeGenerationConfig config);
    [Obsolete("Use RetrieveBusinessProcessFlows(IReadOnlyCollection<string>) instead.")]
    IReadOnlyList<Tuple<string, string, Guid, string>> RetrieveBusinessProcessFlows(CodeGenerationConfig config);
    [Obsolete("Use RetrieveBusinessProcessFlowControlsForMainEntity(IReadOnlyCollection<string>, string) instead.")]
    IReadOnlyList<BpfControlDetail> RetrieveBusinessProcessFlowControlsForMainEntity(CodeGenerationConfig config, string entityName);
    #endregion
}
