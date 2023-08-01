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

	[DataContractAttribute()]
	[EntityLogicalNameAttribute("dgt_carrier")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class DgtCarrier : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public DgtCarrier() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public DgtCarrier(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtCarrier(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtCarrier(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtCarrier(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "dgt_carrier";
        public const string PrimaryNameAttribute = "dgt_reference";
        public const int EntityTypeCode = 10394;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

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
		[AttributeLogicalNameAttribute("dgt_carrierid")]
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
				DgtCarrierId = value;
			}
		}

		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[AttributeLogicalName("dgt_carrierid")]
        public Guid? DgtCarrierId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("dgt_carrierid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtCarrierId));
                SetAttributeValue("dgt_carrierid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(DgtCarrierId));
            }
        }

		/// <summary>
		/// Unique identifier of the user who created the record.
		/// </summary>
		[AttributeLogicalName("createdby")]
        public EntityReference CreatedBy
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference>("createdby");
            }
        }

		/// <summary>
		/// Date and time when the record was created.
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
		/// Unique identifier of the delegate user who created the record.
		/// </summary>
		[AttributeLogicalName("createdonbehalfby")]
        public EntityReference CreatedOnBehalfBy
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference>("createdonbehalfby");
            }
        }


		[AttributeLogicalName("dgt_constraint_mset")]
        public Microsoft.Xrm.Sdk.OptionSetValueCollection DgtConstraintMset
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValueCollection>("dgt_constraint_mset");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtConstraintMset));
                SetAttributeValue("dgt_constraint_mset", value);
                OnPropertyChanged(nameof(DgtConstraintMset));
            }
        }


		[AttributeLogicalName("dgt_handshake_ts")]
        public DateTime? DgtHandshakeTs
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("dgt_handshake_ts");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtHandshakeTs));
                SetAttributeValue("dgt_handshake_ts", value);
                OnPropertyChanged(nameof(DgtHandshakeTs));
            }
        }


		[AttributeLogicalName("dgt_locked_opt")]
        public bool? DgtLockedOpt
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("dgt_locked_opt");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtLockedOpt));
                SetAttributeValue("dgt_locked_opt", value);
                OnPropertyChanged(nameof(DgtLockedOpt));
            }
        }

		/// <summary>
		/// The Carrier Reference
		/// </summary>
		[AttributeLogicalName("dgt_reference")]
        public string DgtReference
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string>("dgt_reference");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtReference));
                SetAttributeValue("dgt_reference", value);
                OnPropertyChanged(nameof(DgtReference));
            }
        }


		[AttributeLogicalName("dgt_solutionfriendlyname")]
        public string DgtSolutionfriendlyname
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string>("dgt_solutionfriendlyname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtSolutionfriendlyname));
                SetAttributeValue("dgt_solutionfriendlyname", value);
                OnPropertyChanged(nameof(DgtSolutionfriendlyname));
            }
        }


		[AttributeLogicalName("dgt_solutionid")]
        public string DgtSolutionid
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string>("dgt_solutionid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtSolutionid));
                SetAttributeValue("dgt_solutionid", value);
                OnPropertyChanged(nameof(DgtSolutionid));
            }
        }


		[AttributeLogicalName("dgt_solutionuniquename")]
        public string DgtSolutionuniquename
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string>("dgt_solutionuniquename");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtSolutionuniquename));
                SetAttributeValue("dgt_solutionuniquename", value);
                OnPropertyChanged(nameof(DgtSolutionuniquename));
            }
        }


		[AttributeLogicalName("dgt_solutionversion")]
        public string DgtSolutionversion
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string>("dgt_solutionversion");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtSolutionversion));
                SetAttributeValue("dgt_solutionversion", value);
                OnPropertyChanged(nameof(DgtSolutionversion));
            }
        }


		[AttributeLogicalName("dgt_transport_order_no")]
        public int? DgtTransportOrderNo
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("dgt_transport_order_no");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtTransportOrderNo));
                SetAttributeValue("dgt_transport_order_no", value);
                OnPropertyChanged(nameof(DgtTransportOrderNo));
            }
        }


		[AttributeLogicalName("dgt_workbench_id")]
        public EntityReference DgtWorkbenchId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference>("dgt_workbench_id");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtWorkbenchId));
                SetAttributeValue("dgt_workbench_id", value);
                OnPropertyChanged(nameof(DgtWorkbenchId));
            }
        }

		/// <summary>
		/// Sequence number of the import that created this record.
		/// </summary>
		[AttributeLogicalName("importsequencenumber")]
        public int? ImportSequenceNumber
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("importsequencenumber");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ImportSequenceNumber));
                SetAttributeValue("importsequencenumber", value);
                OnPropertyChanged(nameof(ImportSequenceNumber));
            }
        }

		/// <summary>
		/// Unique identifier of the user who modified the record.
		/// </summary>
		[AttributeLogicalName("modifiedby")]
        public EntityReference ModifiedBy
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference>("modifiedby");
            }
        }

		/// <summary>
		/// Date and time when the record was modified.
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
		/// Unique identifier of the delegate user who modified the record.
		/// </summary>
		[AttributeLogicalName("modifiedonbehalfby")]
        public EntityReference ModifiedOnBehalfBy
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference>("modifiedonbehalfby");
            }
        }

		/// <summary>
		/// Unique identifier for the organization
		/// </summary>
		[AttributeLogicalName("organizationid")]
        public EntityReference OrganizationId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference>("organizationid");
            }
        }

		/// <summary>
		/// Date and time that the record was migrated.
		/// </summary>
		[AttributeLogicalName("overriddencreatedon")]
        public DateTime? OverriddenCreatedOn
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("overriddencreatedon");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OverriddenCreatedOn));
                SetAttributeValue("overriddencreatedon", value);
                OnPropertyChanged(nameof(OverriddenCreatedOn));
            }
        }

		/// <summary>
		/// Status of the Carrier
		/// </summary>
		[AttributeLogicalName("statecode")]
        public OptionSetValue Statecode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue>("statecode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Statecode));
                SetAttributeValue("statecode", value);
                OnPropertyChanged(nameof(Statecode));
            }
        }

		/// <summary>
		/// Reason for the status of the Carrier
		/// </summary>
		[AttributeLogicalName("statuscode")]
        public OptionSetValue Statuscode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue>("statuscode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Statuscode));
                SetAttributeValue("statuscode", value);
                OnPropertyChanged(nameof(Statuscode));
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
                OnPropertyChanging(nameof(TimeZoneRuleVersionNumber));
                SetAttributeValue("timezoneruleversionnumber", value);
                OnPropertyChanged(nameof(TimeZoneRuleVersionNumber));
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
                OnPropertyChanging(nameof(UTCConversionTimeZoneCode));
                SetAttributeValue("utcconversiontimezonecode", value);
                OnPropertyChanged(nameof(UTCConversionTimeZoneCode));
            }
        }

		/// <summary>
		/// Version Number
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
                public struct DgtConstraintMset
                {
					public const int PreventFlows = 283510000;
					public const int PreventManagedEntitiesWithAllAssets = 283510001;
					public const int PreventItemsWithouthActiveLayer = 283510002;
					public const int PreventPluginAssemblys = 283510003;
                }
                public struct DgtLockedOpt
                {
                    public const bool Nein = false;
                    public const bool Ja = true;
                }
                public struct Statecode
                {
					public const int Active = 0;
					public const int Inactive = 1;
                }
                public struct Statuscode
                {
					public const int Active = 1;
					public const int Inactive = 2;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string DgtCarrierId = "dgt_carrierid";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string DgtConstraintMset = "dgt_constraint_mset";
				public const string DgtHandshakeTs = "dgt_handshake_ts";
				public const string DgtLockedOpt = "dgt_locked_opt";
				public const string DgtReference = "dgt_reference";
				public const string DgtSolutionfriendlyname = "dgt_solutionfriendlyname";
				public const string DgtSolutionid = "dgt_solutionid";
				public const string DgtSolutionuniquename = "dgt_solutionuniquename";
				public const string DgtSolutionversion = "dgt_solutionversion";
				public const string DgtTransportOrderNo = "dgt_transport_order_no";
				public const string DgtWorkbenchId = "dgt_workbench_id";
				public const string ImportSequenceNumber = "importsequencenumber";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string OrganizationId = "organizationid";
				public const string OverriddenCreatedOn = "overriddencreatedon";
				public const string Statecode = "statecode";
				public const string Statuscode = "statuscode";
				public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
				public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string DgtCarrierAsyncOperations = "dgt_carrier_AsyncOperations";
				public const string DgtCarrierBulkDeleteFailures = "dgt_carrier_BulkDeleteFailures";
				public const string DgtCarrierDuplicateBaseRecord = "dgt_carrier_DuplicateBaseRecord";
				public const string DgtCarrierDuplicateMatchingRecord = "dgt_carrier_DuplicateMatchingRecord";
				public const string DgtCarrierMailboxTrackingFolders = "dgt_carrier_MailboxTrackingFolders";
				public const string DgtCarrierPrincipalObjectAttributeAccesses = "dgt_carrier_PrincipalObjectAttributeAccesses";
				public const string DgtCarrierProcessSession = "dgt_carrier_ProcessSession";
				public const string DgtCarrierSyncErrors = "dgt_carrier_SyncErrors";
				public const string DgtCarrierToCarrierDependencyCheck = "dgt_carrier_to_carrier_dependency_check";
				public const string DgtCarrierToCarrierMissingDependency = "dgt_carrier_to_carrier_missing_dependency";
				public const string DgtCarrierToWorkbenchHistoryOnCarrier = "dgt_carrier_to_workbench_history_on_carrier";
				public const string DgtCarrierToWorkbenchOnCarrier = "dgt_carrier_to_workbench_on_carrier";
				public const string DgtCarrierToWorkbenchOnTargetCarrier = "dgt_carrier_to_workbench_on_target_carrier";
				public const string DgtCarrierUserEntityInstanceDatas = "dgt_carrier_UserEntityInstanceDatas";
            }

            public static class ManyToOne
            {
				public const string DgtWorkbenchToCarrierOnWorkbench = "dgt_workbench_to_carrier_on_workbench";
				public const string LkDgtCarrierCreatedby = "lk_dgt_carrier_createdby";
				public const string LkDgtCarrierCreatedonbehalfby = "lk_dgt_carrier_createdonbehalfby";
				public const string LkDgtCarrierModifiedby = "lk_dgt_carrier_modifiedby";
				public const string LkDgtCarrierModifiedonbehalfby = "lk_dgt_carrier_modifiedonbehalfby";
				public const string OrganizationDgtCarrier = "organization_dgt_carrier";
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
            reference.Name = GetAttributeValue<string>(PrimaryNameAttribute);

            return reference;
        }
        public static DgtCarrier Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static DgtCarrier Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("dgt_carrier", id, columnSet).ToEntity<DgtCarrier>();
        }

        public DgtCarrier GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty).GetCustomAttribute(typeof (AttributeLogicalNameAttribute))).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  DgtCarrier(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<DgtCarrier> DgtCarrierSet
		{
			get
			{
				return CreateQuery<DgtCarrier>();
			}
		}
	}
	#endregion
}
