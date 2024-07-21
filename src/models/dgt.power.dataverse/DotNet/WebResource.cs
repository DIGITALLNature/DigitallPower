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
	/// Data equivalent to files used in Web development. Web resources provide client-side components that are used to provide custom user interface elements.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("webresource")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class WebResource : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public WebResource() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public WebResource(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public WebResource(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public WebResource(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public WebResource(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "webresource";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 9333;
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
		[AttributeLogicalNameAttribute("webresourceid")]
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
				WebResourceId = value;
			}
		}

		/// <summary>
		/// Unique identifier of the web resource.
		/// </summary>
		[AttributeLogicalName("webresourceid")]
        public Guid? WebResourceId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("webresourceid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(WebResourceId));
                SetAttributeValue("webresourceid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(WebResourceId));
            }
        }

		/// <summary>
		/// Information that specifies whether this component can be deleted.
		/// </summary>
		[AttributeLogicalName("canbedeleted")]
        public BooleanManagedProperty? CanBeDeleted
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<BooleanManagedProperty?>("canbedeleted");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(CanBeDeleted));
                SetAttributeValue("canbedeleted", value);
                OnPropertyChanged(nameof(CanBeDeleted));
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
		/// Bytes of the web resource, in Base64 format.
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
		/// Reference to the content file on Azure.
		/// </summary>
		[AttributeLogicalName("contentfileref")]
        public Guid? ContentFileRef
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("contentfileref");
            }
        }

		
		[AttributeLogicalName("contentfileref_name")]
        public string? ContentFileRefName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("contentfileref_name");
            }
        }

		/// <summary>
		/// Json representation of the content of the resource.
		/// </summary>
		[AttributeLogicalName("contentjson")]
        public string? ContentJson
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("contentjson");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ContentJson));
                SetAttributeValue("contentjson", value);
                OnPropertyChanged(nameof(ContentJson));
            }
        }

		/// <summary>
		/// Reference to the Json content file on Azure.
		/// </summary>
		[AttributeLogicalName("contentjsonfileref")]
        public Guid? ContentJsonFileRef
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("contentjsonfileref");
            }
        }

		
		[AttributeLogicalName("contentjsonfileref_name")]
        public string? ContentJsonFileRefName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("contentjsonfileref_name");
            }
        }

		/// <summary>
		/// Unique identifier of the user who created the web resource.
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
		/// Date and time when the web resource was created.
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
		/// Unique identifier of the delegate user who created the web resource.
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
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("dependencyxml")]
        public string? DependencyXml
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dependencyxml");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DependencyXml));
                SetAttributeValue("dependencyxml", value);
                OnPropertyChanged(nameof(DependencyXml));
            }
        }

		/// <summary>
		/// Description of the web resource.
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
		/// Display name of the web resource.
		/// </summary>
		[AttributeLogicalName("displayname")]
        public string? DisplayName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("displayname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DisplayName));
                SetAttributeValue("displayname", value);
                OnPropertyChanged(nameof(DisplayName));
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
		/// Information that specifies whether this web resource is available for mobile client in offline mode.
		/// </summary>
		[AttributeLogicalName("isavailableformobileoffline")]
        public bool? IsAvailableForMobileOffline
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isavailableformobileoffline");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsAvailableForMobileOffline));
                SetAttributeValue("isavailableformobileoffline", value);
                OnPropertyChanged(nameof(IsAvailableForMobileOffline));
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
		/// Information that specifies whether this web resource is enabled for mobile client.
		/// </summary>
		[AttributeLogicalName("isenabledformobileclient")]
        public bool? IsEnabledForMobileClient
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isenabledformobileclient");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsEnabledForMobileClient));
                SetAttributeValue("isenabledformobileclient", value);
                OnPropertyChanged(nameof(IsEnabledForMobileClient));
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
		/// Language of the web resource.
		/// </summary>
		[AttributeLogicalName("languagecode")]
        public int? LanguageCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("languagecode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(LanguageCode));
                SetAttributeValue("languagecode", value);
                OnPropertyChanged(nameof(LanguageCode));
            }
        }

		/// <summary>
		/// Unique identifier of the user who last modified the web resource.
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
		/// Date and time when the web resource was last modified.
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
		/// Unique identifier of the delegate user who modified the web resource.
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
		/// Name of the web resource.
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
		/// Unique identifier of the organization associated with the web resource.
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
		/// Silverlight runtime version number required by a silverlight web resource.
		/// </summary>
		[AttributeLogicalName("silverlightversion")]
        public string? SilverlightVersion
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("silverlightversion");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SilverlightVersion));
                SetAttributeValue("silverlightversion", value);
                OnPropertyChanged(nameof(SilverlightVersion));
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
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("webresourceidunique")]
        public Guid? WebResourceIdUnique
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("webresourceidunique");
            }
        }

		/// <summary>
		/// Drop-down list for selecting the type of the web resource.
		/// </summary>
		[AttributeLogicalName("webresourcetype")]
        public OptionSetValue? WebResourceType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("webresourcetype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(WebResourceType));
                SetAttributeValue("webresourcetype", value);
                OnPropertyChanged(nameof(WebResourceType));
            }
        }


		#endregion

		#region NavigationProperties
		/// <summary>
		/// 1:N solution_configuration_webresource
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("solution_configuration_webresource")]
		public System.Collections.Generic.IEnumerable<Solution> SolutionConfigurationWebresource
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Solution>("solution_configuration_webresource", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SolutionConfigurationWebresource");
				this.SetRelatedEntities<Solution>("solution_configuration_webresource", null, value);
				this.OnPropertyChanged("SolutionConfigurationWebresource");
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
                public struct IsAvailableForMobileOffline
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct IsEnabledForMobileClient
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct IsManaged
                {
                    public const bool Unmanaged = false;
                    public const bool Managed = true;
                }
			    public struct WebResourceType
                {
					public const int Webpage_HTML_ = 1;
					public const int StyleSheet_CSS_ = 2;
					public const int Script_JScript_ = 3;
					public const int Data_XML_ = 4;
					public const int PNGFormat = 5;
					public const int JPGFormat = 6;
					public const int GIFFormat = 7;
					public const int Silverlight_XAP_ = 8;
					public const int StyleSheet_XSL_ = 9;
					public const int ICOFormat = 10;
					public const int VectorFormat_SVG_ = 11;
					public const int String_RESX_ = 12;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string WebResourceId = "webresourceid";
				public const string CanBeDeleted = "canbedeleted";
				public const string ComponentState = "componentstate";
				public const string Content = "content";
				public const string ContentFileRef = "contentfileref";
				public const string ContentFileRefName = "contentfileref_name";
				public const string ContentJson = "contentjson";
				public const string ContentJsonFileRef = "contentjsonfileref";
				public const string ContentJsonFileRefName = "contentjsonfileref_name";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string DependencyXml = "dependencyxml";
				public const string Description = "description";
				public const string DisplayName = "displayname";
				public const string IntroducedVersion = "introducedversion";
				public const string IsAvailableForMobileOffline = "isavailableformobileoffline";
				public const string IsCustomizable = "iscustomizable";
				public const string IsEnabledForMobileClient = "isenabledformobileclient";
				public const string IsHidden = "ishidden";
				public const string IsManaged = "ismanaged";
				public const string LanguageCode = "languagecode";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string Name = "name";
				public const string OrganizationId = "organizationid";
				public const string OverwriteTime = "overwritetime";
				public const string SilverlightVersion = "silverlightversion";
				public const string SolutionId = "solutionid";
				public const string VersionNumber = "versionnumber";
				public const string WebResourceIdUnique = "webresourceidunique";
				public const string WebResourceType = "webresourcetype";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string LkThemeLogoid = "lk_theme_logoid";
				public const string MsdynWebresourceMsdynToolconfigIcon = "msdyn_webresource_msdyn_toolconfig_icon";
				public const string MsdynWebresourceSessiontemplateSessionicon = "msdyn_webresource_sessiontemplate_sessionicon";
				public const string SolutionConfigurationWebresource = "solution_configuration_webresource";
				public const string UserentityinstancedataWebresource = "userentityinstancedata_webresource";
				public const string WebresourceAppactionIconwebresourceid = "webresource_appaction_iconwebresourceid";
				public const string WebresourceAppactionOnclickeventjavascriptwebresourceid = "webresource_appaction_onclickeventjavascriptwebresourceid";
				public const string WebresourceFileAttachments = "webresource_FileAttachments";
				public const string WebresourceSavedqueryvisualizations = "webresource_savedqueryvisualizations";
				public const string WebresourceUserqueryvisualizations = "webresource_userqueryvisualizations";
            }

            public static class ManyToOne
            {
				public const string FileAttachmentWebResourceContentFileRef = "FileAttachment_WebResource_ContentFileRef";
				public const string FileAttachmentWebResourceContentJsonFileRef = "FileAttachment_WebResource_ContentJsonFileRef";
				public const string LkWebresourcebaseCreatedonbehalfby = "lk_webresourcebase_createdonbehalfby";
				public const string LkWebresourcebaseModifiedonbehalfby = "lk_webresourcebase_modifiedonbehalfby";
				public const string WebresourceCreatedby = "webresource_createdby";
				public const string WebresourceModifiedby = "webresource_modifiedby";
				public const string WebresourceOrganization = "webresource_organization";
            }

            public static class ManyToMany
            {
				public const string AppactionruleWebresourceScripts = "appactionrule_webresource_scripts";
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
        public static WebResource Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static WebResource Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("webresource", id, columnSet).ToEntity<WebResource>();
        }

        public WebResource GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  WebResource(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<WebResource> WebResourceSet
		{
			get
			{
				return CreateQuery<WebResource>();
			}
		}
	}
	#endregion
}
