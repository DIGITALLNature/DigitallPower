// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.webresource.Local;
using dgt.power.webresource.Remote;
using dgt.power.webresource;

namespace dgt.power.webresource.Planning;

public static class WebResourcePushPlanner
{
    public static WebResourcePushPlan Plan(
        IReadOnlyList<LocalWebResource> local,
        IReadOnlyList<RemoteWebResource> remote,
        IReadOnlyList<RemoteSolutionWebResource>? solutionResources = null,
        bool deleteObsolete = false)
    {
        ArgumentNullException.ThrowIfNull(local);
        ArgumentNullException.ThrowIfNull(remote);

        var remoteByKey = remote.ToDictionary(resource => Key(resource.Name, resource.Type));
        var items = new List<WebResourcePlanItem>(local.Count);
        foreach (var localResource in local)
        {
            var remoteResource = remoteByKey.GetValueOrDefault(Key(localResource.Name, localResource.Type));
            var action = WebResourceAction.Create;
            if (remoteResource is not null)
            {
                if (remoteResource.IsManaged)
                {
                    throw new ManagedWebResourceException(localResource.Name);
                }

                action = string.Equals(remoteResource.Content, localResource.Content, StringComparison.Ordinal)
                    ? WebResourceAction.Keep
                    : WebResourceAction.Update;
            }

            items.Add(new WebResourcePlanItem(localResource, action, remoteResource));
        }

        var obsolete = deleteObsolete
            ? (solutionResources ?? throw new ArgumentException(
                    "Solution resources are required when obsolete deletion is enabled.",
                    nameof(solutionResources)))
                .Where(solutionResource => !local.Any(localResource =>
                    string.Equals(localResource.Name, solutionResource.Name, StringComparison.OrdinalIgnoreCase) &&
                    localResource.Type == solutionResource.Type))
                .Where(solutionResource => !solutionResource.IsManaged)
                .ToList()
            : [];

        return new WebResourcePushPlan(items, obsolete);
    }

    private static string Key(string name, int type) => $"{type}:{name}";
}
