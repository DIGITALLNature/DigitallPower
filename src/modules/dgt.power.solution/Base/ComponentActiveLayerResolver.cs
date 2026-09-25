// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.solution.Base;

/// <summary>
/// Resolves whether a solutioncomponent row's top <c>msdyn_componentlayer</c> is the synthetic
/// "Active" layer, keyed by (componenttype, objectid). A top layer of "Active" means the component
/// has been customized on top of its managed baseline; that customization is what copy-components'
/// best-practice mode carries forward for managed components, everything else is redundant since
/// the managed baseline already provides it. Mirrors <c>BaseAnalyze.GetSolutionLayers</c>/
/// <c>GetTopNotActiveLayer</c> (dgt.power.analyzer), ported to <see cref="IOrganizationServiceAsync2"/>.
/// </summary>
public sealed class ComponentActiveLayerResolver(IOrganizationServiceAsync2 connection)
{
    private const string ActiveLayerName = "Active";
    private const int PageSize = 5000;

    public Task<IReadOnlyDictionary<(int ComponentType, Guid ObjectId), bool>> ResolveAsync(
        IReadOnlyCollection<SolutionComponent> components,
        IReadOnlyDictionary<int, SolutionComponentDefinitionInfo> definitionsByType,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(components);
        ArgumentNullException.ThrowIfNull(definitionsByType);

        return ResolveCoreAsync(components, definitionsByType, cancellationToken);
    }

    private async Task<IReadOnlyDictionary<(int ComponentType, Guid ObjectId), bool>> ResolveCoreAsync(
        IReadOnlyCollection<SolutionComponent> components,
        IReadOnlyDictionary<int, SolutionComponentDefinitionInfo> definitionsByType,
        CancellationToken cancellationToken)
    {
        var result = new Dictionary<(int, Guid), bool>();

        // ComponentType/ObjectId are only ever null for malformed rows - filter and unwrap to the
        // plain (int, Guid) shape once here so the rest of the method deals with concrete values.
        var typed = components
            .Where(static component => component.ComponentType?.Value != null && component.ObjectId.HasValue)
            .Select(static component => (ComponentType: component.ComponentType!.Value, ObjectId: component.ObjectId!.Value))
            .Where(entry => definitionsByType.TryGetValue(entry.ComponentType, out var definition) && !string.IsNullOrEmpty(definition.Name))
            .ToList();

        var groupsByLayerName = typed.GroupBy(entry => definitionsByType[entry.ComponentType].Name!);

        foreach (var group in groupsByLayerName)
        {
            var componentIds = group
                .Select(static entry => $"{entry.ObjectId:B}")
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            var query = new QueryExpression(MsdynComponentlayer.EntityLogicalName)
            {
                NoLock = true,
                ColumnSet = new ColumnSet(
                    MsdynComponentlayer.LogicalNames.MsdynComponentid,
                    MsdynComponentlayer.LogicalNames.MsdynSolutionname,
                    MsdynComponentlayer.LogicalNames.MsdynOrder)
            };
            query.Criteria.AddCondition(MsdynComponentlayer.LogicalNames.MsdynSolutioncomponentname, ConditionOperator.Equal, group.Key);
            query.Criteria.AddCondition(MsdynComponentlayer.LogicalNames.MsdynComponentid, ConditionOperator.In, componentIds.Cast<object>().ToArray());
            query.AddOrder(MsdynComponentlayer.LogicalNames.MsdynOrder, OrderType.Descending);
            query.PageInfo = new PagingInfo { Count = PageSize, PageNumber = 1 };

            var layers = new List<MsdynComponentlayer>();
            bool moreRecords;
            do
            {
                var page = await connection.RetrieveMultipleAsync(query, cancellationToken);
                layers.AddRange(page.Entities.Select(static entity => entity.ToEntity<MsdynComponentlayer>()));
                moreRecords = page.MoreRecords;
                if (moreRecords)
                {
                    query.PageInfo.PageNumber++;
                    query.PageInfo.PagingCookie = page.PagingCookie;
                }
            } while (moreRecords);

            var topLayerByComponentId = layers
                .Where(static layer => layer.MsdynComponentid != null)
                .GroupBy(static layer => layer.MsdynComponentid!, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(
                    static layerGroup => layerGroup.Key,
                    static layerGroup => layerGroup.OrderByDescending(static layer => layer.MsdynOrder ?? int.MinValue).First(),
                    StringComparer.OrdinalIgnoreCase);

            foreach (var entry in group)
            {
                var componentId = $"{entry.ObjectId:B}";
                var hasActiveLayer = topLayerByComponentId.TryGetValue(componentId, out var topLayer) &&
                    topLayer.MsdynSolutionname == ActiveLayerName;

                result[(entry.ComponentType, entry.ObjectId)] = hasActiveLayer;
            }
        }

        return result;
    }
}
