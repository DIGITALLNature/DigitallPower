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
	/// Assembly that contains one or more plug-in types.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("pluginassembly")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class PluginAssembly : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public PluginAssembly() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public PluginAssembly(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public PluginAssembly(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public PluginAssembly(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public PluginAssembly(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "pluginassembly";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4605;
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
		[AttributeLogicalNameAttribute("pluginassemblyid")]
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
				PluginAssemblyId = value;
			}
		}

		/// <summary>
		/// Unique identifier of the plug-in assembly.
		/// </summary>
		[AttributeLogicalName("pluginassemblyid")]
        public Guid? PluginAssemblyId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("pluginassemblyid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PluginAssemblyId));
                SetAttributeValue("pluginassemblyid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(PluginAssemblyId));
            }
        }

		/// <summary>
		/// Specifies mode of authentication with web sources like WebApp
		/// </summary>
		[AttributeLogicalName("authtype")]
        public OptionSetValue? AuthType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("authtype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AuthType));
                SetAttributeValue("authtype", value);
                OnPropertyChanged(nameof(AuthType));
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
		/// Bytes of the assembly, in Base64 format.
		/// </summary>
		[AttributeLogicalName("content")]
        public string? Content
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("content");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Content));
                SetAttributeValue("content", value);
                OnPropertyChanged(nameof(Content));
            }
        }

		/// <summary>
		/// Unique identifier of the user who created the plug-in assembly.
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
		/// Date and time when the plug-in assembly was created.
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
		/// Unique identifier of the delegate user who created the pluginassembly.
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
		/// Culture code for the plug-in assembly.
		/// </summary>
		[AttributeLogicalName("culture")]
        public string? Culture
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("culture");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Culture));
                SetAttributeValue("culture", value);
                OnPropertyChanged(nameof(Culture));
            }
        }

		/// <summary>
		/// Customization Level.
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
		/// Description of the plug-in assembly.
		/// </summary>
		[AttributeLogicalName("description")]
        public string? Description
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("description");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Description));
                SetAttributeValue("description", value);
                OnPropertyChanged(nameof(Description));
            }
        }

		/// <summary>
		/// Version in which the form is introduced.
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
		/// Information that specifies whether this component can be customized.
		/// </summary>
		[AttributeLogicalName("iscustomizable")]
        public BooleanManagedProperty? IsCustomizable
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<BooleanManagedProperty?>("iscustomizable");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsCustomizable));
                SetAttributeValue("iscustomizable", value);
                OnPropertyChanged(nameof(IsCustomizable));
            }
        }

		/// <summary>
		/// Information that specifies whether this component should be hidden.
		/// </summary>
		[AttributeLogicalName("ishidden")]
        public BooleanManagedProperty? IsHidden
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<BooleanManagedProperty?>("ishidden");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsHidden));
                SetAttributeValue("ishidden", value);
                OnPropertyChanged(nameof(IsHidden));
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
		/// Information about how the plugin assembly is to be isolated at execution time; None / Sandboxed.
		/// </summary>
		[AttributeLogicalName("isolationmode")]
        public OptionSetValue? IsolationMode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("isolationmode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsolationMode));
                SetAttributeValue("isolationmode", value);
                OnPropertyChanged(nameof(IsolationMode));
            }
        }

		
		[AttributeLogicalName("ispasswordset")]
        public bool? IsPasswordSet
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("ispasswordset");
            }
        }

		/// <summary>
		/// Major of the assembly version.
		/// </summary>
		[AttributeLogicalName("major")]
        public int? Major
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("major");
            }
        }

		/// <summary>
		/// Unique identifier for managedidentity associated with pluginassembly.
		/// </summary>
		[AttributeLogicalName("managedidentityid")]
        public EntityReference? ManagedIdentityId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("managedidentityid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ManagedIdentityId));
                SetAttributeValue("managedidentityid", value);
                OnPropertyChanged(nameof(ManagedIdentityId));
            }
        }

		/// <summary>
		/// Minor of the assembly version.
		/// </summary>
		[AttributeLogicalName("minor")]
        public int? Minor
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("minor");
            }
        }

		/// <summary>
		/// Unique identifier of the user who last modified the plug-in assembly.
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
		/// Date and time when the plug-in assembly was last modified.
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
		/// Unique identifier of the delegate user who last modified the pluginassembly.
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
		/// Name of the plug-in assembly.
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
		/// Unique identifier of the organization with which the plug-in assembly is associated.
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
		/// Unique identifier for Plugin Package associated with Plug-in Assembly.
		/// </summary>
		[AttributeLogicalName("packageid")]
        public EntityReference? PackageId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("packageid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PackageId));
                SetAttributeValue("packageid", value);
                OnPropertyChanged(nameof(PackageId));
            }
        }

		/// <summary>
		/// User Password
		/// </summary>
		[AttributeLogicalName("password")]
        public string? Password
        {
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Password));
                SetAttributeValue("password", value);
                OnPropertyChanged(nameof(Password));
            }
        }

		/// <summary>
		/// File name of the plug-in assembly. Used when the source type is set to 1.
		/// </summary>
		[AttributeLogicalName("path")]
        public string? Path
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("path");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Path));
                SetAttributeValue("path", value);
                OnPropertyChanged(nameof(Path));
            }
        }

		/// <summary>
		/// Unique identifier of the plug-in assembly.
		/// </summary>
		[AttributeLogicalName("pluginassemblyidunique")]
        public Guid? PluginAssemblyIdUnique
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("pluginassemblyidunique");
            }
        }

		/// <summary>
		/// Public key token of the assembly. This value can be obtained from the assembly by using reflection.
		/// </summary>
		[AttributeLogicalName("publickeytoken")]
        public string? PublicKeyToken
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("publickeytoken");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PublicKeyToken));
                SetAttributeValue("publickeytoken", value);
                OnPropertyChanged(nameof(PublicKeyToken));
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

		/// <summary>
		/// Hash of the source of the assembly.
		/// </summary>
		[AttributeLogicalName("sourcehash")]
        public string? SourceHash
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("sourcehash");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SourceHash));
                SetAttributeValue("sourcehash", value);
                OnPropertyChanged(nameof(SourceHash));
            }
        }

		/// <summary>
		/// Location of the assembly, for example 0=database, 1=on-disk.
		/// </summary>
		[AttributeLogicalName("sourcetype")]
        public OptionSetValue? SourceType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("sourcetype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SourceType));
                SetAttributeValue("sourcetype", value);
                OnPropertyChanged(nameof(SourceType));
            }
        }

		/// <summary>
		/// Web Url
		/// </summary>
		[AttributeLogicalName("url")]
        public string? Url
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("url");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Url));
                SetAttributeValue("url", value);
                OnPropertyChanged(nameof(Url));
            }
        }

		/// <summary>
		/// User Name
		/// </summary>
		[AttributeLogicalName("username")]
        public string? UserName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("username");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(UserName));
                SetAttributeValue("username", value);
                OnPropertyChanged(nameof(UserName));
            }
        }

		/// <summary>
		/// Version number of the assembly. The value can be obtained from the assembly through reflection.
		/// </summary>
		[AttributeLogicalName("version")]
        public string? Version
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("version");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Version));
                SetAttributeValue("version", value);
                OnPropertyChanged(nameof(Version));
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


		#endregion

		#region NavigationProperties
		/// <summary>
		/// 1:N pluginassembly_plugintype
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("pluginassembly_plugintype")]
		public System.Collections.Generic.IEnumerable<PluginType> PluginassemblyPlugintype
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<PluginType>("pluginassembly_plugintype", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("PluginassemblyPlugintype");
				this.SetRelatedEntities<PluginType>("pluginassembly_plugintype", null, value);
				this.OnPropertyChanged("PluginassemblyPlugintype");
			}
		}

		#endregion

		#region Options
		public static class Options
		{
			    public struct AuthType
                {
					public const int BasicAuth = 0;
                }
			    public struct ComponentState
                {
					public const int Published = 0;
					public const int Unpublished = 1;
					public const int Deleted = 2;
					public const int DeletedUnpublished = 3;
                }
                public struct IsManaged
                {
                    public const bool Unmanaged = false;
                    public const bool Managed = true;
                }
			    public struct IsolationMode
                {
					public const int None = 1;
					public const int Sandbox = 2;
					public const int External = 3;
                }
                public struct IsPasswordSet
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
			    public struct SourceType
                {
					public const int Database = 0;
					public const int Disk = 1;
					public const int Normal = 2;
					public const int AzureWebApp = 3;
					public const int FileStore = 4;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string PluginAssemblyId = "pluginassemblyid";
				public const string AuthType = "authtype";
				public const string ComponentState = "componentstate";
				public const string Content = "content";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string Culture = "culture";
				public const string CustomizationLevel = "customizationlevel";
				public const string Description = "description";
				public const string IntroducedVersion = "introducedversion";
				public const string IsCustomizable = "iscustomizable";
				public const string IsHidden = "ishidden";
				public const string IsManaged = "ismanaged";
				public const string IsolationMode = "isolationmode";
				public const string IsPasswordSet = "ispasswordset";
				public const string Major = "major";
				public const string ManagedIdentityId = "managedidentityid";
				public const string Minor = "minor";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string Name = "name";
				public const string OrganizationId = "organizationid";
				public const string OverwriteTime = "overwritetime";
				public const string PackageId = "packageid";
				public const string Password = "password";
				public const string Path = "path";
				public const string PluginAssemblyIdUnique = "pluginassemblyidunique";
				public const string PublicKeyToken = "publickeytoken";
				public const string SolutionId = "solutionid";
				public const string SourceHash = "sourcehash";
				public const string SourceType = "sourcetype";
				public const string Url = "url";
				public const string UserName = "username";
				public const string Version = "version";
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string PluginassemblyPlugintype = "pluginassembly_plugintype";
				public const string UserentityinstancedataPluginassembly = "userentityinstancedata_pluginassembly";
            }

            public static class ManyToOne
            {
				public const string CreatedbyPluginassembly = "createdby_pluginassembly";
				public const string LkPluginassemblyCreatedonbehalfby = "lk_pluginassembly_createdonbehalfby";
				public const string LkPluginassemblyModifiedonbehalfby = "lk_pluginassembly_modifiedonbehalfby";
				public const string ManagedidentityPluginAssembly = "managedidentity_PluginAssembly";
				public const string ModifiedbyPluginassembly = "modifiedby_pluginassembly";
				public const string OrganizationPluginassembly = "organization_pluginassembly";
				public const string PluginpackagePluginassembly = "pluginpackage_pluginassembly";
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
        public static PluginAssembly Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static PluginAssembly Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("pluginassembly", id, columnSet).ToEntity<PluginAssembly>();
        }

        public PluginAssembly GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  PluginAssembly(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<PluginAssembly> PluginAssemblySet
		{
			get
			{
				return CreateQuery<PluginAssembly>();
			}
		}
	}
	#endregion
}
