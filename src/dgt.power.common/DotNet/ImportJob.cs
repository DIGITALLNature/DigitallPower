using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// For internal use only.
	/// </summary>
    [EntityLogicalName("importjob")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class ImportJob : Entity
    {
        #region ctor
        public ImportJob() : base(EntityLogicalName) { }

        public ImportJob(Guid id) : base(EntityLogicalName, id) { }

        public ImportJob(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public ImportJob(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "importjob";
        public const int EntityTypeCode = 9107;
        #endregion

        #region Attributes
        [AttributeLogicalName("importjobid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                ImportJobId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the import job.
		/// </summary>
        [AttributeLogicalName("importjobid")]
        public Guid? ImportJobId
        {
            get
            {
                return GetAttributeValue<Guid?>("importjobid");
            }
            set
            {
                SetAttributeValue("importjobid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Date and time when the import job was completed.
		/// </summary>
        [AttributeLogicalName("completedon")]
        public DateTime? CompletedOn
        {
            get
            {
                return GetAttributeValue<DateTime?>("completedon");
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the importJob.
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
		/// Date and time when the import job record was created.
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
		/// Unique identifier of the delegate user who created the import job record.
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
		/// Unstructured data associated with the import job.
		/// </summary>
        [AttributeLogicalName("data")]
        public string? Data
        {
            get
            {
                return GetAttributeValue<string?>("data");
            }
            set
            {
                SetAttributeValue("data", value);
            }
        }

        /// <summary>
		/// The context of the import
		/// </summary>
        [AttributeLogicalName("importcontext")]
        public string? ImportContext
        {
            get
            {
                return GetAttributeValue<string?>("importcontext");
            }
            set
            {
                SetAttributeValue("importcontext", value);
            }
        }

        /// <summary>
		/// Unique identifier of the user who modified the importJob.
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
		/// Date and time when the import job was last modified.
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
		/// Unique identifier of the delegate user who modified the import job record.
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
		/// Name of the import job.
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
		/// The context of the solution operation
		/// </summary>
        [AttributeLogicalName("operationcontext")]
        public string? OperationContext
        {
            get
            {
                return GetAttributeValue<string?>("operationcontext");
            }
            set
            {
                SetAttributeValue("operationcontext", value);
            }
        }

        /// <summary>
		/// Unique identifier of the organization associated with the importjob.
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
		/// Import Progress Percentage.
		/// </summary>
        [AttributeLogicalName("progress")]
        public double? Progress
        {
            get
            {
                return GetAttributeValue<double?>("progress");
            }
            set
            {
                SetAttributeValue("progress", value);
            }
        }

        /// <summary>
		/// Unique identifier of the associated solution.
		/// </summary>
        [AttributeLogicalName("solutionid")]
        public Guid? SolutionId
        {
            get
            {
                return GetAttributeValue<Guid?>("solutionid");
            }
        }

        /// <summary>
		/// Unique identifier of the solution.
		/// </summary>
        [AttributeLogicalName("solutionname")]
        public string? SolutionName
        {
            get
            {
                return GetAttributeValue<string?>("solutionname");
            }
            set
            {
                SetAttributeValue("solutionname", value);
            }
        }

        /// <summary>
		/// Date and time when the import job was started.
		/// </summary>
        [AttributeLogicalName("startedon")]
        public DateTime? StartedOn
        {
            get
            {
                return GetAttributeValue<DateTime?>("startedon");
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("timezoneruleversionnumber")]
        public int? TimeZoneRuleVersionNumber
        {
            get
            {
                return GetAttributeValue<int?>("timezoneruleversionnumber");
            }
            set
            {
                SetAttributeValue("timezoneruleversionnumber", value);
            }
        }

        /// <summary>
		/// Time zone code that was in use when the record was created.
		/// </summary>
        [AttributeLogicalName("utcconversiontimezonecode")]
        public int? UTCConversionTimeZoneCode
        {
            get
            {
                return GetAttributeValue<int?>("utcconversiontimezonecode");
            }
            set
            {
                SetAttributeValue("utcconversiontimezonecode", value);
            }
        }
        #endregion

        #region NavigationProperties
        #endregion

        #region Options
        public static partial class Options
        {
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string ImportJobId = "importjobid";
            public const string CompletedOn = "completedon";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string Data = "data";
            public const string ImportContext = "importcontext";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OperationContext = "operationcontext";
            public const string OrganizationId = "organizationid";
            public const string Progress = "progress";
            public const string SolutionId = "solutionid";
            public const string SolutionName = "solutionname";
            public const string StartedOn = "startedon";
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string UserentityinstancedataImportjob = "userentityinstancedata_importjob";
            }

            public static partial class ManyToOne
            {
                public const string LkImportjobbaseCreatedby = "lk_importjobbase_createdby";
                public const string LkImportjobbaseCreatedonbehalfby = "lk_importjobbase_createdonbehalfby";
                public const string LkImportjobbaseModifiedby = "lk_importjobbase_modifiedby";
                public const string LkImportjobbaseModifiedonbehalfby = "lk_importjobbase_modifiedonbehalfby";
                public const string OrganizationImportjob = "organization_importjob";
            }

            public static partial class ManyToMany
            {
            }
        }
        #endregion

        #region Methods
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<ImportJob> ImportJobSet
        {
            get
            {
                return CreateQuery<ImportJob>();
            }
        }
    }
    #endregion
}
