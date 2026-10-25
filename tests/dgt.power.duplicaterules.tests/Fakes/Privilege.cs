// Copyright (c) DIGITALL Nature. All rights reserved

using System.Runtime.Serialization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

[assembly: ProxyTypesAssembly]

namespace dgt.power.duplicaterules.tests.Fakes;

/// <summary>
/// Minimal early-bound privilege table so the fake service can query privileges by name.
/// </summary>
[DataContract]
[EntityLogicalName(EntityLogicalName)]
public sealed class Privilege : Entity
{
    public const string EntityLogicalName = "privilege";

    public Privilege() : base(EntityLogicalName)
    {
    }

    public Privilege(Guid id) : base(EntityLogicalName, id)
    {
    }

    [AttributeLogicalName("privilegeid")]
    public Guid? PrivilegeId
    {
        get => GetAttributeValue<Guid?>("privilegeid");
        set
        {
            SetAttributeValue("privilegeid", value);
            base.Id = value ?? Guid.Empty;
        }
    }

    [AttributeLogicalName("privilegeid")]
    public override Guid Id
    {
        get => base.Id;
        set => PrivilegeId = value;
    }

    [AttributeLogicalName("name")]
    public string? Name
    {
        get => GetAttributeValue<string?>("name");
        set => SetAttributeValue("name", value);
    }
}
