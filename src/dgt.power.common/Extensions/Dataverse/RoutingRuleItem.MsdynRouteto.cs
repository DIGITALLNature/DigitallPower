// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using Microsoft.Xrm.Sdk;

// ReSharper disable All
namespace dgt.power.dataverse;

/// <summary>
/// Extension for attributes not present in the base routingruleitem entity generation.
/// - msdyn_routeto: Requires Dynamics 365 Customer Service module.
/// - assignobjectidtype: Virtual attribute for the polymorphic lookup type.
/// </summary>
public partial class RoutingRuleItem
{
    /// <summary>
    /// Route To (msdyn_routeto) - determines whether the routing rule item
    /// routes to a Queue or to a User/Team.
    /// </summary>
    [AttributeLogicalName("msdyn_routeto")]
    public OptionSetValue? MsdynRouteto
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<OptionSetValue?>("msdyn_routeto");
        }
        [DebuggerNonUserCode]
        set
        {
            OnPropertyChanging();
            SetAttributeValue("msdyn_routeto", value);
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Entity type of the polymorphic AssignObjectId lookup.
    /// </summary>
    [AttributeLogicalName("assignobjectidtype")]
    public string? AssignObjectIdType
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<string?>("assignobjectidtype");
        }
        [DebuggerNonUserCode]
        set
        {
            OnPropertyChanging();
            SetAttributeValue("assignobjectidtype", value);
            OnPropertyChanged();
        }
    }

    public static partial class Options
    {
        public struct MsdynRouteto
        {
            /// <summary>Route to a Queue</summary>
            public const int Queue = 0;
            /// <summary>Route to a User or Team</summary>
            public const int User_Team = 1;
        }
    }
}
