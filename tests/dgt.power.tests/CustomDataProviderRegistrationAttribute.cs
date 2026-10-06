// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace Digitall.Plugins.Registration;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class CustomDataProviderRegistrationAttribute : Attribute
{
    public CustomDataProviderRegistrationAttribute() { }

    public CustomDataProviderRegistrationAttribute(string entityName, int eventRegistration)
    {
        EntityName = entityName;
        EventRegistration = eventRegistration;
    }

    public string? EntityName { get; }
    public int EventRegistration { get; }
    public string? DataSourceSchemaName { get; set; }
    public DataProviderEvent Event { get; set; } = DataProviderEvent.Unspecified;
    public string? ProviderName { get; set; }
    public string? DataSourceDisplayName { get; set; }
    public string? DataSourcePluralName { get; set; }
    public string? Description { get; set; }
}
