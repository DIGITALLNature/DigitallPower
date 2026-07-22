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
    /// <inheritdoc cref="RetrieveSdkMessageNames(IReadOnlyCollection{string})"/>
    IReadOnlyList<(string Name, string Message)> RetrieveSdkMessageNames(CodeGenerationConfig config);

    SortedDictionary<string, List<Option>> RetrieveOptionSets(IReadOnlyCollection<string> globalOptionSets);
    /// <inheritdoc cref="RetrieveOptionSets(IReadOnlyCollection{string})"/>
    SortedDictionary<string, List<Option>> RetrieveOptionSets(CodeGenerationConfig config);

    EntityMetadata RetrieveEntityMetadata(string entity, EntityFilters filter = EntityFilters.Default);
    int RetrieveOrganizationLanguage();

    /// <summary>
    ///     Returns the UI language (LCID) of the currently connected user, as configured in their personal
    ///     <c>usersettings</c>. Dataverse resolves translatable out-of-box record text (e.g. system form
    ///     <c>name</c>) based on this session language, not on any per-request parameter - so it can differ
    ///     from the language configured for code generation. Used to warn about this known limitation.
    /// </summary>
    int RetrieveConnectionUserLanguage();

    IReadOnlyList<Tuple<string, string, Guid, string>> RetrieveBusinessProcessFlows(IReadOnlyCollection<string> businessProcessFlows);
    /// <inheritdoc cref="RetrieveBusinessProcessFlows(IReadOnlyCollection{string})"/>
    IReadOnlyList<Tuple<string, string, Guid, string>> RetrieveBusinessProcessFlows(CodeGenerationConfig config);

    IReadOnlyList<Tuple<string, string, List<Guid>>> RetrieveBusinessProcessFlowStages(Guid processId);

    Dictionary<string, FormDetail> RetrieveFormsDetailsFromSolutions(string entityLogicalName, string[] configSolutions, SortedSet<BpfControlDetail>? bpfControls);
    Dictionary<string, FormDetail> RetrieveFormsDetails(string entityLogicalName, SortedSet<BpfControlDetail>? bpfControls);

    IReadOnlyList<BpfControlDetail> RetrieveBusinessProcessFlowControlsForMainEntity(IReadOnlyCollection<string> businessProcessFlows, string entityName);
    /// <inheritdoc cref="RetrieveBusinessProcessFlowControlsForMainEntity(IReadOnlyCollection{string},string)"/>
    IReadOnlyList<BpfControlDetail> RetrieveBusinessProcessFlowControlsForMainEntity(CodeGenerationConfig config, string entityName);

    void PopulateEntitiesAndSolutions(CodeGenerationConfigBase config);
    /// <inheritdoc cref="PopulateEntitiesAndSolutions(CodeGenerationConfigBase)"/>
    void PopulateEntitiesAndSolutions(CodeGenerationConfig config);

    // V1 legacy methods without V2 counterparts
    IEnumerable<WfAction> RetrieveActions(CodeGenerationConfig config);
    IEnumerable<WfAction> RetrieveCustomApis(CodeGenerationConfig config);
}
