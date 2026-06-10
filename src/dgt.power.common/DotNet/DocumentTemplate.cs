using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using AttributeCollection = Microsoft.Xrm.Sdk.AttributeCollection;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Used to store Document Templates in database in binary format.
	/// </summary>
    [DataContract]
    [EntityLogicalName("documenttemplate")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
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
        public DocumentTemplate(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public DocumentTemplate(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
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
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "documenttemplate";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 9940;
        #endregion

        #region Events
        #pragma warning disable CS8612
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;
        #pragma warning restore CS8612
        [DebuggerNonUserCode]
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        [AttributeLogicalName("documenttemplateid")]
        public new Guid Id
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
                OnPropertyChanging();
                SetAttributeValue("documenttemplateid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("associatedentitytypecode", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("clientdata", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("content", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("description", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("documenttype", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("languagecode", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("name", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("status", value);
                OnPropertyChanged();
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
        public static partial class Options
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
        public static partial class LogicalNames
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
        public static partial class Relations
        {
            public static class OneToMany
            {
            }

            public static partial class ManyToOne
            {
                public const string LkDocumenttemplatebaseCreatedby = "lk_documenttemplatebase_createdby";
                public const string LkDocumenttemplatebaseCreatedonbehalfby = "lk_documenttemplatebase_createdonbehalfby";
                public const string LkDocumenttemplatebaseModifiedby = "lk_documenttemplatebase_modifiedby";
                public const string LkDocumenttemplatebaseModifiedonbehalfby = "lk_documenttemplatebase_modifiedonbehalfby";
                public const string LkDocumenttemplatebaseOrganization = "lk_documenttemplatebase_organization";
            }

            public static partial class ManyToMany
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

        public static DocumentTemplate Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static DocumentTemplate Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("documenttemplate", id, columnSet).ToEntity<DocumentTemplate>();
        }

        public DocumentTemplate GetChangedEntity()
        {
            if (!_trackChanges)
            {
                return this;
            }
                var attr = new AttributeCollection();
            foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof(AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
            {
                attr.Add(attrName, this[attrName]);
            }
            return new DocumentTemplate(Id) { Attributes = attr };
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
