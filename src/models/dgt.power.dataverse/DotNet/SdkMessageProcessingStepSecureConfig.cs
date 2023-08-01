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
	/// Non-public custom configuration that is passed to a plug-in's constructor.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("sdkmessageprocessingstepsecureconfig")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class SdkMessageProcessingStepSecureConfig : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public SdkMessageProcessingStepSecureConfig() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public SdkMessageProcessingStepSecureConfig(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SdkMessageProcessingStepSecureConfig(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SdkMessageProcessingStepSecureConfig(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SdkMessageProcessingStepSecureConfig(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "sdkmessageprocessingstepsecureconfig";
        public const int EntityTypeCode = 4616;
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
		[AttributeLogicalNameAttribute("sdkmessageprocessingstepsecureconfigid")]
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
				SdkMessageProcessingStepSecureConfigId = value;
			}
		}

		/// <summary>
		/// Unique identifier of the SDK message processing step secure configuration.
		/// </summary>
		[AttributeLogicalName("sdkmessageprocessingstepsecureconfigid")]
        public Guid? SdkMessageProcessingStepSecureConfigId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("sdkmessageprocessingstepsecureconfigid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SdkMessageProcessingStepSecureConfigId));
                SetAttributeValue("sdkmessageprocessingstepsecureconfigid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(SdkMessageProcessingStepSecureConfigId));
            }
        }

		/// <summary>
		/// Unique identifier of the user who created the SDK message processing step.
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
		/// Date and time when the SDK message processing step was created.
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
		/// Unique identifier of the delegate user who created the sdkmessageprocessingstepsecureconfig.
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
		/// Customization level of the SDK message processing step secure configuration.
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
		/// Unique identifier of the user who last modified the SDK message processing step.
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
		/// Date and time when the SDK message processing step was last modified.
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
		/// Unique identifier of the delegate user who last modified the sdkmessageprocessingstepsecureconfig.
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
		/// Unique identifier of the organization with which the SDK message processing step is associated.
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
		/// Unique identifier of the SDK message processing step.
		/// </summary>
		[AttributeLogicalName("sdkmessageprocessingstepsecureconfigidunique")]
        public Guid? SdkMessageProcessingStepSecureConfigIdUnique
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("sdkmessageprocessingstepsecureconfigidunique");
            }
        }

		/// <summary>
		/// Secure step-specific configuration for the plug-in type that is passed to the plug-in's constructor at run time.
		/// </summary>
		[AttributeLogicalName("secureconfig")]
        public string? SecureConfig
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("secureconfig");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SecureConfig));
                SetAttributeValue("secureconfig", value);
                OnPropertyChanged(nameof(SecureConfig));
            }
        }


		#endregion

		#region NavigationProperties
		/// <summary>
		/// 1:N sdkmessageprocessingstepsecureconfigid_sdkmessageprocessingstep
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("sdkmessageprocessingstepsecureconfigid_sdkmessageprocessingstep")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStep> SdkmessageprocessingstepsecureconfigidSdkmessageprocessingstep
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStep>("sdkmessageprocessingstepsecureconfigid_sdkmessageprocessingstep", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SdkmessageprocessingstepsecureconfigidSdkmessageprocessingstep");
				this.SetRelatedEntities<SdkMessageProcessingStep>("sdkmessageprocessingstepsecureconfigid_sdkmessageprocessingstep", null, value);
				this.OnPropertyChanged("SdkmessageprocessingstepsecureconfigidSdkmessageprocessingstep");
			}
		}

		#endregion

		#region Options
		public static class Options
		{
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string SdkMessageProcessingStepSecureConfigId = "sdkmessageprocessingstepsecureconfigid";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string CustomizationLevel = "customizationlevel";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string OrganizationId = "organizationid";
				public const string SdkMessageProcessingStepSecureConfigIdUnique = "sdkmessageprocessingstepsecureconfigidunique";
				public const string SecureConfig = "secureconfig";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string SdkmessageprocessingstepsecureconfigidSdkmessageprocessingstep = "sdkmessageprocessingstepsecureconfigid_sdkmessageprocessingstep";
				public const string UserentityinstancedataSdkmessageprocessingstepsecureconfig = "userentityinstancedata_sdkmessageprocessingstepsecureconfig";
            }

            public static class ManyToOne
            {
				public const string CreatedbySdkmessageprocessingstepsecureconfig = "createdby_sdkmessageprocessingstepsecureconfig";
				public const string LkSdkmessageprocessingstepsecureconfigCreatedonbehalfby = "lk_sdkmessageprocessingstepsecureconfig_createdonbehalfby";
				public const string LkSdkmessageprocessingstepsecureconfigModifiedonbehalfby = "lk_sdkmessageprocessingstepsecureconfig_modifiedonbehalfby";
				public const string ModifiedbySdkmessageprocessingstepsecureconfig = "modifiedby_sdkmessageprocessingstepsecureconfig";
				public const string OrganizationSdkmessageprocessingstepsecureconfig = "organization_sdkmessageprocessingstepsecureconfig";
            }

            public static class ManyToMany
            {
            }
        }

        #endregion

		#region Methods
        public static SdkMessageProcessingStepSecureConfig Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static SdkMessageProcessingStepSecureConfig Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("sdkmessageprocessingstepsecureconfig", id, columnSet).ToEntity<SdkMessageProcessingStepSecureConfig>();
        }

        public SdkMessageProcessingStepSecureConfig GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  SdkMessageProcessingStepSecureConfig(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<SdkMessageProcessingStepSecureConfig> SdkMessageProcessingStepSecureConfigSet
		{
			get
			{
				return CreateQuery<SdkMessageProcessingStepSecureConfig>();
			}
		}
	}
	#endregion
}
