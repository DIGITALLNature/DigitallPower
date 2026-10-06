// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Local;

internal static class DataProviderRegistrationGrouper
{
    internal static IReadOnlyList<LocalDataProvider> Group(IReadOnlyList<LocalPluginType> types)
    {
        var groups = types.SelectMany(type => type.DataProviders.Select(registration => (type.TypeName, Registration: registration)))
            .GroupBy(item => item.Registration.DataSourceSchemaName, StringComparer.OrdinalIgnoreCase);
        var result = new List<LocalDataProvider>();
        foreach (var group in groups)
        {
            var schemaName = group.Key;
            var validLength = !string.IsNullOrWhiteSpace(schemaName) && schemaName.Length <= 128;
            var validCharacters = validLength && char.IsAsciiLetter(schemaName[0]) &&
                                  schemaName.All(character => char.IsAsciiLetterOrDigit(character) || character == '_');
            if (!validCharacters || !schemaName.Contains('_', StringComparison.Ordinal))
            {
                throw new AssemblyException($"DataSourceSchemaName '{schemaName}' must be a publisher-prefixed Dataverse schema name (letters, digits, underscores; maximum 128 characters).");
            }

            var registrations = group.Select(item => item.Registration).ToList();
            var providerName = Merge(registrations.Select(item => item.ProviderName), schemaName, "ProviderName");
            if (string.IsNullOrWhiteSpace(providerName) || providerName.Length > 100)
            {
                throw new AssemblyException($"Provider '{schemaName}' requires a nonempty ProviderName (maximum 100 characters) on at least one declaration.");
            }

            var handlers = new Dictionary<DataProviderOperation, string>();
            foreach (var item in group)
            {
                var operation = item.Registration.Event;
                _ = operation.HandlerField();
                if (handlers.TryGetValue(operation, out var existing) && existing != item.TypeName)
                {
                    throw new AssemblyException($"Provider '{schemaName}' operation '{operation}' is claimed by both '{existing}' and '{item.TypeName}'.");
                }

                handlers[operation] = item.TypeName;
            }

            var description = Merge(registrations.Select(item => item.Description), schemaName, "Description", allowEmpty: true);
            if (description?.Length > 1000)
            {
                throw new AssemblyException($"Provider '{schemaName}' Description exceeds 1000 characters.");
            }

            result.Add(new LocalDataProvider(schemaName, providerName,
                Merge(registrations.Select(item => item.DataSourceDisplayName), schemaName, "DataSourceDisplayName"),
                Merge(registrations.Select(item => item.DataSourcePluralName), schemaName, "DataSourcePluralName"), description, handlers));
        }

        return result;
    }

    private static string? Merge(IEnumerable<string?> values, string schemaName, string property, bool allowEmpty = false)
    {
        var explicitValues = values.OfType<string>().Distinct(StringComparer.Ordinal).ToList();
        if (explicitValues.Count > 1 || !allowEmpty && explicitValues.Exists(string.IsNullOrWhiteSpace))
        {
            throw new AssemblyException($"Provider '{schemaName}' has conflicting or empty metadata for {property}.");
        }

        return explicitValues.SingleOrDefault();
    }

}
