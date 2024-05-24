// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query; 
using AttributeCollection = Microsoft.Xrm.Sdk.AttributeCollection;

// ReSharper disable All
namespace dgt.power.dataverse
{
	/// <inheritdoc />
	/// <summary>
	/// Filter that defines which SDK messages are valid for each type of entity.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("sdkmessagefilter")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class SdkMessageFilter : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public SdkMessageFilter() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public SdkMessageFilter(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SdkMessageFilter(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SdkMessageFilter(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SdkMessageFilter(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "sdkmessagefilter";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4607;
        #endregion

        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        [DebuggerNonUserCode]
		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (_trackChanges)
            {
                _changedProperties.Value.Add(propertyName);
            }
        }

        [DebuggerNonUserCode]
		private void OnPropertyChanging([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanging != null) PropertyChanging.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        #endregion

		#region Attributes
		[AttributeLogicalNameAttribute("sdkmessagefilterid")]
		public new System.Guid Id
		{
		    [DebuggerNonUserCode]
			get
			{
				return base.Id;
			}
            [DebuggerNonUserCode]
			set
			{
				SdkMessageFilterId = value;
			}
		}

		/// <summary>
		/// Unique identifier of the SDK message filter entity.
		/// </summary>
		[AttributeLogicalName("sdkmessagefilterid")]
        public Guid? SdkMessageFilterId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("sdkmessagefilterid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SdkMessageFilterId));
                SetAttributeValue("sdkmessagefilterid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(SdkMessageFilterId));
            }
        }

		/// <summary>
		/// Identifies where a method will be exposed. 0 - Server, 1 - Client, 2 - both.
		/// </summary>
		[AttributeLogicalName("availability")]
        public int? Availability
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("availability");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Availability));
                SetAttributeValue("availability", value);
                OnPropertyChanged(nameof(Availability));
            }
        }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("componentstate")]
        public OptionSetValue? ComponentState
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("componentstate");
            }
        }

		/// <summary>
		/// Unique identifier of the user who created the SDK message filter.
		/// </summary>
		[AttributeLogicalName("createdby")]
        public EntityReference? CreatedBy
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("createdby");
            }
        }

		/// <summary>
		/// Date and time when the SDK message filter was created.
		/// </summary>
		[AttributeLogicalName("createdon")]
        public DateTime? CreatedOn
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("createdon");
            }
        }

		/// <summary>
		/// Unique identifier of the delegate user who created the sdkmessagefilter.
		/// </summary>
		[AttributeLogicalName("createdonbehalfby")]
        public EntityReference? CreatedOnBehalfBy
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("createdonbehalfby");
            }
        }

		/// <summary>
		/// Customization level of the SDK message filter.
		/// </summary>
		[AttributeLogicalName("customizationlevel")]
        public int? CustomizationLevel
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("customizationlevel");
            }
        }

		/// <summary>
		/// Version in which the component is introduced.
		/// </summary>
		[AttributeLogicalName("introducedversion")]
        public string? IntroducedVersion
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("introducedversion");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IntroducedVersion));
                SetAttributeValue("introducedversion", value);
                OnPropertyChanged(nameof(IntroducedVersion));
            }
        }

		/// <summary>
		/// Indicates whether a custom SDK message processing step is allowed.
		/// </summary>
		[AttributeLogicalName("iscustomprocessingstepallowed")]
        public bool? IsCustomProcessingStepAllowed
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("iscustomprocessingstepallowed");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsCustomProcessingStepAllowed));
                SetAttributeValue("iscustomprocessingstepallowed", value);
                OnPropertyChanged(nameof(IsCustomProcessingStepAllowed));
            }
        }

		/// <summary>
		/// Information that specifies whether this component is managed.
		/// </summary>
		[AttributeLogicalName("ismanaged")]
        public bool? IsManaged
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("ismanaged");
            }
        }

		/// <summary>
		/// Indicates whether the filter should be visible.
		/// </summary>
		[AttributeLogicalName("isvisible")]
        public bool? IsVisible
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isvisible");
            }
        }

		/// <summary>
		/// Unique identifier of the user who last modified the SDK message filter.
		/// </summary>
		[AttributeLogicalName("modifiedby")]
        public EntityReference? ModifiedBy
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("modifiedby");
            }
        }

		/// <summary>
		/// Date and time when the SDK message filter was last modified.
		/// </summary>
		[AttributeLogicalName("modifiedon")]
        public DateTime? ModifiedOn
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("modifiedon");
            }
        }

		/// <summary>
		/// Unique identifier of the delegate user who last modified the sdkmessagefilter.
		/// </summary>
		[AttributeLogicalName("modifiedonbehalfby")]
        public EntityReference? ModifiedOnBehalfBy
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("modifiedonbehalfby");
            }
        }

		/// <summary>
		/// Name of the SDK message filter.
		/// </summary>
		[AttributeLogicalName("name")]
        public string? Name
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("name");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Name));
                SetAttributeValue("name", value);
                OnPropertyChanged(nameof(Name));
            }
        }

		/// <summary>
		/// Unique identifier of the organization with which the SDK message filter is associated.
		/// </summary>
		[AttributeLogicalName("organizationid")]
        public EntityReference? OrganizationId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("organizationid");
            }
        }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("overwritetime")]
        public DateTime? OverwriteTime
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("overwritetime");
            }
        }

		/// <summary>
		/// Type of entity with which the SDK message filter is primarily associated.
		/// </summary>
		[AttributeLogicalName("primaryobjecttypecode")]
        public string? PrimaryObjectTypeCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("primaryobjecttypecode");
            }
        }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("restrictionlevel")]
        public int? RestrictionLevel
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("restrictionlevel");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(RestrictionLevel));
                SetAttributeValue("restrictionlevel", value);
                OnPropertyChanged(nameof(RestrictionLevel));
            }
        }

		/// <summary>
		/// Unique identifier of the SDK message filter.
		/// </summary>
		[AttributeLogicalName("sdkmessagefilteridunique")]
        public Guid? SdkMessageFilterIdUnique
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("sdkmessagefilteridunique");
            }
        }

		/// <summary>
		/// Unique identifier of the related SDK message.
		/// </summary>
		[AttributeLogicalName("sdkmessageid")]
        public EntityReference? SdkMessageId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("sdkmessageid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SdkMessageId));
                SetAttributeValue("sdkmessageid", value);
                OnPropertyChanged(nameof(SdkMessageId));
            }
        }

		/// <summary>
		/// Type of entity with which the SDK message filter is secondarily associated.
		/// </summary>
		[AttributeLogicalName("secondaryobjecttypecode")]
        public string? SecondaryObjectTypeCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("secondaryobjecttypecode");
            }
        }

		/// <summary>
		/// Unique identifier of the associated solution.
		/// </summary>
		[AttributeLogicalName("solutionid")]
        public Guid? SolutionId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("solutionid");
            }
        }

		
		[AttributeLogicalName("versionnumber")]
        public long? VersionNumber
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<long?>("versionnumber");
            }
        }

		/// <summary>
		/// Whether or not the SDK message can be called from a workflow.
		/// </summary>
		[AttributeLogicalName("workflowsdkstepenabled")]
        public bool? WorkflowSdkStepEnabled
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("workflowsdkstepenabled");
            }
        }


		#endregion

		#region NavigationProperties
		/// <summary>
		/// 1:N sdkmessagefilterid_sdkmessageprocessingstep
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("sdkmessagefilterid_sdkmessageprocessingstep")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStep> SdkmessagefilteridSdkmessageprocessingstep
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStep>("sdkmessagefilterid_sdkmessageprocessingstep", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SdkmessagefilteridSdkmessageprocessingstep");
				this.SetRelatedEntities<SdkMessageProcessingStep>("sdkmessagefilterid_sdkmessageprocessingstep", null, value);
				this.OnPropertyChanged("SdkmessagefilteridSdkmessageprocessingstep");
			}
		}

		#endregion

		#region Options
		public static class Options
		{
			    public struct ComponentState
                {
					public const int Published = 0;
					public const int Unpublished = 1;
					public const int Deleted = 2;
					public const int DeletedUnpublished = 3;
                }
                public struct IsCustomProcessingStepAllowed
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct IsManaged
                {
                    public const bool Unmanaged = false;
                    public const bool Managed = true;
                }
                public struct IsVisible
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct WorkflowSdkStepEnabled
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string SdkMessageFilterId = "sdkmessagefilterid";
				public const string Availability = "availability";
				public const string ComponentState = "componentstate";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string CustomizationLevel = "customizationlevel";
				public const string IntroducedVersion = "introducedversion";
				public const string IsCustomProcessingStepAllowed = "iscustomprocessingstepallowed";
				public const string IsManaged = "ismanaged";
				public const string IsVisible = "isvisible";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string Name = "name";
				public const string OrganizationId = "organizationid";
				public const string OverwriteTime = "overwritetime";
				public const string PrimaryObjectTypeCode = "primaryobjecttypecode";
				public const string RestrictionLevel = "restrictionlevel";
				public const string SdkMessageFilterIdUnique = "sdkmessagefilteridunique";
				public const string SdkMessageId = "sdkmessageid";
				public const string SecondaryObjectTypeCode = "secondaryobjecttypecode";
				public const string SolutionId = "solutionid";
				public const string VersionNumber = "versionnumber";
				public const string WorkflowSdkStepEnabled = "workflowsdkstepenabled";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string SdkmessagefilterInternalcatalogassignment = "sdkmessagefilter_internalcatalogassignment";
				public const string SdkmessagefilteridSdkmessageprocessingstep = "sdkmessagefilterid_sdkmessageprocessingstep";
				public const string UserentityinstancedataSdkmessagefilter = "userentityinstancedata_sdkmessagefilter";
            }

            public static class ManyToOne
            {
				public const string CreatedbySdkmessagefilter = "createdby_sdkmessagefilter";
				public const string LkSdkmessagefilterCreatedonbehalfby = "lk_sdkmessagefilter_createdonbehalfby";
				public const string LkSdkmessagefilterModifiedonbehalfby = "lk_sdkmessagefilter_modifiedonbehalfby";
				public const string ModifiedbySdkmessagefilter = "modifiedby_sdkmessagefilter";
				public const string OrganizationSdkmessagefilter = "organization_sdkmessagefilter";
				public const string SdkmessageidSdkmessagefilter = "sdkmessageid_sdkmessagefilter";
            }

            public static class ManyToMany
            {
            }
        }

        #endregion

		#region Methods
        public EntityReference ToNamedEntityReference()
        {
            var reference = ToEntityReference();
            reference.Name = GetAttributeValue<string?>(PrimaryNameAttribute);
            return reference;
        }
        public static SdkMessageFilter Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static SdkMessageFilter Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("sdkmessagefilter", id, columnSet).ToEntity<SdkMessageFilter>();
        }

        public SdkMessageFilter GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  SdkMessageFilter(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<SdkMessageFilter> SdkMessageFilterSet
		{
			get
			{
				return CreateQuery<SdkMessageFilter>();
			}
		}
	}
	#endregion
}
