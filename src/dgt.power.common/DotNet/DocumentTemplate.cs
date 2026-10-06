using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Used to store Document Templates in database in binary format.
	/// </summary>
    [EntityLogicalName("documenttemplate")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class DocumentTemplate : Entity
    {
        #region ctor
        public DocumentTemplate() : base(EntityLogicalName) { }

        public DocumentTemplate(Guid id) : base(EntityLogicalName, id) { }

        public DocumentTemplate(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public DocumentTemplate(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "documenttemplate";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 9940;
        #endregion

        #region Attributes
        [AttributeLogicalName("documenttemplateid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
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
            get
            {
                return GetAttributeValue<Guid?>("documenttemplateid");
            }
            set
            {
                SetAttributeValue("documenttemplateid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Associated Entity Type Code.
		/// </summary>
        [AttributeLogicalName("associatedentitytypecode")]
        public string? AssociatedEntityTypeCode
        {
            get
            {
                return GetAttributeValue<string?>("associatedentitytypecode");
            }
            set
            {
                SetAttributeValue("associatedentitytypecode", value);
            }
        }

        /// <summary>
		/// Client data regarding this document template.
		/// </summary>
        [AttributeLogicalName("clientdata")]
        public string? ClientData
        {
            get
            {
                return GetAttributeValue<string?>("clientdata");
            }
            set
            {
                SetAttributeValue("clientdata", value);
            }
        }

        /// <summary>
		/// Bytes of the document template.
		/// </summary>
        [AttributeLogicalName("content")]
        public string? Content
        {
            get
            {
                return GetAttributeValue<string?>("content");
            }
            set
            {
                SetAttributeValue("content", value);
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the document template.
		/// </summary>
        [AttributeLogicalName("createdby")]
        public EntityReference? CreatedBy
        {
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
            get
            {
                return GetAttributeValue<string?>("description");
            }
            set
            {
                SetAttributeValue("description", value);
            }
        }

        /// <summary>
		/// Option set for selecting the type of the document template
		/// </summary>
        [AttributeLogicalName("documenttype")]
        public OptionSetValue? DocumentType
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("documenttype");
            }
            set
            {
                SetAttributeValue("documenttype", value);
            }
        }

        /// <summary>
		/// Language of Document Template.
		/// </summary>
        [AttributeLogicalName("languagecode")]
        public int? LanguageCode
        {
            get
            {
                return GetAttributeValue<int?>("languagecode");
            }
            set
            {
                SetAttributeValue("languagecode", value);
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the document template.
		/// </summary>
        [AttributeLogicalName("modifiedby")]
        public EntityReference? ModifiedBy
        {
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
            get
            {
                return GetAttributeValue<string?>("name");
            }
            set
            {
                SetAttributeValue("name", value);
            }
        }

        /// <summary>
		/// Unique identifier of the organization associated with the web resource.
		/// </summary>
        [AttributeLogicalName("organizationid")]
        public EntityReference? OrganizationId
        {
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
            get
            {
                return GetAttributeValue<bool?>("status");
            }
            set
            {
                SetAttributeValue("status", value);
            }
        }

        
        [AttributeLogicalName("versionnumber")]
        public long? VersionNumber
        {
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
                public const string DocumenttemplateDeletedItemReferences = "documenttemplate_DeletedItemReferences";
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
