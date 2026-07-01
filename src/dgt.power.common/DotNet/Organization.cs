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
	/// Top level of the Microsoft Dynamics 365 business hierarchy. The organization can be a specific business, holding company, or corporation.
	/// </summary>
    [DataContract]
    [EntityLogicalName("organization")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class Organization : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public Organization() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public Organization(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Organization(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Organization(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Organization(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "organization";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 1019;
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
        [AttributeLogicalName("organizationid")]
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
                base.Id = value;
            }
        }

        /// <summary>
		/// Unique identifier of the organization.
		/// </summary>
        [AttributeLogicalName("organizationid")]
        public Guid? OrganizationId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("organizationid");
            }
        }

        /// <summary>
		/// ACI Web Endpoint URL.
		/// </summary>
        [AttributeLogicalName("aciwebendpointurl")]
        public string? ACIWebEndpointUrl
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("aciwebendpointurl");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("aciwebendpointurl", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the template to be used for acknowledgement when a user unsubscribes.
		/// </summary>
        [AttributeLogicalName("acknowledgementtemplateid")]
        public EntityReference? AcknowledgementTemplateId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("acknowledgementtemplateid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("acknowledgementtemplateid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information on whether filtering activity based on entity in app.
		/// </summary>
        [AttributeLogicalName("activitytypefilter")]
        public bool? ActivityTypeFilter
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("activitytypefilter");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("activitytypefilter", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Whether to show only activities configured in this app or all activities in the 'New activity' button.
		/// </summary>
        [AttributeLogicalName("activitytypefilterv2")]
        public bool? ActivityTypeFilterV2
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("activitytypefilterv2");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("activitytypefilterv2", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Flag to indicate if the display column options on a view in model-driven apps is enabled
		/// </summary>
        [AttributeLogicalName("advancedcolumneditorenabled")]
        public bool? AdvancedColumnEditorEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("advancedcolumneditorenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("advancedcolumneditorenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Flag to indicate if the advanced column filtering in a view in model-driven apps is enabled
		/// </summary>
        [AttributeLogicalName("advancedcolumnfilteringenabled")]
        public bool? AdvancedColumnFilteringEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("advancedcolumnfilteringenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("advancedcolumnfilteringenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Flag to indicate if the advanced filtering on all tables in a model-driven app is enabled
		/// </summary>
        [AttributeLogicalName("advancedfilteringenabled")]
        public bool? AdvancedFilteringEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("advancedfilteringenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("advancedfilteringenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Flag to indicate if the Advanced Lookup feature is enabled for lookup controls
		/// </summary>
        [AttributeLogicalName("advancedlookupenabled")]
        public bool? AdvancedLookupEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("advancedlookupenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("advancedlookupenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enables advanced lookup in grid edit filter panel
		/// </summary>
        [AttributeLogicalName("advancedlookupineditfilter")]
        public int? AdvancedLookupInEditFilter
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("advancedlookupineditfilter");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("advancedlookupineditfilter", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether AI Builder features are blocked from using Copilot Credits.
		/// </summary>
        [AttributeLogicalName("aibuildercreditsonlyenabled")]
        public bool? AiBuilderCreditsOnlyEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("aibuildercreditsonlyenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("aibuildercreditsonlyenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Azure AI Foundry model types for AI Prompts are enabled.
		/// </summary>
        [AttributeLogicalName("aipromptsazureaifoundrymodeltypesenabled")]
        public bool? AiPromptsAzureAIFoundryModelTypesEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("aipromptsazureaifoundrymodeltypesenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("aipromptsazureaifoundrymodeltypesenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Basic model types for AI Prompts are enabled.
		/// </summary>
        [AttributeLogicalName("aipromptsbasicmodeltypesenabled")]
        public bool? AiPromptsBasicModelTypesEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("aipromptsbasicmodeltypesenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("aipromptsbasicmodeltypesenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether AI Prompts feature is enabled.
		/// </summary>
        [AttributeLogicalName("aipromptsenabled")]
        public bool? AiPromptsEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("aipromptsenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("aipromptsenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Premium model types for AI Prompts are enabled.
		/// </summary>
        [AttributeLogicalName("aipromptspremiummodeltypesenabled")]
        public bool? AiPromptsPremiumModelTypesEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("aipromptspremiummodeltypesenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("aipromptspremiummodeltypesenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Standard model types for AI Prompts are enabled.
		/// </summary>
        [AttributeLogicalName("aipromptsstandardmodeltypesenabled")]
        public bool? AiPromptsStandardModelTypesEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("aipromptsstandardmodeltypesenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("aipromptsstandardmodeltypesenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether background address book synchronization in Microsoft Office Outlook is allowed.
		/// </summary>
        [AttributeLogicalName("allowaddressbooksyncs")]
        public bool? AllowAddressBookSyncs
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowaddressbooksyncs");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowaddressbooksyncs", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether all application users are allowed to access the environment
		/// </summary>
        [AttributeLogicalName("allowapplicationuseraccess")]
        public bool? AllowApplicationUserAccess
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowapplicationuseraccess");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowapplicationuseraccess", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether automatic response creation is allowed.
		/// </summary>
        [AttributeLogicalName("allowautoresponsecreation")]
        public bool? AllowAutoResponseCreation
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowautoresponsecreation");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowautoresponsecreation", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether automatic unsubscribe is allowed.
		/// </summary>
        [AttributeLogicalName("allowautounsubscribe")]
        public bool? AllowAutoUnsubscribe
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowautounsubscribe");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowautounsubscribe", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether automatic unsubscribe acknowledgement email is allowed to send.
		/// </summary>
        [AttributeLogicalName("allowautounsubscribeacknowledgement")]
        public bool? AllowAutoUnsubscribeAcknowledgement
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowautounsubscribeacknowledgement");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowautounsubscribeacknowledgement", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Outlook Client message bar advertisement is allowed.
		/// </summary>
        [AttributeLogicalName("allowclientmessagebarad")]
        public bool? AllowClientMessageBarAd
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowclientmessagebarad");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowclientmessagebarad", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information on whether connectors on power fx actions is enabled.
		/// </summary>
        [AttributeLogicalName("allowconnectorsonpowerfxactions")]
        public bool? AllowConnectorsOnPowerFXActions
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowconnectorsonpowerfxactions");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowconnectorsonpowerfxactions", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies the Applications that are in allow list for the accessing DV resources.
		/// </summary>
        [AttributeLogicalName("allowedapplicationsfordvaccess")]
        public string? AllowedApplicationsForDVAccess
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("allowedapplicationsfordvaccess");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowedapplicationsfordvaccess", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies the range of IP addresses that are in allow list for the firewall.
		/// </summary>
        [AttributeLogicalName("allowediprangeforfirewall")]
        public string? AllowedIpRangeForFirewall
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("allowediprangeforfirewall");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowediprangeforfirewall", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies the range of IP addresses that are in allowed list for generating the SAS URIs.
		/// </summary>
        [AttributeLogicalName("allowediprangeforstorageaccesssignatures")]
        public string? AllowedIpRangeForStorageAccessSignatures
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("allowediprangeforstorageaccesssignatures");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowediprangeforstorageaccesssignatures", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Specifies list of allowed IP addresses for firewall.
		/// </summary>
        [AttributeLogicalName("allowedlistofiprangesforfirewall")]
        public string? AllowedListOfIpRangesForFirewall
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("allowedlistofiprangesforfirewall");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowedlistofiprangesforfirewall", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Allow upload or download of certain mime types.
		/// </summary>
        [AttributeLogicalName("allowedmimetypes")]
        public string? AllowedMimeTypes
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("allowedmimetypes");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowedmimetypes", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies the List of Service Tags that should be allowed by the firewall.
		/// </summary>
        [AttributeLogicalName("allowedservicetagsforfirewall")]
        public string? AllowedServiceTagsForFirewall
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("allowedservicetagsforfirewall");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowedservicetagsforfirewall", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether auditing of changes to entity is allowed when no attributes have changed.
		/// </summary>
        [AttributeLogicalName("allowentityonlyaudit")]
        public bool? AllowEntityOnlyAudit
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowentityonlyaudit");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowentityonlyaudit", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enables ends-with searches in grids with the use of a leading wildcard on all tables in the environment
		/// </summary>
        [AttributeLogicalName("allowleadingwildcardsingridsearch")]
        public bool? AllowLeadingWildcardsInGridSearch
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowleadingwildcardsingridsearch");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowleadingwildcardsingridsearch", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enables ends-with searches in grids with the use of a leading wildcard on all tables in the environment
		/// </summary>
        [AttributeLogicalName("allowleadingwildcardsinquickfind")]
        public int? AllowLeadingWildcardsInQuickFind
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("allowleadingwildcardsinquickfind");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowleadingwildcardsinquickfind", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable access to legacy web client UI
		/// </summary>
        [AttributeLogicalName("allowlegacyclientexperience")]
        public bool? AllowLegacyClientExperience
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowlegacyclientexperience");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowlegacyclientexperience", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable embedding of certain legacy dialogs in Unified Interface browser client
		/// </summary>
        [AttributeLogicalName("allowlegacydialogsembedding")]
        public bool? AllowLegacyDialogsEmbedding
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowlegacydialogsembedding");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowlegacydialogsembedding", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether marketing emails execution is allowed.
		/// </summary>
        [AttributeLogicalName("allowmarketingemailexecution")]
        public bool? AllowMarketingEmailExecution
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowmarketingemailexecution");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowmarketingemailexecution", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether Microsoft Trusted Service Tags are allowed
		/// </summary>
        [AttributeLogicalName("allowmicrosofttrustedservicetags")]
        public bool? AllowMicrosoftTrustedServiceTags
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowmicrosofttrustedservicetags");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowmicrosofttrustedservicetags", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether background offline synchronization in Microsoft Office Outlook is allowed.
		/// </summary>
        [AttributeLogicalName("allowofflinescheduledsyncs")]
        public bool? AllowOfflineScheduledSyncs
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowofflinescheduledsyncs");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowofflinescheduledsyncs", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether scheduled synchronizations to Outlook are allowed.
		/// </summary>
        [AttributeLogicalName("allowoutlookscheduledsyncs")]
        public bool? AllowOutlookScheduledSyncs
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowoutlookscheduledsyncs");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowoutlookscheduledsyncs", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Control whether the organization Allow Redirect Legacy Admin Settings To Modern UI
		/// </summary>
        [AttributeLogicalName("allowredirectadminsettingstomodernui")]
        public bool? AllowRedirectAdminSettingsToModernUI
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowredirectadminsettingstomodernui");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowredirectadminsettingstomodernui", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether users are allowed to send email to unresolved parties (parties must still have an email address).
		/// </summary>
        [AttributeLogicalName("allowunresolvedpartiesonemailsend")]
        public bool? AllowUnresolvedPartiesOnEmailSend
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowunresolvedpartiesonemailsend");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowunresolvedpartiesonemailsend", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether individuals can select their form mode preference in their personal options.
		/// </summary>
        [AttributeLogicalName("allowuserformmodepreference")]
        public bool? AllowUserFormModePreference
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowuserformmodepreference");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowuserformmodepreference", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Flag to indicate if allow end users to hide system views in model-driven apps is enabled
		/// </summary>
        [AttributeLogicalName("allowusershidingsystemviews")]
        public bool? AllowUsersHidingSystemViews
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowusershidingsystemviews");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowusershidingsystemviews", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the showing tablet application notification bars in a browser is allowed.
		/// </summary>
        [AttributeLogicalName("allowusersseeappdownloadmessage")]
        public bool? AllowUsersSeeAppdownloadMessage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowusersseeappdownloadmessage");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowusersseeappdownloadmessage", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Warning : Allowing  Virtual Entity plugin execution on nested pipeline does not offer transactional support. i.e. if call in native entity pipeline fails, then virtual entity operation will not be reverted.
		/// </summary>
        [AttributeLogicalName("allowvirtualentitypluginexecutiononnestedpipeline")]
        public bool? AllowVirtualEntityPluginExecutionOnNestedPipeline
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowvirtualentitypluginexecutiononnestedpipeline");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowvirtualentitypluginexecutiononnestedpipeline", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Web-based export of grids to Microsoft Office Excel is allowed.
		/// </summary>
        [AttributeLogicalName("allowwebexcelexport")]
        public bool? AllowWebExcelExport
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("allowwebexcelexport");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowwebexcelexport", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// AM designator to use throughout Microsoft Dynamics CRM.
		/// </summary>
        [AttributeLogicalName("amdesignator")]
        public string? AMDesignator
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("amdesignator");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("amdesignator", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the appDesignerExperience is enabled for the organization.
		/// </summary>
        [AttributeLogicalName("appdesignerexperienceenabled")]
        public bool? AppDesignerExperienceEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("appdesignerexperienceenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("appdesignerexperienceenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Application Based Access Control Mode. 0 is Disabled, 1 is audit mode , 2 is enforcement mode
		/// </summary>
        [AttributeLogicalName("applicationbasedaccesscontrolmode")]
        public OptionSetValue? ApplicationBasedAccessControlMode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("applicationbasedaccesscontrolmode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("applicationbasedaccesscontrolmode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information on whether rich editing experience for Appointment is enabled.
		/// </summary>
        [AttributeLogicalName("appointmentricheditorexperience")]
        public bool? AppointmentRichEditorExperience
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("appointmentricheditorexperience");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("appointmentricheditorexperience", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information on whether Teams meeting experience for Appointment is enabled.
		/// </summary>
        [AttributeLogicalName("appointmentwithteamsmeeting")]
        public bool? AppointmentWithTeamsMeeting
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("appointmentwithteamsmeeting");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("appointmentwithteamsmeeting", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Whether Teams meetings experience for appointments is enabled.
		/// </summary>
        [AttributeLogicalName("appointmentwithteamsmeetingv2")]
        public bool? AppointmentWithTeamsMeetingV2
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("appointmentwithteamsmeetingv2");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("appointmentwithteamsmeetingv2", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Power Automate Automation Center preview features will be available for all users in this organization.
		/// </summary>
        [AttributeLogicalName("areautomationcenterpreviewfeaturesenabled")]
        public bool? AreAutomationCenterPreviewFeaturesEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("areautomationcenterpreviewfeaturesenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("areautomationcenterpreviewfeaturesenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Process Insights Preview features are enabled in this organization.
		/// </summary>
        [AttributeLogicalName("areprocessinsightspreviewfeaturesenabled")]
        public bool? AreProcessInsightsPreviewFeaturesEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("areprocessinsightspreviewfeaturesenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("areprocessinsightspreviewfeaturesenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Audit Retention Period settings stored in Organization Database.
		/// </summary>
        [AttributeLogicalName("auditretentionperiod")]
        public int? AuditRetentionPeriod
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("auditretentionperiod");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("auditretentionperiod", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Audit Retention Period settings stored in Organization Database.
		/// </summary>
        [AttributeLogicalName("auditretentionperiodv2")]
        public int? AuditRetentionPeriodV2
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("auditretentionperiodv2");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("auditretentionperiodv2", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Audit Settings of the organization
		/// </summary>
        [AttributeLogicalName("auditsettings")]
        public string? AuditSettings
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("auditsettings");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("auditsettings", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether to auto apply the default customer entitlement on case creation.
		/// </summary>
        [AttributeLogicalName("autoapplydefaultoncasecreate")]
        public bool? AutoApplyDefaultonCaseCreate
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("autoapplydefaultoncasecreate");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("autoapplydefaultoncasecreate", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether to auto apply the default customer entitlement on case update.
		/// </summary>
        [AttributeLogicalName("autoapplydefaultoncaseupdate")]
        public bool? AutoApplyDefaultonCaseUpdate
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("autoapplydefaultoncaseupdate");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("autoapplydefaultoncaseupdate", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether to Auto-apply SLA on case record update after SLA was manually applied.
		/// </summary>
        [AttributeLogicalName("autoapplysla")]
        public bool? AutoApplySLA
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("autoapplysla");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("autoapplysla", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("azureschedulerjobcollectionname")]
        public string? AzureSchedulerJobCollectionName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("azureschedulerjobcollectionname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("azureschedulerjobcollectionname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the base currency of the organization.
		/// </summary>
        [AttributeLogicalName("basecurrencyid")]
        public EntityReference? BaseCurrencyId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("basecurrencyid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("basecurrencyid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Number of decimal places that can be used for the base currency.
		/// </summary>
        [AttributeLogicalName("basecurrencyprecision")]
        public int? BaseCurrencyPrecision
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("basecurrencyprecision");
            }
        }

        /// <summary>
		/// Symbol used for the base currency.
		/// </summary>
        [AttributeLogicalName("basecurrencysymbol")]
        public string? BaseCurrencySymbol
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("basecurrencysymbol");
            }
        }

        /// <summary>
		/// Api Key to be used in requests to Bing Maps services.
		/// </summary>
        [AttributeLogicalName("bingmapsapikey")]
        public string? BingMapsApiKey
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("bingmapsapikey");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("bingmapsapikey", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable this feature to prevent makers from accessing and downloading session transcripts
		/// </summary>
        [AttributeLogicalName("blockaccesstosessiontranscriptsforcopilotstudio")]
        public bool? BlockAccessToSessionTranscriptsForCopilotStudio
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("blockaccesstosessiontranscriptsforcopilotstudio");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("blockaccesstosessiontranscriptsforcopilotstudio", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Prevent makers from allowing end-users to use their credentials during authentication to use connectors, actions, flows, and triggers that are connected to an agent
		/// </summary>
        [AttributeLogicalName("blockcopilotauthorauthentication")]
        public bool? BlockCopilotAuthorAuthentication
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("blockcopilotauthorauthentication");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("blockcopilotauthorauthentication", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies the Applications that are in block list for the accessing DV resources.
		/// </summary>
        [AttributeLogicalName("blockedapplicationsfordvaccess")]
        public string? BlockedApplicationsForDVAccess
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("blockedapplicationsfordvaccess");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("blockedapplicationsfordvaccess", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Prevent upload or download of certain attachment types that are considered dangerous.
		/// </summary>
        [AttributeLogicalName("blockedattachments")]
        public string? BlockedAttachments
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("blockedattachments");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("blockedattachments", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Prevent upload or download of certain mime types that are considered dangerous.
		/// </summary>
        [AttributeLogicalName("blockedmimetypes")]
        public string? BlockedMimeTypes
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("blockedmimetypes");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("blockedmimetypes", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable this feature to block access to session transcripts and conversational transcripts from being written to Dataverse for an individual environment
		/// </summary>
        [AttributeLogicalName("blocktranscriptrecordingforcopilotstudio")]
        public bool? BlockTranscriptRecordingForCopilotStudio
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("blocktranscriptrecordingforcopilotstudio");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("blocktranscriptrecordingforcopilotstudio", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable this feature to block URLs and images in Copilot Studio and agent responses for an individual environment. URLs will be replaced with placeholders.
		/// </summary>
        [AttributeLogicalName("blockurlsinresponsesforcopilotstudio")]
        public bool? BlockUrlsInResponsesForCopilotStudio
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("blockurlsinresponsesforcopilotstudio");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("blockurlsinresponsesforcopilotstudio", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Display cards in expanded state for interactive dashboard
		/// </summary>
        [AttributeLogicalName("bounddashboarddefaultcardexpanded")]
        public bool? BoundDashboardDefaultCardExpanded
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("bounddashboarddefaultcardexpanded");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("bounddashboarddefaultcardexpanded", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Prefix used for bulk operation numbering.
		/// </summary>
        [AttributeLogicalName("bulkoperationprefix")]
        public string? BulkOperationPrefix
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("bulkoperationprefix");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("bulkoperationprefix", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// BusinessCardOptions
		/// </summary>
        [AttributeLogicalName("businesscardoptions")]
        public string? BusinessCardOptions
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("businesscardoptions");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("businesscardoptions", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the business closure calendar of organization.
		/// </summary>
        [AttributeLogicalName("businessclosurecalendarid")]
        public Guid? BusinessClosureCalendarId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("businessclosurecalendarid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("businessclosurecalendarid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Calendar type for the system. Set to Gregorian US by default.
		/// </summary>
        [AttributeLogicalName("calendartype")]
        public int? CalendarType
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("calendartype");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("calendartype", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Prefix used for campaign numbering.
		/// </summary>
        [AttributeLogicalName("campaignprefix")]
        public string? CampaignPrefix
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("campaignprefix");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("campaignprefix", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the organization can opt out of the new Relevance search experience (released in Oct 2020)
		/// </summary>
        [AttributeLogicalName("canoptoutnewsearchexperience")]
        public bool? CanOptOutNewSearchExperience
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("canoptoutnewsearchexperience");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("canoptoutnewsearchexperience", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Flag to cascade Update on incident.
		/// </summary>
        [AttributeLogicalName("cascadestatusupdate")]
        public bool? CascadeStatusUpdate
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("cascadestatusupdate");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("cascadestatusupdate", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Prefix to use for all cases throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("caseprefix")]
        public string? CasePrefix
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("caseprefix");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("caseprefix", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the prefix to use for all categories in Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("categoryprefix")]
        public string? CategoryPrefix
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("categoryprefix");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("categoryprefix", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Client Features to be enabled as an XML BLOB.
		/// </summary>
        [AttributeLogicalName("clientfeatureset")]
        public string? ClientFeatureSet
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("clientfeatureset");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("clientfeatureset", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Policy configuration for CSP
		/// </summary>
        [AttributeLogicalName("contentsecuritypolicyconfiguration")]
        public string? ContentSecurityPolicyConfiguration
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("contentsecuritypolicyconfiguration");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("contentsecuritypolicyconfiguration", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Content Security Policy configuration for Canvas apps.
		/// </summary>
        [AttributeLogicalName("contentsecuritypolicyconfigurationforcanvas")]
        public string? ContentSecurityPolicyConfigurationForCanvas
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("contentsecuritypolicyconfigurationforcanvas");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("contentsecuritypolicyconfigurationforcanvas", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Content Security Policy Options.
		/// </summary>
        [AttributeLogicalName("contentsecuritypolicyoptions")]
        public int? ContentSecurityPolicyOptions
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("contentsecuritypolicyoptions");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("contentsecuritypolicyoptions", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Content Security Policy Report Uri.
		/// </summary>
        [AttributeLogicalName("contentsecuritypolicyreporturi")]
        public string? ContentSecurityPolicyReportUri
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("contentsecuritypolicyreporturi");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("contentsecuritypolicyreporturi", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Prefix to use for all contracts throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("contractprefix")]
        public string? ContractPrefix
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("contractprefix");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("contractprefix", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Refresh rate for copresence data in seconds.
		/// </summary>
        [AttributeLogicalName("copresencerefreshrate")]
        public int? CopresenceRefreshRate
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("copresencerefreshrate");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("copresencerefreshrate", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the feature CortanaProactiveExperience Flow processes should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("cortanaproactiveexperienceenabled")]
        public bool? CortanaProactiveExperienceEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("cortanaproactiveexperienceenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("cortanaproactiveexperienceenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the organization.
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
		/// Date and time when the organization was created.
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
		/// Unique identifier of the delegate user who created the organization.
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
		/// Enable Initial state of newly created products to be Active instead of Draft
		/// </summary>
        [AttributeLogicalName("createproductswithoutparentinactivestate")]
        public bool? CreateProductsWithoutParentInActiveState
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("createproductswithoutparentinactivestate");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("createproductswithoutparentinactivestate", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Default time to live in minutes for new records in the Flow Logs entity for CUA logs.
		/// </summary>
        [AttributeLogicalName("cuaflowlogsttlinminutes")]
        public int? CuaFlowLogsTtlInMinutes
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("cuaflowlogsttlinminutes");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("cuaflowlogsttlinminutes", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Set the level of detail the computer use logs allow.
		/// </summary>
        [AttributeLogicalName("cuaflowlogsverbosity")]
        public OptionSetValue? CuaFlowLogsVerbosity
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("cuaflowlogsverbosity");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("cuaflowlogsverbosity", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Number of decimal places that can be used for currency.
		/// </summary>
        [AttributeLogicalName("currencydecimalprecision")]
        public int? CurrencyDecimalPrecision
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("currencydecimalprecision");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("currencydecimalprecision", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether to display money fields with currency code or currency symbol.
		/// </summary>
        [AttributeLogicalName("currencydisplayoption")]
        public OptionSetValue? CurrencyDisplayOption
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("currencydisplayoption");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("currencydisplayoption", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information about how currency symbols are placed throughout Microsoft Dynamics CRM.
		/// </summary>
        [AttributeLogicalName("currencyformatcode")]
        public OptionSetValue? CurrencyFormatCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("currencyformatcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("currencyformatcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Symbol used for currency throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("currencysymbol")]
        public string? CurrencySymbol
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("currencysymbol");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("currencysymbol", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Current bulk operation number. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentbulkoperationnumber")]
        public int? CurrentBulkOperationNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("currentbulkoperationnumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("currentbulkoperationnumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Current campaign number. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentcampaignnumber")]
        public int? CurrentCampaignNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("currentcampaignnumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("currentcampaignnumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// First case number to use. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentcasenumber")]
        public int? CurrentCaseNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("currentcasenumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("currentcasenumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enter the first number to use for Categories. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentcategorynumber")]
        public int? CurrentCategoryNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("currentcategorynumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("currentcategorynumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// First contract number to use. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentcontractnumber")]
        public int? CurrentContractNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("currentcontractnumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("currentcontractnumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Import sequence to use.
		/// </summary>
        [AttributeLogicalName("currentimportsequencenumber")]
        public int? CurrentImportSequenceNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("currentimportsequencenumber");
            }
        }

        /// <summary>
		/// First invoice number to use. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentinvoicenumber")]
        public int? CurrentInvoiceNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("currentinvoicenumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("currentinvoicenumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enter the first number to use for knowledge articles. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentkanumber")]
        public int? CurrentKaNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("currentkanumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("currentkanumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// First article number to use. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentkbnumber")]
        public int? CurrentKbNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("currentkbnumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("currentkbnumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// First order number to use. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentordernumber")]
        public int? CurrentOrderNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("currentordernumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("currentordernumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// First parsed table number to use.
		/// </summary>
        [AttributeLogicalName("currentparsedtablenumber")]
        public int? CurrentParsedTableNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("currentparsedtablenumber");
            }
        }

        /// <summary>
		/// First quote number to use. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentquotenumber")]
        public int? CurrentQuoteNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("currentquotenumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("currentquotenumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information about how the date is displayed throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("dateformatcode")]
        public OptionSetValue? DateFormatCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("dateformatcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("dateformatcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// String showing how the date is displayed throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("dateformatstring")]
        public string? DateFormatString
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("dateformatstring");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("dateformatstring", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Character used to separate the month, the day, and the year in dates throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("dateseparator")]
        public string? DateSeparator
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("dateseparator");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("dateseparator", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Number of days before we migrate email description to blob.
		/// </summary>
        [AttributeLogicalName("daysbeforeemaildescriptionismigrated")]
        public int? DaysBeforeEmailDescriptionIsMigrated
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("daysbeforeemaildescriptionismigrated");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("daysbeforeemaildescriptionismigrated", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Days of inactivity before sync is disabled for a Teams Chat.
		/// </summary>
        [AttributeLogicalName("daysbeforeinactiveteamschatsyncdisabled")]
        public int? DaysBeforeInactiveTeamsChatSyncDisabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("daysbeforeinactiveteamschatsyncdisabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("daysbeforeinactiveteamschatsyncdisabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The maximum value for the Mobile Offline setting Days since record last modified
		/// </summary>
        [AttributeLogicalName("dayssincerecordlastmodifiedmaxvalue")]
        public int? DaysSinceRecordLastModifiedMaxValue
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("dayssincerecordlastmodifiedmaxvalue");
            }
        }

        /// <summary>
		/// Symbol used for decimal in Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("decimalsymbol")]
        public string? DecimalSymbol
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("decimalsymbol");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("decimalsymbol", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Text area to enter default country code.
		/// </summary>
        [AttributeLogicalName("defaultcountrycode")]
        public string? DefaultCountryCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("defaultcountrycode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("defaultcountrycode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Name of the default crm custom.
		/// </summary>
        [AttributeLogicalName("defaultcrmcustomname")]
        public string? DefaultCrmCustomName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("defaultcrmcustomname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("defaultcrmcustomname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the default email server profile.
		/// </summary>
        [AttributeLogicalName("defaultemailserverprofileid")]
        public EntityReference? DefaultEmailServerProfileId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("defaultemailserverprofileid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("defaultemailserverprofileid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// XML string containing the default email settings that are applied when a user or queue is created.
		/// </summary>
        [AttributeLogicalName("defaultemailsettings")]
        public string? DefaultEmailSettings
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("defaultemailsettings");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("defaultemailsettings", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the default mobile offline profile.
		/// </summary>
        [AttributeLogicalName("defaultmobileofflineprofileid")]
        public EntityReference? DefaultMobileOfflineProfileId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("defaultmobileofflineprofileid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("defaultmobileofflineprofileid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type of default recurrence end range date.
		/// </summary>
        [AttributeLogicalName("defaultrecurrenceendrangetype")]
        public OptionSetValue? DefaultRecurrenceEndRangeType
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("defaultrecurrenceendrangetype");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("defaultrecurrenceendrangetype", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Default theme data for the organization.
		/// </summary>
        [AttributeLogicalName("defaultthemedata")]
        public string? DefaultThemeData
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("defaultthemedata");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("defaultthemedata", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the delegated admin user for the organization.
		/// </summary>
        [AttributeLogicalName("delegatedadminuserid")]
        public Guid? DelegatedAdminUserId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("delegatedadminuserid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("delegatedadminuserid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Default time to live in minutes for new desktop flow queue log records.
		/// </summary>
        [AttributeLogicalName("desktopflowqueuelogsttlinminutes")]
        public int? DesktopFlowQueueLogsTtlInMinutes
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("desktopflowqueuelogsttlinminutes");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("desktopflowqueuelogsttlinminutes", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Toggle the activation of the Power Automate Desktop Flow run action logs.
		/// </summary>
        [AttributeLogicalName("desktopflowrunactionlogsstatus")]
        public OptionSetValue? DesktopFlowRunActionLogsStatus
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("desktopflowrunactionlogsstatus");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("desktopflowrunactionlogsstatus", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// What verbosity level the Power Automate Desktop Flow Run Action Logs allow.
		/// </summary>
        [AttributeLogicalName("desktopflowrunactionlogverbosity")]
        public OptionSetValue? DesktopFlowRunActionLogVerbosity
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("desktopflowrunactionlogverbosity");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("desktopflowrunactionlogverbosity", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Where the Power Automate Desktop Flow Run Action logs are stored.
		/// </summary>
        [AttributeLogicalName("desktopflowrunactionlogversion")]
        public OptionSetValue? DesktopFlowRunActionLogVersion
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("desktopflowrunactionlogversion");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("desktopflowrunactionlogversion", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Reason for disabling the organization.
		/// </summary>
        [AttributeLogicalName("disabledreason")]
        public string? DisabledReason
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("disabledreason");
            }
        }

        /// <summary>
		/// Indicates whether Social Care is disabled.
		/// </summary>
        [AttributeLogicalName("disablesocialcare")]
        public bool? DisableSocialCare
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("disablesocialcare");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("disablesocialcare", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Disable sharing system labels for the organization.
		/// </summary>
        [AttributeLogicalName("disablesystemlabelscachesharing")]
        public bool? DisableSystemLabelsCacheSharing
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("disablesystemlabelscachesharing");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("disablesystemlabelscachesharing", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Discount calculation method for the QOOI product.
		/// </summary>
        [AttributeLogicalName("discountcalculationmethod")]
        public OptionSetValue? DiscountCalculationMethod
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("discountcalculationmethod");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("discountcalculationmethod", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether or not navigation tour is displayed.
		/// </summary>
        [AttributeLogicalName("displaynavigationtour")]
        public bool? DisplayNavigationTour
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("displaynavigationtour");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("displaynavigationtour", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select if you want to use the Email Router or server-side synchronization for email processing.
		/// </summary>
        [AttributeLogicalName("emailconnectionchannel")]
        public OptionSetValue? EmailConnectionChannel
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("emailconnectionchannel");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("emailconnectionchannel", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Flag to turn email correlation on or off.
		/// </summary>
        [AttributeLogicalName("emailcorrelationenabled")]
        public bool? EmailCorrelationEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("emailcorrelationenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("emailcorrelationenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Normal polling frequency used for sending email in Microsoft Office Outlook.
		/// </summary>
        [AttributeLogicalName("emailsendpollingperiod")]
        public int? EmailSendPollingPeriod
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("emailsendpollingperiod");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("emailsendpollingperiod", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Determines whether records merged through the merge dialog in UCI are merged asynchronously
		/// </summary>
        [AttributeLogicalName("enableasyncmergeapiforuci")]
        public bool? EnableAsyncMergeAPIForUCI
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enableasyncmergeapiforuci");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enableasyncmergeapiforuci", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable Integration with Bing Maps
		/// </summary>
        [AttributeLogicalName("enablebingmapsintegration")]
        public bool? EnableBingMapsIntegration
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enablebingmapsintegration");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enablebingmapsintegration", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Note: By enabling this feature, you will also enable the automatic creation of enviornment variables when adding data sources for your apps.
		/// </summary>
        [AttributeLogicalName("enablecanvasappsinsolutionsbydefault")]
        public bool? EnableCanvasAppsInSolutionsByDefault
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enablecanvasappsinsolutionsbydefault");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enablecanvasappsinsolutionsbydefault", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable this feature to allow cross-geo boundary sharing of aggregated analytics data if your preferred data location for Viva Insights is different than the location of your environment
		/// </summary>
        [AttributeLogicalName("enablecopilotstudiocrossgeosharedatawithvivainsights")]
        public bool? EnableCopilotStudioCrossGeoShareDataWithVivaInsights
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enablecopilotstudiocrossgeosharedatawithvivainsights");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enablecopilotstudiocrossgeosharedatawithvivainsights", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// (Deprecated) Enable this feature to allow Copilot Studio to share aggregated analytics data for custom agents with Viva Insights for an individual environment
		/// </summary>
        [AttributeLogicalName("enablecopilotstudiosharedatawithvi")]
        public bool? EnableCopilotStudioShareDataWithVI
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enablecopilotstudiosharedatawithvi");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enablecopilotstudiosharedatawithvi", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable this feature to allow Copilot Studio to share aggregated analytics data for custom agents with Viva Insights for an individual environment
		/// </summary>
        [AttributeLogicalName("enablecopilotstudiosharedatawithvivainsights")]
        public bool? EnableCopilotStudioShareDataWithVivaInsights
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enablecopilotstudiosharedatawithvivainsights");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enablecopilotstudiosharedatawithvivainsights", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enables the Environment Settings App
		/// </summary>
        [AttributeLogicalName("enableenvironmentsettingsapp")]
        public bool? EnableEnvironmentSettingsApp
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enableenvironmentsettingsapp");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enableenvironmentsettingsapp", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the creation of flows is within a solution by default for this organization.
		/// </summary>
        [AttributeLogicalName("enableflowsinsolutionbydefault")]
        public bool? EnableFlowsInSolutionByDefault
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enableflowsinsolutionbydefault");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enableflowsinsolutionbydefault", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Organizations with this attribute set to true will be granted a grace period and excluded from the initial world wide enablement of 'creation of flows within a solution by default' functionality. Once the grace period expires, the functionality will be enabled in your organization.
		/// </summary>
        [AttributeLogicalName("enableflowsinsolutionbydefaultgraceperiod")]
        public bool? EnableFlowsInSolutionByDefaultGracePeriod
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enableflowsinsolutionbydefaultgraceperiod");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enableflowsinsolutionbydefaultgraceperiod", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable Integration with Immersive Skype
		/// </summary>
        [AttributeLogicalName("enableimmersiveskypeintegration")]
        public bool? EnableImmersiveSkypeIntegration
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enableimmersiveskypeintegration");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enableimmersiveskypeintegration", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether IP based cookie binding is enabled
		/// </summary>
        [AttributeLogicalName("enableipbasedcookiebinding")]
        public bool? EnableIpBasedCookieBinding
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enableipbasedcookiebinding");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enableipbasedcookiebinding", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether IP based firewall rule is enabled
		/// </summary>
        [AttributeLogicalName("enableipbasedfirewallrule")]
        public bool? EnableIpBasedFirewallRule
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enableipbasedfirewallrule");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enableipbasedfirewallrule", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether IP based firewall rule is enabled in Audit Only Mode
		/// </summary>
        [AttributeLogicalName("enableipbasedfirewallruleinauditmode")]
        public bool? EnableIpBasedFirewallRuleInAuditMode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enableipbasedfirewallruleinauditmode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enableipbasedfirewallruleinauditmode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether IP based SAS URI generation rule is enabled
		/// </summary>
        [AttributeLogicalName("enableipbasedstorageaccesssignaturerule")]
        public bool? EnableIpBasedStorageAccessSignatureRule
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enableipbasedstorageaccesssignaturerule");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enableipbasedstorageaccesssignaturerule", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the user has enabled or disabled Live Persona Card feature in UCI.
		/// </summary>
        [AttributeLogicalName("enablelivepersonacarduci")]
        public bool? EnableLivePersonaCardUCI
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enablelivepersonacarduci");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enablelivepersonacarduci", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the user has enabled or disabled LivePersonCardIntegration in Office.
		/// </summary>
        [AttributeLogicalName("enablelivepersoncardintegrationinoffice")]
        public bool? EnableLivePersonCardIntegrationInOffice
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enablelivepersoncardintegrationinoffice");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enablelivepersoncardintegrationinoffice", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select to enable learning path auhtoring.
		/// </summary>
        [AttributeLogicalName("enablelpauthoring")]
        public bool? EnableLPAuthoring
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enablelpauthoring");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enablelpauthoring", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Control whether the organization Switch Maker Portal to Classic
		/// </summary>
        [AttributeLogicalName("enablemakerswitchtoclassic")]
        public bool? EnableMakerSwitchToClassic
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enablemakerswitchtoclassic");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enablemakerswitchtoclassic", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable Integration with Microsoft Flow
		/// </summary>
        [AttributeLogicalName("enablemicrosoftflowintegration")]
        public bool? EnableMicrosoftFlowIntegration
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enablemicrosoftflowintegration");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enablemicrosoftflowintegration", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable pricing calculations on a Create call.
		/// </summary>
        [AttributeLogicalName("enablepricingoncreate")]
        public bool? EnablePricingOnCreate
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enablepricingoncreate");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enablepricingoncreate", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable or disable Sensitivity Labels in Email.
		/// </summary>
        [AttributeLogicalName("enablesensitivitylabels")]
        public bool? EnableSensitivityLabels
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enablesensitivitylabels");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enablesensitivitylabels", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Use Smart Matching.
		/// </summary>
        [AttributeLogicalName("enablesmartmatching")]
        public bool? EnableSmartMatching
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enablesmartmatching");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enablesmartmatching", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Leave empty to use default setting. Set to on/off to enable/disable CDN for UCI.
		/// </summary>
        [AttributeLogicalName("enableunifiedclientcdn")]
        public bool? EnableUnifiedClientCDN
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enableunifiedclientcdn");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enableunifiedclientcdn", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable site map and commanding update
		/// </summary>
        [AttributeLogicalName("enableunifiedinterfaceshellrefresh")]
        public bool? EnableUnifiedInterfaceShellRefresh
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enableunifiedinterfaceshellrefresh");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enableunifiedinterfaceshellrefresh", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Organization setting to enforce read only plugins.
		/// </summary>
        [AttributeLogicalName("enforcereadonlyplugins")]
        public bool? EnforceReadOnlyPlugins
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enforcereadonlyplugins");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enforcereadonlyplugins", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The default image for the entity.
		/// </summary>
        [AttributeLogicalName("entityimage")]
        public byte[]? EntityImage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<byte[]?>("entityimage");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("entityimage", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("entityimage_timestamp")]
        public long? EntityImageTimestamp
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<long?>("entityimage_timestamp");
            }
        }

        
        [AttributeLogicalName("entityimage_url")]
        public string? EntityImageURL
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("entityimage_url");
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("entityimageid")]
        public Guid? EntityImageId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("entityimageid");
            }
        }

        /// <summary>
		/// Maximum number of days to keep change tracking deleted records
		/// </summary>
        [AttributeLogicalName("expirechangetrackingindays")]
        public int? ExpireChangeTrackingInDays
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("expirechangetrackingindays");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("expirechangetrackingindays", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum number of days before deleting inactive subscriptions.
		/// </summary>
        [AttributeLogicalName("expiresubscriptionsindays")]
        public int? ExpireSubscriptionsInDays
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("expiresubscriptionsindays");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("expiresubscriptionsindays", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Specify the base URL to use to look for external document suggestions.
		/// </summary>
        [AttributeLogicalName("externalbaseurl")]
        public string? ExternalBaseUrl
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("externalbaseurl");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("externalbaseurl", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// XML string containing the ExternalPartyEnabled entities correlation keys for association of existing External Party instance entities to newly created IsExternalPartyEnabled entities.For internal use only
		/// </summary>
        [AttributeLogicalName("externalpartycorrelationkeys")]
        public string? ExternalPartyCorrelationKeys
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("externalpartycorrelationkeys");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("externalpartycorrelationkeys", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// XML string containing the ExternalPartyEnabled entities settings.
		/// </summary>
        [AttributeLogicalName("externalpartyentitysettings")]
        public string? ExternalPartyEntitySettings
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("externalpartyentitysettings");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("externalpartyentitysettings", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Features to be enabled as an XML BLOB.
		/// </summary>
        [AttributeLogicalName("featureset")]
        public string? FeatureSet
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("featureset");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("featureset", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Start date for the fiscal period that is to be used throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("fiscalcalendarstart")]
        public DateTime? FiscalCalendarStart
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("fiscalcalendarstart");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("fiscalcalendarstart", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies how the name of the fiscal period is displayed throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("fiscalperiodformat")]
        public string? FiscalPeriodFormat
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("fiscalperiodformat");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("fiscalperiodformat", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Format in which the fiscal period will be displayed.
		/// </summary>
        [AttributeLogicalName("fiscalperiodformatperiod")]
        public OptionSetValue? FiscalPeriodFormatPeriod
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("fiscalperiodformatperiod");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("fiscalperiodformatperiod", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type of fiscal period used throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("fiscalperiodtype")]
        public int? FiscalPeriodType
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("fiscalperiodtype");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("fiscalperiodtype", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether the fiscal settings have been updated.
		/// </summary>
        [AttributeLogicalName("fiscalsettingsupdated")]
        public bool? FiscalSettingsUpdated
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("fiscalsettingsupdated");
            }
        }

        /// <summary>
		/// Information that specifies whether the fiscal year should be displayed based on the start date or the end date of the fiscal year.
		/// </summary>
        [AttributeLogicalName("fiscalyeardisplaycode")]
        public int? FiscalYearDisplayCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("fiscalyeardisplaycode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("fiscalyeardisplaycode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies how the name of the fiscal year is displayed throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("fiscalyearformat")]
        public string? FiscalYearFormat
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("fiscalyearformat");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("fiscalyearformat", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Prefix for the display of the fiscal year.
		/// </summary>
        [AttributeLogicalName("fiscalyearformatprefix")]
        public OptionSetValue? FiscalYearFormatPrefix
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("fiscalyearformatprefix");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("fiscalyearformatprefix", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Suffix for the display of the fiscal year.
		/// </summary>
        [AttributeLogicalName("fiscalyearformatsuffix")]
        public OptionSetValue? FiscalYearFormatSuffix
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("fiscalyearformatsuffix");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("fiscalyearformatsuffix", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Format for the year.
		/// </summary>
        [AttributeLogicalName("fiscalyearformatyear")]
        public OptionSetValue? FiscalYearFormatYear
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("fiscalyearformatyear");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("fiscalyearformatyear", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies how the names of the fiscal year and the fiscal period should be connected when displayed together.
		/// </summary>
        [AttributeLogicalName("fiscalyearperiodconnect")]
        public string? FiscalYearPeriodConnect
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("fiscalyearperiodconnect");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("fiscalyearperiodconnect", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Defines how long desktop flow logs are retained in Dataverse (V2 only). The default is 40,320 minutes (28 days). Set to 0 to retain logs indefinitely.
		/// </summary>
        [AttributeLogicalName("flowlogsttlinminutes")]
        public int? FlowLogsTtlInMinutes
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("flowlogsttlinminutes");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("flowlogsttlinminutes", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Time to live (in seconds) for flow run
		/// </summary>
        [AttributeLogicalName("flowruntimetoliveinseconds")]
        public int? FlowRunTimeToLiveInSeconds
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("flowruntimetoliveinseconds");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("flowruntimetoliveinseconds", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Order in which names are to be displayed throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("fullnameconventioncode")]
        public OptionSetValue? FullNameConventionCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("fullnameconventioncode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("fullnameconventioncode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Specifies the maximum number of months in future for which the recurring activities can be created.
		/// </summary>
        [AttributeLogicalName("futureexpansionwindow")]
        public int? FutureExpansionWindow
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("futureexpansionwindow");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("futureexpansionwindow", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether alerts will be generated for errors.
		/// </summary>
        [AttributeLogicalName("generatealertsforerrors")]
        public bool? GenerateAlertsForErrors
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("generatealertsforerrors");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("generatealertsforerrors", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether alerts will be generated for information.
		/// </summary>
        [AttributeLogicalName("generatealertsforinformation")]
        public bool? GenerateAlertsForInformation
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("generatealertsforinformation");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("generatealertsforinformation", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether alerts will be generated for warnings.
		/// </summary>
        [AttributeLogicalName("generatealertsforwarnings")]
        public bool? GenerateAlertsForWarnings
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("generatealertsforwarnings");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("generatealertsforwarnings", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Get Started content is enabled for this organization.
		/// </summary>
        [AttributeLogicalName("getstartedpanecontentenabled")]
        public bool? GetStartedPaneContentEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("getstartedpanecontentenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("getstartedpanecontentenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the append URL parameters is enabled.
		/// </summary>
        [AttributeLogicalName("globalappendurlparametersenabled")]
        public bool? GlobalAppendUrlParametersEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("globalappendurlparametersenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("globalappendurlparametersenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// URL for the web page global help.
		/// </summary>
        [AttributeLogicalName("globalhelpurl")]
        public string? GlobalHelpUrl
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("globalhelpurl");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("globalhelpurl", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the customizable global help is enabled.
		/// </summary>
        [AttributeLogicalName("globalhelpurlenabled")]
        public bool? GlobalHelpUrlEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("globalhelpurlenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("globalhelpurlenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Number of days after the goal's end date after which the rollup of the goal stops automatically.
		/// </summary>
        [AttributeLogicalName("goalrollupexpirytime")]
        public int? GoalRollupExpiryTime
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("goalrollupexpirytime");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("goalrollupexpirytime", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Number of hours between automatic rollup jobs .
		/// </summary>
        [AttributeLogicalName("goalrollupfrequency")]
        public int? GoalRollupFrequency
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("goalrollupfrequency");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("goalrollupfrequency", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("grantaccesstonetworkservice")]
        public bool? GrantAccessToNetworkService
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("grantaccesstonetworkservice");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("grantaccesstonetworkservice", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum difference allowed between subject keywords count of the email messaged to be correlated
		/// </summary>
        [AttributeLogicalName("hashdeltasubjectcount")]
        public int? HashDeltaSubjectCount
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("hashdeltasubjectcount");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("hashdeltasubjectcount", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Filter Subject Keywords
		/// </summary>
        [AttributeLogicalName("hashfilterkeywords")]
        public string? HashFilterKeywords
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("hashfilterkeywords");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("hashfilterkeywords", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum number of subject keywords or recipients used for correlation
		/// </summary>
        [AttributeLogicalName("hashmaxcount")]
        public int? HashMaxCount
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("hashmaxcount");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("hashmaxcount", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Minimum number of recipients required to match for email messaged to be correlated
		/// </summary>
        [AttributeLogicalName("hashminaddresscount")]
        public int? HashMinAddressCount
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("hashminaddresscount");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("hashminaddresscount", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// High contrast theme data for the organization.
		/// </summary>
        [AttributeLogicalName("highcontrastthemedata")]
        public string? HighContrastThemeData
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("highcontrastthemedata");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("highcontrastthemedata", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether incoming email sent by internal Microsoft Dynamics 365 users or queues should be tracked.
		/// </summary>
        [AttributeLogicalName("ignoreinternalemail")]
        public bool? IgnoreInternalEmail
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ignoreinternalemail");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ignoreinternalemail", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether an organization has consented to sharing search query data to help improve search results
		/// </summary>
        [AttributeLogicalName("improvesearchloggingenabled")]
        public bool? ImproveSearchLoggingEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("improvesearchloggingenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("improvesearchloggingenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether Inactivity timeout is enabled
		/// </summary>
        [AttributeLogicalName("inactivitytimeoutenabled")]
        public bool? InactivityTimeoutEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("inactivitytimeoutenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("inactivitytimeoutenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Inactivity timeout in minutes
		/// </summary>
        [AttributeLogicalName("inactivitytimeoutinmins")]
        public int? InactivityTimeoutInMins
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("inactivitytimeoutinmins");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("inactivitytimeoutinmins", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Inactivity timeout reminder in minutes
		/// </summary>
        [AttributeLogicalName("inactivitytimeoutreminderinmins")]
        public int? InactivityTimeoutReminderInMins
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("inactivitytimeoutreminderinmins");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("inactivitytimeoutreminderinmins", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Setting for the Async Service Mailbox Queue. Defines the retrieval batch size of exchange server.
		/// </summary>
        [AttributeLogicalName("incomingemailexchangeemailretrievalbatchsize")]
        public int? IncomingEmailExchangeEmailRetrievalBatchSize
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("incomingemailexchangeemailretrievalbatchsize");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("incomingemailexchangeemailretrievalbatchsize", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Initial version of the organization.
		/// </summary>
        [AttributeLogicalName("initialversion")]
        public string? InitialVersion
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("initialversion");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("initialversion", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the integration user for the organization.
		/// </summary>
        [AttributeLogicalName("integrationuserid")]
        public Guid? IntegrationUserId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("integrationuserid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("integrationuserid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Prefix to use for all invoice numbers throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("invoiceprefix")]
        public string? InvoicePrefix
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("invoiceprefix");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("invoiceprefix", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// IP Based SAS mode.
		/// </summary>
        [AttributeLogicalName("ipbasedstorageaccesssignaturemode")]
        public OptionSetValue? IpBasedStorageAccessSignatureMode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("ipbasedstorageaccesssignaturemode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ipbasedstorageaccesssignaturemode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the feature Action Card should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isactioncardenabled")]
        public bool? IsActionCardEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isactioncardenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isactioncardenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether Action Support Feature is enabled
		/// </summary>
        [AttributeLogicalName("isactionsupportfeatureenabled")]
        public bool? IsActionSupportFeatureEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isactionsupportfeatureenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isactionsupportfeatureenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the feature Relationship Analytics should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isactivityanalysisenabled")]
        public bool? IsActivityAnalysisEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isactivityanalysisenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isactivityanalysisenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether all money attributes are converted to decimal.
		/// </summary>
        [AttributeLogicalName("isallmoneydecimal")]
        public bool? IsAllMoneyDecimal
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isallmoneydecimal");
            }
        }

        /// <summary>
		/// Indicates whether loading of Microsoft Dynamics 365 in a browser window that does not have address, tool, and menu bars is enabled.
		/// </summary>
        [AttributeLogicalName("isappmode")]
        public bool? IsAppMode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isappmode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isappmode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable or disable attachments sync for outlook and exchange.
		/// </summary>
        [AttributeLogicalName("isappointmentattachmentsyncenabled")]
        public bool? IsAppointmentAttachmentSyncEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isappointmentattachmentsyncenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isappointmentattachmentsyncenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable or disable assigned tasks sync for outlook and exchange.
		/// </summary>
        [AttributeLogicalName("isassignedtaskssyncenabled")]
        public bool? IsAssignedTasksSyncEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isassignedtaskssyncenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isassignedtaskssyncenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable or disable auditing of changes.
		/// </summary>
        [AttributeLogicalName("isauditenabled")]
        public bool? IsAuditEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isauditenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isauditenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the feature Auto Capture should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isautodatacaptureenabled")]
        public bool? IsAutoDataCaptureEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isautodatacaptureenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isautodatacaptureenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the V2 feature of Auto Capture should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isautodatacapturev2enabled")]
        public bool? IsAutoDataCaptureV2Enabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isautodatacapturev2enabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isautodatacapturev2enabled", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("isautoinstallappford365inteamsenabled")]
        public bool? IsAutoInstallAppForD365InTeamsEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isautoinstallappford365inteamsenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isautoinstallappford365inteamsenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information on whether auto save is enabled.
		/// </summary>
        [AttributeLogicalName("isautosaveenabled")]
        public bool? IsAutoSaveEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isautosaveenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isautosaveenabled", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("isbasecardstaticfielddataenabled")]
        public bool? IsBaseCardStaticFieldDataEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isbasecardstaticfielddataenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isbasecardstaticfielddataenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Determines whether users can make use of basic Geospatial featuers in Canvas apps.
		/// </summary>
        [AttributeLogicalName("isbasicgeospatialintegrationenabled")]
        public bool? IsBasicGeospatialIntegrationEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isbasicgeospatialintegrationenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isbasicgeospatialintegrationenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether BPF Entity Customization Feature is enabled
		/// </summary>
        [AttributeLogicalName("isbpfentitycustomizationfeatureenabled")]
        public bool? IsBPFEntityCustomizationFeatureEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isbpfentitycustomizationfeatureenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isbpfentitycustomizationfeatureenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Power Automate savings feature is enabled for Cloudflow.
		/// </summary>
        [AttributeLogicalName("iscloudflowsavingsenabled")]
        public bool? IsCloudFlowSavingsEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("iscloudflowsavingsenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("iscloudflowsavingsenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Read-only flag indicating whether clustering is enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isclusteringenabled")]
        public bool? IsClusteringEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isclusteringenabled");
            }
        }

        
        [AttributeLogicalName("iscollaborationexperienceenabled")]
        public bool? IsCollaborationExperienceEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("iscollaborationexperienceenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("iscollaborationexperienceenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Computer Use in MCS feature is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("iscomputeruseinmcsenabled")]
        public bool? IsComputerUseInMCSEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("iscomputeruseinmcsenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("iscomputeruseinmcsenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether conflict detection for mobile client is enabled.
		/// </summary>
        [AttributeLogicalName("isconflictdetectionenabledformobileclient")]
        public bool? IsConflictDetectionEnabledForMobileClient
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isconflictdetectionenabledformobileclient");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isconflictdetectionenabledformobileclient", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable or disable mailing address sync for outlook and exchange.
		/// </summary>
        [AttributeLogicalName("iscontactmailingaddresssyncenabled")]
        public bool? IsContactMailingAddressSyncEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("iscontactmailingaddresssyncenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("iscontactmailingaddresssyncenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Content Security Policy has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("iscontentsecuritypolicyenabled")]
        public bool? IsContentSecurityPolicyEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("iscontentsecuritypolicyenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("iscontentsecuritypolicyenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Content Security Policy has been enabled for this organization's Canvas apps.
		/// </summary>
        [AttributeLogicalName("iscontentsecuritypolicyenabledforcanvas")]
        public bool? IsContentSecurityPolicyEnabledForCanvas
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("iscontentsecuritypolicyenabledforcanvas");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("iscontentsecuritypolicyenabledforcanvas", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Contextual email experience is enabled on this organization
		/// </summary>
        [AttributeLogicalName("iscontextualemailenabled")]
        public bool? IsContextualEmailEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("iscontextualemailenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("iscontextualemailenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select to enable Contextual Help in UCI.
		/// </summary>
        [AttributeLogicalName("iscontextualhelpenabled")]
        public bool? IsContextualHelpEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("iscontextualhelpenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("iscontextualhelpenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Determines whether users can provide feedback Copilot experiences.
		/// </summary>
        [AttributeLogicalName("iscopilotfeedbackenabled")]
        public bool? IsCopilotFeedbackEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("iscopilotfeedbackenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("iscopilotfeedbackenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether CUA on Hosted Groups V2 feature is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("iscuaonhmgv2enabled")]
        public bool? IsCuaOnHmgV2Enabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("iscuaonhmgv2enabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("iscuaonhmgv2enabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Custom Controls in canvas PowerApps feature has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("iscustomcontrolsincanvasappsenabled")]
        public bool? IsCustomControlsInCanvasAppsEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("iscustomcontrolsincanvasappsenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("iscustomcontrolsincanvasappsenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable or disable country code selection.
		/// </summary>
        [AttributeLogicalName("isdefaultcountrycodecheckenabled")]
        public bool? IsDefaultCountryCodeCheckEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isdefaultcountrycodecheckenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isdefaultcountrycodecheckenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable Delegation Access content
		/// </summary>
        [AttributeLogicalName("isdelegateaccessenabled")]
        public bool? IsDelegateAccessEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isdelegateaccessenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isdelegateaccessenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the feature Action Hub should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isdelveactionhubintegrationenabled")]
        public bool? IsDelveActionHubIntegrationEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isdelveactionhubintegrationenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isdelveactionhubintegrationenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether connection embedding in Desktop Flows is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isdesktopflowconnectionembeddingenabled")]
        public bool? IsDesktopFlowConnectionEmbeddingEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isdesktopflowconnectionembeddingenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isdesktopflowconnectionembeddingenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the Desktop Flows UI Automation Runtime Repair for Attended feature for this organization.
		/// </summary>
        [AttributeLogicalName("isdesktopflowruntimerepairattendedenabled")]
        public bool? IsDesktopFlowRuntimeRepairAttendedEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isdesktopflowruntimerepairattendedenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isdesktopflowruntimerepairattendedenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the Desktop Flows UI Automation Runtime Repair for Unattended feature for this organization.
		/// </summary>
        [AttributeLogicalName("isdesktopflowruntimerepairunattendedenabled")]
        public bool? IsDesktopFlowRuntimeRepairUnattendedEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isdesktopflowruntimerepairunattendedenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isdesktopflowruntimerepairunattendedenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Power Automate savings feature is enabled for Desktopflow.
		/// </summary>
        [AttributeLogicalName("isdesktopflowsavingsenabled")]
        public bool? IsDesktopFlowSavingsEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isdesktopflowsavingsenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isdesktopflowsavingsenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether v2 schema for Desktop Flows is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isdesktopflowschemav2enabled")]
        public bool? IsDesktopFlowSchemaV2Enabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isdesktopflowschemav2enabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isdesktopflowschemav2enabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Windows Vanilla Image will be available for Desktop Flow users in this organization.
		/// </summary>
        [AttributeLogicalName("isdesktopflowvanillaimagesharingenabled")]
        public bool? IsDesktopFlowVanillaImageSharingEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isdesktopflowvanillaimagesharingenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isdesktopflowvanillaimagesharingenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether version control for Desktop Flows is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isdesktopflowversioncontrolenabled")]
        public bool? IsDesktopFlowVersionControlEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isdesktopflowversioncontrolenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isdesktopflowversioncontrolenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates if this organization will opt-in to automatically to enable version control for Desktop Flows.
		/// </summary>
        [AttributeLogicalName("isdesktopflowversioncontrolenabledbydefault")]
        public bool? IsDesktopFlowVersionControlEnabledByDefault
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isdesktopflowversioncontrolenabledbydefault");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isdesktopflowversioncontrolenabledbydefault", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Overrides whether version control for Desktop Flows is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isdesktopflowversioncontrolenabledoverride")]
        public OptionSetValue? IsDesktopFlowVersionControlEnabledOverride
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("isdesktopflowversioncontrolenabledoverride");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isdesktopflowversioncontrolenabledoverride", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether the organization is disabled.
		/// </summary>
        [AttributeLogicalName("isdisabled")]
        public bool? IsDisabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isdisabled");
            }
        }

        /// <summary>
		/// Indicates whether duplicate detection of records is enabled.
		/// </summary>
        [AttributeLogicalName("isduplicatedetectionenabled")]
        public bool? IsDuplicateDetectionEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isduplicatedetectionenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isduplicatedetectionenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether duplicate detection of records during import is enabled.
		/// </summary>
        [AttributeLogicalName("isduplicatedetectionenabledforimport")]
        public bool? IsDuplicateDetectionEnabledForImport
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isduplicatedetectionenabledforimport");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isduplicatedetectionenabledforimport", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether duplicate detection of records during offline synchronization is enabled.
		/// </summary>
        [AttributeLogicalName("isduplicatedetectionenabledforofflinesync")]
        public bool? IsDuplicateDetectionEnabledForOfflineSync
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isduplicatedetectionenabledforofflinesync");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isduplicatedetectionenabledforofflinesync", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether duplicate detection during online create or update is enabled.
		/// </summary>
        [AttributeLogicalName("isduplicatedetectionenabledforonlinecreateupdate")]
        public bool? IsDuplicateDetectionEnabledForOnlineCreateUpdate
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isduplicatedetectionenabledforonlinecreateupdate");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isduplicatedetectionenabledforonlinecreateupdate", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information on whether Smart Email Address Validation is enabled.
		/// </summary>
        [AttributeLogicalName("isemailaddressvalidationenabled")]
        public bool? IsEmailAddressValidationEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isemailaddressvalidationenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isemailaddressvalidationenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Allow tracking recipient activity on sent emails.
		/// </summary>
        [AttributeLogicalName("isemailmonitoringallowed")]
        public bool? IsEmailMonitoringAllowed
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isemailmonitoringallowed");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isemailmonitoringallowed", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable Email Server Profile content filtering
		/// </summary>
        [AttributeLogicalName("isemailserverprofilecontentfilteringenabled")]
        public bool? IsEmailServerProfileContentFilteringEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isemailserverprofilecontentfilteringenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isemailserverprofilecontentfilteringenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether appmodule is enabled for all roles
		/// </summary>
        [AttributeLogicalName("isenabledforallroles")]
        public bool? IsEnabledForAllRoles
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isenabledforallroles");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isenabledforallroles", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the organization's files are being stored in Azure.
		/// </summary>
        [AttributeLogicalName("isexternalfilestorageenabled")]
        public bool? IsExternalFileStorageEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isexternalfilestorageenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isexternalfilestorageenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether data can be synchronized with an external search index.
		/// </summary>
        [AttributeLogicalName("isexternalsearchindexenabled")]
        public bool? IsExternalSearchIndexEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isexternalsearchindexenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isexternalsearchindexenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the fiscal period is displayed as the month number.
		/// </summary>
        [AttributeLogicalName("isfiscalperiodmonthbased")]
        public bool? IsFiscalPeriodMonthBased
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isfiscalperiodmonthbased");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isfiscalperiodmonthbased", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether folders should be automatically created on SharePoint.
		/// </summary>
        [AttributeLogicalName("isfolderautocreatedonsp")]
        public bool? IsFolderAutoCreatedonSP
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isfolderautocreatedonsp");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isfolderautocreatedonsp", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable or disable folder based tracking for Server Side Sync.
		/// </summary>
        [AttributeLogicalName("isfolderbasedtrackingenabled")]
        public bool? IsFolderBasedTrackingEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isfolderbasedtrackingenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isfolderbasedtrackingenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether full-text search for Quick Find entities should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isfulltextsearchenabled")]
        public bool? IsFullTextSearchEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isfulltextsearchenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isfulltextsearchenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether geospatial capabilities leveraging Azure Maps are enabled.
		/// </summary>
        [AttributeLogicalName("isgeospatialazuremapsintegrationenabled")]
        public bool? IsGeospatialAzureMapsIntegrationEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isgeospatialazuremapsintegrationenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isgeospatialazuremapsintegrationenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable Hierarchical Security Model
		/// </summary>
        [AttributeLogicalName("ishierarchicalsecuritymodelenabled")]
        public bool? IsHierarchicalSecurityModelEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ishierarchicalsecuritymodelenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ishierarchicalsecuritymodelenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether data collection for ideas in canvas PowerApps has been enabled.
		/// </summary>
        [AttributeLogicalName("isideasdatacollectionenabled")]
        public bool? IsIdeasDataCollectionEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isideasdatacollectionenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isideasdatacollectionenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Give Consent to use LUIS in Dynamics 365 Bot
		/// </summary>
        [AttributeLogicalName("isluisenabledford365bot")]
        public bool? IsLUISEnabledforD365Bot
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isluisenabledford365bot");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isluisenabledford365bot", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable or disable forced unlocking for Server Side Sync mailboxes.
		/// </summary>
        [AttributeLogicalName("ismailboxforcedunlockingenabled")]
        public bool? IsMailboxForcedUnlockingEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ismailboxforcedunlockingenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ismailboxforcedunlockingenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable or disable mailbox keep alive for Server Side Sync.
		/// </summary>
        [AttributeLogicalName("ismailboxinactivebackoffenabled")]
        public bool? IsMailboxInactiveBackoffEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ismailboxinactivebackoffenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ismailboxinactivebackoffenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Manual Sales Forecasting feature has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("ismanualsalesforecastingenabled")]
        public bool? IsManualSalesForecastingEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ismanualsalesforecastingenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ismanualsalesforecastingenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether mobile client on demand sync is enabled.
		/// </summary>
        [AttributeLogicalName("ismobileclientondemandsyncenabled")]
        public bool? IsMobileClientOnDemandSyncEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ismobileclientondemandsyncenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ismobileclientondemandsyncenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the feature MobileOffline should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("ismobileofflineenabled")]
        public bool? IsMobileOfflineEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ismobileofflineenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ismobileofflineenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Model Apps can be embedded within Microsoft Teams. This is a tenant admin controlled preview/experimental feature.
		/// </summary>
        [AttributeLogicalName("ismodeldrivenappsinmsteamsenabled")]
        public bool? IsModelDrivenAppsInMSTeamsEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ismodeldrivenappsinmsteamsenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ismodeldrivenappsinmsteamsenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the maker can create Power Automate money based saving rules.
		/// </summary>
        [AttributeLogicalName("ismoneysavingsallowed")]
        public bool? IsMoneySavingsAllowed
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ismoneysavingsallowed");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ismoneysavingsallowed", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Microsoft Teams Collaboration feature has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("ismsteamscollaborationenabled")]
        public bool? IsMSTeamsCollaborationEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ismsteamscollaborationenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ismsteamscollaborationenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Microsoft Teams integration has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("ismsteamsenabled")]
        public bool? IsMSTeamsEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ismsteamsenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ismsteamsenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the user has enabled or disabled Microsoft Teams integration.
		/// </summary>
        [AttributeLogicalName("ismsteamssettingchangedbyuser")]
        public bool? IsMSTeamsSettingChangedByUser
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ismsteamssettingchangedbyuser");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ismsteamssettingchangedbyuser", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Microsoft Teams User Sync feature has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("ismsteamsusersyncenabled")]
        public bool? IsMSTeamsUserSyncEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ismsteamsusersyncenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ismsteamsusersyncenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether new add product experience is enabled.
		/// </summary>
        [AttributeLogicalName("isnewaddproductexperienceenabled")]
        public bool? IsNewAddProductExperienceEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isnewaddproductexperienceenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isnewaddproductexperienceenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the feature Notes Analysis should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isnotesanalysisenabled")]
        public bool? IsNotesAnalysisEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isnotesanalysisenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isnotesanalysisenabled", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("isnotificationford365inteamsenabled")]
        public bool? IsNotificationForD365InTeamsEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isnotificationford365inteamsenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isnotificationford365inteamsenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the feature OfficeGraph should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isofficegraphenabled")]
        public bool? IsOfficeGraphEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isofficegraphenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isofficegraphenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the feature One Drive should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isonedriveenabled")]
        public bool? IsOneDriveEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isonedriveenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isonedriveenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether PAI feature has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("ispaienabled")]
        public bool? IsPAIEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ispaienabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ispaienabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether PDF Generation feature has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("ispdfgenerationenabled")]
        public string? IsPDFGenerationEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("ispdfgenerationenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ispdfgenerationenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the Per Process overage feature is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isperprocesscapacityoverageenabled")]
        public bool? IsPerProcessCapacityOverageEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isperprocesscapacityoverageenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isperprocesscapacityoverageenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether playbook feature has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isplaybookenabled")]
        public bool? IsPlaybookEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isplaybookenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isplaybookenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information on whether IM presence is enabled.
		/// </summary>
        [AttributeLogicalName("ispresenceenabled")]
        public bool? IsPresenceEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ispresenceenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ispresenceenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the Preview feature for Action Card should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("ispreviewenabledforactioncard")]
        public bool? IsPreviewEnabledForActionCard
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ispreviewenabledforactioncard");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ispreviewenabledforactioncard", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the feature Auto Capture should be enabled for the organization at Preview Settings.
		/// </summary>
        [AttributeLogicalName("ispreviewforautocaptureenabled")]
        public bool? IsPreviewForAutoCaptureEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ispreviewforautocaptureenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ispreviewforautocaptureenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Is Preview For Email Monitoring Allowed.
		/// </summary>
        [AttributeLogicalName("ispreviewforemailmonitoringallowed")]
        public bool? IsPreviewForEmailMonitoringAllowed
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ispreviewforemailmonitoringallowed");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ispreviewforemailmonitoringallowed", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether PriceList is mandatory for adding existing products to sales entities.
		/// </summary>
        [AttributeLogicalName("ispricelistmandatory")]
        public bool? IsPriceListMandatory
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ispricelistmandatory");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ispricelistmandatory", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the Process capacity auto-claim feature is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isprocesscapacityautoclaimenabled")]
        public bool? IsProcessCapacityAutoClaimEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isprocesscapacityautoclaimenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isprocesscapacityautoclaimenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Process Mining is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isprocessminingenabled")]
        public bool? IsProcessMiningEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isprocessminingenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isprocessminingenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether to use the standard Out-of-box Opportunity Close experience or opt to for a customized experience.
		/// </summary>
        [AttributeLogicalName("isquickcreateenabledforopportunityclose")]
        public bool? IsQuickCreateEnabledForOpportunityClose
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isquickcreateenabledforopportunityclose");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isquickcreateenabledforopportunityclose", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable or disable auditing of read operations.
		/// </summary>
        [AttributeLogicalName("isreadauditenabled")]
        public bool? IsReadAuditEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isreadauditenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isreadauditenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the feature Relationship Insights should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isrelationshipinsightsenabled")]
        public bool? IsRelationshipInsightsEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isrelationshipinsightsenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isrelationshipinsightsenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates if the synchronization of user resource booking with Exchange is enabled at organization level.
		/// </summary>
        [AttributeLogicalName("isresourcebookingexchangesyncenabled")]
        public bool? IsResourceBookingExchangeSyncEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isresourcebookingexchangesyncenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isresourcebookingexchangesyncenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether rich text editor for notes experience is enabled on this organization
		/// </summary>
        [AttributeLogicalName("isrichtextnotesenabled")]
        public bool? IsRichTextNotesEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isrichtextnotesenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isrichtextnotesenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether AAD Join for RPA Autoscale is enabled in this organization..
		/// </summary>
        [AttributeLogicalName("isrpaautoscaleaadjoinenabled")]
        public bool? IsRpaAutoscaleAadJoinEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isrpaautoscaleaadjoinenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isrpaautoscaleaadjoinenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Autoscale feature for RPA is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isrpaautoscaleenabled")]
        public bool? IsRpaAutoscaleEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isrpaautoscaleenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isrpaautoscaleenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether RPA Box feature is enabled in this organization in locations outside the tenant's geographical location.
		/// </summary>
        [AttributeLogicalName("isrpaboxcrossgeoenabled")]
        public bool? IsRpaBoxCrossGeoEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isrpaboxcrossgeoenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isrpaboxcrossgeoenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether RPA Box feature is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isrpaboxenabled")]
        public bool? IsRpaBoxEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isrpaboxenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isrpaboxenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Unattended runs feature for RPA is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isrpaunattendedenabled")]
        public bool? IsRpaUnattendedEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isrpaunattendedenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isrpaunattendedenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Sales Assistant mobile app has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("issalesassistantenabled")]
        public bool? IsSalesAssistantEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("issalesassistantenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("issalesassistantenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether sending CUA audit logs to Purview is enabled.
		/// </summary>
        [AttributeLogicalName("issendcuaauditlogtopurviewenabled")]
        public bool? IsSendCuaAuditLogToPurviewEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("issendcuaauditlogtopurviewenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("issendcuaauditlogtopurviewenabled", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("issharinginorgallowed")]
        public bool? IsSharingInOrgAllowed
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("issharinginorgallowed");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("issharinginorgallowed", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable sales order processing integration.
		/// </summary>
        [AttributeLogicalName("issopintegrationenabled")]
        public bool? IsSOPIntegrationEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("issopintegrationenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("issopintegrationenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information on whether text wrap is enabled.
		/// </summary>
        [AttributeLogicalName("istextwrapenabled")]
        public bool? IsTextWrapEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("istextwrapenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("istextwrapenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether CUA log upload to Dataverse is enabled.
		/// </summary>
        [AttributeLogicalName("isuploadcualogtodataverseenabled")]
        public bool? IsUploadCuaLogToDataverseEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isuploadcualogtodataverseenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isuploadcualogtodataverseenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable or disable auditing of user access.
		/// </summary>
        [AttributeLogicalName("isuseraccessauditenabled")]
        public bool? IsUserAccessAuditEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isuseraccessauditenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isuseraccessauditenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether loading of Microsoft Dynamics 365 in a browser window that does not have address, tool, and menu bars is enabled.
		/// </summary>
        [AttributeLogicalName("isvintegrationcode")]
        public OptionSetValue? ISVIntegrationCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("isvintegrationcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isvintegrationcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Power Automate savings feature is enabled for WorkQueue.
		/// </summary>
        [AttributeLogicalName("isworkqueuesavingsenabled")]
        public bool? IsWorkQueueSavingsEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isworkqueuesavingsenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isworkqueuesavingsenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Write-in Products can be added to Opportunity/Quote/Order/Invoice or not.
		/// </summary>
        [AttributeLogicalName("iswriteinproductsallowed")]
        public bool? IsWriteInProductsAllowed
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("iswriteinproductsallowed");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("iswriteinproductsallowed", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the prefix to use for all knowledge articles in Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("kaprefix")]
        public string? KaPrefix
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("kaprefix");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("kaprefix", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Prefix to use for all articles in Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("kbprefix")]
        public string? KbPrefix
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("kbprefix");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("kbprefix", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// XML string containing the Knowledge Management settings that are applied in Knowledge Management Wizard.
		/// </summary>
        [AttributeLogicalName("kmsettings")]
        public string? KMSettings
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("kmsettings");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("kmsettings", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Preferred language for the organization.
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
		/// Show legacy app for admins
		/// </summary>
        [AttributeLogicalName("legacyapptoggle")]
        public OptionSetValue? LegacyAppToggle
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("legacyapptoggle");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("legacyapptoggle", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the locale of the organization.
		/// </summary>
        [AttributeLogicalName("localeid")]
        public int? LocaleId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("localeid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("localeid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies how the Long Date format is displayed in Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("longdateformatcode")]
        public int? LongDateFormatCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("longdateformatcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("longdateformatcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Minimum number of characters that should be entered in the lookup control before resolving for suggestions
		/// </summary>
        [AttributeLogicalName("lookupcharactercountbeforeresolve")]
        public int? LookupCharacterCountBeforeResolve
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("lookupcharactercountbeforeresolve");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("lookupcharactercountbeforeresolve", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Minimum delay (in milliseconds) between consecutive inputs in a lookup control that will trigger a search for suggestions
		/// </summary>
        [AttributeLogicalName("lookupresolvedelayms")]
        public int? LookupResolveDelayMS
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("lookupresolvedelayms");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("lookupresolvedelayms", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Lower Threshold For Mailbox Intermittent Issue.
		/// </summary>
        [AttributeLogicalName("mailboxintermittentissueminrange")]
        public int? MailboxIntermittentIssueMinRange
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("mailboxintermittentissueminrange");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("mailboxintermittentissueminrange", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Lower Threshold For Mailbox Permanent Issue.
		/// </summary>
        [AttributeLogicalName("mailboxpermanentissueminrange")]
        public int? MailboxPermanentIssueMinRange
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("mailboxpermanentissueminrange");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("mailboxpermanentissueminrange", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum number of actionsteps allowed in a BPF
		/// </summary>
        [AttributeLogicalName("maxactionstepsinbpf")]
        public int? MaxActionStepsInBPF
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maxactionstepsinbpf");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maxactionstepsinbpf", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum Allowed Pending Rollup Job Count
		/// </summary>
        [AttributeLogicalName("maxallowedpendingrollupjobcount")]
        public int? MaxAllowedPendingRollupJobCount
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maxallowedpendingrollupjobcount");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maxallowedpendingrollupjobcount", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Percentage Of Entity Table Size For Kicking Off Bootstrap Job
		/// </summary>
        [AttributeLogicalName("maxallowedpendingrollupjobpercentage")]
        public int? MaxAllowedPendingRollupJobPercentage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maxallowedpendingrollupjobpercentage");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maxallowedpendingrollupjobpercentage", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum number of days an appointment can last.
		/// </summary>
        [AttributeLogicalName("maxappointmentdurationdays")]
        public int? MaxAppointmentDurationDays
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maxappointmentdurationdays");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maxappointmentdurationdays", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum number of conditions allowed for mobile offline filters
		/// </summary>
        [AttributeLogicalName("maxconditionsformobileofflinefilters")]
        public int? MaxConditionsForMobileOfflineFilters
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maxconditionsformobileofflinefilters");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maxconditionsformobileofflinefilters", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum depth for hierarchy security propagation.
		/// </summary>
        [AttributeLogicalName("maxdepthforhierarchicalsecuritymodel")]
        public int? MaxDepthForHierarchicalSecurityModel
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maxdepthforhierarchicalsecuritymodel");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maxdepthforhierarchicalsecuritymodel", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum number of Folder Based Tracking mappings user can add
		/// </summary>
        [AttributeLogicalName("maxfolderbasedtrackingmappings")]
        public int? MaxFolderBasedTrackingMappings
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maxfolderbasedtrackingmappings");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maxfolderbasedtrackingmappings", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum number of active business process flows allowed per entity
		/// </summary>
        [AttributeLogicalName("maximumactivebusinessprocessflowsallowedperentity")]
        public int? MaximumActiveBusinessProcessFlowsAllowedPerEntity
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maximumactivebusinessprocessflowsallowedperentity");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maximumactivebusinessprocessflowsallowedperentity", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Restrict the maximum number of product properties for a product family/bundle
		/// </summary>
        [AttributeLogicalName("maximumdynamicpropertiesallowed")]
        public int? MaximumDynamicPropertiesAllowed
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maximumdynamicpropertiesallowed");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maximumdynamicpropertiesallowed", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum number of active SLA allowed per entity in online
		/// </summary>
        [AttributeLogicalName("maximumentitieswithactivesla")]
        public int? MaximumEntitiesWithActiveSLA
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maximumentitieswithactivesla");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maximumentitieswithactivesla", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum number of SLA KPI per active SLA allowed for entity in online
		/// </summary>
        [AttributeLogicalName("maximumslakpiperentitywithactivesla")]
        public int? MaximumSLAKPIPerEntityWithActiveSLA
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maximumslakpiperentitywithactivesla");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maximumslakpiperentitywithactivesla", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum tracking number before recycling takes place.
		/// </summary>
        [AttributeLogicalName("maximumtrackingnumber")]
        public int? MaximumTrackingNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maximumtrackingnumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maximumtrackingnumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Restrict the maximum no of items in a bundle
		/// </summary>
        [AttributeLogicalName("maxproductsinbundle")]
        public int? MaxProductsInBundle
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maxproductsinbundle");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maxproductsinbundle", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum number of records that will be exported to a static Microsoft Office Excel worksheet when exporting from the grid.
		/// </summary>
        [AttributeLogicalName("maxrecordsforexporttoexcel")]
        public int? MaxRecordsForExportToExcel
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maxrecordsforexporttoexcel");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maxrecordsforexporttoexcel", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum number of lookup and picklist records that can be selected by user for filtering.
		/// </summary>
        [AttributeLogicalName("maxrecordsforlookupfilters")]
        public int? MaxRecordsForLookupFilters
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maxrecordsforlookupfilters");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maxrecordsforlookupfilters", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum Rollup Fields Per Entity
		/// </summary>
        [AttributeLogicalName("maxrollupfieldsperentity")]
        public int? MaxRollupFieldsPerEntity
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maxrollupfieldsperentity");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maxrollupfieldsperentity", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum Rollup Fields Per Organization
		/// </summary>
        [AttributeLogicalName("maxrollupfieldsperorg")]
        public int? MaxRollupFieldsPerOrg
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maxrollupfieldsperorg");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maxrollupfieldsperorg", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("maxslaitemspersla")]
        public int? MaxSLAItemsPerSLA
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maxslaitemspersla");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maxslaitemspersla", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The maximum version of IE to run browser emulation for in Outlook client
		/// </summary>
        [AttributeLogicalName("maxsupportedinternetexplorerversion")]
        public int? MaxSupportedInternetExplorerVersion
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maxsupportedinternetexplorerversion");
            }
        }

        /// <summary>
		/// Maximum allowed size of an attachment.
		/// </summary>
        [AttributeLogicalName("maxuploadfilesize")]
        public int? MaxUploadFileSize
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maxuploadfilesize");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("maxuploadfilesize", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum number of mailboxes that can be toggled for verbose logging
		/// </summary>
        [AttributeLogicalName("maxverboseloggingmailbox")]
        public int? MaxVerboseLoggingMailbox
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maxverboseloggingmailbox");
            }
        }

        /// <summary>
		/// Maximum number of sync cycles for which verbose logging will be enabled by default
		/// </summary>
        [AttributeLogicalName("maxverboseloggingsynccycles")]
        public int? MaxVerboseLoggingSyncCycles
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("maxverboseloggingsynccycles");
            }
        }

        /// <summary>
		/// (Deprecated) Environment selected for Integration with Microsoft Flow
		/// </summary>
        [AttributeLogicalName("microsoftflowenvironment")]
        public string? MicrosoftFlowEnvironment
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("microsoftflowenvironment");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("microsoftflowenvironment", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Normal polling frequency used for address book synchronization in Microsoft Office Outlook.
		/// </summary>
        [AttributeLogicalName("minaddressbooksyncinterval")]
        public int? MinAddressBookSyncInterval
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("minaddressbooksyncinterval");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("minaddressbooksyncinterval", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Normal polling frequency used for background offline synchronization in Microsoft Office Outlook.
		/// </summary>
        [AttributeLogicalName("minofflinesyncinterval")]
        public int? MinOfflineSyncInterval
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("minofflinesyncinterval");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("minofflinesyncinterval", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Minimum allowed time between scheduled Outlook synchronizations.
		/// </summary>
        [AttributeLogicalName("minoutlooksyncinterval")]
        public int? MinOutlookSyncInterval
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("minoutlooksyncinterval");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("minoutlooksyncinterval", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Minimum number of user license required for mobile offline service by production/preview organization
		/// </summary>
        [AttributeLogicalName("mobileofflineminlicenseprod")]
        public int? MobileOfflineMinLicenseProd
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("mobileofflineminlicenseprod");
            }
        }

        /// <summary>
		/// Minimum number of user license required for mobile offline service by trial organization
		/// </summary>
        [AttributeLogicalName("mobileofflineminlicensetrial")]
        public int? MobileOfflineMinLicenseTrial
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("mobileofflineminlicensetrial");
            }
        }

        /// <summary>
		/// Sync interval for mobile offline.
		/// </summary>
        [AttributeLogicalName("mobileofflinesyncinterval")]
        public int? MobileOfflineSyncInterval
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("mobileofflinesyncinterval");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("mobileofflinesyncinterval", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Flag to indicate if the modern advanced find filtering on all tables in a model-driven app is enabled
		/// </summary>
        [AttributeLogicalName("modernadvancedfindfiltering")]
        public bool? ModernAdvancedFindFiltering
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("modernadvancedfindfiltering");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("modernadvancedfindfiltering", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether coauthoring is enabled in modern app designer
		/// </summary>
        [AttributeLogicalName("modernappdesignercoauthoringenabled")]
        public bool? ModernAppDesignerCoauthoringEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("modernappdesignercoauthoringenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("modernappdesignercoauthoringenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the organization.
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
		/// Date and time when the organization was last modified.
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
		/// Unique identifier of the delegate user who last modified the organization.
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
		/// Show the sort by button on views
		/// </summary>
        [AttributeLogicalName("multicolumnsortenabled")]
        public int? MultiColumnSortEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("multicolumnsortenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("multicolumnsortenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Name of the organization. The name is set when Microsoft CRM is installed and should not be changed.
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
		/// Enables Natural Language Assist Filter.
		/// </summary>
        [AttributeLogicalName("naturallanguageassistfilter")]
        public bool? NaturalLanguageAssistFilter
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("naturallanguageassistfilter");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("naturallanguageassistfilter", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies how negative currency numbers are displayed throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("negativecurrencyformatcode")]
        public int? NegativeCurrencyFormatCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("negativecurrencyformatcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("negativecurrencyformatcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies how negative numbers are displayed throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("negativeformatcode")]
        public OptionSetValue? NegativeFormatCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("negativeformatcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("negativeformatcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether an organization has enabled the new Relevance search experience (released in Oct 2020) for the organization
		/// </summary>
        [AttributeLogicalName("newsearchexperienceenabled")]
        public bool? NewSearchExperienceEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("newsearchexperienceenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("newsearchexperienceenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Next token to be placed on the subject line of an email message.
		/// </summary>
        [AttributeLogicalName("nexttrackingnumber")]
        public int? NextTrackingNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("nexttrackingnumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("nexttrackingnumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether mailbox owners will be notified of email server profile level alerts.
		/// </summary>
        [AttributeLogicalName("notifymailboxownerofemailserverlevelalerts")]
        public bool? NotifyMailboxOwnerOfEmailServerLevelAlerts
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("notifymailboxownerofemailserverlevelalerts");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("notifymailboxownerofemailserverlevelalerts", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Specification of how numbers are displayed throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("numberformat")]
        public string? NumberFormat
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("numberformat");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("numberformat", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Specifies how numbers are grouped in Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("numbergroupformat")]
        public string? NumberGroupFormat
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("numbergroupformat");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("numbergroupformat", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Symbol used for number separation in Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("numberseparator")]
        public string? NumberSeparator
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("numberseparator");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("numberseparator", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the Office Apps auto deployment is enabled for the organization.
		/// </summary>
        [AttributeLogicalName("officeappsautodeploymentenabled")]
        public bool? OfficeAppsAutoDeploymentEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("officeappsautodeploymentenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("officeappsautodeploymentenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The url to open the Delve for the organization.
		/// </summary>
        [AttributeLogicalName("officegraphdelveurl")]
        public string? OfficeGraphDelveUrl
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("officegraphdelveurl");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("officegraphdelveurl", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable OOB pricing calculation logic for Opportunity, Quote, Order and Invoice entities.
		/// </summary>
        [AttributeLogicalName("oobpricecalculationenabled")]
        public bool? OOBPriceCalculationEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("oobpricecalculationenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("oobpricecalculationenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates if this organization will opt-out from automatically enabling schema v2 on the organization.
		/// </summary>
        [AttributeLogicalName("optoutschemav2enabledbydefault")]
        public bool? OptOutSchemaV2EnabledByDefault
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("optoutschemav2enabledbydefault");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("optoutschemav2enabledbydefault", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Prefix to use for all orders throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("orderprefix")]
        public string? OrderPrefix
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("orderprefix");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("orderprefix", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates the organization lifecycle state
		/// </summary>
        [AttributeLogicalName("organizationstate")]
        public OptionSetValue? OrganizationState
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("organizationstate");
            }
        }

        /// <summary>
		/// Organization settings stored in Organization Database.
		/// </summary>
        [AttributeLogicalName("orgdborgsettings")]
        public string? OrgDbOrgSettings
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("orgdborgsettings");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("orgdborgsettings", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether to turn on OrgInsights for the organization.
		/// </summary>
        [AttributeLogicalName("orginsightsenabled")]
        public bool? OrgInsightsEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("orginsightsenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("orginsightsenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Preview feature has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("paipreviewscenarioenabled")]
        public bool? PaiPreviewScenarioEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("paipreviewscenarioenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("paipreviewscenarioenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Prefix used for parsed table columns.
		/// </summary>
        [AttributeLogicalName("parsedtablecolumnprefix")]
        public string? ParsedTableColumnPrefix
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("parsedtablecolumnprefix");
            }
        }

        /// <summary>
		/// Prefix used for parsed tables.
		/// </summary>
        [AttributeLogicalName("parsedtableprefix")]
        public string? ParsedTablePrefix
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("parsedtableprefix");
            }
        }

        /// <summary>
		/// Specifies the maximum number of months in past for which the recurring activities can be created.
		/// </summary>
        [AttributeLogicalName("pastexpansionwindow")]
        public int? PastExpansionWindow
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("pastexpansionwindow");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("pastexpansionwindow", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Leave empty to use default setting. Set to on/off to enable/disable replacement of default grids with modern ones in model-driven apps.
		/// </summary>
        [AttributeLogicalName("pcfdatasetgridenabled")]
        public string? PcfDatasetGridEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("pcfdatasetgridenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("pcfdatasetgridenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// This setting contains the date time before an ACT sync can execute.
		/// </summary>
        [AttributeLogicalName("performactsyncafter")]
        public DateTime? PerformACTSyncAfter
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("performactsyncafter");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("performactsyncafter", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("picture")]
        public string? Picture
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("picture");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("picture", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("pinpointlanguagecode")]
        public int? PinpointLanguageCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("pinpointlanguagecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("pinpointlanguagecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Plug-in Trace Log Setting for the Organization.
		/// </summary>
        [AttributeLogicalName("plugintracelogsetting")]
        public OptionSetValue? PluginTraceLogSetting
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("plugintracelogsetting");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("plugintracelogsetting", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// PM designator to use throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("pmdesignator")]
        public string? PMDesignator
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("pmdesignator");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("pmdesignator", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("postmessagewhitelistdomains")]
        public string? PostMessageWhitelistDomains
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("postmessagewhitelistdomains");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("postmessagewhitelistdomains", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether bot for makers is enabled.
		/// </summary>
        [AttributeLogicalName("powerappsmakerbotenabled")]
        public bool? PowerAppsMakerBotEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("powerappsmakerbotenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("powerappsmakerbotenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the Power BI feature should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("powerbifeatureenabled")]
        public bool? PowerBiFeatureEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("powerbifeatureenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("powerbifeatureenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Number of decimal places that can be used for prices.
		/// </summary>
        [AttributeLogicalName("pricingdecimalprecision")]
        public int? PricingDecimalPrecision
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("pricingdecimalprecision");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("pricingdecimalprecision", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Privacy Statement URL
		/// </summary>
        [AttributeLogicalName("privacystatementurl")]
        public string? PrivacyStatementUrl
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("privacystatementurl");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("privacystatementurl", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the default privilege for users in the organization.
		/// </summary>
        [AttributeLogicalName("privilegeusergroupid")]
        public Guid? PrivilegeUserGroupId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("privilegeusergroupid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("privilegeusergroupid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("privreportinggroupid")]
        public Guid? PrivReportingGroupId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("privreportinggroupid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("privreportinggroupid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("privreportinggroupname")]
        public string? PrivReportingGroupName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("privreportinggroupname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("privreportinggroupname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether to turn on product recommendations for the organization.
		/// </summary>
        [AttributeLogicalName("productrecommendationsenabled")]
        public bool? ProductRecommendationsEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("productrecommendationsenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("productrecommendationsenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether prompt should be shown for new Qualify Lead Experience
		/// </summary>
        [AttributeLogicalName("qualifyleadadditionaloptions")]
        public string? QualifyLeadAdditionalOptions
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("qualifyleadadditionaloptions");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("qualifyleadadditionaloptions", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Flag to indicate if the feature to use quick action to open records in search side pane is enabled
		/// </summary>
        [AttributeLogicalName("quickactiontoopenrecordsinsidepaneenabled")]
        public bool? QuickActionToOpenRecordsInSidePaneEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("quickactiontoopenrecordsinsidepaneenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("quickactiontoopenrecordsinsidepaneenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether a quick find record limit should be enabled for this organization (allows for faster Quick Find queries but prevents overly broad searches).
		/// </summary>
        [AttributeLogicalName("quickfindrecordlimitenabled")]
        public bool? QuickFindRecordLimitEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("quickfindrecordlimitenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("quickfindrecordlimitenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Prefix to use for all quotes throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("quoteprefix")]
        public string? QuotePrefix
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("quoteprefix");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("quoteprefix", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether SLA Recalculation has been enabled for the organization
		/// </summary>
        [AttributeLogicalName("recalculatesla")]
        public bool? RecalculateSLA
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("recalculatesla");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("recalculatesla", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Specifies the default value for number of occurrences field in the recurrence dialog.
		/// </summary>
        [AttributeLogicalName("recurrencedefaultnumberofoccurrences")]
        public int? RecurrenceDefaultNumberOfOccurrences
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("recurrencedefaultnumberofoccurrences");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("recurrencedefaultnumberofoccurrences", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Specifies the interval (in seconds) for pausing expansion job.
		/// </summary>
        [AttributeLogicalName("recurrenceexpansionjobbatchinterval")]
        public int? RecurrenceExpansionJobBatchInterval
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("recurrenceexpansionjobbatchinterval");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("recurrenceexpansionjobbatchinterval", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Specifies the value for number of instances created in on demand job in one shot.
		/// </summary>
        [AttributeLogicalName("recurrenceexpansionjobbatchsize")]
        public int? RecurrenceExpansionJobBatchSize
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("recurrenceexpansionjobbatchsize");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("recurrenceexpansionjobbatchsize", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Specifies the maximum number of instances to be created synchronously after creating a recurring appointment.
		/// </summary>
        [AttributeLogicalName("recurrenceexpansionsynchcreatemax")]
        public int? RecurrenceExpansionSynchCreateMax
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("recurrenceexpansionsynchcreatemax");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("recurrenceexpansionsynchcreatemax", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// XML string that defines the navigation structure for the application. This is the site map from the previously upgraded build and is used in a 3-way merge during upgrade.
		/// </summary>
        [AttributeLogicalName("referencesitemapxml")]
        public string? ReferenceSiteMapXml
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("referencesitemapxml");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("referencesitemapxml", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Current orgnization release cadence value
		/// </summary>
        [AttributeLogicalName("releasecadence")]
        public int? ReleaseCadence
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("releasecadence");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("releasecadence", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Model app refresh channel
		/// </summary>
        [AttributeLogicalName("releasechannel")]
        public OptionSetValue? ReleaseChannel
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("releasechannel");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("releasechannel", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Release Wave Applied to Environment.
		/// </summary>
        [AttributeLogicalName("releasewavename")]
        public string? ReleaseWaveName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("releasewavename");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("releasewavename", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether relevance search was enabled for the environment as part of Dataverse's relevance search on-by-default sweep
		/// </summary>
        [AttributeLogicalName("relevancesearchenabledbyplatform")]
        public bool? RelevanceSearchEnabledByPlatform
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("relevancesearchenabledbyplatform");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("relevancesearchenabledbyplatform", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// This setting contains the last modified date for relevance search setting that appears as a toggle in PPAC.
		/// </summary>
        [AttributeLogicalName("relevancesearchmodifiedon")]
        public DateTime? RelevanceSearchModifiedOn
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("relevancesearchmodifiedon");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("relevancesearchmodifiedon", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Flag to render the body of email in the Web form in an IFRAME with the security='restricted' attribute set. This is additional security but can cause a credentials prompt.
		/// </summary>
        [AttributeLogicalName("rendersecureiframeforemail")]
        public bool? RenderSecureIFrameForEmail
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("rendersecureiframeforemail");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("rendersecureiframeforemail", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("reportinggroupid")]
        public Guid? ReportingGroupId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("reportinggroupid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("reportinggroupid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("reportinggroupname")]
        public string? ReportingGroupName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("reportinggroupname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("reportinggroupname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Picklist for selecting the organization preference for reporting scripting errors.
		/// </summary>
        [AttributeLogicalName("reportscripterrors")]
        public OptionSetValue? ReportScriptErrors
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("reportscripterrors");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("reportscripterrors", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Send As Other User privilege is enabled.
		/// </summary>
        [AttributeLogicalName("requireapprovalforqueueemail")]
        public bool? RequireApprovalForQueueEmail
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("requireapprovalforqueueemail");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("requireapprovalforqueueemail", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Send As Other User privilege is enabled.
		/// </summary>
        [AttributeLogicalName("requireapprovalforuseremail")]
        public bool? RequireApprovalForUserEmail
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("requireapprovalforuseremail");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("requireapprovalforuseremail", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Apply same email address to all unresolved matches when you manually resolve it for one
		/// </summary>
        [AttributeLogicalName("resolvesimilarunresolvedemailaddress")]
        public bool? ResolveSimilarUnresolvedEmailAddress
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("resolvesimilarunresolvedemailaddress");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("resolvesimilarunresolvedemailaddress", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether guest user restriction is enabled
		/// </summary>
        [AttributeLogicalName("restrictGuestUserAccess")]
        public bool? RestrictGuestUserAccess
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("restrictGuestUserAccess");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("restrictGuestUserAccess", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Flag to restrict Update on incident.
		/// </summary>
        [AttributeLogicalName("restrictstatusupdate")]
        public bool? RestrictStatusUpdate
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("restrictstatusupdate");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("restrictstatusupdate", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies Reverse Proxy IP addresses from which requests have to be allowed.
		/// </summary>
        [AttributeLogicalName("reverseproxyipaddresses")]
        public string? ReverseProxyIpAddresses
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("reverseproxyipaddresses");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("reverseproxyipaddresses", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Error status of Relationship Insights provisioning.
		/// </summary>
        [AttributeLogicalName("rierrorstatus")]
        public int? RiErrorStatus
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("rierrorstatus");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("rierrorstatus", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Samesite mode for Session Cookie 0 is Default, 1 is None, 2 is Lax , 3 is Strict
		/// </summary>
        [AttributeLogicalName("samesitemodeforsessioncookie")]
        public OptionSetValue? SameSiteModeForSessionCookie
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("samesitemodeforsessioncookie");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("samesitemodeforsessioncookie", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the sample data import job.
		/// </summary>
        [AttributeLogicalName("sampledataimportid")]
        public Guid? SampleDataImportId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("sampledataimportid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sampledataimportid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Default time to live in minutes for new Power Automate savings events records in flow aggregation.
		/// </summary>
        [AttributeLogicalName("savingeventsttlinminutes")]
        public int? SavingEventsTTLInMinutes
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("savingeventsttlinminutes");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("savingeventsttlinminutes", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Prefix used for custom entities and attributes.
		/// </summary>
        [AttributeLogicalName("schemanameprefix")]
        public string? SchemaNamePrefix
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("schemanameprefix");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("schemanameprefix", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether Send Bulk Email in UCI is enabled for the org.
		/// </summary>
        [AttributeLogicalName("sendbulkemailinuci")]
        public bool? SendBulkEmailInUCI
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("sendbulkemailinuci");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sendbulkemailinuci", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Serve Static Content From CDN
		/// </summary>
        [AttributeLogicalName("servestaticresourcesfromazurecdn")]
        public bool? ServeStaticResourcesFromAzureCDN
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("servestaticresourcesfromazurecdn");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("servestaticresourcesfromazurecdn", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable the session recording feature to record user sessions in UCI
		/// </summary>
        [AttributeLogicalName("sessionrecordingenabled")]
        public bool? SessionRecordingEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("sessionrecordingenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sessionrecordingenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether session timeout is enabled
		/// </summary>
        [AttributeLogicalName("sessiontimeoutenabled")]
        public bool? SessionTimeoutEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("sessiontimeoutenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sessiontimeoutenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Session timeout in minutes
		/// </summary>
        [AttributeLogicalName("sessiontimeoutinmins")]
        public int? SessionTimeoutInMins
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("sessiontimeoutinmins");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sessiontimeoutinmins", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Session timeout reminder in minutes
		/// </summary>
        [AttributeLogicalName("sessiontimeoutreminderinmins")]
        public int? SessionTimeoutReminderInMins
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("sessiontimeoutreminderinmins");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sessiontimeoutreminderinmins", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates which SharePoint deployment type is configured for Server to Server. (Online or On-Premises)
		/// </summary>
        [AttributeLogicalName("sharepointdeploymenttype")]
        public OptionSetValue? SharePointDeploymentType
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("sharepointdeploymenttype");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sharepointdeploymenttype", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether to share to previous owner on assign.
		/// </summary>
        [AttributeLogicalName("sharetopreviousowneronassign")]
        public bool? ShareToPreviousOwnerOnAssign
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("sharetopreviousowneronassign");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sharetopreviousowneronassign", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether to display a KB article deprecation notification to the user.
		/// </summary>
        [AttributeLogicalName("showkbarticledeprecationnotification")]
        public bool? ShowKBArticleDeprecationNotification
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("showkbarticledeprecationnotification");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("showkbarticledeprecationnotification", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether to display the week number in calendar displays throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("showweeknumber")]
        public bool? ShowWeekNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("showweeknumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("showweeknumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// CRM for Outlook Download URL
		/// </summary>
        [AttributeLogicalName("signupoutlookdownloadfwlink")]
        public string? SignupOutlookDownloadFWLink
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("signupoutlookdownloadfwlink");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("signupoutlookdownloadfwlink", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// XML string that defines the navigation structure for the application.
		/// </summary>
        [AttributeLogicalName("sitemapxml")]
        public string? SiteMapXml
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("sitemapxml");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sitemapxml", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Contains the on hold case status values.
		/// </summary>
        [AttributeLogicalName("slapausestates")]
        public string? SlaPauseStates
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("slapausestates");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("slapausestates", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Flag for whether the organization is using Social Insights.
		/// </summary>
        [AttributeLogicalName("socialinsightsenabled")]
        public bool? SocialInsightsEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("socialinsightsenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("socialinsightsenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Identifier for the Social Insights instance for the organization.
		/// </summary>
        [AttributeLogicalName("socialinsightsinstance")]
        public string? SocialInsightsInstance
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("socialinsightsinstance");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("socialinsightsinstance", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Flag for whether the organization has accepted the Social Insights terms of use.
		/// </summary>
        [AttributeLogicalName("socialinsightstermsaccepted")]
        public bool? SocialInsightsTermsAccepted
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("socialinsightstermsaccepted");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("socialinsightstermsaccepted", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("sortid")]
        public int? SortId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("sortid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sortid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("sqlaccessgroupid")]
        public Guid? SqlAccessGroupId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("sqlaccessgroupid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sqlaccessgroupid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("sqlaccessgroupname")]
        public string? SqlAccessGroupName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("sqlaccessgroupname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sqlaccessgroupname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Setting for SQM data collection, 0 no, 1 yes enabled
		/// </summary>
        [AttributeLogicalName("sqmenabled")]
        public bool? SQMEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("sqmenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sqmenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the support user for the organization.
		/// </summary>
        [AttributeLogicalName("supportuserid")]
        public Guid? SupportUserId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("supportuserid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("supportuserid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether SLA is suppressed.
		/// </summary>
        [AttributeLogicalName("suppresssla")]
        public bool? SuppressSLA
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("suppresssla");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("suppresssla", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Leave empty to use default setting. Set to on/off to enable/disable Admin emails when Solution Checker validation fails.
		/// </summary>
        [AttributeLogicalName("suppressvalidationemails")]
        public bool? SuppressValidationEmails
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("suppressvalidationemails");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("suppressvalidationemails", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Number of records to update per operation in Sync Bulk Pause/Resume/Cancel
		/// </summary>
        [AttributeLogicalName("syncbulkoperationbatchsize")]
        public int? SyncBulkOperationBatchSize
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("syncbulkoperationbatchsize");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("syncbulkoperationbatchsize", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Max total number of records to update in database for Sync Bulk Pause/Resume/Cancel
		/// </summary>
        [AttributeLogicalName("syncbulkoperationmaxlimit")]
        public int? SyncBulkOperationMaxLimit
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("syncbulkoperationmaxlimit");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("syncbulkoperationmaxlimit", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates the selection to use the dynamics 365 azure sync framework or server side sync.
		/// </summary>
        [AttributeLogicalName("syncoptinselection")]
        public bool? SyncOptInSelection
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("syncoptinselection");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("syncoptinselection", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates the status of the opt-in or opt-out operation for dynamics 365 azure sync.
		/// </summary>
        [AttributeLogicalName("syncoptinselectionstatus")]
        public OptionSetValue? SyncOptInSelectionStatus
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("syncoptinselectionstatus");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("syncoptinselectionstatus", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the system user for the organization.
		/// </summary>
        [AttributeLogicalName("systemuserid")]
        public Guid? SystemUserId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("systemuserid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("systemuserid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Controls the appearance of option to search over a single DV search indexed table in model-driven apps’ global search in the header.
		/// </summary>
        [AttributeLogicalName("tablescopeddvsearchinapps")]
        public bool? TableScopedDVSearchInApps
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("tablescopeddvsearchinapps");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("tablescopeddvsearchinapps", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Maximum number of aggressive polling cycles executed for email auto-tagging when a new email is received.
		/// </summary>
        [AttributeLogicalName("tagmaxaggressivecycles")]
        public int? TagMaxAggressiveCycles
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("tagmaxaggressivecycles");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("tagmaxaggressivecycles", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Normal polling frequency used for email receive auto-tagging in outlook.
		/// </summary>
        [AttributeLogicalName("tagpollingperiod")]
        public int? TagPollingPeriod
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("tagpollingperiod");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("tagpollingperiod", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether to turn on task flows for the organization.
		/// </summary>
        [AttributeLogicalName("taskbasedflowenabled")]
        public bool? TaskBasedFlowEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("taskbasedflowenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("taskbasedflowenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information on whether Teams Chat Data Sync is enabled.
		/// </summary>
        [AttributeLogicalName("teamschatdatasync")]
        public bool? TeamsChatDataSync
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("teamschatdatasync");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("teamschatdatasync", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Instrumentation key for Application Insights used to log plugins telemetry.
		/// </summary>
        [AttributeLogicalName("telemetryinstrumentationkey")]
        public string? TelemetryInstrumentationKey
        {
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("telemetryinstrumentationkey", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether to turn on text analytics for the organization.
		/// </summary>
        [AttributeLogicalName("textanalyticsenabled")]
        public bool? TextAnalyticsEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("textanalyticsenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("textanalyticsenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies how the time is displayed throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("timeformatcode")]
        public OptionSetValue? TimeFormatCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("timeformatcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("timeformatcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Text for how time is displayed in Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("timeformatstring")]
        public string? TimeFormatString
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("timeformatstring");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("timeformatstring", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Text for how the time separator is displayed throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("timeseparator")]
        public string? TimeSeparator
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("timeseparator");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("timeseparator", value);
                OnPropertyChanged();
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
		/// Duration used for token expiration.
		/// </summary>
        [AttributeLogicalName("tokenexpiry")]
        public int? TokenExpiry
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("tokenexpiry");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("tokenexpiry", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Token key.
		/// </summary>
        [AttributeLogicalName("tokenkey")]
        public string? TokenKey
        {
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("tokenkey", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Tracelog record maximum age in days
		/// </summary>
        [AttributeLogicalName("tracelogmaximumageindays")]
        public int? TraceLogMaximumAgeInDays
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("tracelogmaximumageindays");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("tracelogmaximumageindays", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// History list of tracking token prefixes.
		/// </summary>
        [AttributeLogicalName("trackingprefix")]
        public string? TrackingPrefix
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("trackingprefix");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("trackingprefix", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Base number used to provide separate tracking token identifiers to users belonging to different deployments.
		/// </summary>
        [AttributeLogicalName("trackingtokenidbase")]
        public int? TrackingTokenIdBase
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("trackingtokenidbase");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("trackingtokenidbase", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Number of digits used to represent a tracking token identifier.
		/// </summary>
        [AttributeLogicalName("trackingtokeniddigits")]
        public int? TrackingTokenIdDigits
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("trackingtokeniddigits");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("trackingtokeniddigits", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Number of characters appended to invoice, quote, and order numbers.
		/// </summary>
        [AttributeLogicalName("uniquespecifierlength")]
        public int? UniqueSpecifierLength
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("uniquespecifierlength");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("uniquespecifierlength", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether email address should be unresolved if multiple matches are found
		/// </summary>
        [AttributeLogicalName("unresolveemailaddressifmultiplematch")]
        public bool? UnresolveEmailAddressIfMultipleMatch
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("unresolveemailaddressifmultiplematch");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("unresolveemailaddressifmultiplematch", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Flag indicates whether to Use Inbuilt Rule For DefaultPricelist.
		/// </summary>
        [AttributeLogicalName("useinbuiltrulefordefaultpricelistselection")]
        public bool? UseInbuiltRuleForDefaultPricelistSelection
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("useinbuiltrulefordefaultpricelistselection");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("useinbuiltrulefordefaultpricelistselection", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether to use legacy form rendering.
		/// </summary>
        [AttributeLogicalName("uselegacyrendering")]
        public bool? UseLegacyRendering
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("uselegacyrendering");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("uselegacyrendering", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Use position hierarchy
		/// </summary>
        [AttributeLogicalName("usepositionhierarchy")]
        public bool? UsePositionHierarchy
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("usepositionhierarchy");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("usepositionhierarchy", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether searching in a grid should use the Quick Find view for the entity.
		/// </summary>
        [AttributeLogicalName("usequickfindviewforgridsearch")]
        public bool? UseQuickFindViewForGridSearch
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("usequickfindviewforgridsearch");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("usequickfindviewforgridsearch", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The interval at which user access is checked for auditing.
		/// </summary>
        [AttributeLogicalName("useraccessauditinginterval")]
        public int? UserAccessAuditingInterval
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("useraccessauditinginterval");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("useraccessauditinginterval", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the read-optimized form should be enabled for this organization.
		/// </summary>
        [AttributeLogicalName("usereadform")]
        public bool? UseReadForm
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("usereadform");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("usereadform", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the default group of users in the organization.
		/// </summary>
        [AttributeLogicalName("usergroupid")]
        public Guid? UserGroupId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("usergroupid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("usergroupid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enable the user rating feature to show the NSAT score and comment to maker
		/// </summary>
        [AttributeLogicalName("userratingenabled")]
        public bool? UserRatingEnabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("userratingenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("userratingenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates default protocol selected for organization.
		/// </summary>
        [AttributeLogicalName("useskypeprotocol")]
        public bool? UseSkypeProtocol
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("useskypeprotocol");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("useskypeprotocol", value);
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

        /// <summary>
		/// Hash of the V3 callout configuration file.
		/// </summary>
        [AttributeLogicalName("v3calloutconfighash")]
        public string? V3CalloutConfigHash
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("v3calloutconfighash");
            }
        }

        /// <summary>
		/// Validation mode for apps in this environment
		/// </summary>
        [AttributeLogicalName("validationmode")]
        public OptionSetValue? ValidationMode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("validationmode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("validationmode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Version number of the organization.
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

        /// <summary>
		/// Hash value of web resources.
		/// </summary>
        [AttributeLogicalName("webresourcehash")]
        public string? WebResourceHash
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("webresourcehash");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("webresourcehash", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Designated first day of the week throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("weekstartdaycode")]
        public OptionSetValue? WeekStartDayCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("weekstartdaycode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("weekstartdaycode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For Internal use only.
		/// </summary>
        [AttributeLogicalName("widgetproperties")]
        public string? WidgetProperties
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("widgetproperties");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("widgetproperties", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Denotes the Yammer group ID
		/// </summary>
        [AttributeLogicalName("yammergroupid")]
        public int? YammerGroupId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("yammergroupid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("yammergroupid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Denotes the Yammer network permalink
		/// </summary>
        [AttributeLogicalName("yammernetworkpermalink")]
        public string? YammerNetworkPermalink
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("yammernetworkpermalink");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("yammernetworkpermalink", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Denotes whether the OAuth access token for Yammer network has expired
		/// </summary>
        [AttributeLogicalName("yammeroauthaccesstokenexpired")]
        public bool? YammerOAuthAccessTokenExpired
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("yammeroauthaccesstokenexpired");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("yammeroauthaccesstokenexpired", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Internal Use Only
		/// </summary>
        [AttributeLogicalName("yammerpostmethod")]
        public OptionSetValue? YammerPostMethod
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("yammerpostmethod");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("yammerpostmethod", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies how the first week of the year is specified in Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("yearstartweekcode")]
        public int? YearStartWeekCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("yearstartweekcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("yearstartweekcode", value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N lk_documenttemplatebase_organization
        /// </summary>
        [RelationshipSchemaName("lk_documenttemplatebase_organization")]
        public IEnumerable<DocumentTemplate> LkDocumenttemplatebaseOrganization
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<DocumentTemplate>("lk_documenttemplatebase_organization", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_documenttemplatebase_organization", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N Organization_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("Organization_AsyncOperations")]
        public IEnumerable<AsyncOperation> OrganizationAsyncOperations
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("Organization_AsyncOperations", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("Organization_AsyncOperations", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_business_units
        /// </summary>
        [RelationshipSchemaName("organization_business_units")]
        public IEnumerable<BusinessUnit> OrganizationBusinessUnits
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<BusinessUnit>("organization_business_units", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_business_units", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_calendars
        /// </summary>
        [RelationshipSchemaName("organization_calendars")]
        public IEnumerable<Calendar> OrganizationCalendars
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Calendar>("organization_calendars", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_calendars", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_importjob
        /// </summary>
        [RelationshipSchemaName("organization_importjob")]
        public IEnumerable<ImportJob> OrganizationImportjob
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<ImportJob>("organization_importjob", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_importjob", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_pluginassembly
        /// </summary>
        [RelationshipSchemaName("organization_pluginassembly")]
        public IEnumerable<PluginAssembly> OrganizationPluginassembly
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<PluginAssembly>("organization_pluginassembly", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_pluginassembly", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_pluginpackage
        /// </summary>
        [RelationshipSchemaName("organization_pluginpackage")]
        public IEnumerable<PluginPackage> OrganizationPluginpackage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<PluginPackage>("organization_pluginpackage", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_pluginpackage", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_plugintype
        /// </summary>
        [RelationshipSchemaName("organization_plugintype")]
        public IEnumerable<PluginType> OrganizationPlugintype
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<PluginType>("organization_plugintype", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_plugintype", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_publisher
        /// </summary>
        [RelationshipSchemaName("organization_publisher")]
        public IEnumerable<Publisher> OrganizationPublisher
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Publisher>("organization_publisher", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_publisher", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_queues
        /// </summary>
        [RelationshipSchemaName("organization_queues")]
        public IEnumerable<Queue> OrganizationQueues
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Queue>("organization_queues", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_queues", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_roles
        /// </summary>
        [RelationshipSchemaName("organization_roles")]
        public IEnumerable<Role> OrganizationRoles
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Role>("organization_roles", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_roles", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_routingruleitems
        /// </summary>
        [RelationshipSchemaName("organization_routingruleitems")]
        public IEnumerable<RoutingRuleItem> OrganizationRoutingruleitems
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<RoutingRuleItem>("organization_routingruleitems", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_routingruleitems", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_RoutingRules
        /// </summary>
        [RelationshipSchemaName("organization_RoutingRules")]
        public IEnumerable<RoutingRule> OrganizationRoutingRules
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<RoutingRule>("organization_RoutingRules", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_RoutingRules", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_saved_queries
        /// </summary>
        [RelationshipSchemaName("organization_saved_queries")]
        public IEnumerable<SavedQuery> OrganizationSavedQueries
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SavedQuery>("organization_saved_queries", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_saved_queries", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_sdkmessage
        /// </summary>
        [RelationshipSchemaName("organization_sdkmessage")]
        public IEnumerable<SdkMessage> OrganizationSdkmessage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessage>("organization_sdkmessage", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_sdkmessage", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_sdkmessagefilter
        /// </summary>
        [RelationshipSchemaName("organization_sdkmessagefilter")]
        public IEnumerable<SdkMessageFilter> OrganizationSdkmessagefilter
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageFilter>("organization_sdkmessagefilter", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_sdkmessagefilter", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_sdkmessageprocessingstep
        /// </summary>
        [RelationshipSchemaName("organization_sdkmessageprocessingstep")]
        public IEnumerable<SdkMessageProcessingStep> OrganizationSdkmessageprocessingstep
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStep>("organization_sdkmessageprocessingstep", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_sdkmessageprocessingstep", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_sdkmessageprocessingstepimage
        /// </summary>
        [RelationshipSchemaName("organization_sdkmessageprocessingstepimage")]
        public IEnumerable<SdkMessageProcessingStepImage> OrganizationSdkmessageprocessingstepimage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStepImage>("organization_sdkmessageprocessingstepimage", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_sdkmessageprocessingstepimage", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_sdkmessageprocessingstepsecureconfig
        /// </summary>
        [RelationshipSchemaName("organization_sdkmessageprocessingstepsecureconfig")]
        public IEnumerable<SdkMessageProcessingStepSecureConfig> OrganizationSdkmessageprocessingstepsecureconfig
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStepSecureConfig>("organization_sdkmessageprocessingstepsecureconfig", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_sdkmessageprocessingstepsecureconfig", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_solution
        /// </summary>
        [RelationshipSchemaName("organization_solution")]
        public IEnumerable<Solution> OrganizationSolution
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Solution>("organization_solution", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_solution", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_system_users
        /// </summary>
        [RelationshipSchemaName("organization_system_users")]
        public IEnumerable<SystemUser> OrganizationSystemUsers
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SystemUser>("organization_system_users", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_system_users", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_systemforms
        /// </summary>
        [RelationshipSchemaName("organization_systemforms")]
        public IEnumerable<SystemForm> OrganizationSystemforms
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SystemForm>("organization_systemforms", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_systemforms", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N organization_teams
        /// </summary>
        [RelationshipSchemaName("organization_teams")]
        public IEnumerable<Team> OrganizationTeams
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Team>("organization_teams", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("organization_teams", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N webresource_organization
        /// </summary>
        [RelationshipSchemaName("webresource_organization")]
        public IEnumerable<WebResource> WebresourceOrganization
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<WebResource>("webresource_organization", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("webresource_organization", null, value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region Options
        public static partial class Options
        {
            public struct ActivityTypeFilter
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct ActivityTypeFilterV2
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AdvancedColumnEditorEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AdvancedColumnFilteringEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AdvancedFilteringEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AdvancedLookupEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AiBuilderCreditsOnlyEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AiPromptsAzureAIFoundryModelTypesEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AiPromptsBasicModelTypesEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AiPromptsEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AiPromptsPremiumModelTypesEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AiPromptsStandardModelTypesEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowAddressBookSyncs
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowApplicationUserAccess
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowAutoResponseCreation
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowAutoUnsubscribe
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowAutoUnsubscribeAcknowledgement
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowClientMessageBarAd
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowConnectorsOnPowerFXActions
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowEntityOnlyAudit
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowLeadingWildcardsInGridSearch
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowLegacyClientExperience
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowLegacyDialogsEmbedding
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowMarketingEmailExecution
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowMicrosoftTrustedServiceTags
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowOfflineScheduledSyncs
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowOutlookScheduledSyncs
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowRedirectAdminSettingsToModernUI
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowUnresolvedPartiesOnEmailSend
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowUserFormModePreference
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowUsersHidingSystemViews
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowUsersSeeAppdownloadMessage
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowVirtualEntityPluginExecutionOnNestedPipeline
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AllowWebExcelExport
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AppDesignerExperienceEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct ApplicationBasedAccessControlMode
            {
                public const int Disabled = 0;
                public const int Enabled = 1;
                public const int AuditMode = 2;
                public const int EnabledForRoles = 3;
            }
            public struct AppointmentRichEditorExperience
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AppointmentWithTeamsMeeting
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AppointmentWithTeamsMeetingV2
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AreAutomationCenterPreviewFeaturesEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AreProcessInsightsPreviewFeaturesEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AutoApplyDefaultonCaseCreate
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AutoApplyDefaultonCaseUpdate
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AutoApplySLA
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct BlockAccessToSessionTranscriptsForCopilotStudio
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct BlockCopilotAuthorAuthentication
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct BlockTranscriptRecordingForCopilotStudio
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct BlockUrlsInResponsesForCopilotStudio
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct BoundDashboardDefaultCardExpanded
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct CanOptOutNewSearchExperience
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct CascadeStatusUpdate
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct CortanaProactiveExperienceEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct CreateProductsWithoutParentInActiveState
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct CuaFlowLogsVerbosity
            {
                public const int AllData = 0;
                public const int DataWithoutScreenshots = 1;
                public const int Minimal = 2;
            }
            public struct CurrencyDisplayOption
            {
                public const int CurrencySymbol = 0;
                public const int CurrencyCode = 1;
            }
            public struct CurrencyFormatCode
            {
                public const int _123 = 0;
                public const int _123_ = 1;
                public const int _123__ = 2;
                public const int _123___ = 3;
            }
            public struct DateFormatCode
            {
            }
            public struct DefaultRecurrenceEndRangeType
            {
                public const int NoEndDate = 1;
                public const int NumberOfOccurrences = 2;
                public const int EndByDate = 3;
            }
            public struct DesktopFlowRunActionLogsStatus
            {
                public const int Enabled = 0;
                public const int OnFailure = 1;
                public const int Disabled = 2;
            }
            public struct DesktopFlowRunActionLogVerbosity
            {
                public const int Full = 0;
                public const int Debug = 1;
                public const int Custom = 2;
                public const int Warning = 3;
                public const int Error = 4;
            }
            public struct DesktopFlowRunActionLogVersion
            {
                public const int AdditionalContext = 0;
                public const int FlowLogs = 1;
                public const int AdditionalContextAndFlowLogs = 2;
            }
            public struct DisableSocialCare
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct DisableSystemLabelsCacheSharing
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct DiscountCalculationMethod
            {
                public const int LineItem = 0;
                public const int PerUnit = 1;
            }
            public struct DisplayNavigationTour
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EmailConnectionChannel
            {
                public const int ServerSideSynchronization = 0;
                public const int MicrosoftDynamics365EmailRouter = 1;
            }
            public struct EmailCorrelationEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableAsyncMergeAPIForUCI
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableBingMapsIntegration
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableCanvasAppsInSolutionsByDefault
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableCopilotStudioCrossGeoShareDataWithVivaInsights
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableCopilotStudioShareDataWithVI
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableCopilotStudioShareDataWithVivaInsights
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableEnvironmentSettingsApp
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableFlowsInSolutionByDefault
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableFlowsInSolutionByDefaultGracePeriod
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableImmersiveSkypeIntegration
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableIpBasedCookieBinding
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableIpBasedFirewallRule
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableIpBasedFirewallRuleInAuditMode
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableIpBasedStorageAccessSignatureRule
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableLivePersonaCardUCI
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableLivePersonCardIntegrationInOffice
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableLPAuthoring
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableMakerSwitchToClassic
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableMicrosoftFlowIntegration
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnablePricingOnCreate
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableSensitivityLabels
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableSmartMatching
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableUnifiedClientCDN
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnableUnifiedInterfaceShellRefresh
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EnforceReadOnlyPlugins
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct FiscalPeriodFormatPeriod
            {
                public const int Quarter0 = 1;
                public const int Q0 = 2;
                public const int P0 = 3;
                public const int Month0 = 4;
                public const int M0 = 5;
                public const int Semester0 = 6;
                public const int MonthName = 7;
            }
            public struct FiscalSettingsUpdated
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct FiscalYearFormatPrefix
            {
                public const int FY = 1;
                public const int _Empty = 2;
            }
            public struct FiscalYearFormatSuffix
            {
                public const int FY = 1;
                public const int FiscalYear = 2;
                public const int _Empty = 3;
            }
            public struct FiscalYearFormatYear
            {
                public const int YYYY = 1;
                public const int YY = 2;
                public const int GGYY = 3;
            }
            public struct FullNameConventionCode
            {
                public const int LastNameFirstName = 0;
                public const int FirstName = 1;
                public const int LastNameFirstNameMiddleInitial = 2;
                public const int FirstNameMiddleInitialLastName = 3;
                public const int LastNameFirstNameMiddleName = 4;
                public const int FirstNameMiddleNameLastName = 5;
                public const int LastNameSpaceFirstName = 6;
                public const int LastNameNoSpaceFirstName = 7;
            }
            public struct GenerateAlertsForErrors
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct GenerateAlertsForInformation
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct GenerateAlertsForWarnings
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct GetStartedPaneContentEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct GlobalAppendUrlParametersEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct GlobalHelpUrlEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct GrantAccessToNetworkService
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IgnoreInternalEmail
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct ImproveSearchLoggingEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct InactivityTimeoutEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IpBasedStorageAccessSignatureMode
            {
                public const int IPBindingOnly = 0;
                public const int IPFirewallOnly = 1;
                public const int IPBindingAndIPFirewall = 2;
                public const int IPBindingOrIPFirewall = 3;
            }
            public struct IsActionCardEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsActionSupportFeatureEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsActivityAnalysisEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsAllMoneyDecimal
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsAppMode
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsAppointmentAttachmentSyncEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsAssignedTasksSyncEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsAuditEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsAutoDataCaptureEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsAutoDataCaptureV2Enabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsAutoInstallAppForD365InTeamsEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsAutoSaveEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsBaseCardStaticFieldDataEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsBasicGeospatialIntegrationEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsBPFEntityCustomizationFeatureEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsCloudFlowSavingsEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsClusteringEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsCollaborationExperienceEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsComputerUseInMCSEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsConflictDetectionEnabledForMobileClient
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsContactMailingAddressSyncEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsContentSecurityPolicyEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsContentSecurityPolicyEnabledForCanvas
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsContextualEmailEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsContextualHelpEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsCopilotFeedbackEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsCuaOnHmgV2Enabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsCustomControlsInCanvasAppsEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDefaultCountryCodeCheckEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDelegateAccessEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDelveActionHubIntegrationEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDesktopFlowConnectionEmbeddingEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDesktopFlowRuntimeRepairAttendedEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDesktopFlowRuntimeRepairUnattendedEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDesktopFlowSavingsEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDesktopFlowSchemaV2Enabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDesktopFlowVanillaImageSharingEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDesktopFlowVersionControlEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDesktopFlowVersionControlEnabledByDefault
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDesktopFlowVersionControlEnabledOverride
            {
                public const int Unset = 0;
                public const int Enabled = 1;
                public const int Disabled = 2;
            }
            public struct IsDisabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDuplicateDetectionEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDuplicateDetectionEnabledForImport
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDuplicateDetectionEnabledForOfflineSync
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDuplicateDetectionEnabledForOnlineCreateUpdate
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsEmailAddressValidationEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsEmailMonitoringAllowed
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsEmailServerProfileContentFilteringEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsEnabledForAllRoles
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsExternalFileStorageEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsExternalSearchIndexEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsFiscalPeriodMonthBased
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsFolderAutoCreatedonSP
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsFolderBasedTrackingEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsFullTextSearchEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsGeospatialAzureMapsIntegrationEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsHierarchicalSecurityModelEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsIdeasDataCollectionEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsLUISEnabledforD365Bot
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsMailboxForcedUnlockingEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsMailboxInactiveBackoffEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsManualSalesForecastingEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsMobileClientOnDemandSyncEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsMobileOfflineEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsModelDrivenAppsInMSTeamsEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsMoneySavingsAllowed
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsMSTeamsCollaborationEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsMSTeamsEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsMSTeamsSettingChangedByUser
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsMSTeamsUserSyncEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsNewAddProductExperienceEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsNotesAnalysisEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsNotificationForD365InTeamsEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsOfficeGraphEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsOneDriveEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsPAIEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsPerProcessCapacityOverageEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsPlaybookEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsPresenceEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsPreviewEnabledForActionCard
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsPreviewForAutoCaptureEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsPreviewForEmailMonitoringAllowed
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsPriceListMandatory
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsProcessCapacityAutoClaimEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsProcessMiningEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsQuickCreateEnabledForOpportunityClose
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsReadAuditEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsRelationshipInsightsEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsResourceBookingExchangeSyncEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsRichTextNotesEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsRpaAutoscaleAadJoinEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsRpaAutoscaleEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsRpaBoxCrossGeoEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsRpaBoxEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsRpaUnattendedEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsSalesAssistantEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsSendCuaAuditLogToPurviewEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsSharingInOrgAllowed
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsSOPIntegrationEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsTextWrapEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsUploadCuaLogToDataverseEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsUserAccessAuditEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct ISVIntegrationCode
            {
                public const int None = 0;
                public const int Web = 1;
                public const int OutlookWorkstationClient = 2;
                public const int WebOutlookWorkstationClient = 3;
                public const int OutlookLaptopClient = 4;
                public const int WebOutlookLaptopClient = 5;
                public const int Outlook = 6;
                public const int All = 7;
            }
            public struct IsWorkQueueSavingsEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsWriteInProductsAllowed
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct LegacyAppToggle
            {
                public const int Auto = 0;
                public const int On = 1;
                public const int Off = 2;
            }
            public struct ModernAdvancedFindFiltering
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct ModernAppDesignerCoauthoringEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct NaturalLanguageAssistFilter
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct NegativeFormatCode
            {
                public const int Brackets = 0;
                public const int Dash = 1;
                public const int DashPlusSpace = 2;
                public const int TrailingDash = 3;
                public const int SpacePlusTrailingDash = 4;
            }
            public struct NewSearchExperienceEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct NotifyMailboxOwnerOfEmailServerLevelAlerts
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct OfficeAppsAutoDeploymentEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct OOBPriceCalculationEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct OptOutSchemaV2EnabledByDefault
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct OrganizationState
            {
                public const int Creating = 0;
                public const int Upgrading = 1;
                public const int Updating = 2;
                public const int Active = 3;
            }
            public struct OrgInsightsEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct PaiPreviewScenarioEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct PluginTraceLogSetting
            {
                public const int Off = 0;
                public const int Exception = 1;
                public const int All = 2;
            }
            public struct PowerAppsMakerBotEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct PowerBiFeatureEnabled
            {
                public const bool Disable = false;
                public const bool Enable = true;
            }
            public struct ProductRecommendationsEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct QuickActionToOpenRecordsInSidePaneEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct QuickFindRecordLimitEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct RecalculateSLA
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct ReleaseChannel
            {
                public const int Auto = 0;
                public const int MonthlyChannel = 1;
                public const int MicrosoftInnerChannel = 2;
                public const int SemiAnnualChannel = 3;
            }
            public struct RelevanceSearchEnabledByPlatform
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct RenderSecureIFrameForEmail
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct ReportScriptErrors
            {
                public const int NoPreferenceForSendingAnErrorReportToMicrosoftAboutMicrosoftDynamics365 = 0;
                public const int AskMeForPermissionToSendAnErrorReportToMicrosoft = 1;
                public const int AutomaticallySendAnErrorReportToMicrosoftWithoutAskingMeForPermission = 2;
                public const int NeverSendAnErrorReportToMicrosoftAboutMicrosoftDynamics365 = 3;
            }
            public struct RequireApprovalForQueueEmail
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct RequireApprovalForUserEmail
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct ResolveSimilarUnresolvedEmailAddress
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct RestrictGuestUserAccess
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct RestrictStatusUpdate
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct SameSiteModeForSessionCookie
            {
                public const int Default = 0;
                public const int None = 1;
                public const int Lax = 2;
                public const int Strict = 3;
            }
            public struct SendBulkEmailInUCI
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct ServeStaticResourcesFromAzureCDN
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct SessionRecordingEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct SessionTimeoutEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct SharePointDeploymentType
            {
                public const int Online = 0;
                public const int OnPremises = 1;
            }
            public struct ShareToPreviousOwnerOnAssign
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct ShowKBArticleDeprecationNotification
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct ShowWeekNumber
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct SocialInsightsEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct SocialInsightsTermsAccepted
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct SQMEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct SuppressSLA
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct SuppressValidationEmails
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct SyncOptInSelection
            {
                public const bool Disable = false;
                public const bool Enable = true;
            }
            public struct SyncOptInSelectionStatus
            {
                public const int Processing = 1;
                public const int Passed = 2;
                public const int Failed = 3;
            }
            public struct TableScopedDVSearchInApps
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct TaskBasedFlowEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct TeamsChatDataSync
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct TextAnalyticsEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct TimeFormatCode
            {
            }
            public struct UnresolveEmailAddressIfMultipleMatch
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct UseInbuiltRuleForDefaultPricelistSelection
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct UseLegacyRendering
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct UsePositionHierarchy
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct UseQuickFindViewForGridSearch
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct UseReadForm
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct UserRatingEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct UseSkypeProtocol
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct ValidationMode
            {
                public const int Off = 0;
                public const int Warn = 1;
                public const int Block = 2;
            }
            public struct WeekStartDayCode
            {
            }
            public struct YammerOAuthAccessTokenExpired
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct YammerPostMethod
            {
                public const int Public = 0;
                public const int Private = 1;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string OrganizationId = "organizationid";
            public const string ACIWebEndpointUrl = "aciwebendpointurl";
            public const string AcknowledgementTemplateId = "acknowledgementtemplateid";
            public const string ActivityTypeFilter = "activitytypefilter";
            public const string ActivityTypeFilterV2 = "activitytypefilterv2";
            public const string AdvancedColumnEditorEnabled = "advancedcolumneditorenabled";
            public const string AdvancedColumnFilteringEnabled = "advancedcolumnfilteringenabled";
            public const string AdvancedFilteringEnabled = "advancedfilteringenabled";
            public const string AdvancedLookupEnabled = "advancedlookupenabled";
            public const string AdvancedLookupInEditFilter = "advancedlookupineditfilter";
            public const string AiBuilderCreditsOnlyEnabled = "aibuildercreditsonlyenabled";
            public const string AiPromptsAzureAIFoundryModelTypesEnabled = "aipromptsazureaifoundrymodeltypesenabled";
            public const string AiPromptsBasicModelTypesEnabled = "aipromptsbasicmodeltypesenabled";
            public const string AiPromptsEnabled = "aipromptsenabled";
            public const string AiPromptsPremiumModelTypesEnabled = "aipromptspremiummodeltypesenabled";
            public const string AiPromptsStandardModelTypesEnabled = "aipromptsstandardmodeltypesenabled";
            public const string AllowAddressBookSyncs = "allowaddressbooksyncs";
            public const string AllowApplicationUserAccess = "allowapplicationuseraccess";
            public const string AllowAutoResponseCreation = "allowautoresponsecreation";
            public const string AllowAutoUnsubscribe = "allowautounsubscribe";
            public const string AllowAutoUnsubscribeAcknowledgement = "allowautounsubscribeacknowledgement";
            public const string AllowClientMessageBarAd = "allowclientmessagebarad";
            public const string AllowConnectorsOnPowerFXActions = "allowconnectorsonpowerfxactions";
            public const string AllowedApplicationsForDVAccess = "allowedapplicationsfordvaccess";
            public const string AllowedIpRangeForFirewall = "allowediprangeforfirewall";
            public const string AllowedIpRangeForStorageAccessSignatures = "allowediprangeforstorageaccesssignatures";
            public const string AllowedListOfIpRangesForFirewall = "allowedlistofiprangesforfirewall";
            public const string AllowedMimeTypes = "allowedmimetypes";
            public const string AllowedServiceTagsForFirewall = "allowedservicetagsforfirewall";
            public const string AllowEntityOnlyAudit = "allowentityonlyaudit";
            public const string AllowLeadingWildcardsInGridSearch = "allowleadingwildcardsingridsearch";
            public const string AllowLeadingWildcardsInQuickFind = "allowleadingwildcardsinquickfind";
            public const string AllowLegacyClientExperience = "allowlegacyclientexperience";
            public const string AllowLegacyDialogsEmbedding = "allowlegacydialogsembedding";
            public const string AllowMarketingEmailExecution = "allowmarketingemailexecution";
            public const string AllowMicrosoftTrustedServiceTags = "allowmicrosofttrustedservicetags";
            public const string AllowOfflineScheduledSyncs = "allowofflinescheduledsyncs";
            public const string AllowOutlookScheduledSyncs = "allowoutlookscheduledsyncs";
            public const string AllowRedirectAdminSettingsToModernUI = "allowredirectadminsettingstomodernui";
            public const string AllowUnresolvedPartiesOnEmailSend = "allowunresolvedpartiesonemailsend";
            public const string AllowUserFormModePreference = "allowuserformmodepreference";
            public const string AllowUsersHidingSystemViews = "allowusershidingsystemviews";
            public const string AllowUsersSeeAppdownloadMessage = "allowusersseeappdownloadmessage";
            public const string AllowVirtualEntityPluginExecutionOnNestedPipeline = "allowvirtualentitypluginexecutiononnestedpipeline";
            public const string AllowWebExcelExport = "allowwebexcelexport";
            public const string AMDesignator = "amdesignator";
            public const string AppDesignerExperienceEnabled = "appdesignerexperienceenabled";
            public const string ApplicationBasedAccessControlMode = "applicationbasedaccesscontrolmode";
            public const string AppointmentRichEditorExperience = "appointmentricheditorexperience";
            public const string AppointmentWithTeamsMeeting = "appointmentwithteamsmeeting";
            public const string AppointmentWithTeamsMeetingV2 = "appointmentwithteamsmeetingv2";
            public const string AreAutomationCenterPreviewFeaturesEnabled = "areautomationcenterpreviewfeaturesenabled";
            public const string AreProcessInsightsPreviewFeaturesEnabled = "areprocessinsightspreviewfeaturesenabled";
            public const string AuditRetentionPeriod = "auditretentionperiod";
            public const string AuditRetentionPeriodV2 = "auditretentionperiodv2";
            public const string AuditSettings = "auditsettings";
            public const string AutoApplyDefaultonCaseCreate = "autoapplydefaultoncasecreate";
            public const string AutoApplyDefaultonCaseUpdate = "autoapplydefaultoncaseupdate";
            public const string AutoApplySLA = "autoapplysla";
            public const string AzureSchedulerJobCollectionName = "azureschedulerjobcollectionname";
            public const string BaseCurrencyId = "basecurrencyid";
            public const string BaseCurrencyPrecision = "basecurrencyprecision";
            public const string BaseCurrencySymbol = "basecurrencysymbol";
            public const string BingMapsApiKey = "bingmapsapikey";
            public const string BlockAccessToSessionTranscriptsForCopilotStudio = "blockaccesstosessiontranscriptsforcopilotstudio";
            public const string BlockCopilotAuthorAuthentication = "blockcopilotauthorauthentication";
            public const string BlockedApplicationsForDVAccess = "blockedapplicationsfordvaccess";
            public const string BlockedAttachments = "blockedattachments";
            public const string BlockedMimeTypes = "blockedmimetypes";
            public const string BlockTranscriptRecordingForCopilotStudio = "blocktranscriptrecordingforcopilotstudio";
            public const string BlockUrlsInResponsesForCopilotStudio = "blockurlsinresponsesforcopilotstudio";
            public const string BoundDashboardDefaultCardExpanded = "bounddashboarddefaultcardexpanded";
            public const string BulkOperationPrefix = "bulkoperationprefix";
            public const string BusinessCardOptions = "businesscardoptions";
            public const string BusinessClosureCalendarId = "businessclosurecalendarid";
            public const string CalendarType = "calendartype";
            public const string CampaignPrefix = "campaignprefix";
            public const string CanOptOutNewSearchExperience = "canoptoutnewsearchexperience";
            public const string CascadeStatusUpdate = "cascadestatusupdate";
            public const string CasePrefix = "caseprefix";
            public const string CategoryPrefix = "categoryprefix";
            public const string ClientFeatureSet = "clientfeatureset";
            public const string ContentSecurityPolicyConfiguration = "contentsecuritypolicyconfiguration";
            public const string ContentSecurityPolicyConfigurationForCanvas = "contentsecuritypolicyconfigurationforcanvas";
            public const string ContentSecurityPolicyOptions = "contentsecuritypolicyoptions";
            public const string ContentSecurityPolicyReportUri = "contentsecuritypolicyreporturi";
            public const string ContractPrefix = "contractprefix";
            public const string CopresenceRefreshRate = "copresencerefreshrate";
            public const string CortanaProactiveExperienceEnabled = "cortanaproactiveexperienceenabled";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string CreateProductsWithoutParentInActiveState = "createproductswithoutparentinactivestate";
            public const string CuaFlowLogsTtlInMinutes = "cuaflowlogsttlinminutes";
            public const string CuaFlowLogsVerbosity = "cuaflowlogsverbosity";
            public const string CurrencyDecimalPrecision = "currencydecimalprecision";
            public const string CurrencyDisplayOption = "currencydisplayoption";
            public const string CurrencyFormatCode = "currencyformatcode";
            public const string CurrencySymbol = "currencysymbol";
            public const string CurrentBulkOperationNumber = "currentbulkoperationnumber";
            public const string CurrentCampaignNumber = "currentcampaignnumber";
            public const string CurrentCaseNumber = "currentcasenumber";
            public const string CurrentCategoryNumber = "currentcategorynumber";
            public const string CurrentContractNumber = "currentcontractnumber";
            public const string CurrentImportSequenceNumber = "currentimportsequencenumber";
            public const string CurrentInvoiceNumber = "currentinvoicenumber";
            public const string CurrentKaNumber = "currentkanumber";
            public const string CurrentKbNumber = "currentkbnumber";
            public const string CurrentOrderNumber = "currentordernumber";
            public const string CurrentParsedTableNumber = "currentparsedtablenumber";
            public const string CurrentQuoteNumber = "currentquotenumber";
            public const string DateFormatCode = "dateformatcode";
            public const string DateFormatString = "dateformatstring";
            public const string DateSeparator = "dateseparator";
            public const string DaysBeforeEmailDescriptionIsMigrated = "daysbeforeemaildescriptionismigrated";
            public const string DaysBeforeInactiveTeamsChatSyncDisabled = "daysbeforeinactiveteamschatsyncdisabled";
            public const string DaysSinceRecordLastModifiedMaxValue = "dayssincerecordlastmodifiedmaxvalue";
            public const string DecimalSymbol = "decimalsymbol";
            public const string DefaultCountryCode = "defaultcountrycode";
            public const string DefaultCrmCustomName = "defaultcrmcustomname";
            public const string DefaultEmailServerProfileId = "defaultemailserverprofileid";
            public const string DefaultEmailSettings = "defaultemailsettings";
            public const string DefaultMobileOfflineProfileId = "defaultmobileofflineprofileid";
            public const string DefaultRecurrenceEndRangeType = "defaultrecurrenceendrangetype";
            public const string DefaultThemeData = "defaultthemedata";
            public const string DelegatedAdminUserId = "delegatedadminuserid";
            public const string DesktopFlowQueueLogsTtlInMinutes = "desktopflowqueuelogsttlinminutes";
            public const string DesktopFlowRunActionLogsStatus = "desktopflowrunactionlogsstatus";
            public const string DesktopFlowRunActionLogVerbosity = "desktopflowrunactionlogverbosity";
            public const string DesktopFlowRunActionLogVersion = "desktopflowrunactionlogversion";
            public const string DisabledReason = "disabledreason";
            public const string DisableSocialCare = "disablesocialcare";
            public const string DisableSystemLabelsCacheSharing = "disablesystemlabelscachesharing";
            public const string DiscountCalculationMethod = "discountcalculationmethod";
            public const string DisplayNavigationTour = "displaynavigationtour";
            public const string EmailConnectionChannel = "emailconnectionchannel";
            public const string EmailCorrelationEnabled = "emailcorrelationenabled";
            public const string EmailSendPollingPeriod = "emailsendpollingperiod";
            public const string EnableAsyncMergeAPIForUCI = "enableasyncmergeapiforuci";
            public const string EnableBingMapsIntegration = "enablebingmapsintegration";
            public const string EnableCanvasAppsInSolutionsByDefault = "enablecanvasappsinsolutionsbydefault";
            public const string EnableCopilotStudioCrossGeoShareDataWithVivaInsights = "enablecopilotstudiocrossgeosharedatawithvivainsights";
            public const string EnableCopilotStudioShareDataWithVI = "enablecopilotstudiosharedatawithvi";
            public const string EnableCopilotStudioShareDataWithVivaInsights = "enablecopilotstudiosharedatawithvivainsights";
            public const string EnableEnvironmentSettingsApp = "enableenvironmentsettingsapp";
            public const string EnableFlowsInSolutionByDefault = "enableflowsinsolutionbydefault";
            public const string EnableFlowsInSolutionByDefaultGracePeriod = "enableflowsinsolutionbydefaultgraceperiod";
            public const string EnableImmersiveSkypeIntegration = "enableimmersiveskypeintegration";
            public const string EnableIpBasedCookieBinding = "enableipbasedcookiebinding";
            public const string EnableIpBasedFirewallRule = "enableipbasedfirewallrule";
            public const string EnableIpBasedFirewallRuleInAuditMode = "enableipbasedfirewallruleinauditmode";
            public const string EnableIpBasedStorageAccessSignatureRule = "enableipbasedstorageaccesssignaturerule";
            public const string EnableLivePersonaCardUCI = "enablelivepersonacarduci";
            public const string EnableLivePersonCardIntegrationInOffice = "enablelivepersoncardintegrationinoffice";
            public const string EnableLPAuthoring = "enablelpauthoring";
            public const string EnableMakerSwitchToClassic = "enablemakerswitchtoclassic";
            public const string EnableMicrosoftFlowIntegration = "enablemicrosoftflowintegration";
            public const string EnablePricingOnCreate = "enablepricingoncreate";
            public const string EnableSensitivityLabels = "enablesensitivitylabels";
            public const string EnableSmartMatching = "enablesmartmatching";
            public const string EnableUnifiedClientCDN = "enableunifiedclientcdn";
            public const string EnableUnifiedInterfaceShellRefresh = "enableunifiedinterfaceshellrefresh";
            public const string EnforceReadOnlyPlugins = "enforcereadonlyplugins";
            public const string EntityImage = "entityimage";
            public const string EntityImageTimestamp = "entityimage_timestamp";
            public const string EntityImageURL = "entityimage_url";
            public const string EntityImageId = "entityimageid";
            public const string ExpireChangeTrackingInDays = "expirechangetrackingindays";
            public const string ExpireSubscriptionsInDays = "expiresubscriptionsindays";
            public const string ExternalBaseUrl = "externalbaseurl";
            public const string ExternalPartyCorrelationKeys = "externalpartycorrelationkeys";
            public const string ExternalPartyEntitySettings = "externalpartyentitysettings";
            public const string FeatureSet = "featureset";
            public const string FiscalCalendarStart = "fiscalcalendarstart";
            public const string FiscalPeriodFormat = "fiscalperiodformat";
            public const string FiscalPeriodFormatPeriod = "fiscalperiodformatperiod";
            public const string FiscalPeriodType = "fiscalperiodtype";
            public const string FiscalSettingsUpdated = "fiscalsettingsupdated";
            public const string FiscalYearDisplayCode = "fiscalyeardisplaycode";
            public const string FiscalYearFormat = "fiscalyearformat";
            public const string FiscalYearFormatPrefix = "fiscalyearformatprefix";
            public const string FiscalYearFormatSuffix = "fiscalyearformatsuffix";
            public const string FiscalYearFormatYear = "fiscalyearformatyear";
            public const string FiscalYearPeriodConnect = "fiscalyearperiodconnect";
            public const string FlowLogsTtlInMinutes = "flowlogsttlinminutes";
            public const string FlowRunTimeToLiveInSeconds = "flowruntimetoliveinseconds";
            public const string FullNameConventionCode = "fullnameconventioncode";
            public const string FutureExpansionWindow = "futureexpansionwindow";
            public const string GenerateAlertsForErrors = "generatealertsforerrors";
            public const string GenerateAlertsForInformation = "generatealertsforinformation";
            public const string GenerateAlertsForWarnings = "generatealertsforwarnings";
            public const string GetStartedPaneContentEnabled = "getstartedpanecontentenabled";
            public const string GlobalAppendUrlParametersEnabled = "globalappendurlparametersenabled";
            public const string GlobalHelpUrl = "globalhelpurl";
            public const string GlobalHelpUrlEnabled = "globalhelpurlenabled";
            public const string GoalRollupExpiryTime = "goalrollupexpirytime";
            public const string GoalRollupFrequency = "goalrollupfrequency";
            public const string GrantAccessToNetworkService = "grantaccesstonetworkservice";
            public const string HashDeltaSubjectCount = "hashdeltasubjectcount";
            public const string HashFilterKeywords = "hashfilterkeywords";
            public const string HashMaxCount = "hashmaxcount";
            public const string HashMinAddressCount = "hashminaddresscount";
            public const string HighContrastThemeData = "highcontrastthemedata";
            public const string IgnoreInternalEmail = "ignoreinternalemail";
            public const string ImproveSearchLoggingEnabled = "improvesearchloggingenabled";
            public const string InactivityTimeoutEnabled = "inactivitytimeoutenabled";
            public const string InactivityTimeoutInMins = "inactivitytimeoutinmins";
            public const string InactivityTimeoutReminderInMins = "inactivitytimeoutreminderinmins";
            public const string IncomingEmailExchangeEmailRetrievalBatchSize = "incomingemailexchangeemailretrievalbatchsize";
            public const string InitialVersion = "initialversion";
            public const string IntegrationUserId = "integrationuserid";
            public const string InvoicePrefix = "invoiceprefix";
            public const string IpBasedStorageAccessSignatureMode = "ipbasedstorageaccesssignaturemode";
            public const string IsActionCardEnabled = "isactioncardenabled";
            public const string IsActionSupportFeatureEnabled = "isactionsupportfeatureenabled";
            public const string IsActivityAnalysisEnabled = "isactivityanalysisenabled";
            public const string IsAllMoneyDecimal = "isallmoneydecimal";
            public const string IsAppMode = "isappmode";
            public const string IsAppointmentAttachmentSyncEnabled = "isappointmentattachmentsyncenabled";
            public const string IsAssignedTasksSyncEnabled = "isassignedtaskssyncenabled";
            public const string IsAuditEnabled = "isauditenabled";
            public const string IsAutoDataCaptureEnabled = "isautodatacaptureenabled";
            public const string IsAutoDataCaptureV2Enabled = "isautodatacapturev2enabled";
            public const string IsAutoInstallAppForD365InTeamsEnabled = "isautoinstallappford365inteamsenabled";
            public const string IsAutoSaveEnabled = "isautosaveenabled";
            public const string IsBaseCardStaticFieldDataEnabled = "isbasecardstaticfielddataenabled";
            public const string IsBasicGeospatialIntegrationEnabled = "isbasicgeospatialintegrationenabled";
            public const string IsBPFEntityCustomizationFeatureEnabled = "isbpfentitycustomizationfeatureenabled";
            public const string IsCloudFlowSavingsEnabled = "iscloudflowsavingsenabled";
            public const string IsClusteringEnabled = "isclusteringenabled";
            public const string IsCollaborationExperienceEnabled = "iscollaborationexperienceenabled";
            public const string IsComputerUseInMCSEnabled = "iscomputeruseinmcsenabled";
            public const string IsConflictDetectionEnabledForMobileClient = "isconflictdetectionenabledformobileclient";
            public const string IsContactMailingAddressSyncEnabled = "iscontactmailingaddresssyncenabled";
            public const string IsContentSecurityPolicyEnabled = "iscontentsecuritypolicyenabled";
            public const string IsContentSecurityPolicyEnabledForCanvas = "iscontentsecuritypolicyenabledforcanvas";
            public const string IsContextualEmailEnabled = "iscontextualemailenabled";
            public const string IsContextualHelpEnabled = "iscontextualhelpenabled";
            public const string IsCopilotFeedbackEnabled = "iscopilotfeedbackenabled";
            public const string IsCuaOnHmgV2Enabled = "iscuaonhmgv2enabled";
            public const string IsCustomControlsInCanvasAppsEnabled = "iscustomcontrolsincanvasappsenabled";
            public const string IsDefaultCountryCodeCheckEnabled = "isdefaultcountrycodecheckenabled";
            public const string IsDelegateAccessEnabled = "isdelegateaccessenabled";
            public const string IsDelveActionHubIntegrationEnabled = "isdelveactionhubintegrationenabled";
            public const string IsDesktopFlowConnectionEmbeddingEnabled = "isdesktopflowconnectionembeddingenabled";
            public const string IsDesktopFlowRuntimeRepairAttendedEnabled = "isdesktopflowruntimerepairattendedenabled";
            public const string IsDesktopFlowRuntimeRepairUnattendedEnabled = "isdesktopflowruntimerepairunattendedenabled";
            public const string IsDesktopFlowSavingsEnabled = "isdesktopflowsavingsenabled";
            public const string IsDesktopFlowSchemaV2Enabled = "isdesktopflowschemav2enabled";
            public const string IsDesktopFlowVanillaImageSharingEnabled = "isdesktopflowvanillaimagesharingenabled";
            public const string IsDesktopFlowVersionControlEnabled = "isdesktopflowversioncontrolenabled";
            public const string IsDesktopFlowVersionControlEnabledByDefault = "isdesktopflowversioncontrolenabledbydefault";
            public const string IsDesktopFlowVersionControlEnabledOverride = "isdesktopflowversioncontrolenabledoverride";
            public const string IsDisabled = "isdisabled";
            public const string IsDuplicateDetectionEnabled = "isduplicatedetectionenabled";
            public const string IsDuplicateDetectionEnabledForImport = "isduplicatedetectionenabledforimport";
            public const string IsDuplicateDetectionEnabledForOfflineSync = "isduplicatedetectionenabledforofflinesync";
            public const string IsDuplicateDetectionEnabledForOnlineCreateUpdate = "isduplicatedetectionenabledforonlinecreateupdate";
            public const string IsEmailAddressValidationEnabled = "isemailaddressvalidationenabled";
            public const string IsEmailMonitoringAllowed = "isemailmonitoringallowed";
            public const string IsEmailServerProfileContentFilteringEnabled = "isemailserverprofilecontentfilteringenabled";
            public const string IsEnabledForAllRoles = "isenabledforallroles";
            public const string IsExternalFileStorageEnabled = "isexternalfilestorageenabled";
            public const string IsExternalSearchIndexEnabled = "isexternalsearchindexenabled";
            public const string IsFiscalPeriodMonthBased = "isfiscalperiodmonthbased";
            public const string IsFolderAutoCreatedonSP = "isfolderautocreatedonsp";
            public const string IsFolderBasedTrackingEnabled = "isfolderbasedtrackingenabled";
            public const string IsFullTextSearchEnabled = "isfulltextsearchenabled";
            public const string IsGeospatialAzureMapsIntegrationEnabled = "isgeospatialazuremapsintegrationenabled";
            public const string IsHierarchicalSecurityModelEnabled = "ishierarchicalsecuritymodelenabled";
            public const string IsIdeasDataCollectionEnabled = "isideasdatacollectionenabled";
            public const string IsLUISEnabledforD365Bot = "isluisenabledford365bot";
            public const string IsMailboxForcedUnlockingEnabled = "ismailboxforcedunlockingenabled";
            public const string IsMailboxInactiveBackoffEnabled = "ismailboxinactivebackoffenabled";
            public const string IsManualSalesForecastingEnabled = "ismanualsalesforecastingenabled";
            public const string IsMobileClientOnDemandSyncEnabled = "ismobileclientondemandsyncenabled";
            public const string IsMobileOfflineEnabled = "ismobileofflineenabled";
            public const string IsModelDrivenAppsInMSTeamsEnabled = "ismodeldrivenappsinmsteamsenabled";
            public const string IsMoneySavingsAllowed = "ismoneysavingsallowed";
            public const string IsMSTeamsCollaborationEnabled = "ismsteamscollaborationenabled";
            public const string IsMSTeamsEnabled = "ismsteamsenabled";
            public const string IsMSTeamsSettingChangedByUser = "ismsteamssettingchangedbyuser";
            public const string IsMSTeamsUserSyncEnabled = "ismsteamsusersyncenabled";
            public const string IsNewAddProductExperienceEnabled = "isnewaddproductexperienceenabled";
            public const string IsNotesAnalysisEnabled = "isnotesanalysisenabled";
            public const string IsNotificationForD365InTeamsEnabled = "isnotificationford365inteamsenabled";
            public const string IsOfficeGraphEnabled = "isofficegraphenabled";
            public const string IsOneDriveEnabled = "isonedriveenabled";
            public const string IsPAIEnabled = "ispaienabled";
            public const string IsPDFGenerationEnabled = "ispdfgenerationenabled";
            public const string IsPerProcessCapacityOverageEnabled = "isperprocesscapacityoverageenabled";
            public const string IsPlaybookEnabled = "isplaybookenabled";
            public const string IsPresenceEnabled = "ispresenceenabled";
            public const string IsPreviewEnabledForActionCard = "ispreviewenabledforactioncard";
            public const string IsPreviewForAutoCaptureEnabled = "ispreviewforautocaptureenabled";
            public const string IsPreviewForEmailMonitoringAllowed = "ispreviewforemailmonitoringallowed";
            public const string IsPriceListMandatory = "ispricelistmandatory";
            public const string IsProcessCapacityAutoClaimEnabled = "isprocesscapacityautoclaimenabled";
            public const string IsProcessMiningEnabled = "isprocessminingenabled";
            public const string IsQuickCreateEnabledForOpportunityClose = "isquickcreateenabledforopportunityclose";
            public const string IsReadAuditEnabled = "isreadauditenabled";
            public const string IsRelationshipInsightsEnabled = "isrelationshipinsightsenabled";
            public const string IsResourceBookingExchangeSyncEnabled = "isresourcebookingexchangesyncenabled";
            public const string IsRichTextNotesEnabled = "isrichtextnotesenabled";
            public const string IsRpaAutoscaleAadJoinEnabled = "isrpaautoscaleaadjoinenabled";
            public const string IsRpaAutoscaleEnabled = "isrpaautoscaleenabled";
            public const string IsRpaBoxCrossGeoEnabled = "isrpaboxcrossgeoenabled";
            public const string IsRpaBoxEnabled = "isrpaboxenabled";
            public const string IsRpaUnattendedEnabled = "isrpaunattendedenabled";
            public const string IsSalesAssistantEnabled = "issalesassistantenabled";
            public const string IsSendCuaAuditLogToPurviewEnabled = "issendcuaauditlogtopurviewenabled";
            public const string IsSharingInOrgAllowed = "issharinginorgallowed";
            public const string IsSOPIntegrationEnabled = "issopintegrationenabled";
            public const string IsTextWrapEnabled = "istextwrapenabled";
            public const string IsUploadCuaLogToDataverseEnabled = "isuploadcualogtodataverseenabled";
            public const string IsUserAccessAuditEnabled = "isuseraccessauditenabled";
            public const string ISVIntegrationCode = "isvintegrationcode";
            public const string IsWorkQueueSavingsEnabled = "isworkqueuesavingsenabled";
            public const string IsWriteInProductsAllowed = "iswriteinproductsallowed";
            public const string KaPrefix = "kaprefix";
            public const string KbPrefix = "kbprefix";
            public const string KMSettings = "kmsettings";
            public const string LanguageCode = "languagecode";
            public const string LegacyAppToggle = "legacyapptoggle";
            public const string LocaleId = "localeid";
            public const string LongDateFormatCode = "longdateformatcode";
            public const string LookupCharacterCountBeforeResolve = "lookupcharactercountbeforeresolve";
            public const string LookupResolveDelayMS = "lookupresolvedelayms";
            public const string MailboxIntermittentIssueMinRange = "mailboxintermittentissueminrange";
            public const string MailboxPermanentIssueMinRange = "mailboxpermanentissueminrange";
            public const string MaxActionStepsInBPF = "maxactionstepsinbpf";
            public const string MaxAllowedPendingRollupJobCount = "maxallowedpendingrollupjobcount";
            public const string MaxAllowedPendingRollupJobPercentage = "maxallowedpendingrollupjobpercentage";
            public const string MaxAppointmentDurationDays = "maxappointmentdurationdays";
            public const string MaxConditionsForMobileOfflineFilters = "maxconditionsformobileofflinefilters";
            public const string MaxDepthForHierarchicalSecurityModel = "maxdepthforhierarchicalsecuritymodel";
            public const string MaxFolderBasedTrackingMappings = "maxfolderbasedtrackingmappings";
            public const string MaximumActiveBusinessProcessFlowsAllowedPerEntity = "maximumactivebusinessprocessflowsallowedperentity";
            public const string MaximumDynamicPropertiesAllowed = "maximumdynamicpropertiesallowed";
            public const string MaximumEntitiesWithActiveSLA = "maximumentitieswithactivesla";
            public const string MaximumSLAKPIPerEntityWithActiveSLA = "maximumslakpiperentitywithactivesla";
            public const string MaximumTrackingNumber = "maximumtrackingnumber";
            public const string MaxProductsInBundle = "maxproductsinbundle";
            public const string MaxRecordsForExportToExcel = "maxrecordsforexporttoexcel";
            public const string MaxRecordsForLookupFilters = "maxrecordsforlookupfilters";
            public const string MaxRollupFieldsPerEntity = "maxrollupfieldsperentity";
            public const string MaxRollupFieldsPerOrg = "maxrollupfieldsperorg";
            public const string MaxSLAItemsPerSLA = "maxslaitemspersla";
            public const string MaxSupportedInternetExplorerVersion = "maxsupportedinternetexplorerversion";
            public const string MaxUploadFileSize = "maxuploadfilesize";
            public const string MaxVerboseLoggingMailbox = "maxverboseloggingmailbox";
            public const string MaxVerboseLoggingSyncCycles = "maxverboseloggingsynccycles";
            public const string MicrosoftFlowEnvironment = "microsoftflowenvironment";
            public const string MinAddressBookSyncInterval = "minaddressbooksyncinterval";
            public const string MinOfflineSyncInterval = "minofflinesyncinterval";
            public const string MinOutlookSyncInterval = "minoutlooksyncinterval";
            public const string MobileOfflineMinLicenseProd = "mobileofflineminlicenseprod";
            public const string MobileOfflineMinLicenseTrial = "mobileofflineminlicensetrial";
            public const string MobileOfflineSyncInterval = "mobileofflinesyncinterval";
            public const string ModernAdvancedFindFiltering = "modernadvancedfindfiltering";
            public const string ModernAppDesignerCoauthoringEnabled = "modernappdesignercoauthoringenabled";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string MultiColumnSortEnabled = "multicolumnsortenabled";
            public const string Name = "name";
            public const string NaturalLanguageAssistFilter = "naturallanguageassistfilter";
            public const string NegativeCurrencyFormatCode = "negativecurrencyformatcode";
            public const string NegativeFormatCode = "negativeformatcode";
            public const string NewSearchExperienceEnabled = "newsearchexperienceenabled";
            public const string NextTrackingNumber = "nexttrackingnumber";
            public const string NotifyMailboxOwnerOfEmailServerLevelAlerts = "notifymailboxownerofemailserverlevelalerts";
            public const string NumberFormat = "numberformat";
            public const string NumberGroupFormat = "numbergroupformat";
            public const string NumberSeparator = "numberseparator";
            public const string OfficeAppsAutoDeploymentEnabled = "officeappsautodeploymentenabled";
            public const string OfficeGraphDelveUrl = "officegraphdelveurl";
            public const string OOBPriceCalculationEnabled = "oobpricecalculationenabled";
            public const string OptOutSchemaV2EnabledByDefault = "optoutschemav2enabledbydefault";
            public const string OrderPrefix = "orderprefix";
            public const string OrganizationState = "organizationstate";
            public const string OrgDbOrgSettings = "orgdborgsettings";
            public const string OrgInsightsEnabled = "orginsightsenabled";
            public const string PaiPreviewScenarioEnabled = "paipreviewscenarioenabled";
            public const string ParsedTableColumnPrefix = "parsedtablecolumnprefix";
            public const string ParsedTablePrefix = "parsedtableprefix";
            public const string PastExpansionWindow = "pastexpansionwindow";
            public const string PcfDatasetGridEnabled = "pcfdatasetgridenabled";
            public const string PerformACTSyncAfter = "performactsyncafter";
            public const string Picture = "picture";
            public const string PinpointLanguageCode = "pinpointlanguagecode";
            public const string PluginTraceLogSetting = "plugintracelogsetting";
            public const string PMDesignator = "pmdesignator";
            public const string PostMessageWhitelistDomains = "postmessagewhitelistdomains";
            public const string PowerAppsMakerBotEnabled = "powerappsmakerbotenabled";
            public const string PowerBiFeatureEnabled = "powerbifeatureenabled";
            public const string PricingDecimalPrecision = "pricingdecimalprecision";
            public const string PrivacyStatementUrl = "privacystatementurl";
            public const string PrivilegeUserGroupId = "privilegeusergroupid";
            public const string PrivReportingGroupId = "privreportinggroupid";
            public const string PrivReportingGroupName = "privreportinggroupname";
            public const string ProductRecommendationsEnabled = "productrecommendationsenabled";
            public const string QualifyLeadAdditionalOptions = "qualifyleadadditionaloptions";
            public const string QuickActionToOpenRecordsInSidePaneEnabled = "quickactiontoopenrecordsinsidepaneenabled";
            public const string QuickFindRecordLimitEnabled = "quickfindrecordlimitenabled";
            public const string QuotePrefix = "quoteprefix";
            public const string RecalculateSLA = "recalculatesla";
            public const string RecurrenceDefaultNumberOfOccurrences = "recurrencedefaultnumberofoccurrences";
            public const string RecurrenceExpansionJobBatchInterval = "recurrenceexpansionjobbatchinterval";
            public const string RecurrenceExpansionJobBatchSize = "recurrenceexpansionjobbatchsize";
            public const string RecurrenceExpansionSynchCreateMax = "recurrenceexpansionsynchcreatemax";
            public const string ReferenceSiteMapXml = "referencesitemapxml";
            public const string ReleaseCadence = "releasecadence";
            public const string ReleaseChannel = "releasechannel";
            public const string ReleaseWaveName = "releasewavename";
            public const string RelevanceSearchEnabledByPlatform = "relevancesearchenabledbyplatform";
            public const string RelevanceSearchModifiedOn = "relevancesearchmodifiedon";
            public const string RenderSecureIFrameForEmail = "rendersecureiframeforemail";
            public const string ReportingGroupId = "reportinggroupid";
            public const string ReportingGroupName = "reportinggroupname";
            public const string ReportScriptErrors = "reportscripterrors";
            public const string RequireApprovalForQueueEmail = "requireapprovalforqueueemail";
            public const string RequireApprovalForUserEmail = "requireapprovalforuseremail";
            public const string ResolveSimilarUnresolvedEmailAddress = "resolvesimilarunresolvedemailaddress";
            public const string RestrictGuestUserAccess = "restrictGuestUserAccess";
            public const string RestrictStatusUpdate = "restrictstatusupdate";
            public const string ReverseProxyIpAddresses = "reverseproxyipaddresses";
            public const string RiErrorStatus = "rierrorstatus";
            public const string SameSiteModeForSessionCookie = "samesitemodeforsessioncookie";
            public const string SampleDataImportId = "sampledataimportid";
            public const string SavingEventsTTLInMinutes = "savingeventsttlinminutes";
            public const string SchemaNamePrefix = "schemanameprefix";
            public const string SendBulkEmailInUCI = "sendbulkemailinuci";
            public const string ServeStaticResourcesFromAzureCDN = "servestaticresourcesfromazurecdn";
            public const string SessionRecordingEnabled = "sessionrecordingenabled";
            public const string SessionTimeoutEnabled = "sessiontimeoutenabled";
            public const string SessionTimeoutInMins = "sessiontimeoutinmins";
            public const string SessionTimeoutReminderInMins = "sessiontimeoutreminderinmins";
            public const string SharePointDeploymentType = "sharepointdeploymenttype";
            public const string ShareToPreviousOwnerOnAssign = "sharetopreviousowneronassign";
            public const string ShowKBArticleDeprecationNotification = "showkbarticledeprecationnotification";
            public const string ShowWeekNumber = "showweeknumber";
            public const string SignupOutlookDownloadFWLink = "signupoutlookdownloadfwlink";
            public const string SiteMapXml = "sitemapxml";
            public const string SlaPauseStates = "slapausestates";
            public const string SocialInsightsEnabled = "socialinsightsenabled";
            public const string SocialInsightsInstance = "socialinsightsinstance";
            public const string SocialInsightsTermsAccepted = "socialinsightstermsaccepted";
            public const string SortId = "sortid";
            public const string SqlAccessGroupId = "sqlaccessgroupid";
            public const string SqlAccessGroupName = "sqlaccessgroupname";
            public const string SQMEnabled = "sqmenabled";
            public const string SupportUserId = "supportuserid";
            public const string SuppressSLA = "suppresssla";
            public const string SuppressValidationEmails = "suppressvalidationemails";
            public const string SyncBulkOperationBatchSize = "syncbulkoperationbatchsize";
            public const string SyncBulkOperationMaxLimit = "syncbulkoperationmaxlimit";
            public const string SyncOptInSelection = "syncoptinselection";
            public const string SyncOptInSelectionStatus = "syncoptinselectionstatus";
            public const string SystemUserId = "systemuserid";
            public const string TableScopedDVSearchInApps = "tablescopeddvsearchinapps";
            public const string TagMaxAggressiveCycles = "tagmaxaggressivecycles";
            public const string TagPollingPeriod = "tagpollingperiod";
            public const string TaskBasedFlowEnabled = "taskbasedflowenabled";
            public const string TeamsChatDataSync = "teamschatdatasync";
            public const string TelemetryInstrumentationKey = "telemetryinstrumentationkey";
            public const string TextAnalyticsEnabled = "textanalyticsenabled";
            public const string TimeFormatCode = "timeformatcode";
            public const string TimeFormatString = "timeformatstring";
            public const string TimeSeparator = "timeseparator";
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
            public const string TokenExpiry = "tokenexpiry";
            public const string TokenKey = "tokenkey";
            public const string TraceLogMaximumAgeInDays = "tracelogmaximumageindays";
            public const string TrackingPrefix = "trackingprefix";
            public const string TrackingTokenIdBase = "trackingtokenidbase";
            public const string TrackingTokenIdDigits = "trackingtokeniddigits";
            public const string UniqueSpecifierLength = "uniquespecifierlength";
            public const string UnresolveEmailAddressIfMultipleMatch = "unresolveemailaddressifmultiplematch";
            public const string UseInbuiltRuleForDefaultPricelistSelection = "useinbuiltrulefordefaultpricelistselection";
            public const string UseLegacyRendering = "uselegacyrendering";
            public const string UsePositionHierarchy = "usepositionhierarchy";
            public const string UseQuickFindViewForGridSearch = "usequickfindviewforgridsearch";
            public const string UserAccessAuditingInterval = "useraccessauditinginterval";
            public const string UseReadForm = "usereadform";
            public const string UserGroupId = "usergroupid";
            public const string UserRatingEnabled = "userratingenabled";
            public const string UseSkypeProtocol = "useskypeprotocol";
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
            public const string V3CalloutConfigHash = "v3calloutconfighash";
            public const string ValidationMode = "validationmode";
            public const string VersionNumber = "versionnumber";
            public const string WebResourceHash = "webresourcehash";
            public const string WeekStartDayCode = "weekstartdaycode";
            public const string WidgetProperties = "widgetproperties";
            public const string YammerGroupId = "yammergroupid";
            public const string YammerNetworkPermalink = "yammernetworkpermalink";
            public const string YammerOAuthAccessTokenExpired = "yammeroauthaccesstokenexpired";
            public const string YammerPostMethod = "yammerpostmethod";
            public const string YearStartWeekCode = "yearstartweekcode";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string ChannelpropertyOrganization = "channelproperty_organization";
                public const string ChannelpropertygroupOrganization = "channelpropertygroup_organization";
                public const string CustomcontrolOrganization = "customcontrol_organization";
                public const string CustomcontroldefaultconfigOrganization = "customcontroldefaultconfig_organization";
                public const string CustomcontrolresourceOrganization = "customcontrolresource_organization";
                public const string LanguagelocaleOrganization = "languagelocale_organization";
                public const string LkAuthorizationserverOrganizationid = "lk_authorizationserver_organizationid";
                public const string LkDataperformanceOrganizationid = "lk_dataperformance_organizationid";
                public const string LkDocumenttemplatebaseOrganization = "lk_documenttemplatebase_organization";
                public const string LkFieldsecurityprofileOrganizationid = "lk_fieldsecurityprofile_organizationid";
                public const string LkOrganizationuiOrganizationid = "lk_organizationui_organizationid";
                public const string LkPartnerapplicationOrganizationid = "lk_partnerapplication_organizationid";
                public const string LkPrincipalobjectattributeaccessOrganizationid = "lk_principalobjectattributeaccess_organizationid";
                public const string LkPrincipalsyncattributemapOrganizationid = "lk_principalsyncattributemap_organizationid";
                public const string LkSyncattributemappingprofileOrganizationid = "lk_syncattributemappingprofile_organizationid";
                public const string MobileOfflineProfileOrganization = "MobileOfflineProfile_organization";
                public const string MobileOfflineProfileItemOrganization = "MobileOfflineProfileItem_organization";
                public const string MobileOfflineProfileItemAssociationOrganization = "MobileOfflineProfileItemAssociation_organization";
                public const string OfflinecommanddefinitionOrganization = "offlinecommanddefinition_organization";
                public const string OrganizationAciviewmapper = "organization_aciviewmapper";
                public const string OrganizationAdvancedsimilarityrule = "organization_advancedsimilarityrule";
                public const string OrganizationAdxExternalidentity = "organization_adx_externalidentity";
                public const string OrganizationAdxWebformsession = "organization_adx_webformsession";
                public const string OrganizationAicopilot = "organization_aicopilot";
                public const string OrganizationAiplugintitle = "organization_aiplugintitle";
                public const string OrganizationAllowedmcpclient = "organization_allowedmcpclient";
                public const string OrganizationAnyprivilegeentity = "organization_anyprivilegeentity";
                public const string OrganizationAppaction = "organization_appaction";
                public const string OrganizationAppactionmigration = "organization_appactionmigration";
                public const string OrganizationAppactionrule = "organization_appactionrule";
                public const string OrganizationAppconfig = "organization_appconfig";
                public const string OrganizationAppconfiginstance = "organization_appconfiginstance";
                public const string OrganizationAppconfigmaster = "organization_appconfigmaster";
                public const string OrganizationAppelement = "organization_appelement";
                public const string OrganizationAppentitysearchview = "organization_appentitysearchview";
                public const string OrganizationApplication = "organization_application";
                public const string OrganizationApplicationfile = "organization_applicationfile";
                public const string OrganizationAppmodule = "organization_appmodule";
                public const string OrganizationAppmodulecomponentedge = "organization_appmodulecomponentedge";
                public const string OrganizationAppmodulecomponentnode = "organization_appmodulecomponentnode";
                public const string OrganizationAppsetting = "organization_appsetting";
                public const string OrganizationAppusersetting = "organization_appusersetting";
                public const string OrganizationAsyncOperations = "Organization_AsyncOperations";
                public const string OrganizationAthenareconciliationinfo = "organization_athenareconciliationinfo";
                public const string OrganizationAttributeclusterconfig = "organization_attributeclusterconfig";
                public const string OrganizationAttributemap = "organization_attributemap";
                public const string OrganizationAzureserviceconnection = "organization_azureserviceconnection";
                public const string OrganizationBulkarchiveoperationdetail = "organization_bulkarchiveoperationdetail";
                public const string OrganizationBulkDeleteFailures = "Organization_BulkDeleteFailures";
                public const string OrganizationBusinessUnitNewsArticles = "organization_business_unit_news_articles";
                public const string OrganizationBusinessUnits = "organization_business_units";
                public const string OrganizationCalendars = "organization_calendars";
                public const string OrganizationCatalog = "organization_catalog";
                public const string OrganizationCatalogassignment = "organization_catalogassignment";
                public const string OrganizationComplexcontrols = "organization_complexcontrols";
                public const string OrganizationConnectionRoles = "organization_connection_roles";
                public const string OrganizationCopilotexamplequestion = "organization_copilotexamplequestion";
                public const string OrganizationCustomDisplaystrings = "organization_custom_displaystrings";
                public const string OrganizationDatalakeworkspace = "organization_datalakeworkspace";
                public const string OrganizationDatalakeworkspacepermission = "organization_datalakeworkspacepermission";
                public const string OrganizationDataprocessingconfiguration = "organization_dataprocessingconfiguration";
                public const string OrganizationDelegatedauthorization = "organization_delegatedauthorization";
                public const string OrganizationDeleteditemreference = "organization_deleteditemreference";
                public const string OrganizationDelveactionhub = "organization_delveactionhub";
                public const string OrganizationEmailaddressconfiguration = "organization_emailaddressconfiguration";
                public const string OrganizationEmailserverprofile = "organization_emailserverprofile";
                public const string OrganizationEntityanalyticsconfig = "organization_entityanalyticsconfig";
                public const string OrganizationEntityclusterconfig = "organization_entityclusterconfig";
                public const string OrganizationEntitydataprovider = "organization_entitydataprovider";
                public const string OrganizationEntitydatasource = "organization_entitydatasource";
                public const string OrganizationEntitymap = "organization_entitymap";
                public const string OrganizationEntityrecordfilter = "organization_entityrecordfilter";
                public const string OrganizationExpanderevent = "organization_expanderevent";
                public const string OrganizationExpiredprocess = "organization_expiredprocess";
                public const string OrganizationHierarchyrules = "organization_hierarchyrules";
                public const string OrganizationImportjob = "organization_importjob";
                public const string OrganizationIndexedDocuments = "organization_indexed_documents";
                public const string OrganizationIntegrationStatuses = "organization_integration_statuses";
                public const string OrganizationInternalcatalogassignment = "organization_internalcatalogassignment";
                public const string OrganizationIsvconfigs = "organization_isvconfigs";
                public const string OrganizationKbArticleTemplates = "organization_kb_article_templates";
                public const string OrganizationKbArticles = "organization_kb_articles";
                public const string OrganizationKnowledgeBaseRecord = "organization_KnowledgeBaseRecord";
                public const string OrganizationKnowledgesearchmodel = "organization_knowledgesearchmodel";
                public const string OrganizationLicenses = "organization_licenses";
                public const string OrganizationMailbox = "organization_mailbox";
                public const string OrganizationMailboxstatistics = "organization_mailboxstatistics";
                public const string OrganizationMailboxTrackingFolder = "Organization_MailboxTrackingFolder";
                public const string OrganizationMainfewshot = "organization_mainfewshot";
                public const string OrganizationMakerfewshot = "organization_makerfewshot";
                public const string OrganizationMaskingrule = "organization_maskingrule";
                public const string OrganizationMetadataforarchival = "organization_metadataforarchival";
                public const string OrganizationMetric = "organization_metric";
                public const string OrganizationMobileofflineprofileextension = "organization_mobileofflineprofileextension";
                public const string OrganizationMsdynAppinsightsmetadata = "organization_msdyn_appinsightsmetadata";
                public const string OrganizationMsdynFederatedarticleincident = "organization_msdyn_federatedarticleincident";
                public const string OrganizationMsdynHelppage = "organization_msdyn_helppage";
                public const string OrganizationMsdynInsightsstorevirtualentity = "organization_msdyn_insightsstorevirtualentity";
                public const string OrganizationMsdynKmpersonalizationsetting = "organization_msdyn_kmpersonalizationsetting";
                public const string OrganizationMsdynKnowledgeconfiguration = "organization_msdyn_knowledgeconfiguration";
                public const string OrganizationMsdynModulerundetail = "organization_msdyn_modulerundetail";
                public const string OrganizationMsdynRtestructuredtemplate = "organization_msdyn_rtestructuredtemplate";
                public const string OrganizationMsdynRtetemplatemapping = "organization_msdyn_rtetemplatemapping";
                public const string OrganizationMsdynSolutionhealthruleset = "organization_msdyn_solutionhealthruleset";
                public const string OrganizationMsdynTour = "organization_msdyn_tour";
                public const string OrganizationMsdynWorkflowactionstatus = "organization_msdyn_workflowactionstatus";
                public const string OrganizationNavigationsetting = "organization_navigationsetting";
                public const string OrganizationNewprocess = "organization_newprocess";
                public const string OrganizationOfficegraphdocument = "organization_officegraphdocument";
                public const string OrganizationOrganizationdatasyncfnostate = "organization_organizationdatasyncfnostate";
                public const string OrganizationOrganizationdatasyncstate = "organization_organizationdatasyncstate";
                public const string OrganizationOrganizationdatasyncsubscription = "organization_organizationdatasyncsubscription";
                public const string OrganizationOrganizationdatasyncsubscriptionentity = "organization_organizationdatasyncsubscriptionentity";
                public const string OrganizationOrganizationdatasyncsubscriptionfnotable = "organization_organizationdatasyncsubscriptionfnotable";
                public const string OrganizationOrganizationsetting = "organization_organizationsetting";
                public const string OrganizationOrginsightsmetric = "organization_orginsightsmetric";
                public const string OrganizationOrginsightsnotification = "organization_orginsightsnotification";
                public const string OrganizationPackage = "organization_package";
                public const string OrganizationPackagehistory = "organization_packagehistory";
                public const string OrganizationPluginassembly = "organization_pluginassembly";
                public const string OrganizationPluginpackage = "organization_pluginpackage";
                public const string OrganizationPlugintype = "organization_plugintype";
                public const string OrganizationPlugintypestatistic = "organization_plugintypestatistic";
                public const string OrganizationPosition = "organization_position";
                public const string OrganizationPost = "organization_post";
                public const string OrganizationPostComment = "organization_PostComment";
                public const string OrganizationPostlike = "organization_postlike";
                public const string OrganizationPostrole = "organization_postrole";
                public const string OrganizationPrivilegesremovalsetting = "organization_privilegesremovalsetting";
                public const string OrganizationPublisher = "organization_publisher";
                public const string OrganizationPurviewlabelinfo = "organization_purviewlabelinfo";
                public const string OrganizationPurviewlabelsynccache = "organization_purviewlabelsynccache";
                public const string OrganizationQueueitems = "organization_queueitems";
                public const string OrganizationQueues = "organization_queues";
                public const string OrganizationRecommendeddocument = "organization_recommendeddocument";
                public const string OrganizationRecordfilter = "organization_recordfilter";
                public const string OrganizationRecyclebinconfig = "organization_recyclebinconfig";
                public const string OrganizationRelationshipRoles = "organization_relationship_roles";
                public const string OrganizationRelationshipattribute = "organization_relationshipattribute";
                public const string OrganizationRetentionoperationdetail = "organization_retentionoperationdetail";
                public const string OrganizationRibbonCommand = "organization_ribbon_command";
                public const string OrganizationRibbonContextGroup = "organization_ribbon_context_group";
                public const string OrganizationRibbonCustomization = "organization_ribbon_customization";
                public const string OrganizationRibbonDiff = "organization_ribbon_diff";
                public const string OrganizationRibbonRule = "organization_ribbon_rule";
                public const string OrganizationRibbonTabToCommandMap = "organization_ribbon_tab_to_command_map";
                public const string OrganizationRoleeditorlayout = "organization_roleeditorlayout";
                public const string OrganizationRoles = "organization_roles";
                public const string OrganizationRoutingruleitems = "organization_routingruleitems";
                public const string OrganizationRoutingRules = "organization_RoutingRules";
                public const string OrganizationSaSuggestedaction = "organization_sa_suggestedaction";
                public const string OrganizationSaSuggestedactioncriteria = "organization_sa_suggestedactioncriteria";
                public const string OrganizationSavedQueries = "organization_saved_queries";
                public const string OrganizationSavedQueryVisualizations = "organization_saved_query_visualizations";
                public const string OrganizationSavedorginsightsconfiguration = "organization_savedorginsightsconfiguration";
                public const string OrganizationSdkmessage = "organization_sdkmessage";
                public const string OrganizationSdkmessagefilter = "organization_sdkmessagefilter";
                public const string OrganizationSdkmessagepair = "organization_sdkmessagepair";
                public const string OrganizationSdkmessageprocessingstep = "organization_sdkmessageprocessingstep";
                public const string OrganizationSdkmessageprocessingstepimage = "organization_sdkmessageprocessingstepimage";
                public const string OrganizationSdkmessageprocessingstepsecureconfig = "organization_sdkmessageprocessingstepsecureconfig";
                public const string OrganizationSdkmessagerequest = "organization_sdkmessagerequest";
                public const string OrganizationSdkmessagerequestfield = "organization_sdkmessagerequestfield";
                public const string OrganizationSdkmessageresponse = "organization_sdkmessageresponse";
                public const string OrganizationSdkmessageresponsefield = "organization_sdkmessageresponsefield";
                public const string OrganizationSearchattributesettings = "organization_searchattributesettings";
                public const string OrganizationSearchcustomanalyzer = "organization_searchcustomanalyzer";
                public const string OrganizationSearchrelationshipsettings = "organization_searchrelationshipsettings";
                public const string OrganizationSensitivitylabelattributemapping = "organization_sensitivitylabelattributemapping";
                public const string OrganizationServiceendpoint = "organization_serviceendpoint";
                public const string OrganizationSettingdefinition = "organization_settingdefinition";
                public const string OrganizationSharedlinksetting = "organization_sharedlinksetting";
                public const string OrganizationSharepointdata = "organization_sharepointdata";
                public const string OrganizationSharepointdocument = "organization_sharepointdocument";
                public const string OrganizationSharepointmanagedidentity = "organization_sharepointmanagedidentity";
                public const string OrganizationSimilarityrule = "organization_similarityrule";
                public const string OrganizationSitemap = "organization_sitemap";
                public const string OrganizationSocialinsightsconfiguration = "organization_socialinsightsconfiguration";
                public const string OrganizationSolution = "organization_solution";
                public const string OrganizationSolutioncomponentattributeconfiguration = "organization_solutioncomponentattributeconfiguration";
                public const string OrganizationSolutioncomponentconfiguration = "organization_solutioncomponentconfiguration";
                public const string OrganizationSolutioncomponentrelationshipconfiguration = "organization_solutioncomponentrelationshipconfiguration";
                public const string OrganizationStatusMaps = "organization_status_maps";
                public const string OrganizationStringMaps = "organization_string_maps";
                public const string OrganizationSubjects = "organization_subjects";
                public const string OrganizationSuggestioncardtemplate = "organization_suggestioncardtemplate";
                public const string OrganizationSupportusertable = "organization_supportusertable";
                public const string OrganizationSynapselinkexternaltablestate = "organization_synapselinkexternaltablestate";
                public const string OrganizationSynapselinkprofile = "organization_synapselinkprofile";
                public const string OrganizationSynapselinkprofileentity = "organization_synapselinkprofileentity";
                public const string OrganizationSynapselinkprofileentitystate = "organization_synapselinkprofileentitystate";
                public const string OrganizationSynapselinkschedule = "organization_synapselinkschedule";
                public const string OrganizationSyncErrors = "Organization_SyncErrors";
                public const string OrganizationSystemUsers = "organization_system_users";
                public const string OrganizationSystemapplicationmetadata = "organization_systemapplicationmetadata";
                public const string OrganizationSystemforms = "organization_systemforms";
                public const string OrganizationTeammobileofflineprofilemembership = "organization_teammobileofflineprofilemembership";
                public const string OrganizationTeams = "organization_teams";
                public const string OrganizationTerritories = "organization_territories";
                public const string OrganizationTextanalyticsentitymapping = "organization_textanalyticsentitymapping";
                public const string OrganizationTheme = "organization_theme";
                public const string OrganizationTraceassociation = "organization_traceassociation";
                public const string OrganizationTracelog = "organization_tracelog";
                public const string OrganizationTransactioncurrencies = "organization_transactioncurrencies";
                public const string OrganizationTranslationprocess = "organization_translationprocess";
                public const string OrganizationUserMapping = "organization_UserMapping";
                public const string OrganizationUsermobileofflineprofilemembership = "organization_usermobileofflineprofilemembership";
                public const string OrganizationUserrating = "organization_userrating";
                public const string OrganizationUxagentproject = "organization_uxagentproject";
                public const string OrganizationUxagentprojectfile = "organization_uxagentprojectfile";
                public const string OrganizationViewasexamplequestion = "organization_viewasexamplequestion";
                public const string OrganizationVirtualentitymetadata = "organization_virtualentitymetadata";
                public const string OrganizationWebwizard = "organization_webwizard";
                public const string OrganizationWizardaccessprivilege = "organization_wizardaccessprivilege";
                public const string OrganizationWizardpage = "organization_wizardpage";
                public const string UserentityinstancedataOrganization = "userentityinstancedata_organization";
                public const string WebresourceOrganization = "webresource_organization";
            }

            public static partial class ManyToOne
            {
                public const string BasecurrencyOrganization = "basecurrency_organization";
                public const string CalendarOrganization = "calendar_organization";
                public const string DefaultMobileOfflineProfileOrganization = "DefaultMobileOfflineProfile_Organization";
                public const string EmailServerProfileOrganization = "EmailServerProfile_Organization";
                public const string LkOrganizationCreatedonbehalfby = "lk_organization_createdonbehalfby";
                public const string LkOrganizationEntityimage = "lk_organization_entityimage";
                public const string LkOrganizationModifiedonbehalfby = "lk_organization_modifiedonbehalfby";
                public const string LkOrganizationbaseCreatedby = "lk_organizationbase_createdby";
                public const string LkOrganizationbaseModifiedby = "lk_organizationbase_modifiedby";
                public const string TemplateOrganization = "Template_Organization";
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

        public static Organization Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static Organization Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("organization", id, columnSet).ToEntity<Organization>();
        }

        public Organization GetChangedEntity()
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
            return new Organization(Id) { Attributes = attr };
        }
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<Organization> OrganizationSet
        {
            get
            {
                return CreateQuery<Organization>();
            }
        }
    }
    #endregion
}
