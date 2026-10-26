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
    private const string EntityLogicalName = "privilege";

    // ReSharper disable once UnusedMember.Global -- required by the SDK proxy-type materialization
    public Privilege() : base(EntityLogicalName)
    {
    }

    public Privilege(Guid id) : base(EntityLogicalName, id)
    {
    }

    [AttributeLogicalName("privilegeid")]
    public Guid? PrivilegeId
    {
        // ReSharper disable once UnusedMember.Global -- read by the SDK proxy-type materialization
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
        // ReSharper disable once UnusedMember.Global -- read by the fake service query evaluation
        get => GetAttributeValue<string?>("name");
        set => SetAttributeValue("name", value);
    }
}
