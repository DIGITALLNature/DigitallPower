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
	/// Copy of an entity's attributes before or after the core system operation.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("sdkmessageprocessingstepimage")]
	[System.CodeDom.Compiler.GeneratedCode("ec4u.automation", "1.0.0")]
	public partial class SdkMessageProcessingStepImage : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public SdkMessageProcessingStepImage() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public SdkMessageProcessingStepImage(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SdkMessageProcessingStepImage(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SdkMessageProcessingStepImage(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SdkMessageProcessingStepImage(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "sdkmessageprocessingstepimage";
        public const int EntityTypeCode = 4615;
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
		[AttributeLogicalNameAttribute("sdkmessageprocessingstepimageid")]
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
				SdkMessageProcessingStepImageId = value;
			}
		}

		/// <summary>
		/// Unique identifier of the SDK message processing step image entity.
		/// </summary>
		[AttributeLogicalName("sdkmessageprocessingstepimageid")]
        public Guid? SdkMessageProcessingStepImageId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("sdkmessageprocessingstepimageid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SdkMessageProcessingStepImageId));
                SetAttributeValue("sdkmessageprocessingstepimageid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(SdkMessageProcessingStepImageId));
            }
        }

		/// <summary>
		/// Comma-separated list of attributes that are to be passed into the SDK message processing step image.
		/// </summary>
		[AttributeLogicalName("attributes")]
        public string? AttributesField
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("attributes");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AttributesField));
                SetAttributeValue("attributes", value);
                OnPropertyChanged(nameof(AttributesField));
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
		/// Unique identifier of the user who created the SDK message processing step image.
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
		/// Date and time when the SDK message processing step image was created.
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
		/// Unique identifier of the delegate user who created the sdkmessageprocessingstepimage.
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
		/// Customization level of the SDK message processing step image.
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
		/// Description of the SDK message processing step image.
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
		/// Key name used to access the pre-image or post-image property bags in a step.
		/// </summary>
		[AttributeLogicalName("entityalias")]
        public string? EntityAlias
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("entityalias");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(EntityAlias));
                SetAttributeValue("entityalias", value);
                OnPropertyChanged(nameof(EntityAlias));
            }
        }

		/// <summary>
		/// Type of image requested.
		/// </summary>
		[AttributeLogicalName("imagetype")]
        public OptionSetValue? ImageType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("imagetype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ImageType));
                SetAttributeValue("imagetype", value);
                OnPropertyChanged(nameof(ImageType));
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
		/// Name of the property on the Request message.
		/// </summary>
		[AttributeLogicalName("messagepropertyname")]
        public string? MessagePropertyName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("messagepropertyname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MessagePropertyName));
                SetAttributeValue("messagepropertyname", value);
                OnPropertyChanged(nameof(MessagePropertyName));
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
		/// Unique identifier of the delegate user who last modified the sdkmessageprocessingstepimage.
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
		/// Name of SdkMessage processing step image.
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
		/// Name of the related entity.
		/// </summary>
		[AttributeLogicalName("relatedattributename")]
        public string? RelatedAttributeName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("relatedattributename");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(RelatedAttributeName));
                SetAttributeValue("relatedattributename", value);
                OnPropertyChanged(nameof(RelatedAttributeName));
            }
        }

		/// <summary>
		/// Unique identifier of the SDK message processing step.
		/// </summary>
		[AttributeLogicalName("sdkmessageprocessingstepid")]
        public EntityReference? SdkMessageProcessingStepId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("sdkmessageprocessingstepid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SdkMessageProcessingStepId));
                SetAttributeValue("sdkmessageprocessingstepid", value);
                OnPropertyChanged(nameof(SdkMessageProcessingStepId));
            }
        }

		/// <summary>
		/// Unique identifier of the SDK message processing step image.
		/// </summary>
		[AttributeLogicalName("sdkmessageprocessingstepimageidunique")]
        public Guid? SdkMessageProcessingStepImageIdUnique
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("sdkmessageprocessingstepimageidunique");
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
		/// Number that identifies a specific revision of the step image.
		/// </summary>
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
			    public struct ImageType
                {
					public const int PreImage = 0;
					public const int PostImage = 1;
					public const int Both = 2;
                }
                public struct IsManaged
                {
                    public const bool Unmanaged = false;
                    public const bool Managed = true;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string SdkMessageProcessingStepImageId = "sdkmessageprocessingstepimageid";
				public const string Attributes = "attributes";
				public const string ComponentState = "componentstate";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string CustomizationLevel = "customizationlevel";
				public const string Description = "description";
				public const string EntityAlias = "entityalias";
				public const string ImageType = "imagetype";
				public const string IntroducedVersion = "introducedversion";
				public const string IsCustomizable = "iscustomizable";
				public const string IsManaged = "ismanaged";
				public const string MessagePropertyName = "messagepropertyname";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string Name = "name";
				public const string OrganizationId = "organizationid";
				public const string OverwriteTime = "overwritetime";
				public const string RelatedAttributeName = "relatedattributename";
				public const string SdkMessageProcessingStepId = "sdkmessageprocessingstepid";
				public const string SdkMessageProcessingStepImageIdUnique = "sdkmessageprocessingstepimageidunique";
				public const string SolutionId = "solutionid";
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string UserentityinstancedataSdkmessageprocessingstepimage = "userentityinstancedata_sdkmessageprocessingstepimage";
            }

            public static class ManyToOne
            {
				public const string CreatedbySdkmessageprocessingstepimage = "createdby_sdkmessageprocessingstepimage";
				public const string LkSdkmessageprocessingstepimageCreatedonbehalfby = "lk_sdkmessageprocessingstepimage_createdonbehalfby";
				public const string LkSdkmessageprocessingstepimageModifiedonbehalfby = "lk_sdkmessageprocessingstepimage_modifiedonbehalfby";
				public const string ModifiedbySdkmessageprocessingstepimage = "modifiedby_sdkmessageprocessingstepimage";
				public const string OrganizationSdkmessageprocessingstepimage = "organization_sdkmessageprocessingstepimage";
				public const string SdkmessageprocessingstepidSdkmessageprocessingstepimage = "sdkmessageprocessingstepid_sdkmessageprocessingstepimage";
            }

            public static class ManyToMany
            {
            }
        }

        #endregion

		#region Methods

        public static SdkMessageProcessingStepImage Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static SdkMessageProcessingStepImage Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("sdkmessageprocessingstepimage", id, columnSet).ToEntity<SdkMessageProcessingStepImage>();
        }

        public SdkMessageProcessingStepImage GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  SdkMessageProcessingStepImage(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<SdkMessageProcessingStepImage> SdkMessageProcessingStepImageSet
		{
			get
			{
				return CreateQuery<SdkMessageProcessingStepImage>();
			}
		}
	}
	#endregion
}
