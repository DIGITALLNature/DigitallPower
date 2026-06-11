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
	/// For internal use only.
	/// </summary>
    [DataContract]
    [EntityLogicalName("importjob")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class ImportJob : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public ImportJob() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public ImportJob(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public ImportJob(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public ImportJob(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public ImportJob(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "importjob";
        public const int EntityTypeCode = 9107;
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
        [AttributeLogicalName("importjobid")]
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
                ImportJobId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the import job.
		/// </summary>
        [AttributeLogicalName("importjobid")]
        public Guid? ImportJobId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("importjobid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("importjobid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Date and time when the import job was completed.
		/// </summary>
        [AttributeLogicalName("completedon")]
        public DateTime? CompletedOn
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("data");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("data", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The context of the import
		/// </summary>
        [AttributeLogicalName("importcontext")]
        public string? ImportContext
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("importcontext");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("importcontext", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who modified the importJob.
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
		/// Date and time when the import job was last modified.
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
		/// Unique identifier of the delegate user who modified the import job record.
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
		/// Name of the import job.
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
		/// The context of the solution operation
		/// </summary>
        [AttributeLogicalName("operationcontext")]
        public string? OperationContext
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("operationcontext");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("operationcontext", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the organization associated with the importjob.
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
		/// Import Progress Percentage.
		/// </summary>
        [AttributeLogicalName("progress")]
        public double? Progress
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<double?>("progress");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("progress", value);
                OnPropertyChanged();
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
		/// Unique identifier of the solution.
		/// </summary>
        [AttributeLogicalName("solutionname")]
        public string? SolutionName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("solutionname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("solutionname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Date and time when the import job was started.
		/// </summary>
        [AttributeLogicalName("startedon")]
        public DateTime? StartedOn
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("timezoneruleversionnumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("timezoneruleversionnumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Time zone code that was in use when the record was created.
		/// </summary>
        [AttributeLogicalName("utcconversiontimezonecode")]
        public int? UTCConversionTimeZoneCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("utcconversiontimezonecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("utcconversiontimezonecode", value);
                OnPropertyChanged();
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

        public static ImportJob Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static ImportJob Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("importjob", id, columnSet).ToEntity<ImportJob>();
        }

        public ImportJob GetChangedEntity()
        {
            if (!_trackChanges)
            {
                return this;
            }

            var attr = new AttributeCollection();
            foreach (var attrName in _changedProperties.Value.Select(changedProperty => GetType().GetProperty(changedProperty)!.GetCustomAttribute<AttributeLogicalNameAttribute>()!.LogicalName).Where(attrName => Contains(attrName)))
            {
                attr.Add(attrName, this[attrName]);
            }
            return new ImportJob(Id) { Attributes = attr };
        }
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
