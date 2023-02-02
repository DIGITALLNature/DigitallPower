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
	/// Used to store Document Templates in database in binary format.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("documenttemplate")]
	[System.CodeDom.Compiler.GeneratedCode("ec4u.automation", "1.0.0")]
	public partial class DocumentTemplate : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public DocumentTemplate() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public DocumentTemplate(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DocumentTemplate(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DocumentTemplate(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DocumentTemplate(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "documenttemplate";
        public const int EntityTypeCode = 9940;
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
		[AttributeLogicalNameAttribute("documenttemplateid")]
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
				DocumentTemplateId = value;
			}
		}

		/// <summary>
		/// Unique identifier of the document template.
		/// </summary>
		[AttributeLogicalName("documenttemplateid")]
        public Guid? DocumentTemplateId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("documenttemplateid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DocumentTemplateId));
                SetAttributeValue("documenttemplateid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(DocumentTemplateId));
            }
        }

		/// <summary>
		/// Associated Entity Type Code.
		/// </summary>
		[AttributeLogicalName("associatedentitytypecode")]
        public string? AssociatedEntityTypeCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("associatedentitytypecode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AssociatedEntityTypeCode));
                SetAttributeValue("associatedentitytypecode", value);
                OnPropertyChanged(nameof(AssociatedEntityTypeCode));
            }
        }

		/// <summary>
		/// Client data regarding this document template.
		/// </summary>
		[AttributeLogicalName("clientdata")]
        public string? ClientData
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("clientdata");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ClientData));
                SetAttributeValue("clientdata", value);
                OnPropertyChanged(nameof(ClientData));
            }
        }

		/// <summary>
		/// Bytes of the document template.
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
		/// Unique identifier of the user who created the document template.
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
		/// Date and time when the document template was created.
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
		/// Unique identifier of the delegate user who created the document template.
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
		/// Additional information to describe the Document Template
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
		/// Option set for selecting the type of the document template
		/// </summary>
		[AttributeLogicalName("documenttype")]
        public OptionSetValue? DocumentType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("documenttype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DocumentType));
                SetAttributeValue("documenttype", value);
                OnPropertyChanged(nameof(DocumentType));
            }
        }

		/// <summary>
		/// Language of Document Template.
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
		/// Unique identifier of the user who last modified the document template.
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
		/// Date and time when the document template was last modified.
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
		/// Unique identifier of the delegate user who modified the document template.
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
		/// Name of the document template.
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
		/// Information about whether the document template is active.
		/// </summary>
		[AttributeLogicalName("status")]
        public bool? Status
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("status");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Status));
                SetAttributeValue("status", value);
                OnPropertyChanged(nameof(Status));
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
		#endregion

		#region Options
		public static class Options
		{
			    public struct DocumentType
                {
					public const int MicrosoftExcel = 1;
					public const int MicrosoftWord = 2;
                }
                public struct Status
                {
                    public const bool Activated = false;
                    public const bool Draft = true;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string DocumentTemplateId = "documenttemplateid";
				public const string AssociatedEntityTypeCode = "associatedentitytypecode";
				public const string ClientData = "clientdata";
				public const string Content = "content";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string Description = "description";
				public const string DocumentType = "documenttype";
				public const string LanguageCode = "languagecode";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string Name = "name";
				public const string OrganizationId = "organizationid";
				public const string Status = "status";
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
            }

            public static class ManyToOne
            {
				public const string LkDocumenttemplatebaseCreatedby = "lk_documenttemplatebase_createdby";
				public const string LkDocumenttemplatebaseCreatedonbehalfby = "lk_documenttemplatebase_createdonbehalfby";
				public const string LkDocumenttemplatebaseModifiedby = "lk_documenttemplatebase_modifiedby";
				public const string LkDocumenttemplatebaseModifiedonbehalfby = "lk_documenttemplatebase_modifiedonbehalfby";
				public const string LkDocumenttemplatebaseOrganization = "lk_documenttemplatebase_organization";
            }

            public static class ManyToMany
            {
            }
        }

        #endregion

		#region Methods

        public static DocumentTemplate Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static DocumentTemplate Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("documenttemplate", id, columnSet).ToEntity<DocumentTemplate>();
        }

        public DocumentTemplate GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  DocumentTemplate(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<DocumentTemplate> DocumentTemplateSet
		{
			get
			{
				return CreateQuery<DocumentTemplate>();
			}
		}
	}
	#endregion
}
