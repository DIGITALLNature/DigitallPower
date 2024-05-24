// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Model;
using dgt.power.codegeneration.Templates;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Services.Contracts;

public interface IMetadataService
{
    IEnumerable<WfAction> RetrieveActions(CodeGenerationConfig config);
    IEnumerable<WfAction> RetrieveCustomAPIs(CodeGenerationConfig config);
    IEnumerable<Tuple<string, string>> RetrieveSdkMessageNames(CodeGenerationConfig config);
    SortedDictionary<string, List<Option>> RetrieveOptionSets(CodeGenerationConfig config);
    EntityMetadata RetrieveEntityMetadata(string entity, EntityFilters filter = EntityFilters.Default);
    int RetrieveOrganizationLanguage();
    List<Tuple<string, string, Guid, string>> RetrieveBusinessProcessFlows(CodeGenerationConfig config);
    List<Tuple<string, string, List<Guid>>> RetrieveBusinessProcessFlowStages(Guid processId);

    Dictionary<string, FormDetail> RetrieveFormsDetailsFromSolutions(string entityLogicalName, string[] configSolutions);

    Dictionary<string, FormDetail> RetrieveFormsDetails(string entityLogicalName);
    void PopulateEntitiesAndSolutions(CodeGenerationConfig config);
}
