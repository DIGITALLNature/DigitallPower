using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Top level of the Microsoft Dynamics 365 business hierarchy. The organization can be a specific business, holding company, or corporation.
	/// </summary>
    [EntityLogicalName("organization")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class Organization : Entity
    {
        #region ctor
        public Organization() : base(EntityLogicalName) { }

        public Organization(Guid id) : base(EntityLogicalName, id) { }

        public Organization(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public Organization(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "organization";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 1019;
        #endregion

        #region Attributes
        [AttributeLogicalName("organizationid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
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
            get
            {
                return GetAttributeValue<string?>("aciwebendpointurl");
            }
            set
            {
                SetAttributeValue("aciwebendpointurl", value);
            }
        }

        /// <summary>
		/// Unique identifier of the template to be used for acknowledgement when a user unsubscribes.
		/// </summary>
        [AttributeLogicalName("acknowledgementtemplateid")]
        public EntityReference? AcknowledgementTemplateId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("acknowledgementtemplateid");
            }
            set
            {
                SetAttributeValue("acknowledgementtemplateid", value);
            }
        }

        /// <summary>
		/// Information on whether filtering activity based on entity in app.
		/// </summary>
        [AttributeLogicalName("activitytypefilter")]
        public bool? ActivityTypeFilter
        {
            get
            {
                return GetAttributeValue<bool?>("activitytypefilter");
            }
            set
            {
                SetAttributeValue("activitytypefilter", value);
            }
        }

        /// <summary>
		/// Whether to show only activities configured in this app or all activities in the 'New activity' button.
		/// </summary>
        [AttributeLogicalName("activitytypefilterv2")]
        public bool? ActivityTypeFilterV2
        {
            get
            {
                return GetAttributeValue<bool?>("activitytypefilterv2");
            }
            set
            {
                SetAttributeValue("activitytypefilterv2", value);
            }
        }

        /// <summary>
		/// Flag to indicate if the display column options on a view in model-driven apps is enabled
		/// </summary>
        [AttributeLogicalName("advancedcolumneditorenabled")]
        public bool? AdvancedColumnEditorEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("advancedcolumneditorenabled");
            }
            set
            {
                SetAttributeValue("advancedcolumneditorenabled", value);
            }
        }

        /// <summary>
		/// Flag to indicate if the advanced column filtering in a view in model-driven apps is enabled
		/// </summary>
        [AttributeLogicalName("advancedcolumnfilteringenabled")]
        public bool? AdvancedColumnFilteringEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("advancedcolumnfilteringenabled");
            }
            set
            {
                SetAttributeValue("advancedcolumnfilteringenabled", value);
            }
        }

        /// <summary>
		/// Flag to indicate if the advanced filtering on all tables in a model-driven app is enabled
		/// </summary>
        [AttributeLogicalName("advancedfilteringenabled")]
        public bool? AdvancedFilteringEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("advancedfilteringenabled");
            }
            set
            {
                SetAttributeValue("advancedfilteringenabled", value);
            }
        }

        /// <summary>
		/// Flag to indicate if the Advanced Lookup feature is enabled for lookup controls
		/// </summary>
        [AttributeLogicalName("advancedlookupenabled")]
        public bool? AdvancedLookupEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("advancedlookupenabled");
            }
            set
            {
                SetAttributeValue("advancedlookupenabled", value);
            }
        }

        /// <summary>
		/// Enables advanced lookup in grid edit filter panel
		/// </summary>
        [AttributeLogicalName("advancedlookupineditfilter")]
        public int? AdvancedLookupInEditFilter
        {
            get
            {
                return GetAttributeValue<int?>("advancedlookupineditfilter");
            }
            set
            {
                SetAttributeValue("advancedlookupineditfilter", value);
            }
        }

        /// <summary>
		/// Indicates whether AI Builder features are blocked from using Copilot Credits.
		/// </summary>
        [AttributeLogicalName("aibuildercreditsonlyenabled")]
        public bool? AiBuilderCreditsOnlyEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("aibuildercreditsonlyenabled");
            }
            set
            {
                SetAttributeValue("aibuildercreditsonlyenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Azure AI Foundry model types for AI Prompts are enabled.
		/// </summary>
        [AttributeLogicalName("aipromptsazureaifoundrymodeltypesenabled")]
        public bool? AiPromptsAzureAIFoundryModelTypesEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("aipromptsazureaifoundrymodeltypesenabled");
            }
            set
            {
                SetAttributeValue("aipromptsazureaifoundrymodeltypesenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Basic model types for AI Prompts are enabled.
		/// </summary>
        [AttributeLogicalName("aipromptsbasicmodeltypesenabled")]
        public bool? AiPromptsBasicModelTypesEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("aipromptsbasicmodeltypesenabled");
            }
            set
            {
                SetAttributeValue("aipromptsbasicmodeltypesenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether AI Prompts feature is enabled.
		/// </summary>
        [AttributeLogicalName("aipromptsenabled")]
        public bool? AiPromptsEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("aipromptsenabled");
            }
            set
            {
                SetAttributeValue("aipromptsenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Premium model types for AI Prompts are enabled.
		/// </summary>
        [AttributeLogicalName("aipromptspremiummodeltypesenabled")]
        public bool? AiPromptsPremiumModelTypesEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("aipromptspremiummodeltypesenabled");
            }
            set
            {
                SetAttributeValue("aipromptspremiummodeltypesenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Standard model types for AI Prompts are enabled.
		/// </summary>
        [AttributeLogicalName("aipromptsstandardmodeltypesenabled")]
        public bool? AiPromptsStandardModelTypesEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("aipromptsstandardmodeltypesenabled");
            }
            set
            {
                SetAttributeValue("aipromptsstandardmodeltypesenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether background address book synchronization in Microsoft Office Outlook is allowed.
		/// </summary>
        [AttributeLogicalName("allowaddressbooksyncs")]
        public bool? AllowAddressBookSyncs
        {
            get
            {
                return GetAttributeValue<bool?>("allowaddressbooksyncs");
            }
            set
            {
                SetAttributeValue("allowaddressbooksyncs", value);
            }
        }

        /// <summary>
		/// Information that specifies whether all application users are allowed to access the environment
		/// </summary>
        [AttributeLogicalName("allowapplicationuseraccess")]
        public bool? AllowApplicationUserAccess
        {
            get
            {
                return GetAttributeValue<bool?>("allowapplicationuseraccess");
            }
            set
            {
                SetAttributeValue("allowapplicationuseraccess", value);
            }
        }

        /// <summary>
		/// Indicates whether automatic response creation is allowed.
		/// </summary>
        [AttributeLogicalName("allowautoresponsecreation")]
        public bool? AllowAutoResponseCreation
        {
            get
            {
                return GetAttributeValue<bool?>("allowautoresponsecreation");
            }
            set
            {
                SetAttributeValue("allowautoresponsecreation", value);
            }
        }

        /// <summary>
		/// Indicates whether automatic unsubscribe is allowed.
		/// </summary>
        [AttributeLogicalName("allowautounsubscribe")]
        public bool? AllowAutoUnsubscribe
        {
            get
            {
                return GetAttributeValue<bool?>("allowautounsubscribe");
            }
            set
            {
                SetAttributeValue("allowautounsubscribe", value);
            }
        }

        /// <summary>
		/// Indicates whether automatic unsubscribe acknowledgement email is allowed to send.
		/// </summary>
        [AttributeLogicalName("allowautounsubscribeacknowledgement")]
        public bool? AllowAutoUnsubscribeAcknowledgement
        {
            get
            {
                return GetAttributeValue<bool?>("allowautounsubscribeacknowledgement");
            }
            set
            {
                SetAttributeValue("allowautounsubscribeacknowledgement", value);
            }
        }

        /// <summary>
		/// Indicates whether Outlook Client message bar advertisement is allowed.
		/// </summary>
        [AttributeLogicalName("allowclientmessagebarad")]
        public bool? AllowClientMessageBarAd
        {
            get
            {
                return GetAttributeValue<bool?>("allowclientmessagebarad");
            }
            set
            {
                SetAttributeValue("allowclientmessagebarad", value);
            }
        }

        /// <summary>
		/// Information on whether connectors on power fx actions is enabled.
		/// </summary>
        [AttributeLogicalName("allowconnectorsonpowerfxactions")]
        public bool? AllowConnectorsOnPowerFXActions
        {
            get
            {
                return GetAttributeValue<bool?>("allowconnectorsonpowerfxactions");
            }
            set
            {
                SetAttributeValue("allowconnectorsonpowerfxactions", value);
            }
        }

        /// <summary>
		/// Information that specifies the Applications that are in allow list for the accessing DV resources.
		/// </summary>
        [AttributeLogicalName("allowedapplicationsfordvaccess")]
        public string? AllowedApplicationsForDVAccess
        {
            get
            {
                return GetAttributeValue<string?>("allowedapplicationsfordvaccess");
            }
            set
            {
                SetAttributeValue("allowedapplicationsfordvaccess", value);
            }
        }

        /// <summary>
		/// Information that specifies the range of IP addresses that are in allow list for the firewall.
		/// </summary>
        [AttributeLogicalName("allowediprangeforfirewall")]
        public string? AllowedIpRangeForFirewall
        {
            get
            {
                return GetAttributeValue<string?>("allowediprangeforfirewall");
            }
            set
            {
                SetAttributeValue("allowediprangeforfirewall", value);
            }
        }

        /// <summary>
		/// Information that specifies the range of IP addresses that are in allowed list for generating the SAS URIs.
		/// </summary>
        [AttributeLogicalName("allowediprangeforstorageaccesssignatures")]
        public string? AllowedIpRangeForStorageAccessSignatures
        {
            get
            {
                return GetAttributeValue<string?>("allowediprangeforstorageaccesssignatures");
            }
            set
            {
                SetAttributeValue("allowediprangeforstorageaccesssignatures", value);
            }
        }

        /// <summary>
		/// Specifies list of allowed IP addresses for firewall.
		/// </summary>
        [AttributeLogicalName("allowedlistofiprangesforfirewall")]
        public string? AllowedListOfIpRangesForFirewall
        {
            get
            {
                return GetAttributeValue<string?>("allowedlistofiprangesforfirewall");
            }
            set
            {
                SetAttributeValue("allowedlistofiprangesforfirewall", value);
            }
        }

        /// <summary>
		/// Allow upload or download of certain mime types.
		/// </summary>
        [AttributeLogicalName("allowedmimetypes")]
        public string? AllowedMimeTypes
        {
            get
            {
                return GetAttributeValue<string?>("allowedmimetypes");
            }
            set
            {
                SetAttributeValue("allowedmimetypes", value);
            }
        }

        /// <summary>
		/// Information that specifies the List of Service Tags that should be allowed by the firewall.
		/// </summary>
        [AttributeLogicalName("allowedservicetagsforfirewall")]
        public string? AllowedServiceTagsForFirewall
        {
            get
            {
                return GetAttributeValue<string?>("allowedservicetagsforfirewall");
            }
            set
            {
                SetAttributeValue("allowedservicetagsforfirewall", value);
            }
        }

        /// <summary>
		/// Indicates whether auditing of changes to entity is allowed when no attributes have changed.
		/// </summary>
        [AttributeLogicalName("allowentityonlyaudit")]
        public bool? AllowEntityOnlyAudit
        {
            get
            {
                return GetAttributeValue<bool?>("allowentityonlyaudit");
            }
            set
            {
                SetAttributeValue("allowentityonlyaudit", value);
            }
        }

        /// <summary>
		/// Enables ends-with searches in grids with the use of a leading wildcard on all tables in the environment
		/// </summary>
        [AttributeLogicalName("allowleadingwildcardsingridsearch")]
        public bool? AllowLeadingWildcardsInGridSearch
        {
            get
            {
                return GetAttributeValue<bool?>("allowleadingwildcardsingridsearch");
            }
            set
            {
                SetAttributeValue("allowleadingwildcardsingridsearch", value);
            }
        }

        /// <summary>
		/// Enables ends-with searches in grids with the use of a leading wildcard on all tables in the environment
		/// </summary>
        [AttributeLogicalName("allowleadingwildcardsinquickfind")]
        public int? AllowLeadingWildcardsInQuickFind
        {
            get
            {
                return GetAttributeValue<int?>("allowleadingwildcardsinquickfind");
            }
            set
            {
                SetAttributeValue("allowleadingwildcardsinquickfind", value);
            }
        }

        /// <summary>
		/// Enable access to legacy web client UI
		/// </summary>
        [AttributeLogicalName("allowlegacyclientexperience")]
        public bool? AllowLegacyClientExperience
        {
            get
            {
                return GetAttributeValue<bool?>("allowlegacyclientexperience");
            }
            set
            {
                SetAttributeValue("allowlegacyclientexperience", value);
            }
        }

        /// <summary>
		/// Enable embedding of certain legacy dialogs in Unified Interface browser client
		/// </summary>
        [AttributeLogicalName("allowlegacydialogsembedding")]
        public bool? AllowLegacyDialogsEmbedding
        {
            get
            {
                return GetAttributeValue<bool?>("allowlegacydialogsembedding");
            }
            set
            {
                SetAttributeValue("allowlegacydialogsembedding", value);
            }
        }

        /// <summary>
		/// Indicates whether marketing emails execution is allowed.
		/// </summary>
        [AttributeLogicalName("allowmarketingemailexecution")]
        public bool? AllowMarketingEmailExecution
        {
            get
            {
                return GetAttributeValue<bool?>("allowmarketingemailexecution");
            }
            set
            {
                SetAttributeValue("allowmarketingemailexecution", value);
            }
        }

        /// <summary>
		/// Information that specifies whether Microsoft Trusted Service Tags are allowed
		/// </summary>
        [AttributeLogicalName("allowmicrosofttrustedservicetags")]
        public bool? AllowMicrosoftTrustedServiceTags
        {
            get
            {
                return GetAttributeValue<bool?>("allowmicrosofttrustedservicetags");
            }
            set
            {
                SetAttributeValue("allowmicrosofttrustedservicetags", value);
            }
        }

        /// <summary>
		/// Indicates whether background offline synchronization in Microsoft Office Outlook is allowed.
		/// </summary>
        [AttributeLogicalName("allowofflinescheduledsyncs")]
        public bool? AllowOfflineScheduledSyncs
        {
            get
            {
                return GetAttributeValue<bool?>("allowofflinescheduledsyncs");
            }
            set
            {
                SetAttributeValue("allowofflinescheduledsyncs", value);
            }
        }

        /// <summary>
		/// Indicates whether scheduled synchronizations to Outlook are allowed.
		/// </summary>
        [AttributeLogicalName("allowoutlookscheduledsyncs")]
        public bool? AllowOutlookScheduledSyncs
        {
            get
            {
                return GetAttributeValue<bool?>("allowoutlookscheduledsyncs");
            }
            set
            {
                SetAttributeValue("allowoutlookscheduledsyncs", value);
            }
        }

        /// <summary>
		/// Control whether the organization Allow Redirect Legacy Admin Settings To Modern UI
		/// </summary>
        [AttributeLogicalName("allowredirectadminsettingstomodernui")]
        public bool? AllowRedirectAdminSettingsToModernUI
        {
            get
            {
                return GetAttributeValue<bool?>("allowredirectadminsettingstomodernui");
            }
            set
            {
                SetAttributeValue("allowredirectadminsettingstomodernui", value);
            }
        }

        /// <summary>
		/// Indicates whether users are allowed to send email to unresolved parties (parties must still have an email address).
		/// </summary>
        [AttributeLogicalName("allowunresolvedpartiesonemailsend")]
        public bool? AllowUnresolvedPartiesOnEmailSend
        {
            get
            {
                return GetAttributeValue<bool?>("allowunresolvedpartiesonemailsend");
            }
            set
            {
                SetAttributeValue("allowunresolvedpartiesonemailsend", value);
            }
        }

        /// <summary>
		/// Indicates whether individuals can select their form mode preference in their personal options.
		/// </summary>
        [AttributeLogicalName("allowuserformmodepreference")]
        public bool? AllowUserFormModePreference
        {
            get
            {
                return GetAttributeValue<bool?>("allowuserformmodepreference");
            }
            set
            {
                SetAttributeValue("allowuserformmodepreference", value);
            }
        }

        /// <summary>
		/// Flag to indicate if allow end users to hide system views in model-driven apps is enabled
		/// </summary>
        [AttributeLogicalName("allowusershidingsystemviews")]
        public bool? AllowUsersHidingSystemViews
        {
            get
            {
                return GetAttributeValue<bool?>("allowusershidingsystemviews");
            }
            set
            {
                SetAttributeValue("allowusershidingsystemviews", value);
            }
        }

        /// <summary>
		/// Indicates whether the showing tablet application notification bars in a browser is allowed.
		/// </summary>
        [AttributeLogicalName("allowusersseeappdownloadmessage")]
        public bool? AllowUsersSeeAppdownloadMessage
        {
            get
            {
                return GetAttributeValue<bool?>("allowusersseeappdownloadmessage");
            }
            set
            {
                SetAttributeValue("allowusersseeappdownloadmessage", value);
            }
        }

        /// <summary>
		/// Warning : Allowing  Virtual Entity plugin execution on nested pipeline does not offer transactional support. i.e. if call in native entity pipeline fails, then virtual entity operation will not be reverted.
		/// </summary>
        [AttributeLogicalName("allowvirtualentitypluginexecutiononnestedpipeline")]
        public bool? AllowVirtualEntityPluginExecutionOnNestedPipeline
        {
            get
            {
                return GetAttributeValue<bool?>("allowvirtualentitypluginexecutiononnestedpipeline");
            }
            set
            {
                SetAttributeValue("allowvirtualentitypluginexecutiononnestedpipeline", value);
            }
        }

        /// <summary>
		/// Indicates whether Web-based export of grids to Microsoft Office Excel is allowed.
		/// </summary>
        [AttributeLogicalName("allowwebexcelexport")]
        public bool? AllowWebExcelExport
        {
            get
            {
                return GetAttributeValue<bool?>("allowwebexcelexport");
            }
            set
            {
                SetAttributeValue("allowwebexcelexport", value);
            }
        }

        /// <summary>
		/// AM designator to use throughout Microsoft Dynamics CRM.
		/// </summary>
        [AttributeLogicalName("amdesignator")]
        public string? AMDesignator
        {
            get
            {
                return GetAttributeValue<string?>("amdesignator");
            }
            set
            {
                SetAttributeValue("amdesignator", value);
            }
        }

        /// <summary>
		/// Indicates whether the appDesignerExperience is enabled for the organization.
		/// </summary>
        [AttributeLogicalName("appdesignerexperienceenabled")]
        public bool? AppDesignerExperienceEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("appdesignerexperienceenabled");
            }
            set
            {
                SetAttributeValue("appdesignerexperienceenabled", value);
            }
        }

        /// <summary>
		/// Application Based Access Control Mode. 0 is Disabled, 1 is audit mode , 2 is enforcement mode
		/// </summary>
        [AttributeLogicalName("applicationbasedaccesscontrolmode")]
        public OptionSetValue? ApplicationBasedAccessControlMode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("applicationbasedaccesscontrolmode");
            }
            set
            {
                SetAttributeValue("applicationbasedaccesscontrolmode", value);
            }
        }

        /// <summary>
		/// Information on whether rich editing experience for Appointment is enabled.
		/// </summary>
        [AttributeLogicalName("appointmentricheditorexperience")]
        public bool? AppointmentRichEditorExperience
        {
            get
            {
                return GetAttributeValue<bool?>("appointmentricheditorexperience");
            }
            set
            {
                SetAttributeValue("appointmentricheditorexperience", value);
            }
        }

        /// <summary>
		/// Information on whether Teams meeting experience for Appointment is enabled.
		/// </summary>
        [AttributeLogicalName("appointmentwithteamsmeeting")]
        public bool? AppointmentWithTeamsMeeting
        {
            get
            {
                return GetAttributeValue<bool?>("appointmentwithteamsmeeting");
            }
            set
            {
                SetAttributeValue("appointmentwithteamsmeeting", value);
            }
        }

        /// <summary>
		/// Whether Teams meetings experience for appointments is enabled.
		/// </summary>
        [AttributeLogicalName("appointmentwithteamsmeetingv2")]
        public bool? AppointmentWithTeamsMeetingV2
        {
            get
            {
                return GetAttributeValue<bool?>("appointmentwithteamsmeetingv2");
            }
            set
            {
                SetAttributeValue("appointmentwithteamsmeetingv2", value);
            }
        }

        /// <summary>
		/// Indicates whether Power Automate Automation Center preview features will be available for all users in this organization.
		/// </summary>
        [AttributeLogicalName("areautomationcenterpreviewfeaturesenabled")]
        public bool? AreAutomationCenterPreviewFeaturesEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("areautomationcenterpreviewfeaturesenabled");
            }
            set
            {
                SetAttributeValue("areautomationcenterpreviewfeaturesenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Process Insights Preview features are enabled in this organization.
		/// </summary>
        [AttributeLogicalName("areprocessinsightspreviewfeaturesenabled")]
        public bool? AreProcessInsightsPreviewFeaturesEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("areprocessinsightspreviewfeaturesenabled");
            }
            set
            {
                SetAttributeValue("areprocessinsightspreviewfeaturesenabled", value);
            }
        }

        /// <summary>
		/// Audit Retention Period settings stored in Organization Database.
		/// </summary>
        [AttributeLogicalName("auditretentionperiod")]
        public int? AuditRetentionPeriod
        {
            get
            {
                return GetAttributeValue<int?>("auditretentionperiod");
            }
            set
            {
                SetAttributeValue("auditretentionperiod", value);
            }
        }

        /// <summary>
		/// Audit Retention Period settings stored in Organization Database.
		/// </summary>
        [AttributeLogicalName("auditretentionperiodv2")]
        public int? AuditRetentionPeriodV2
        {
            get
            {
                return GetAttributeValue<int?>("auditretentionperiodv2");
            }
            set
            {
                SetAttributeValue("auditretentionperiodv2", value);
            }
        }

        /// <summary>
		/// Audit Settings of the organization
		/// </summary>
        [AttributeLogicalName("auditsettings")]
        public string? AuditSettings
        {
            get
            {
                return GetAttributeValue<string?>("auditsettings");
            }
            set
            {
                SetAttributeValue("auditsettings", value);
            }
        }

        /// <summary>
		/// Select whether to auto apply the default customer entitlement on case creation.
		/// </summary>
        [AttributeLogicalName("autoapplydefaultoncasecreate")]
        public bool? AutoApplyDefaultonCaseCreate
        {
            get
            {
                return GetAttributeValue<bool?>("autoapplydefaultoncasecreate");
            }
            set
            {
                SetAttributeValue("autoapplydefaultoncasecreate", value);
            }
        }

        /// <summary>
		/// Select whether to auto apply the default customer entitlement on case update.
		/// </summary>
        [AttributeLogicalName("autoapplydefaultoncaseupdate")]
        public bool? AutoApplyDefaultonCaseUpdate
        {
            get
            {
                return GetAttributeValue<bool?>("autoapplydefaultoncaseupdate");
            }
            set
            {
                SetAttributeValue("autoapplydefaultoncaseupdate", value);
            }
        }

        /// <summary>
		/// Indicates whether to Auto-apply SLA on case record update after SLA was manually applied.
		/// </summary>
        [AttributeLogicalName("autoapplysla")]
        public bool? AutoApplySLA
        {
            get
            {
                return GetAttributeValue<bool?>("autoapplysla");
            }
            set
            {
                SetAttributeValue("autoapplysla", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("azureschedulerjobcollectionname")]
        public string? AzureSchedulerJobCollectionName
        {
            get
            {
                return GetAttributeValue<string?>("azureschedulerjobcollectionname");
            }
            set
            {
                SetAttributeValue("azureschedulerjobcollectionname", value);
            }
        }

        /// <summary>
		/// Unique identifier of the base currency of the organization.
		/// </summary>
        [AttributeLogicalName("basecurrencyid")]
        public EntityReference? BaseCurrencyId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("basecurrencyid");
            }
            set
            {
                SetAttributeValue("basecurrencyid", value);
            }
        }

        /// <summary>
		/// Number of decimal places that can be used for the base currency.
		/// </summary>
        [AttributeLogicalName("basecurrencyprecision")]
        public int? BaseCurrencyPrecision
        {
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
            get
            {
                return GetAttributeValue<string?>("bingmapsapikey");
            }
            set
            {
                SetAttributeValue("bingmapsapikey", value);
            }
        }

        /// <summary>
		/// Enable this feature to prevent makers from accessing and downloading session transcripts
		/// </summary>
        [AttributeLogicalName("blockaccesstosessiontranscriptsforcopilotstudio")]
        public bool? BlockAccessToSessionTranscriptsForCopilotStudio
        {
            get
            {
                return GetAttributeValue<bool?>("blockaccesstosessiontranscriptsforcopilotstudio");
            }
            set
            {
                SetAttributeValue("blockaccesstosessiontranscriptsforcopilotstudio", value);
            }
        }

        /// <summary>
		/// Prevent makers from allowing end-users to use their credentials during authentication to use connectors, actions, flows, and triggers that are connected to an agent
		/// </summary>
        [AttributeLogicalName("blockcopilotauthorauthentication")]
        public bool? BlockCopilotAuthorAuthentication
        {
            get
            {
                return GetAttributeValue<bool?>("blockcopilotauthorauthentication");
            }
            set
            {
                SetAttributeValue("blockcopilotauthorauthentication", value);
            }
        }

        /// <summary>
		/// Information that specifies the Applications that are in block list for the accessing DV resources.
		/// </summary>
        [AttributeLogicalName("blockedapplicationsfordvaccess")]
        public string? BlockedApplicationsForDVAccess
        {
            get
            {
                return GetAttributeValue<string?>("blockedapplicationsfordvaccess");
            }
            set
            {
                SetAttributeValue("blockedapplicationsfordvaccess", value);
            }
        }

        /// <summary>
		/// Prevent upload or download of certain attachment types that are considered dangerous.
		/// </summary>
        [AttributeLogicalName("blockedattachments")]
        public string? BlockedAttachments
        {
            get
            {
                return GetAttributeValue<string?>("blockedattachments");
            }
            set
            {
                SetAttributeValue("blockedattachments", value);
            }
        }

        /// <summary>
		/// Prevent upload or download of certain mime types that are considered dangerous.
		/// </summary>
        [AttributeLogicalName("blockedmimetypes")]
        public string? BlockedMimeTypes
        {
            get
            {
                return GetAttributeValue<string?>("blockedmimetypes");
            }
            set
            {
                SetAttributeValue("blockedmimetypes", value);
            }
        }

        /// <summary>
		/// Enable this feature to block access to session transcripts and conversational transcripts from being written to Dataverse for an individual environment
		/// </summary>
        [AttributeLogicalName("blocktranscriptrecordingforcopilotstudio")]
        public bool? BlockTranscriptRecordingForCopilotStudio
        {
            get
            {
                return GetAttributeValue<bool?>("blocktranscriptrecordingforcopilotstudio");
            }
            set
            {
                SetAttributeValue("blocktranscriptrecordingforcopilotstudio", value);
            }
        }

        /// <summary>
		/// Enable this feature to block URLs and images in Copilot Studio and agent responses for an individual environment. URLs will be replaced with placeholders.
		/// </summary>
        [AttributeLogicalName("blockurlsinresponsesforcopilotstudio")]
        public bool? BlockUrlsInResponsesForCopilotStudio
        {
            get
            {
                return GetAttributeValue<bool?>("blockurlsinresponsesforcopilotstudio");
            }
            set
            {
                SetAttributeValue("blockurlsinresponsesforcopilotstudio", value);
            }
        }

        /// <summary>
		/// Display cards in expanded state for interactive dashboard
		/// </summary>
        [AttributeLogicalName("bounddashboarddefaultcardexpanded")]
        public bool? BoundDashboardDefaultCardExpanded
        {
            get
            {
                return GetAttributeValue<bool?>("bounddashboarddefaultcardexpanded");
            }
            set
            {
                SetAttributeValue("bounddashboarddefaultcardexpanded", value);
            }
        }

        /// <summary>
		/// Prefix used for bulk operation numbering.
		/// </summary>
        [AttributeLogicalName("bulkoperationprefix")]
        public string? BulkOperationPrefix
        {
            get
            {
                return GetAttributeValue<string?>("bulkoperationprefix");
            }
            set
            {
                SetAttributeValue("bulkoperationprefix", value);
            }
        }

        /// <summary>
		/// BusinessCardOptions
		/// </summary>
        [AttributeLogicalName("businesscardoptions")]
        public string? BusinessCardOptions
        {
            get
            {
                return GetAttributeValue<string?>("businesscardoptions");
            }
            set
            {
                SetAttributeValue("businesscardoptions", value);
            }
        }

        /// <summary>
		/// Unique identifier of the business closure calendar of organization.
		/// </summary>
        [AttributeLogicalName("businessclosurecalendarid")]
        public Guid? BusinessClosureCalendarId
        {
            get
            {
                return GetAttributeValue<Guid?>("businessclosurecalendarid");
            }
            set
            {
                SetAttributeValue("businessclosurecalendarid", value);
            }
        }

        /// <summary>
		/// Calendar type for the system. Set to Gregorian US by default.
		/// </summary>
        [AttributeLogicalName("calendartype")]
        public int? CalendarType
        {
            get
            {
                return GetAttributeValue<int?>("calendartype");
            }
            set
            {
                SetAttributeValue("calendartype", value);
            }
        }

        /// <summary>
		/// Prefix used for campaign numbering.
		/// </summary>
        [AttributeLogicalName("campaignprefix")]
        public string? CampaignPrefix
        {
            get
            {
                return GetAttributeValue<string?>("campaignprefix");
            }
            set
            {
                SetAttributeValue("campaignprefix", value);
            }
        }

        /// <summary>
		/// Indicates whether the organization can opt out of the new Relevance search experience (released in Oct 2020)
		/// </summary>
        [AttributeLogicalName("canoptoutnewsearchexperience")]
        public bool? CanOptOutNewSearchExperience
        {
            get
            {
                return GetAttributeValue<bool?>("canoptoutnewsearchexperience");
            }
            set
            {
                SetAttributeValue("canoptoutnewsearchexperience", value);
            }
        }

        /// <summary>
		/// Flag to cascade Update on incident.
		/// </summary>
        [AttributeLogicalName("cascadestatusupdate")]
        public bool? CascadeStatusUpdate
        {
            get
            {
                return GetAttributeValue<bool?>("cascadestatusupdate");
            }
            set
            {
                SetAttributeValue("cascadestatusupdate", value);
            }
        }

        /// <summary>
		/// Prefix to use for all cases throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("caseprefix")]
        public string? CasePrefix
        {
            get
            {
                return GetAttributeValue<string?>("caseprefix");
            }
            set
            {
                SetAttributeValue("caseprefix", value);
            }
        }

        /// <summary>
		/// Type the prefix to use for all categories in Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("categoryprefix")]
        public string? CategoryPrefix
        {
            get
            {
                return GetAttributeValue<string?>("categoryprefix");
            }
            set
            {
                SetAttributeValue("categoryprefix", value);
            }
        }

        /// <summary>
		/// Client Features to be enabled as an XML BLOB.
		/// </summary>
        [AttributeLogicalName("clientfeatureset")]
        public string? ClientFeatureSet
        {
            get
            {
                return GetAttributeValue<string?>("clientfeatureset");
            }
            set
            {
                SetAttributeValue("clientfeatureset", value);
            }
        }

        /// <summary>
		/// Policy configuration for CSP
		/// </summary>
        [AttributeLogicalName("contentsecuritypolicyconfiguration")]
        public string? ContentSecurityPolicyConfiguration
        {
            get
            {
                return GetAttributeValue<string?>("contentsecuritypolicyconfiguration");
            }
            set
            {
                SetAttributeValue("contentsecuritypolicyconfiguration", value);
            }
        }

        /// <summary>
		/// Content Security Policy configuration for Canvas apps.
		/// </summary>
        [AttributeLogicalName("contentsecuritypolicyconfigurationforcanvas")]
        public string? ContentSecurityPolicyConfigurationForCanvas
        {
            get
            {
                return GetAttributeValue<string?>("contentsecuritypolicyconfigurationforcanvas");
            }
            set
            {
                SetAttributeValue("contentsecuritypolicyconfigurationforcanvas", value);
            }
        }

        /// <summary>
		/// Content Security Policy Options.
		/// </summary>
        [AttributeLogicalName("contentsecuritypolicyoptions")]
        public int? ContentSecurityPolicyOptions
        {
            get
            {
                return GetAttributeValue<int?>("contentsecuritypolicyoptions");
            }
            set
            {
                SetAttributeValue("contentsecuritypolicyoptions", value);
            }
        }

        /// <summary>
		/// Content Security Policy Report Uri.
		/// </summary>
        [AttributeLogicalName("contentsecuritypolicyreporturi")]
        public string? ContentSecurityPolicyReportUri
        {
            get
            {
                return GetAttributeValue<string?>("contentsecuritypolicyreporturi");
            }
            set
            {
                SetAttributeValue("contentsecuritypolicyreporturi", value);
            }
        }

        /// <summary>
		/// Prefix to use for all contracts throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("contractprefix")]
        public string? ContractPrefix
        {
            get
            {
                return GetAttributeValue<string?>("contractprefix");
            }
            set
            {
                SetAttributeValue("contractprefix", value);
            }
        }

        /// <summary>
		/// Refresh rate for copresence data in seconds.
		/// </summary>
        [AttributeLogicalName("copresencerefreshrate")]
        public int? CopresenceRefreshRate
        {
            get
            {
                return GetAttributeValue<int?>("copresencerefreshrate");
            }
            set
            {
                SetAttributeValue("copresencerefreshrate", value);
            }
        }

        /// <summary>
		/// Indicates whether the feature CortanaProactiveExperience Flow processes should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("cortanaproactiveexperienceenabled")]
        public bool? CortanaProactiveExperienceEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("cortanaproactiveexperienceenabled");
            }
            set
            {
                SetAttributeValue("cortanaproactiveexperienceenabled", value);
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the organization.
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
		/// Date and time when the organization was created.
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
		/// Unique identifier of the delegate user who created the organization.
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
		/// Enable Initial state of newly created products to be Active instead of Draft
		/// </summary>
        [AttributeLogicalName("createproductswithoutparentinactivestate")]
        public bool? CreateProductsWithoutParentInActiveState
        {
            get
            {
                return GetAttributeValue<bool?>("createproductswithoutparentinactivestate");
            }
            set
            {
                SetAttributeValue("createproductswithoutparentinactivestate", value);
            }
        }

        /// <summary>
		/// Default time to live in minutes for new records in the Flow Logs entity for CUA logs.
		/// </summary>
        [AttributeLogicalName("cuaflowlogsttlinminutes")]
        public int? CuaFlowLogsTtlInMinutes
        {
            get
            {
                return GetAttributeValue<int?>("cuaflowlogsttlinminutes");
            }
            set
            {
                SetAttributeValue("cuaflowlogsttlinminutes", value);
            }
        }

        /// <summary>
		/// Set the level of detail the computer use logs allow.
		/// </summary>
        [AttributeLogicalName("cuaflowlogsverbosity")]
        public OptionSetValue? CuaFlowLogsVerbosity
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("cuaflowlogsverbosity");
            }
            set
            {
                SetAttributeValue("cuaflowlogsverbosity", value);
            }
        }

        /// <summary>
		/// Number of decimal places that can be used for currency.
		/// </summary>
        [AttributeLogicalName("currencydecimalprecision")]
        public int? CurrencyDecimalPrecision
        {
            get
            {
                return GetAttributeValue<int?>("currencydecimalprecision");
            }
            set
            {
                SetAttributeValue("currencydecimalprecision", value);
            }
        }

        /// <summary>
		/// Indicates whether to display money fields with currency code or currency symbol.
		/// </summary>
        [AttributeLogicalName("currencydisplayoption")]
        public OptionSetValue? CurrencyDisplayOption
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("currencydisplayoption");
            }
            set
            {
                SetAttributeValue("currencydisplayoption", value);
            }
        }

        /// <summary>
		/// Information about how currency symbols are placed throughout Microsoft Dynamics CRM.
		/// </summary>
        [AttributeLogicalName("currencyformatcode")]
        public OptionSetValue? CurrencyFormatCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("currencyformatcode");
            }
            set
            {
                SetAttributeValue("currencyformatcode", value);
            }
        }

        /// <summary>
		/// Symbol used for currency throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("currencysymbol")]
        public string? CurrencySymbol
        {
            get
            {
                return GetAttributeValue<string?>("currencysymbol");
            }
            set
            {
                SetAttributeValue("currencysymbol", value);
            }
        }

        /// <summary>
		/// Current bulk operation number. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentbulkoperationnumber")]
        public int? CurrentBulkOperationNumber
        {
            get
            {
                return GetAttributeValue<int?>("currentbulkoperationnumber");
            }
            set
            {
                SetAttributeValue("currentbulkoperationnumber", value);
            }
        }

        /// <summary>
		/// Current campaign number. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentcampaignnumber")]
        public int? CurrentCampaignNumber
        {
            get
            {
                return GetAttributeValue<int?>("currentcampaignnumber");
            }
            set
            {
                SetAttributeValue("currentcampaignnumber", value);
            }
        }

        /// <summary>
		/// First case number to use. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentcasenumber")]
        public int? CurrentCaseNumber
        {
            get
            {
                return GetAttributeValue<int?>("currentcasenumber");
            }
            set
            {
                SetAttributeValue("currentcasenumber", value);
            }
        }

        /// <summary>
		/// Enter the first number to use for Categories. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentcategorynumber")]
        public int? CurrentCategoryNumber
        {
            get
            {
                return GetAttributeValue<int?>("currentcategorynumber");
            }
            set
            {
                SetAttributeValue("currentcategorynumber", value);
            }
        }

        /// <summary>
		/// First contract number to use. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentcontractnumber")]
        public int? CurrentContractNumber
        {
            get
            {
                return GetAttributeValue<int?>("currentcontractnumber");
            }
            set
            {
                SetAttributeValue("currentcontractnumber", value);
            }
        }

        /// <summary>
		/// Import sequence to use.
		/// </summary>
        [AttributeLogicalName("currentimportsequencenumber")]
        public int? CurrentImportSequenceNumber
        {
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
            get
            {
                return GetAttributeValue<int?>("currentinvoicenumber");
            }
            set
            {
                SetAttributeValue("currentinvoicenumber", value);
            }
        }

        /// <summary>
		/// Enter the first number to use for knowledge articles. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentkanumber")]
        public int? CurrentKaNumber
        {
            get
            {
                return GetAttributeValue<int?>("currentkanumber");
            }
            set
            {
                SetAttributeValue("currentkanumber", value);
            }
        }

        /// <summary>
		/// First article number to use. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentkbnumber")]
        public int? CurrentKbNumber
        {
            get
            {
                return GetAttributeValue<int?>("currentkbnumber");
            }
            set
            {
                SetAttributeValue("currentkbnumber", value);
            }
        }

        /// <summary>
		/// First order number to use. Deprecated. Use SetAutoNumberSeed message.
		/// </summary>
        [AttributeLogicalName("currentordernumber")]
        public int? CurrentOrderNumber
        {
            get
            {
                return GetAttributeValue<int?>("currentordernumber");
            }
            set
            {
                SetAttributeValue("currentordernumber", value);
            }
        }

        /// <summary>
		/// First parsed table number to use.
		/// </summary>
        [AttributeLogicalName("currentparsedtablenumber")]
        public int? CurrentParsedTableNumber
        {
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
            get
            {
                return GetAttributeValue<int?>("currentquotenumber");
            }
            set
            {
                SetAttributeValue("currentquotenumber", value);
            }
        }

        /// <summary>
		/// Information about how the date is displayed throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("dateformatcode")]
        public OptionSetValue? DateFormatCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("dateformatcode");
            }
            set
            {
                SetAttributeValue("dateformatcode", value);
            }
        }

        /// <summary>
		/// String showing how the date is displayed throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("dateformatstring")]
        public string? DateFormatString
        {
            get
            {
                return GetAttributeValue<string?>("dateformatstring");
            }
            set
            {
                SetAttributeValue("dateformatstring", value);
            }
        }

        /// <summary>
		/// Character used to separate the month, the day, and the year in dates throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("dateseparator")]
        public string? DateSeparator
        {
            get
            {
                return GetAttributeValue<string?>("dateseparator");
            }
            set
            {
                SetAttributeValue("dateseparator", value);
            }
        }

        /// <summary>
		/// Number of days before we migrate email description to blob.
		/// </summary>
        [AttributeLogicalName("daysbeforeemaildescriptionismigrated")]
        public int? DaysBeforeEmailDescriptionIsMigrated
        {
            get
            {
                return GetAttributeValue<int?>("daysbeforeemaildescriptionismigrated");
            }
            set
            {
                SetAttributeValue("daysbeforeemaildescriptionismigrated", value);
            }
        }

        /// <summary>
		/// Days of inactivity before sync is disabled for a Teams Chat.
		/// </summary>
        [AttributeLogicalName("daysbeforeinactiveteamschatsyncdisabled")]
        public int? DaysBeforeInactiveTeamsChatSyncDisabled
        {
            get
            {
                return GetAttributeValue<int?>("daysbeforeinactiveteamschatsyncdisabled");
            }
            set
            {
                SetAttributeValue("daysbeforeinactiveteamschatsyncdisabled", value);
            }
        }

        /// <summary>
		/// The maximum value for the Mobile Offline setting Days since record last modified
		/// </summary>
        [AttributeLogicalName("dayssincerecordlastmodifiedmaxvalue")]
        public int? DaysSinceRecordLastModifiedMaxValue
        {
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
            get
            {
                return GetAttributeValue<string?>("decimalsymbol");
            }
            set
            {
                SetAttributeValue("decimalsymbol", value);
            }
        }

        /// <summary>
		/// Text area to enter default country code.
		/// </summary>
        [AttributeLogicalName("defaultcountrycode")]
        public string? DefaultCountryCode
        {
            get
            {
                return GetAttributeValue<string?>("defaultcountrycode");
            }
            set
            {
                SetAttributeValue("defaultcountrycode", value);
            }
        }

        /// <summary>
		/// Name of the default crm custom.
		/// </summary>
        [AttributeLogicalName("defaultcrmcustomname")]
        public string? DefaultCrmCustomName
        {
            get
            {
                return GetAttributeValue<string?>("defaultcrmcustomname");
            }
            set
            {
                SetAttributeValue("defaultcrmcustomname", value);
            }
        }

        /// <summary>
		/// Unique identifier of the default email server profile.
		/// </summary>
        [AttributeLogicalName("defaultemailserverprofileid")]
        public EntityReference? DefaultEmailServerProfileId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("defaultemailserverprofileid");
            }
            set
            {
                SetAttributeValue("defaultemailserverprofileid", value);
            }
        }

        /// <summary>
		/// XML string containing the default email settings that are applied when a user or queue is created.
		/// </summary>
        [AttributeLogicalName("defaultemailsettings")]
        public string? DefaultEmailSettings
        {
            get
            {
                return GetAttributeValue<string?>("defaultemailsettings");
            }
            set
            {
                SetAttributeValue("defaultemailsettings", value);
            }
        }

        /// <summary>
		/// Unique identifier of the default mobile offline profile.
		/// </summary>
        [AttributeLogicalName("defaultmobileofflineprofileid")]
        public EntityReference? DefaultMobileOfflineProfileId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("defaultmobileofflineprofileid");
            }
            set
            {
                SetAttributeValue("defaultmobileofflineprofileid", value);
            }
        }

        /// <summary>
		/// Type of default recurrence end range date.
		/// </summary>
        [AttributeLogicalName("defaultrecurrenceendrangetype")]
        public OptionSetValue? DefaultRecurrenceEndRangeType
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("defaultrecurrenceendrangetype");
            }
            set
            {
                SetAttributeValue("defaultrecurrenceendrangetype", value);
            }
        }

        /// <summary>
		/// Default theme data for the organization.
		/// </summary>
        [AttributeLogicalName("defaultthemedata")]
        public string? DefaultThemeData
        {
            get
            {
                return GetAttributeValue<string?>("defaultthemedata");
            }
            set
            {
                SetAttributeValue("defaultthemedata", value);
            }
        }

        /// <summary>
		/// Unique identifier of the delegated admin user for the organization.
		/// </summary>
        [AttributeLogicalName("delegatedadminuserid")]
        public Guid? DelegatedAdminUserId
        {
            get
            {
                return GetAttributeValue<Guid?>("delegatedadminuserid");
            }
            set
            {
                SetAttributeValue("delegatedadminuserid", value);
            }
        }

        /// <summary>
		/// Default time to live in minutes for new desktop flow queue log records.
		/// </summary>
        [AttributeLogicalName("desktopflowqueuelogsttlinminutes")]
        public int? DesktopFlowQueueLogsTtlInMinutes
        {
            get
            {
                return GetAttributeValue<int?>("desktopflowqueuelogsttlinminutes");
            }
            set
            {
                SetAttributeValue("desktopflowqueuelogsttlinminutes", value);
            }
        }

        /// <summary>
		/// Customer-managed URL template for external desktop flow run action logs.
		/// </summary>
        [AttributeLogicalName("desktopflowrunactionlogscustomurl")]
        public string? DesktopFlowRunActionLogsCustomUrl
        {
            get
            {
                return GetAttributeValue<string?>("desktopflowrunactionlogscustomurl");
            }
            set
            {
                SetAttributeValue("desktopflowrunactionlogscustomurl", value);
            }
        }

        /// <summary>
		/// Indicates whether desktop flow run action logs use the customer-managed custom URL template.
		/// </summary>
        [AttributeLogicalName("desktopflowrunactionlogscustomurlenabled")]
        public bool? DesktopFlowRunActionLogsCustomUrlEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("desktopflowrunactionlogscustomurlenabled");
            }
            set
            {
                SetAttributeValue("desktopflowrunactionlogscustomurlenabled", value);
            }
        }

        /// <summary>
		/// Toggle the activation of the Power Automate Desktop Flow run action logs.
		/// </summary>
        [AttributeLogicalName("desktopflowrunactionlogsstatus")]
        public OptionSetValue? DesktopFlowRunActionLogsStatus
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("desktopflowrunactionlogsstatus");
            }
            set
            {
                SetAttributeValue("desktopflowrunactionlogsstatus", value);
            }
        }

        /// <summary>
		/// What verbosity level the Power Automate Desktop Flow Run Action Logs allow.
		/// </summary>
        [AttributeLogicalName("desktopflowrunactionlogverbosity")]
        public OptionSetValue? DesktopFlowRunActionLogVerbosity
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("desktopflowrunactionlogverbosity");
            }
            set
            {
                SetAttributeValue("desktopflowrunactionlogverbosity", value);
            }
        }

        /// <summary>
		/// Where the Power Automate Desktop Flow Run Action logs are stored.
		/// </summary>
        [AttributeLogicalName("desktopflowrunactionlogversion")]
        public OptionSetValue? DesktopFlowRunActionLogVersion
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("desktopflowrunactionlogversion");
            }
            set
            {
                SetAttributeValue("desktopflowrunactionlogversion", value);
            }
        }

        /// <summary>
		/// Reason for disabling the organization.
		/// </summary>
        [AttributeLogicalName("disabledreason")]
        public string? DisabledReason
        {
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
            get
            {
                return GetAttributeValue<bool?>("disablesocialcare");
            }
            set
            {
                SetAttributeValue("disablesocialcare", value);
            }
        }

        /// <summary>
		/// Disable sharing system labels for the organization.
		/// </summary>
        [AttributeLogicalName("disablesystemlabelscachesharing")]
        public bool? DisableSystemLabelsCacheSharing
        {
            get
            {
                return GetAttributeValue<bool?>("disablesystemlabelscachesharing");
            }
            set
            {
                SetAttributeValue("disablesystemlabelscachesharing", value);
            }
        }

        /// <summary>
		/// Discount calculation method for the QOOI product.
		/// </summary>
        [AttributeLogicalName("discountcalculationmethod")]
        public OptionSetValue? DiscountCalculationMethod
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("discountcalculationmethod");
            }
            set
            {
                SetAttributeValue("discountcalculationmethod", value);
            }
        }

        /// <summary>
		/// Indicates whether or not navigation tour is displayed.
		/// </summary>
        [AttributeLogicalName("displaynavigationtour")]
        public bool? DisplayNavigationTour
        {
            get
            {
                return GetAttributeValue<bool?>("displaynavigationtour");
            }
            set
            {
                SetAttributeValue("displaynavigationtour", value);
            }
        }

        /// <summary>
		/// Select if you want to use the Email Router or server-side synchronization for email processing.
		/// </summary>
        [AttributeLogicalName("emailconnectionchannel")]
        public OptionSetValue? EmailConnectionChannel
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("emailconnectionchannel");
            }
            set
            {
                SetAttributeValue("emailconnectionchannel", value);
            }
        }

        /// <summary>
		/// Flag to turn email correlation on or off.
		/// </summary>
        [AttributeLogicalName("emailcorrelationenabled")]
        public bool? EmailCorrelationEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("emailcorrelationenabled");
            }
            set
            {
                SetAttributeValue("emailcorrelationenabled", value);
            }
        }

        /// <summary>
		/// Normal polling frequency used for sending email in Microsoft Office Outlook.
		/// </summary>
        [AttributeLogicalName("emailsendpollingperiod")]
        public int? EmailSendPollingPeriod
        {
            get
            {
                return GetAttributeValue<int?>("emailsendpollingperiod");
            }
            set
            {
                SetAttributeValue("emailsendpollingperiod", value);
            }
        }

        /// <summary>
		/// Determines whether records merged through the merge dialog in UCI are merged asynchronously
		/// </summary>
        [AttributeLogicalName("enableasyncmergeapiforuci")]
        public bool? EnableAsyncMergeAPIForUCI
        {
            get
            {
                return GetAttributeValue<bool?>("enableasyncmergeapiforuci");
            }
            set
            {
                SetAttributeValue("enableasyncmergeapiforuci", value);
            }
        }

        /// <summary>
		/// Enable Integration with Bing Maps
		/// </summary>
        [AttributeLogicalName("enablebingmapsintegration")]
        public bool? EnableBingMapsIntegration
        {
            get
            {
                return GetAttributeValue<bool?>("enablebingmapsintegration");
            }
            set
            {
                SetAttributeValue("enablebingmapsintegration", value);
            }
        }

        /// <summary>
		/// Note: By enabling this feature, you will also enable the automatic creation of enviornment variables when adding data sources for your apps.
		/// </summary>
        [AttributeLogicalName("enablecanvasappsinsolutionsbydefault")]
        public bool? EnableCanvasAppsInSolutionsByDefault
        {
            get
            {
                return GetAttributeValue<bool?>("enablecanvasappsinsolutionsbydefault");
            }
            set
            {
                SetAttributeValue("enablecanvasappsinsolutionsbydefault", value);
            }
        }

        /// <summary>
		/// Enable this feature to allow cross-geo boundary sharing of aggregated analytics data if your preferred data location for Viva Insights is different than the location of your environment
		/// </summary>
        [AttributeLogicalName("enablecopilotstudiocrossgeosharedatawithvivainsights")]
        public bool? EnableCopilotStudioCrossGeoShareDataWithVivaInsights
        {
            get
            {
                return GetAttributeValue<bool?>("enablecopilotstudiocrossgeosharedatawithvivainsights");
            }
            set
            {
                SetAttributeValue("enablecopilotstudiocrossgeosharedatawithvivainsights", value);
            }
        }

        /// <summary>
		/// (Deprecated) Enable this feature to allow Copilot Studio to share aggregated analytics data for custom agents with Viva Insights for an individual environment
		/// </summary>
        [AttributeLogicalName("enablecopilotstudiosharedatawithvi")]
        public bool? EnableCopilotStudioShareDataWithVI
        {
            get
            {
                return GetAttributeValue<bool?>("enablecopilotstudiosharedatawithvi");
            }
            set
            {
                SetAttributeValue("enablecopilotstudiosharedatawithvi", value);
            }
        }

        /// <summary>
		/// Enable this feature to allow Copilot Studio to share aggregated analytics data for custom agents with Viva Insights for an individual environment
		/// </summary>
        [AttributeLogicalName("enablecopilotstudiosharedatawithvivainsights")]
        public bool? EnableCopilotStudioShareDataWithVivaInsights
        {
            get
            {
                return GetAttributeValue<bool?>("enablecopilotstudiosharedatawithvivainsights");
            }
            set
            {
                SetAttributeValue("enablecopilotstudiosharedatawithvivainsights", value);
            }
        }

        /// <summary>
		/// Enable or disable Mentions in Email.
		/// </summary>
        [AttributeLogicalName("enableemailmention")]
        public bool? EnableEmailMention
        {
            get
            {
                return GetAttributeValue<bool?>("enableemailmention");
            }
            set
            {
                SetAttributeValue("enableemailmention", value);
            }
        }

        /// <summary>
		/// Enables the Environment Settings App
		/// </summary>
        [AttributeLogicalName("enableenvironmentsettingsapp")]
        public bool? EnableEnvironmentSettingsApp
        {
            get
            {
                return GetAttributeValue<bool?>("enableenvironmentsettingsapp");
            }
            set
            {
                SetAttributeValue("enableenvironmentsettingsapp", value);
            }
        }

        /// <summary>
		/// Indicates whether the creation of flows is within a solution by default for this organization.
		/// </summary>
        [AttributeLogicalName("enableflowsinsolutionbydefault")]
        public bool? EnableFlowsInSolutionByDefault
        {
            get
            {
                return GetAttributeValue<bool?>("enableflowsinsolutionbydefault");
            }
            set
            {
                SetAttributeValue("enableflowsinsolutionbydefault", value);
            }
        }

        /// <summary>
		/// Organizations with this attribute set to true will be granted a grace period and excluded from the initial world wide enablement of 'creation of flows within a solution by default' functionality. Once the grace period expires, the functionality will be enabled in your organization.
		/// </summary>
        [AttributeLogicalName("enableflowsinsolutionbydefaultgraceperiod")]
        public bool? EnableFlowsInSolutionByDefaultGracePeriod
        {
            get
            {
                return GetAttributeValue<bool?>("enableflowsinsolutionbydefaultgraceperiod");
            }
            set
            {
                SetAttributeValue("enableflowsinsolutionbydefaultgraceperiod", value);
            }
        }

        /// <summary>
		/// Enable Integration with Immersive Skype
		/// </summary>
        [AttributeLogicalName("enableimmersiveskypeintegration")]
        public bool? EnableImmersiveSkypeIntegration
        {
            get
            {
                return GetAttributeValue<bool?>("enableimmersiveskypeintegration");
            }
            set
            {
                SetAttributeValue("enableimmersiveskypeintegration", value);
            }
        }

        /// <summary>
		/// Information that specifies whether IP based cookie binding is enabled
		/// </summary>
        [AttributeLogicalName("enableipbasedcookiebinding")]
        public bool? EnableIpBasedCookieBinding
        {
            get
            {
                return GetAttributeValue<bool?>("enableipbasedcookiebinding");
            }
            set
            {
                SetAttributeValue("enableipbasedcookiebinding", value);
            }
        }

        /// <summary>
		/// Information that specifies whether IP based firewall rule is enabled
		/// </summary>
        [AttributeLogicalName("enableipbasedfirewallrule")]
        public bool? EnableIpBasedFirewallRule
        {
            get
            {
                return GetAttributeValue<bool?>("enableipbasedfirewallrule");
            }
            set
            {
                SetAttributeValue("enableipbasedfirewallrule", value);
            }
        }

        /// <summary>
		/// Information that specifies whether IP based firewall rule is enabled in Audit Only Mode
		/// </summary>
        [AttributeLogicalName("enableipbasedfirewallruleinauditmode")]
        public bool? EnableIpBasedFirewallRuleInAuditMode
        {
            get
            {
                return GetAttributeValue<bool?>("enableipbasedfirewallruleinauditmode");
            }
            set
            {
                SetAttributeValue("enableipbasedfirewallruleinauditmode", value);
            }
        }

        /// <summary>
		/// Information that specifies whether IP based SAS URI generation rule is enabled
		/// </summary>
        [AttributeLogicalName("enableipbasedstorageaccesssignaturerule")]
        public bool? EnableIpBasedStorageAccessSignatureRule
        {
            get
            {
                return GetAttributeValue<bool?>("enableipbasedstorageaccesssignaturerule");
            }
            set
            {
                SetAttributeValue("enableipbasedstorageaccesssignaturerule", value);
            }
        }

        /// <summary>
		/// Indicates whether the user has enabled or disabled Live Persona Card feature in UCI.
		/// </summary>
        [AttributeLogicalName("enablelivepersonacarduci")]
        public bool? EnableLivePersonaCardUCI
        {
            get
            {
                return GetAttributeValue<bool?>("enablelivepersonacarduci");
            }
            set
            {
                SetAttributeValue("enablelivepersonacarduci", value);
            }
        }

        /// <summary>
		/// Indicates whether the user has enabled or disabled LivePersonCardIntegration in Office.
		/// </summary>
        [AttributeLogicalName("enablelivepersoncardintegrationinoffice")]
        public bool? EnableLivePersonCardIntegrationInOffice
        {
            get
            {
                return GetAttributeValue<bool?>("enablelivepersoncardintegrationinoffice");
            }
            set
            {
                SetAttributeValue("enablelivepersoncardintegrationinoffice", value);
            }
        }

        /// <summary>
		/// Select to enable learning path auhtoring.
		/// </summary>
        [AttributeLogicalName("enablelpauthoring")]
        public bool? EnableLPAuthoring
        {
            get
            {
                return GetAttributeValue<bool?>("enablelpauthoring");
            }
            set
            {
                SetAttributeValue("enablelpauthoring", value);
            }
        }

        /// <summary>
		/// Control whether the organization Switch Maker Portal to Classic
		/// </summary>
        [AttributeLogicalName("enablemakerswitchtoclassic")]
        public bool? EnableMakerSwitchToClassic
        {
            get
            {
                return GetAttributeValue<bool?>("enablemakerswitchtoclassic");
            }
            set
            {
                SetAttributeValue("enablemakerswitchtoclassic", value);
            }
        }

        /// <summary>
		/// Enable Integration with Microsoft Flow
		/// </summary>
        [AttributeLogicalName("enablemicrosoftflowintegration")]
        public bool? EnableMicrosoftFlowIntegration
        {
            get
            {
                return GetAttributeValue<bool?>("enablemicrosoftflowintegration");
            }
            set
            {
                SetAttributeValue("enablemicrosoftflowintegration", value);
            }
        }

        /// <summary>
		/// Enable pricing calculations on a Create call.
		/// </summary>
        [AttributeLogicalName("enablepricingoncreate")]
        public bool? EnablePricingOnCreate
        {
            get
            {
                return GetAttributeValue<bool?>("enablepricingoncreate");
            }
            set
            {
                SetAttributeValue("enablepricingoncreate", value);
            }
        }

        /// <summary>
		/// Enable or disable Sensitivity Labels in Email.
		/// </summary>
        [AttributeLogicalName("enablesensitivitylabels")]
        public bool? EnableSensitivityLabels
        {
            get
            {
                return GetAttributeValue<bool?>("enablesensitivitylabels");
            }
            set
            {
                SetAttributeValue("enablesensitivitylabels", value);
            }
        }

        /// <summary>
		/// Use Smart Matching.
		/// </summary>
        [AttributeLogicalName("enablesmartmatching")]
        public bool? EnableSmartMatching
        {
            get
            {
                return GetAttributeValue<bool?>("enablesmartmatching");
            }
            set
            {
                SetAttributeValue("enablesmartmatching", value);
            }
        }

        /// <summary>
		/// Leave empty to use default setting. Set to on/off to enable/disable CDN for UCI.
		/// </summary>
        [AttributeLogicalName("enableunifiedclientcdn")]
        public bool? EnableUnifiedClientCDN
        {
            get
            {
                return GetAttributeValue<bool?>("enableunifiedclientcdn");
            }
            set
            {
                SetAttributeValue("enableunifiedclientcdn", value);
            }
        }

        /// <summary>
		/// Enable site map and commanding update
		/// </summary>
        [AttributeLogicalName("enableunifiedinterfaceshellrefresh")]
        public bool? EnableUnifiedInterfaceShellRefresh
        {
            get
            {
                return GetAttributeValue<bool?>("enableunifiedinterfaceshellrefresh");
            }
            set
            {
                SetAttributeValue("enableunifiedinterfaceshellrefresh", value);
            }
        }

        /// <summary>
		/// Organization setting to enforce read only plugins.
		/// </summary>
        [AttributeLogicalName("enforcereadonlyplugins")]
        public bool? EnforceReadOnlyPlugins
        {
            get
            {
                return GetAttributeValue<bool?>("enforcereadonlyplugins");
            }
            set
            {
                SetAttributeValue("enforcereadonlyplugins", value);
            }
        }

        /// <summary>
		/// The default image for the entity.
		/// </summary>
        [AttributeLogicalName("entityimage")]
        public byte[]? EntityImage
        {
            get
            {
                return GetAttributeValue<byte[]?>("entityimage");
            }
            set
            {
                SetAttributeValue("entityimage", value);
            }
        }

        
        [AttributeLogicalName("entityimage_timestamp")]
        public long? EntityImageTimestamp
        {
            get
            {
                return GetAttributeValue<long?>("entityimage_timestamp");
            }
        }

        
        [AttributeLogicalName("entityimage_url")]
        public string? EntityImageURL
        {
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
            get
            {
                return GetAttributeValue<int?>("expirechangetrackingindays");
            }
            set
            {
                SetAttributeValue("expirechangetrackingindays", value);
            }
        }

        /// <summary>
		/// Maximum number of days before deleting inactive subscriptions.
		/// </summary>
        [AttributeLogicalName("expiresubscriptionsindays")]
        public int? ExpireSubscriptionsInDays
        {
            get
            {
                return GetAttributeValue<int?>("expiresubscriptionsindays");
            }
            set
            {
                SetAttributeValue("expiresubscriptionsindays", value);
            }
        }

        /// <summary>
		/// Specify the base URL to use to look for external document suggestions.
		/// </summary>
        [AttributeLogicalName("externalbaseurl")]
        public string? ExternalBaseUrl
        {
            get
            {
                return GetAttributeValue<string?>("externalbaseurl");
            }
            set
            {
                SetAttributeValue("externalbaseurl", value);
            }
        }

        /// <summary>
		/// XML string containing the ExternalPartyEnabled entities correlation keys for association of existing External Party instance entities to newly created IsExternalPartyEnabled entities.For internal use only
		/// </summary>
        [AttributeLogicalName("externalpartycorrelationkeys")]
        public string? ExternalPartyCorrelationKeys
        {
            get
            {
                return GetAttributeValue<string?>("externalpartycorrelationkeys");
            }
            set
            {
                SetAttributeValue("externalpartycorrelationkeys", value);
            }
        }

        /// <summary>
		/// XML string containing the ExternalPartyEnabled entities settings.
		/// </summary>
        [AttributeLogicalName("externalpartyentitysettings")]
        public string? ExternalPartyEntitySettings
        {
            get
            {
                return GetAttributeValue<string?>("externalpartyentitysettings");
            }
            set
            {
                SetAttributeValue("externalpartyentitysettings", value);
            }
        }

        /// <summary>
		/// Features to be enabled as an XML BLOB.
		/// </summary>
        [AttributeLogicalName("featureset")]
        public string? FeatureSet
        {
            get
            {
                return GetAttributeValue<string?>("featureset");
            }
            set
            {
                SetAttributeValue("featureset", value);
            }
        }

        /// <summary>
		/// Start date for the fiscal period that is to be used throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("fiscalcalendarstart")]
        public DateTime? FiscalCalendarStart
        {
            get
            {
                return GetAttributeValue<DateTime?>("fiscalcalendarstart");
            }
            set
            {
                SetAttributeValue("fiscalcalendarstart", value);
            }
        }

        /// <summary>
		/// Information that specifies how the name of the fiscal period is displayed throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("fiscalperiodformat")]
        public string? FiscalPeriodFormat
        {
            get
            {
                return GetAttributeValue<string?>("fiscalperiodformat");
            }
            set
            {
                SetAttributeValue("fiscalperiodformat", value);
            }
        }

        /// <summary>
		/// Format in which the fiscal period will be displayed.
		/// </summary>
        [AttributeLogicalName("fiscalperiodformatperiod")]
        public OptionSetValue? FiscalPeriodFormatPeriod
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("fiscalperiodformatperiod");
            }
            set
            {
                SetAttributeValue("fiscalperiodformatperiod", value);
            }
        }

        /// <summary>
		/// Type of fiscal period used throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("fiscalperiodtype")]
        public int? FiscalPeriodType
        {
            get
            {
                return GetAttributeValue<int?>("fiscalperiodtype");
            }
            set
            {
                SetAttributeValue("fiscalperiodtype", value);
            }
        }

        /// <summary>
		/// Information that specifies whether the fiscal settings have been updated.
		/// </summary>
        [AttributeLogicalName("fiscalsettingsupdated")]
        public bool? FiscalSettingsUpdated
        {
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
            get
            {
                return GetAttributeValue<int?>("fiscalyeardisplaycode");
            }
            set
            {
                SetAttributeValue("fiscalyeardisplaycode", value);
            }
        }

        /// <summary>
		/// Information that specifies how the name of the fiscal year is displayed throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("fiscalyearformat")]
        public string? FiscalYearFormat
        {
            get
            {
                return GetAttributeValue<string?>("fiscalyearformat");
            }
            set
            {
                SetAttributeValue("fiscalyearformat", value);
            }
        }

        /// <summary>
		/// Prefix for the display of the fiscal year.
		/// </summary>
        [AttributeLogicalName("fiscalyearformatprefix")]
        public OptionSetValue? FiscalYearFormatPrefix
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("fiscalyearformatprefix");
            }
            set
            {
                SetAttributeValue("fiscalyearformatprefix", value);
            }
        }

        /// <summary>
		/// Suffix for the display of the fiscal year.
		/// </summary>
        [AttributeLogicalName("fiscalyearformatsuffix")]
        public OptionSetValue? FiscalYearFormatSuffix
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("fiscalyearformatsuffix");
            }
            set
            {
                SetAttributeValue("fiscalyearformatsuffix", value);
            }
        }

        /// <summary>
		/// Format for the year.
		/// </summary>
        [AttributeLogicalName("fiscalyearformatyear")]
        public OptionSetValue? FiscalYearFormatYear
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("fiscalyearformatyear");
            }
            set
            {
                SetAttributeValue("fiscalyearformatyear", value);
            }
        }

        /// <summary>
		/// Information that specifies how the names of the fiscal year and the fiscal period should be connected when displayed together.
		/// </summary>
        [AttributeLogicalName("fiscalyearperiodconnect")]
        public string? FiscalYearPeriodConnect
        {
            get
            {
                return GetAttributeValue<string?>("fiscalyearperiodconnect");
            }
            set
            {
                SetAttributeValue("fiscalyearperiodconnect", value);
            }
        }

        /// <summary>
		/// Defines how long desktop flow logs are retained in Dataverse (V2 only). The default is 40,320 minutes (28 days). Set to 0 to retain logs indefinitely.
		/// </summary>
        [AttributeLogicalName("flowlogsttlinminutes")]
        public int? FlowLogsTtlInMinutes
        {
            get
            {
                return GetAttributeValue<int?>("flowlogsttlinminutes");
            }
            set
            {
                SetAttributeValue("flowlogsttlinminutes", value);
            }
        }

        /// <summary>
		/// Time to live (in seconds) for flow run
		/// </summary>
        [AttributeLogicalName("flowruntimetoliveinseconds")]
        public int? FlowRunTimeToLiveInSeconds
        {
            get
            {
                return GetAttributeValue<int?>("flowruntimetoliveinseconds");
            }
            set
            {
                SetAttributeValue("flowruntimetoliveinseconds", value);
            }
        }

        /// <summary>
		/// Order in which names are to be displayed throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("fullnameconventioncode")]
        public OptionSetValue? FullNameConventionCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("fullnameconventioncode");
            }
            set
            {
                SetAttributeValue("fullnameconventioncode", value);
            }
        }

        /// <summary>
		/// Specifies the maximum number of months in future for which the recurring activities can be created.
		/// </summary>
        [AttributeLogicalName("futureexpansionwindow")]
        public int? FutureExpansionWindow
        {
            get
            {
                return GetAttributeValue<int?>("futureexpansionwindow");
            }
            set
            {
                SetAttributeValue("futureexpansionwindow", value);
            }
        }

        /// <summary>
		/// Indicates whether alerts will be generated for errors.
		/// </summary>
        [AttributeLogicalName("generatealertsforerrors")]
        public bool? GenerateAlertsForErrors
        {
            get
            {
                return GetAttributeValue<bool?>("generatealertsforerrors");
            }
            set
            {
                SetAttributeValue("generatealertsforerrors", value);
            }
        }

        /// <summary>
		/// Indicates whether alerts will be generated for information.
		/// </summary>
        [AttributeLogicalName("generatealertsforinformation")]
        public bool? GenerateAlertsForInformation
        {
            get
            {
                return GetAttributeValue<bool?>("generatealertsforinformation");
            }
            set
            {
                SetAttributeValue("generatealertsforinformation", value);
            }
        }

        /// <summary>
		/// Indicates whether alerts will be generated for warnings.
		/// </summary>
        [AttributeLogicalName("generatealertsforwarnings")]
        public bool? GenerateAlertsForWarnings
        {
            get
            {
                return GetAttributeValue<bool?>("generatealertsforwarnings");
            }
            set
            {
                SetAttributeValue("generatealertsforwarnings", value);
            }
        }

        /// <summary>
		/// Indicates whether Get Started content is enabled for this organization.
		/// </summary>
        [AttributeLogicalName("getstartedpanecontentenabled")]
        public bool? GetStartedPaneContentEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("getstartedpanecontentenabled");
            }
            set
            {
                SetAttributeValue("getstartedpanecontentenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether the append URL parameters is enabled.
		/// </summary>
        [AttributeLogicalName("globalappendurlparametersenabled")]
        public bool? GlobalAppendUrlParametersEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("globalappendurlparametersenabled");
            }
            set
            {
                SetAttributeValue("globalappendurlparametersenabled", value);
            }
        }

        /// <summary>
		/// URL for the web page global help.
		/// </summary>
        [AttributeLogicalName("globalhelpurl")]
        public string? GlobalHelpUrl
        {
            get
            {
                return GetAttributeValue<string?>("globalhelpurl");
            }
            set
            {
                SetAttributeValue("globalhelpurl", value);
            }
        }

        /// <summary>
		/// Indicates whether the customizable global help is enabled.
		/// </summary>
        [AttributeLogicalName("globalhelpurlenabled")]
        public bool? GlobalHelpUrlEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("globalhelpurlenabled");
            }
            set
            {
                SetAttributeValue("globalhelpurlenabled", value);
            }
        }

        /// <summary>
		/// Number of days after the goal's end date after which the rollup of the goal stops automatically.
		/// </summary>
        [AttributeLogicalName("goalrollupexpirytime")]
        public int? GoalRollupExpiryTime
        {
            get
            {
                return GetAttributeValue<int?>("goalrollupexpirytime");
            }
            set
            {
                SetAttributeValue("goalrollupexpirytime", value);
            }
        }

        /// <summary>
		/// Number of hours between automatic rollup jobs .
		/// </summary>
        [AttributeLogicalName("goalrollupfrequency")]
        public int? GoalRollupFrequency
        {
            get
            {
                return GetAttributeValue<int?>("goalrollupfrequency");
            }
            set
            {
                SetAttributeValue("goalrollupfrequency", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("grantaccesstonetworkservice")]
        public bool? GrantAccessToNetworkService
        {
            get
            {
                return GetAttributeValue<bool?>("grantaccesstonetworkservice");
            }
            set
            {
                SetAttributeValue("grantaccesstonetworkservice", value);
            }
        }

        /// <summary>
		/// Maximum difference allowed between subject keywords count of the email messaged to be correlated
		/// </summary>
        [AttributeLogicalName("hashdeltasubjectcount")]
        public int? HashDeltaSubjectCount
        {
            get
            {
                return GetAttributeValue<int?>("hashdeltasubjectcount");
            }
            set
            {
                SetAttributeValue("hashdeltasubjectcount", value);
            }
        }

        /// <summary>
		/// Filter Subject Keywords
		/// </summary>
        [AttributeLogicalName("hashfilterkeywords")]
        public string? HashFilterKeywords
        {
            get
            {
                return GetAttributeValue<string?>("hashfilterkeywords");
            }
            set
            {
                SetAttributeValue("hashfilterkeywords", value);
            }
        }

        /// <summary>
		/// Maximum number of subject keywords or recipients used for correlation
		/// </summary>
        [AttributeLogicalName("hashmaxcount")]
        public int? HashMaxCount
        {
            get
            {
                return GetAttributeValue<int?>("hashmaxcount");
            }
            set
            {
                SetAttributeValue("hashmaxcount", value);
            }
        }

        /// <summary>
		/// Minimum number of recipients required to match for email messaged to be correlated
		/// </summary>
        [AttributeLogicalName("hashminaddresscount")]
        public int? HashMinAddressCount
        {
            get
            {
                return GetAttributeValue<int?>("hashminaddresscount");
            }
            set
            {
                SetAttributeValue("hashminaddresscount", value);
            }
        }

        /// <summary>
		/// High contrast theme data for the organization.
		/// </summary>
        [AttributeLogicalName("highcontrastthemedata")]
        public string? HighContrastThemeData
        {
            get
            {
                return GetAttributeValue<string?>("highcontrastthemedata");
            }
            set
            {
                SetAttributeValue("highcontrastthemedata", value);
            }
        }

        /// <summary>
		/// Indicates whether incoming email sent by internal Microsoft Dynamics 365 users or queues should be tracked.
		/// </summary>
        [AttributeLogicalName("ignoreinternalemail")]
        public bool? IgnoreInternalEmail
        {
            get
            {
                return GetAttributeValue<bool?>("ignoreinternalemail");
            }
            set
            {
                SetAttributeValue("ignoreinternalemail", value);
            }
        }

        /// <summary>
		/// Indicates whether an organization has consented to sharing search query data to help improve search results
		/// </summary>
        [AttributeLogicalName("improvesearchloggingenabled")]
        public bool? ImproveSearchLoggingEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("improvesearchloggingenabled");
            }
            set
            {
                SetAttributeValue("improvesearchloggingenabled", value);
            }
        }

        /// <summary>
		/// Information that specifies whether Inactivity timeout is enabled
		/// </summary>
        [AttributeLogicalName("inactivitytimeoutenabled")]
        public bool? InactivityTimeoutEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("inactivitytimeoutenabled");
            }
            set
            {
                SetAttributeValue("inactivitytimeoutenabled", value);
            }
        }

        /// <summary>
		/// Inactivity timeout in minutes
		/// </summary>
        [AttributeLogicalName("inactivitytimeoutinmins")]
        public int? InactivityTimeoutInMins
        {
            get
            {
                return GetAttributeValue<int?>("inactivitytimeoutinmins");
            }
            set
            {
                SetAttributeValue("inactivitytimeoutinmins", value);
            }
        }

        /// <summary>
		/// Inactivity timeout reminder in minutes
		/// </summary>
        [AttributeLogicalName("inactivitytimeoutreminderinmins")]
        public int? InactivityTimeoutReminderInMins
        {
            get
            {
                return GetAttributeValue<int?>("inactivitytimeoutreminderinmins");
            }
            set
            {
                SetAttributeValue("inactivitytimeoutreminderinmins", value);
            }
        }

        /// <summary>
		/// Setting for the Async Service Mailbox Queue. Defines the retrieval batch size of exchange server.
		/// </summary>
        [AttributeLogicalName("incomingemailexchangeemailretrievalbatchsize")]
        public int? IncomingEmailExchangeEmailRetrievalBatchSize
        {
            get
            {
                return GetAttributeValue<int?>("incomingemailexchangeemailretrievalbatchsize");
            }
            set
            {
                SetAttributeValue("incomingemailexchangeemailretrievalbatchsize", value);
            }
        }

        /// <summary>
		/// Initial version of the organization.
		/// </summary>
        [AttributeLogicalName("initialversion")]
        public string? InitialVersion
        {
            get
            {
                return GetAttributeValue<string?>("initialversion");
            }
            set
            {
                SetAttributeValue("initialversion", value);
            }
        }

        /// <summary>
		/// Unique identifier of the integration user for the organization.
		/// </summary>
        [AttributeLogicalName("integrationuserid")]
        public Guid? IntegrationUserId
        {
            get
            {
                return GetAttributeValue<Guid?>("integrationuserid");
            }
            set
            {
                SetAttributeValue("integrationuserid", value);
            }
        }

        /// <summary>
		/// Prefix to use for all invoice numbers throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("invoiceprefix")]
        public string? InvoicePrefix
        {
            get
            {
                return GetAttributeValue<string?>("invoiceprefix");
            }
            set
            {
                SetAttributeValue("invoiceprefix", value);
            }
        }

        /// <summary>
		/// IP Based SAS mode.
		/// </summary>
        [AttributeLogicalName("ipbasedstorageaccesssignaturemode")]
        public OptionSetValue? IpBasedStorageAccessSignatureMode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("ipbasedstorageaccesssignaturemode");
            }
            set
            {
                SetAttributeValue("ipbasedstorageaccesssignaturemode", value);
            }
        }

        /// <summary>
		/// Indicates whether the feature Action Card should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isactioncardenabled")]
        public bool? IsActionCardEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isactioncardenabled");
            }
            set
            {
                SetAttributeValue("isactioncardenabled", value);
            }
        }

        /// <summary>
		/// Information that specifies whether Action Support Feature is enabled
		/// </summary>
        [AttributeLogicalName("isactionsupportfeatureenabled")]
        public bool? IsActionSupportFeatureEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isactionsupportfeatureenabled");
            }
            set
            {
                SetAttributeValue("isactionsupportfeatureenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether the feature Relationship Analytics should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isactivityanalysisenabled")]
        public bool? IsActivityAnalysisEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isactivityanalysisenabled");
            }
            set
            {
                SetAttributeValue("isactivityanalysisenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether all money attributes are converted to decimal.
		/// </summary>
        [AttributeLogicalName("isallmoneydecimal")]
        public bool? IsAllMoneyDecimal
        {
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
            get
            {
                return GetAttributeValue<bool?>("isappmode");
            }
            set
            {
                SetAttributeValue("isappmode", value);
            }
        }

        /// <summary>
		/// Enable or disable attachments sync for outlook and exchange.
		/// </summary>
        [AttributeLogicalName("isappointmentattachmentsyncenabled")]
        public bool? IsAppointmentAttachmentSyncEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isappointmentattachmentsyncenabled");
            }
            set
            {
                SetAttributeValue("isappointmentattachmentsyncenabled", value);
            }
        }

        /// <summary>
		/// Enable or disable assigned tasks sync for outlook and exchange.
		/// </summary>
        [AttributeLogicalName("isassignedtaskssyncenabled")]
        public bool? IsAssignedTasksSyncEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isassignedtaskssyncenabled");
            }
            set
            {
                SetAttributeValue("isassignedtaskssyncenabled", value);
            }
        }

        /// <summary>
		/// Enable or disable auditing of changes.
		/// </summary>
        [AttributeLogicalName("isauditenabled")]
        public bool? IsAuditEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isauditenabled");
            }
            set
            {
                SetAttributeValue("isauditenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether the feature Auto Capture should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isautodatacaptureenabled")]
        public bool? IsAutoDataCaptureEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isautodatacaptureenabled");
            }
            set
            {
                SetAttributeValue("isautodatacaptureenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether the V2 feature of Auto Capture should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isautodatacapturev2enabled")]
        public bool? IsAutoDataCaptureV2Enabled
        {
            get
            {
                return GetAttributeValue<bool?>("isautodatacapturev2enabled");
            }
            set
            {
                SetAttributeValue("isautodatacapturev2enabled", value);
            }
        }

        
        [AttributeLogicalName("isautoinstallappford365inteamsenabled")]
        public bool? IsAutoInstallAppForD365InTeamsEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isautoinstallappford365inteamsenabled");
            }
            set
            {
                SetAttributeValue("isautoinstallappford365inteamsenabled", value);
            }
        }

        /// <summary>
		/// Information on whether auto save is enabled.
		/// </summary>
        [AttributeLogicalName("isautosaveenabled")]
        public bool? IsAutoSaveEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isautosaveenabled");
            }
            set
            {
                SetAttributeValue("isautosaveenabled", value);
            }
        }

        
        [AttributeLogicalName("isbasecardstaticfielddataenabled")]
        public bool? IsBaseCardStaticFieldDataEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isbasecardstaticfielddataenabled");
            }
            set
            {
                SetAttributeValue("isbasecardstaticfielddataenabled", value);
            }
        }

        /// <summary>
		/// Determines whether users can make use of basic Geospatial featuers in Canvas apps.
		/// </summary>
        [AttributeLogicalName("isbasicgeospatialintegrationenabled")]
        public bool? IsBasicGeospatialIntegrationEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isbasicgeospatialintegrationenabled");
            }
            set
            {
                SetAttributeValue("isbasicgeospatialintegrationenabled", value);
            }
        }

        /// <summary>
		/// Information that specifies whether BPF Entity Customization Feature is enabled
		/// </summary>
        [AttributeLogicalName("isbpfentitycustomizationfeatureenabled")]
        public bool? IsBPFEntityCustomizationFeatureEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isbpfentitycustomizationfeatureenabled");
            }
            set
            {
                SetAttributeValue("isbpfentitycustomizationfeatureenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Power Automate savings feature is enabled for Cloudflow.
		/// </summary>
        [AttributeLogicalName("iscloudflowsavingsenabled")]
        public bool? IsCloudFlowSavingsEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("iscloudflowsavingsenabled");
            }
            set
            {
                SetAttributeValue("iscloudflowsavingsenabled", value);
            }
        }

        /// <summary>
		/// Read-only flag indicating whether clustering is enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isclusteringenabled")]
        public bool? IsClusteringEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isclusteringenabled");
            }
        }

        
        [AttributeLogicalName("iscollaborationexperienceenabled")]
        public bool? IsCollaborationExperienceEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("iscollaborationexperienceenabled");
            }
            set
            {
                SetAttributeValue("iscollaborationexperienceenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Computer Use in MCS feature is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("iscomputeruseinmcsenabled")]
        public bool? IsComputerUseInMCSEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("iscomputeruseinmcsenabled");
            }
            set
            {
                SetAttributeValue("iscomputeruseinmcsenabled", value);
            }
        }

        /// <summary>
		/// Information that specifies whether conflict detection for mobile client is enabled.
		/// </summary>
        [AttributeLogicalName("isconflictdetectionenabledformobileclient")]
        public bool? IsConflictDetectionEnabledForMobileClient
        {
            get
            {
                return GetAttributeValue<bool?>("isconflictdetectionenabledformobileclient");
            }
            set
            {
                SetAttributeValue("isconflictdetectionenabledformobileclient", value);
            }
        }

        /// <summary>
		/// Enable or disable mailing address sync for outlook and exchange.
		/// </summary>
        [AttributeLogicalName("iscontactmailingaddresssyncenabled")]
        public bool? IsContactMailingAddressSyncEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("iscontactmailingaddresssyncenabled");
            }
            set
            {
                SetAttributeValue("iscontactmailingaddresssyncenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Content Security Policy has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("iscontentsecuritypolicyenabled")]
        public bool? IsContentSecurityPolicyEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("iscontentsecuritypolicyenabled");
            }
            set
            {
                SetAttributeValue("iscontentsecuritypolicyenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Content Security Policy has been enabled for this organization's Canvas apps.
		/// </summary>
        [AttributeLogicalName("iscontentsecuritypolicyenabledforcanvas")]
        public bool? IsContentSecurityPolicyEnabledForCanvas
        {
            get
            {
                return GetAttributeValue<bool?>("iscontentsecuritypolicyenabledforcanvas");
            }
            set
            {
                SetAttributeValue("iscontentsecuritypolicyenabledforcanvas", value);
            }
        }

        /// <summary>
		/// Indicates whether Contextual email experience is enabled on this organization
		/// </summary>
        [AttributeLogicalName("iscontextualemailenabled")]
        public bool? IsContextualEmailEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("iscontextualemailenabled");
            }
            set
            {
                SetAttributeValue("iscontextualemailenabled", value);
            }
        }

        /// <summary>
		/// Select to enable Contextual Help in UCI.
		/// </summary>
        [AttributeLogicalName("iscontextualhelpenabled")]
        public bool? IsContextualHelpEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("iscontextualhelpenabled");
            }
            set
            {
                SetAttributeValue("iscontextualhelpenabled", value);
            }
        }

        /// <summary>
		/// Determines whether users can provide feedback Copilot experiences.
		/// </summary>
        [AttributeLogicalName("iscopilotfeedbackenabled")]
        public bool? IsCopilotFeedbackEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("iscopilotfeedbackenabled");
            }
            set
            {
                SetAttributeValue("iscopilotfeedbackenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether CUA on Hosted Groups V2 feature is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("iscuaonhmgv2enabled")]
        public bool? IsCuaOnHmgV2Enabled
        {
            get
            {
                return GetAttributeValue<bool?>("iscuaonhmgv2enabled");
            }
            set
            {
                SetAttributeValue("iscuaonhmgv2enabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Custom Controls in canvas PowerApps feature has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("iscustomcontrolsincanvasappsenabled")]
        public bool? IsCustomControlsInCanvasAppsEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("iscustomcontrolsincanvasappsenabled");
            }
            set
            {
                SetAttributeValue("iscustomcontrolsincanvasappsenabled", value);
            }
        }

        /// <summary>
		/// Enable or disable country code selection.
		/// </summary>
        [AttributeLogicalName("isdefaultcountrycodecheckenabled")]
        public bool? IsDefaultCountryCodeCheckEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isdefaultcountrycodecheckenabled");
            }
            set
            {
                SetAttributeValue("isdefaultcountrycodecheckenabled", value);
            }
        }

        /// <summary>
		/// Enable Delegation Access content
		/// </summary>
        [AttributeLogicalName("isdelegateaccessenabled")]
        public bool? IsDelegateAccessEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isdelegateaccessenabled");
            }
            set
            {
                SetAttributeValue("isdelegateaccessenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether the feature Action Hub should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isdelveactionhubintegrationenabled")]
        public bool? IsDelveActionHubIntegrationEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isdelveactionhubintegrationenabled");
            }
            set
            {
                SetAttributeValue("isdelveactionhubintegrationenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether connection embedding in Desktop Flows is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isdesktopflowconnectionembeddingenabled")]
        public bool? IsDesktopFlowConnectionEmbeddingEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isdesktopflowconnectionembeddingenabled");
            }
            set
            {
                SetAttributeValue("isdesktopflowconnectionembeddingenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether remote monitoring and control for unattended desktop flows is enabled for this organization.
		/// </summary>
        [AttributeLogicalName("isdesktopflowremotemonitoringcontrolenabled")]
        public bool? IsDesktopFlowRemoteMonitoringControlEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isdesktopflowremotemonitoringcontrolenabled");
            }
            set
            {
                SetAttributeValue("isdesktopflowremotemonitoringcontrolenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether the Desktop Flows UI Automation Runtime Repair for Attended feature for this organization.
		/// </summary>
        [AttributeLogicalName("isdesktopflowruntimerepairattendedenabled")]
        public bool? IsDesktopFlowRuntimeRepairAttendedEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isdesktopflowruntimerepairattendedenabled");
            }
            set
            {
                SetAttributeValue("isdesktopflowruntimerepairattendedenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether the Desktop Flows UI Automation Runtime Repair for Unattended feature for this organization.
		/// </summary>
        [AttributeLogicalName("isdesktopflowruntimerepairunattendedenabled")]
        public bool? IsDesktopFlowRuntimeRepairUnattendedEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isdesktopflowruntimerepairunattendedenabled");
            }
            set
            {
                SetAttributeValue("isdesktopflowruntimerepairunattendedenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Power Automate savings feature is enabled for Desktopflow.
		/// </summary>
        [AttributeLogicalName("isdesktopflowsavingsenabled")]
        public bool? IsDesktopFlowSavingsEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isdesktopflowsavingsenabled");
            }
            set
            {
                SetAttributeValue("isdesktopflowsavingsenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether v2 schema for Desktop Flows is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isdesktopflowschemav2enabled")]
        public bool? IsDesktopFlowSchemaV2Enabled
        {
            get
            {
                return GetAttributeValue<bool?>("isdesktopflowschemav2enabled");
            }
            set
            {
                SetAttributeValue("isdesktopflowschemav2enabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Windows Vanilla Image will be available for Desktop Flow users in this organization.
		/// </summary>
        [AttributeLogicalName("isdesktopflowvanillaimagesharingenabled")]
        public bool? IsDesktopFlowVanillaImageSharingEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isdesktopflowvanillaimagesharingenabled");
            }
            set
            {
                SetAttributeValue("isdesktopflowvanillaimagesharingenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether version control for Desktop Flows is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isdesktopflowversioncontrolenabled")]
        public bool? IsDesktopFlowVersionControlEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isdesktopflowversioncontrolenabled");
            }
            set
            {
                SetAttributeValue("isdesktopflowversioncontrolenabled", value);
            }
        }

        /// <summary>
		/// Indicates if this organization will opt-in to automatically to enable version control for Desktop Flows.
		/// </summary>
        [AttributeLogicalName("isdesktopflowversioncontrolenabledbydefault")]
        public bool? IsDesktopFlowVersionControlEnabledByDefault
        {
            get
            {
                return GetAttributeValue<bool?>("isdesktopflowversioncontrolenabledbydefault");
            }
            set
            {
                SetAttributeValue("isdesktopflowversioncontrolenabledbydefault", value);
            }
        }

        /// <summary>
		/// Overrides whether version control for Desktop Flows is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isdesktopflowversioncontrolenabledoverride")]
        public OptionSetValue? IsDesktopFlowVersionControlEnabledOverride
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("isdesktopflowversioncontrolenabledoverride");
            }
            set
            {
                SetAttributeValue("isdesktopflowversioncontrolenabledoverride", value);
            }
        }

        /// <summary>
		/// Information that specifies whether the organization is disabled.
		/// </summary>
        [AttributeLogicalName("isdisabled")]
        public bool? IsDisabled
        {
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
            get
            {
                return GetAttributeValue<bool?>("isduplicatedetectionenabled");
            }
            set
            {
                SetAttributeValue("isduplicatedetectionenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether duplicate detection of records during import is enabled.
		/// </summary>
        [AttributeLogicalName("isduplicatedetectionenabledforimport")]
        public bool? IsDuplicateDetectionEnabledForImport
        {
            get
            {
                return GetAttributeValue<bool?>("isduplicatedetectionenabledforimport");
            }
            set
            {
                SetAttributeValue("isduplicatedetectionenabledforimport", value);
            }
        }

        /// <summary>
		/// Indicates whether duplicate detection of records during offline synchronization is enabled.
		/// </summary>
        [AttributeLogicalName("isduplicatedetectionenabledforofflinesync")]
        public bool? IsDuplicateDetectionEnabledForOfflineSync
        {
            get
            {
                return GetAttributeValue<bool?>("isduplicatedetectionenabledforofflinesync");
            }
            set
            {
                SetAttributeValue("isduplicatedetectionenabledforofflinesync", value);
            }
        }

        /// <summary>
		/// Indicates whether duplicate detection during online create or update is enabled.
		/// </summary>
        [AttributeLogicalName("isduplicatedetectionenabledforonlinecreateupdate")]
        public bool? IsDuplicateDetectionEnabledForOnlineCreateUpdate
        {
            get
            {
                return GetAttributeValue<bool?>("isduplicatedetectionenabledforonlinecreateupdate");
            }
            set
            {
                SetAttributeValue("isduplicatedetectionenabledforonlinecreateupdate", value);
            }
        }

        /// <summary>
		/// Information on whether Smart Email Address Validation is enabled.
		/// </summary>
        [AttributeLogicalName("isemailaddressvalidationenabled")]
        public bool? IsEmailAddressValidationEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isemailaddressvalidationenabled");
            }
            set
            {
                SetAttributeValue("isemailaddressvalidationenabled", value);
            }
        }

        /// <summary>
		/// Allow tracking recipient activity on sent emails.
		/// </summary>
        [AttributeLogicalName("isemailmonitoringallowed")]
        public bool? IsEmailMonitoringAllowed
        {
            get
            {
                return GetAttributeValue<bool?>("isemailmonitoringallowed");
            }
            set
            {
                SetAttributeValue("isemailmonitoringallowed", value);
            }
        }

        /// <summary>
		/// Enable Email Server Profile content filtering
		/// </summary>
        [AttributeLogicalName("isemailserverprofilecontentfilteringenabled")]
        public bool? IsEmailServerProfileContentFilteringEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isemailserverprofilecontentfilteringenabled");
            }
            set
            {
                SetAttributeValue("isemailserverprofilecontentfilteringenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether appmodule is enabled for all roles
		/// </summary>
        [AttributeLogicalName("isenabledforallroles")]
        public bool? IsEnabledForAllRoles
        {
            get
            {
                return GetAttributeValue<bool?>("isenabledforallroles");
            }
            set
            {
                SetAttributeValue("isenabledforallroles", value);
            }
        }

        /// <summary>
		/// Indicates whether the organization's files are being stored in Azure.
		/// </summary>
        [AttributeLogicalName("isexternalfilestorageenabled")]
        public bool? IsExternalFileStorageEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isexternalfilestorageenabled");
            }
            set
            {
                SetAttributeValue("isexternalfilestorageenabled", value);
            }
        }

        /// <summary>
		/// Select whether data can be synchronized with an external search index.
		/// </summary>
        [AttributeLogicalName("isexternalsearchindexenabled")]
        public bool? IsExternalSearchIndexEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isexternalsearchindexenabled");
            }
            set
            {
                SetAttributeValue("isexternalsearchindexenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether the fiscal period is displayed as the month number.
		/// </summary>
        [AttributeLogicalName("isfiscalperiodmonthbased")]
        public bool? IsFiscalPeriodMonthBased
        {
            get
            {
                return GetAttributeValue<bool?>("isfiscalperiodmonthbased");
            }
            set
            {
                SetAttributeValue("isfiscalperiodmonthbased", value);
            }
        }

        /// <summary>
		/// Select whether folders should be automatically created on SharePoint.
		/// </summary>
        [AttributeLogicalName("isfolderautocreatedonsp")]
        public bool? IsFolderAutoCreatedonSP
        {
            get
            {
                return GetAttributeValue<bool?>("isfolderautocreatedonsp");
            }
            set
            {
                SetAttributeValue("isfolderautocreatedonsp", value);
            }
        }

        /// <summary>
		/// Enable or disable folder based tracking for Server Side Sync.
		/// </summary>
        [AttributeLogicalName("isfolderbasedtrackingenabled")]
        public bool? IsFolderBasedTrackingEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isfolderbasedtrackingenabled");
            }
            set
            {
                SetAttributeValue("isfolderbasedtrackingenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether full-text search for Quick Find entities should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isfulltextsearchenabled")]
        public bool? IsFullTextSearchEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isfulltextsearchenabled");
            }
            set
            {
                SetAttributeValue("isfulltextsearchenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether geospatial capabilities leveraging Azure Maps are enabled.
		/// </summary>
        [AttributeLogicalName("isgeospatialazuremapsintegrationenabled")]
        public bool? IsGeospatialAzureMapsIntegrationEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isgeospatialazuremapsintegrationenabled");
            }
            set
            {
                SetAttributeValue("isgeospatialazuremapsintegrationenabled", value);
            }
        }

        /// <summary>
		/// Enable Hierarchical Security Model
		/// </summary>
        [AttributeLogicalName("ishierarchicalsecuritymodelenabled")]
        public bool? IsHierarchicalSecurityModelEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("ishierarchicalsecuritymodelenabled");
            }
            set
            {
                SetAttributeValue("ishierarchicalsecuritymodelenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether data collection for ideas in canvas PowerApps has been enabled.
		/// </summary>
        [AttributeLogicalName("isideasdatacollectionenabled")]
        public bool? IsIdeasDataCollectionEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isideasdatacollectionenabled");
            }
            set
            {
                SetAttributeValue("isideasdatacollectionenabled", value);
            }
        }

        /// <summary>
		/// Give Consent to use LUIS in Dynamics 365 Bot
		/// </summary>
        [AttributeLogicalName("isluisenabledford365bot")]
        public bool? IsLUISEnabledforD365Bot
        {
            get
            {
                return GetAttributeValue<bool?>("isluisenabledford365bot");
            }
            set
            {
                SetAttributeValue("isluisenabledford365bot", value);
            }
        }

        /// <summary>
		/// Enable or disable forced unlocking for Server Side Sync mailboxes.
		/// </summary>
        [AttributeLogicalName("ismailboxforcedunlockingenabled")]
        public bool? IsMailboxForcedUnlockingEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("ismailboxforcedunlockingenabled");
            }
            set
            {
                SetAttributeValue("ismailboxforcedunlockingenabled", value);
            }
        }

        /// <summary>
		/// Enable or disable mailbox keep alive for Server Side Sync.
		/// </summary>
        [AttributeLogicalName("ismailboxinactivebackoffenabled")]
        public bool? IsMailboxInactiveBackoffEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("ismailboxinactivebackoffenabled");
            }
            set
            {
                SetAttributeValue("ismailboxinactivebackoffenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Manual Sales Forecasting feature has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("ismanualsalesforecastingenabled")]
        public bool? IsManualSalesForecastingEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("ismanualsalesforecastingenabled");
            }
            set
            {
                SetAttributeValue("ismanualsalesforecastingenabled", value);
            }
        }

        /// <summary>
		/// Information that specifies whether mobile client on demand sync is enabled.
		/// </summary>
        [AttributeLogicalName("ismobileclientondemandsyncenabled")]
        public bool? IsMobileClientOnDemandSyncEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("ismobileclientondemandsyncenabled");
            }
            set
            {
                SetAttributeValue("ismobileclientondemandsyncenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether the feature MobileOffline should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("ismobileofflineenabled")]
        public bool? IsMobileOfflineEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("ismobileofflineenabled");
            }
            set
            {
                SetAttributeValue("ismobileofflineenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Model Apps can be embedded within Microsoft Teams. This is a tenant admin controlled preview/experimental feature.
		/// </summary>
        [AttributeLogicalName("ismodeldrivenappsinmsteamsenabled")]
        public bool? IsModelDrivenAppsInMSTeamsEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("ismodeldrivenappsinmsteamsenabled");
            }
            set
            {
                SetAttributeValue("ismodeldrivenappsinmsteamsenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether the maker can create Power Automate money based saving rules.
		/// </summary>
        [AttributeLogicalName("ismoneysavingsallowed")]
        public bool? IsMoneySavingsAllowed
        {
            get
            {
                return GetAttributeValue<bool?>("ismoneysavingsallowed");
            }
            set
            {
                SetAttributeValue("ismoneysavingsallowed", value);
            }
        }

        /// <summary>
		/// Indicates whether Microsoft Teams Collaboration feature has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("ismsteamscollaborationenabled")]
        public bool? IsMSTeamsCollaborationEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("ismsteamscollaborationenabled");
            }
            set
            {
                SetAttributeValue("ismsteamscollaborationenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Microsoft Teams integration has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("ismsteamsenabled")]
        public bool? IsMSTeamsEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("ismsteamsenabled");
            }
            set
            {
                SetAttributeValue("ismsteamsenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether the user has enabled or disabled Microsoft Teams integration.
		/// </summary>
        [AttributeLogicalName("ismsteamssettingchangedbyuser")]
        public bool? IsMSTeamsSettingChangedByUser
        {
            get
            {
                return GetAttributeValue<bool?>("ismsteamssettingchangedbyuser");
            }
            set
            {
                SetAttributeValue("ismsteamssettingchangedbyuser", value);
            }
        }

        /// <summary>
		/// Indicates whether Microsoft Teams User Sync feature has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("ismsteamsusersyncenabled")]
        public bool? IsMSTeamsUserSyncEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("ismsteamsusersyncenabled");
            }
            set
            {
                SetAttributeValue("ismsteamsusersyncenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether new add product experience is enabled.
		/// </summary>
        [AttributeLogicalName("isnewaddproductexperienceenabled")]
        public bool? IsNewAddProductExperienceEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isnewaddproductexperienceenabled");
            }
            set
            {
                SetAttributeValue("isnewaddproductexperienceenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether the feature Notes Analysis should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isnotesanalysisenabled")]
        public bool? IsNotesAnalysisEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isnotesanalysisenabled");
            }
            set
            {
                SetAttributeValue("isnotesanalysisenabled", value);
            }
        }

        
        [AttributeLogicalName("isnotificationford365inteamsenabled")]
        public bool? IsNotificationForD365InTeamsEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isnotificationford365inteamsenabled");
            }
            set
            {
                SetAttributeValue("isnotificationford365inteamsenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether the feature OfficeGraph should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isofficegraphenabled")]
        public bool? IsOfficeGraphEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isofficegraphenabled");
            }
            set
            {
                SetAttributeValue("isofficegraphenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether the feature One Drive should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isonedriveenabled")]
        public bool? IsOneDriveEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isonedriveenabled");
            }
            set
            {
                SetAttributeValue("isonedriveenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether PAI feature has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("ispaienabled")]
        public bool? IsPAIEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("ispaienabled");
            }
            set
            {
                SetAttributeValue("ispaienabled", value);
            }
        }

        /// <summary>
		/// Indicates whether PDF Generation feature has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("ispdfgenerationenabled")]
        public string? IsPDFGenerationEnabled
        {
            get
            {
                return GetAttributeValue<string?>("ispdfgenerationenabled");
            }
            set
            {
                SetAttributeValue("ispdfgenerationenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether the Per Process overage feature is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isperprocesscapacityoverageenabled")]
        public bool? IsPerProcessCapacityOverageEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isperprocesscapacityoverageenabled");
            }
            set
            {
                SetAttributeValue("isperprocesscapacityoverageenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether playbook feature has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isplaybookenabled")]
        public bool? IsPlaybookEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isplaybookenabled");
            }
            set
            {
                SetAttributeValue("isplaybookenabled", value);
            }
        }

        /// <summary>
		/// Information on whether IM presence is enabled.
		/// </summary>
        [AttributeLogicalName("ispresenceenabled")]
        public bool? IsPresenceEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("ispresenceenabled");
            }
            set
            {
                SetAttributeValue("ispresenceenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether the Preview feature for Action Card should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("ispreviewenabledforactioncard")]
        public bool? IsPreviewEnabledForActionCard
        {
            get
            {
                return GetAttributeValue<bool?>("ispreviewenabledforactioncard");
            }
            set
            {
                SetAttributeValue("ispreviewenabledforactioncard", value);
            }
        }

        /// <summary>
		/// Indicates whether the feature Auto Capture should be enabled for the organization at Preview Settings.
		/// </summary>
        [AttributeLogicalName("ispreviewforautocaptureenabled")]
        public bool? IsPreviewForAutoCaptureEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("ispreviewforautocaptureenabled");
            }
            set
            {
                SetAttributeValue("ispreviewforautocaptureenabled", value);
            }
        }

        /// <summary>
		/// Is Preview For Email Monitoring Allowed.
		/// </summary>
        [AttributeLogicalName("ispreviewforemailmonitoringallowed")]
        public bool? IsPreviewForEmailMonitoringAllowed
        {
            get
            {
                return GetAttributeValue<bool?>("ispreviewforemailmonitoringallowed");
            }
            set
            {
                SetAttributeValue("ispreviewforemailmonitoringallowed", value);
            }
        }

        /// <summary>
		/// Indicates whether PriceList is mandatory for adding existing products to sales entities.
		/// </summary>
        [AttributeLogicalName("ispricelistmandatory")]
        public bool? IsPriceListMandatory
        {
            get
            {
                return GetAttributeValue<bool?>("ispricelistmandatory");
            }
            set
            {
                SetAttributeValue("ispricelistmandatory", value);
            }
        }

        /// <summary>
		/// Indicates whether the Process capacity auto-claim feature is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isprocesscapacityautoclaimenabled")]
        public bool? IsProcessCapacityAutoClaimEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isprocesscapacityautoclaimenabled");
            }
            set
            {
                SetAttributeValue("isprocesscapacityautoclaimenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Process Mining is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isprocessminingenabled")]
        public bool? IsProcessMiningEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isprocessminingenabled");
            }
            set
            {
                SetAttributeValue("isprocessminingenabled", value);
            }
        }

        /// <summary>
		/// Select whether to use the standard Out-of-box Opportunity Close experience or opt to for a customized experience.
		/// </summary>
        [AttributeLogicalName("isquickcreateenabledforopportunityclose")]
        public bool? IsQuickCreateEnabledForOpportunityClose
        {
            get
            {
                return GetAttributeValue<bool?>("isquickcreateenabledforopportunityclose");
            }
            set
            {
                SetAttributeValue("isquickcreateenabledforopportunityclose", value);
            }
        }

        /// <summary>
		/// Enable or disable auditing of read operations.
		/// </summary>
        [AttributeLogicalName("isreadauditenabled")]
        public bool? IsReadAuditEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isreadauditenabled");
            }
            set
            {
                SetAttributeValue("isreadauditenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether the feature Relationship Insights should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("isrelationshipinsightsenabled")]
        public bool? IsRelationshipInsightsEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isrelationshipinsightsenabled");
            }
            set
            {
                SetAttributeValue("isrelationshipinsightsenabled", value);
            }
        }

        /// <summary>
		/// Indicates if the synchronization of user resource booking with Exchange is enabled at organization level.
		/// </summary>
        [AttributeLogicalName("isresourcebookingexchangesyncenabled")]
        public bool? IsResourceBookingExchangeSyncEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isresourcebookingexchangesyncenabled");
            }
            set
            {
                SetAttributeValue("isresourcebookingexchangesyncenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether rich text editor for notes experience is enabled on this organization
		/// </summary>
        [AttributeLogicalName("isrichtextnotesenabled")]
        public bool? IsRichTextNotesEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isrichtextnotesenabled");
            }
            set
            {
                SetAttributeValue("isrichtextnotesenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether AAD Join for RPA Autoscale is enabled in this organization..
		/// </summary>
        [AttributeLogicalName("isrpaautoscaleaadjoinenabled")]
        public bool? IsRpaAutoscaleAadJoinEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isrpaautoscaleaadjoinenabled");
            }
            set
            {
                SetAttributeValue("isrpaautoscaleaadjoinenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Autoscale feature for RPA is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isrpaautoscaleenabled")]
        public bool? IsRpaAutoscaleEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isrpaautoscaleenabled");
            }
            set
            {
                SetAttributeValue("isrpaautoscaleenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether RPA Box feature is enabled in this organization in locations outside the tenant's geographical location.
		/// </summary>
        [AttributeLogicalName("isrpaboxcrossgeoenabled")]
        public bool? IsRpaBoxCrossGeoEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isrpaboxcrossgeoenabled");
            }
            set
            {
                SetAttributeValue("isrpaboxcrossgeoenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether RPA Box feature is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isrpaboxenabled")]
        public bool? IsRpaBoxEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isrpaboxenabled");
            }
            set
            {
                SetAttributeValue("isrpaboxenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Unattended runs feature for RPA is enabled in this organization.
		/// </summary>
        [AttributeLogicalName("isrpaunattendedenabled")]
        public bool? IsRpaUnattendedEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isrpaunattendedenabled");
            }
            set
            {
                SetAttributeValue("isrpaunattendedenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Sales Assistant mobile app has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("issalesassistantenabled")]
        public bool? IsSalesAssistantEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("issalesassistantenabled");
            }
            set
            {
                SetAttributeValue("issalesassistantenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether sending CUA audit logs to Purview is enabled.
		/// </summary>
        [AttributeLogicalName("issendcuaauditlogtopurviewenabled")]
        public bool? IsSendCuaAuditLogToPurviewEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("issendcuaauditlogtopurviewenabled");
            }
            set
            {
                SetAttributeValue("issendcuaauditlogtopurviewenabled", value);
            }
        }

        
        [AttributeLogicalName("issharinginorgallowed")]
        public bool? IsSharingInOrgAllowed
        {
            get
            {
                return GetAttributeValue<bool?>("issharinginorgallowed");
            }
            set
            {
                SetAttributeValue("issharinginorgallowed", value);
            }
        }

        /// <summary>
		/// Enable sales order processing integration.
		/// </summary>
        [AttributeLogicalName("issopintegrationenabled")]
        public bool? IsSOPIntegrationEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("issopintegrationenabled");
            }
            set
            {
                SetAttributeValue("issopintegrationenabled", value);
            }
        }

        /// <summary>
		/// Information on whether text wrap is enabled.
		/// </summary>
        [AttributeLogicalName("istextwrapenabled")]
        public bool? IsTextWrapEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("istextwrapenabled");
            }
            set
            {
                SetAttributeValue("istextwrapenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether CUA log upload to Dataverse is enabled.
		/// </summary>
        [AttributeLogicalName("isuploadcualogtodataverseenabled")]
        public bool? IsUploadCuaLogToDataverseEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isuploadcualogtodataverseenabled");
            }
            set
            {
                SetAttributeValue("isuploadcualogtodataverseenabled", value);
            }
        }

        /// <summary>
		/// Enable or disable auditing of user access.
		/// </summary>
        [AttributeLogicalName("isuseraccessauditenabled")]
        public bool? IsUserAccessAuditEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isuseraccessauditenabled");
            }
            set
            {
                SetAttributeValue("isuseraccessauditenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether loading of Microsoft Dynamics 365 in a browser window that does not have address, tool, and menu bars is enabled.
		/// </summary>
        [AttributeLogicalName("isvintegrationcode")]
        public OptionSetValue? ISVIntegrationCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("isvintegrationcode");
            }
            set
            {
                SetAttributeValue("isvintegrationcode", value);
            }
        }

        /// <summary>
		/// Indicates whether Power Automate savings feature is enabled for WorkQueue.
		/// </summary>
        [AttributeLogicalName("isworkqueuesavingsenabled")]
        public bool? IsWorkQueueSavingsEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isworkqueuesavingsenabled");
            }
            set
            {
                SetAttributeValue("isworkqueuesavingsenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Write-in Products can be added to Opportunity/Quote/Order/Invoice or not.
		/// </summary>
        [AttributeLogicalName("iswriteinproductsallowed")]
        public bool? IsWriteInProductsAllowed
        {
            get
            {
                return GetAttributeValue<bool?>("iswriteinproductsallowed");
            }
            set
            {
                SetAttributeValue("iswriteinproductsallowed", value);
            }
        }

        /// <summary>
		/// Type the prefix to use for all knowledge articles in Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("kaprefix")]
        public string? KaPrefix
        {
            get
            {
                return GetAttributeValue<string?>("kaprefix");
            }
            set
            {
                SetAttributeValue("kaprefix", value);
            }
        }

        /// <summary>
		/// Prefix to use for all articles in Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("kbprefix")]
        public string? KbPrefix
        {
            get
            {
                return GetAttributeValue<string?>("kbprefix");
            }
            set
            {
                SetAttributeValue("kbprefix", value);
            }
        }

        /// <summary>
		/// XML string containing the Knowledge Management settings that are applied in Knowledge Management Wizard.
		/// </summary>
        [AttributeLogicalName("kmsettings")]
        public string? KMSettings
        {
            get
            {
                return GetAttributeValue<string?>("kmsettings");
            }
            set
            {
                SetAttributeValue("kmsettings", value);
            }
        }

        /// <summary>
		/// Preferred language for the organization.
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
		/// Show legacy app for admins
		/// </summary>
        [AttributeLogicalName("legacyapptoggle")]
        public OptionSetValue? LegacyAppToggle
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("legacyapptoggle");
            }
            set
            {
                SetAttributeValue("legacyapptoggle", value);
            }
        }

        /// <summary>
		/// Unique identifier of the locale of the organization.
		/// </summary>
        [AttributeLogicalName("localeid")]
        public int? LocaleId
        {
            get
            {
                return GetAttributeValue<int?>("localeid");
            }
            set
            {
                SetAttributeValue("localeid", value);
            }
        }

        /// <summary>
		/// Information that specifies how the Long Date format is displayed in Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("longdateformatcode")]
        public int? LongDateFormatCode
        {
            get
            {
                return GetAttributeValue<int?>("longdateformatcode");
            }
            set
            {
                SetAttributeValue("longdateformatcode", value);
            }
        }

        /// <summary>
		/// Minimum number of characters that should be entered in the lookup control before resolving for suggestions
		/// </summary>
        [AttributeLogicalName("lookupcharactercountbeforeresolve")]
        public int? LookupCharacterCountBeforeResolve
        {
            get
            {
                return GetAttributeValue<int?>("lookupcharactercountbeforeresolve");
            }
            set
            {
                SetAttributeValue("lookupcharactercountbeforeresolve", value);
            }
        }

        /// <summary>
		/// Minimum delay (in milliseconds) between consecutive inputs in a lookup control that will trigger a search for suggestions
		/// </summary>
        [AttributeLogicalName("lookupresolvedelayms")]
        public int? LookupResolveDelayMS
        {
            get
            {
                return GetAttributeValue<int?>("lookupresolvedelayms");
            }
            set
            {
                SetAttributeValue("lookupresolvedelayms", value);
            }
        }

        /// <summary>
		/// Lower Threshold For Mailbox Intermittent Issue.
		/// </summary>
        [AttributeLogicalName("mailboxintermittentissueminrange")]
        public int? MailboxIntermittentIssueMinRange
        {
            get
            {
                return GetAttributeValue<int?>("mailboxintermittentissueminrange");
            }
            set
            {
                SetAttributeValue("mailboxintermittentissueminrange", value);
            }
        }

        /// <summary>
		/// Lower Threshold For Mailbox Permanent Issue.
		/// </summary>
        [AttributeLogicalName("mailboxpermanentissueminrange")]
        public int? MailboxPermanentIssueMinRange
        {
            get
            {
                return GetAttributeValue<int?>("mailboxpermanentissueminrange");
            }
            set
            {
                SetAttributeValue("mailboxpermanentissueminrange", value);
            }
        }

        /// <summary>
		/// Maximum number of actionsteps allowed in a BPF
		/// </summary>
        [AttributeLogicalName("maxactionstepsinbpf")]
        public int? MaxActionStepsInBPF
        {
            get
            {
                return GetAttributeValue<int?>("maxactionstepsinbpf");
            }
            set
            {
                SetAttributeValue("maxactionstepsinbpf", value);
            }
        }

        /// <summary>
		/// Maximum Allowed Pending Rollup Job Count
		/// </summary>
        [AttributeLogicalName("maxallowedpendingrollupjobcount")]
        public int? MaxAllowedPendingRollupJobCount
        {
            get
            {
                return GetAttributeValue<int?>("maxallowedpendingrollupjobcount");
            }
            set
            {
                SetAttributeValue("maxallowedpendingrollupjobcount", value);
            }
        }

        /// <summary>
		/// Percentage Of Entity Table Size For Kicking Off Bootstrap Job
		/// </summary>
        [AttributeLogicalName("maxallowedpendingrollupjobpercentage")]
        public int? MaxAllowedPendingRollupJobPercentage
        {
            get
            {
                return GetAttributeValue<int?>("maxallowedpendingrollupjobpercentage");
            }
            set
            {
                SetAttributeValue("maxallowedpendingrollupjobpercentage", value);
            }
        }

        /// <summary>
		/// Maximum number of days an appointment can last.
		/// </summary>
        [AttributeLogicalName("maxappointmentdurationdays")]
        public int? MaxAppointmentDurationDays
        {
            get
            {
                return GetAttributeValue<int?>("maxappointmentdurationdays");
            }
            set
            {
                SetAttributeValue("maxappointmentdurationdays", value);
            }
        }

        /// <summary>
		/// Maximum number of conditions allowed for mobile offline filters
		/// </summary>
        [AttributeLogicalName("maxconditionsformobileofflinefilters")]
        public int? MaxConditionsForMobileOfflineFilters
        {
            get
            {
                return GetAttributeValue<int?>("maxconditionsformobileofflinefilters");
            }
            set
            {
                SetAttributeValue("maxconditionsformobileofflinefilters", value);
            }
        }

        /// <summary>
		/// Maximum depth for hierarchy security propagation.
		/// </summary>
        [AttributeLogicalName("maxdepthforhierarchicalsecuritymodel")]
        public int? MaxDepthForHierarchicalSecurityModel
        {
            get
            {
                return GetAttributeValue<int?>("maxdepthforhierarchicalsecuritymodel");
            }
            set
            {
                SetAttributeValue("maxdepthforhierarchicalsecuritymodel", value);
            }
        }

        /// <summary>
		/// Maximum number of Folder Based Tracking mappings user can add
		/// </summary>
        [AttributeLogicalName("maxfolderbasedtrackingmappings")]
        public int? MaxFolderBasedTrackingMappings
        {
            get
            {
                return GetAttributeValue<int?>("maxfolderbasedtrackingmappings");
            }
            set
            {
                SetAttributeValue("maxfolderbasedtrackingmappings", value);
            }
        }

        /// <summary>
		/// Maximum number of active business process flows allowed per entity
		/// </summary>
        [AttributeLogicalName("maximumactivebusinessprocessflowsallowedperentity")]
        public int? MaximumActiveBusinessProcessFlowsAllowedPerEntity
        {
            get
            {
                return GetAttributeValue<int?>("maximumactivebusinessprocessflowsallowedperentity");
            }
            set
            {
                SetAttributeValue("maximumactivebusinessprocessflowsallowedperentity", value);
            }
        }

        /// <summary>
		/// Restrict the maximum number of product properties for a product family/bundle
		/// </summary>
        [AttributeLogicalName("maximumdynamicpropertiesallowed")]
        public int? MaximumDynamicPropertiesAllowed
        {
            get
            {
                return GetAttributeValue<int?>("maximumdynamicpropertiesallowed");
            }
            set
            {
                SetAttributeValue("maximumdynamicpropertiesallowed", value);
            }
        }

        /// <summary>
		/// Maximum number of active SLA allowed per entity in online
		/// </summary>
        [AttributeLogicalName("maximumentitieswithactivesla")]
        public int? MaximumEntitiesWithActiveSLA
        {
            get
            {
                return GetAttributeValue<int?>("maximumentitieswithactivesla");
            }
            set
            {
                SetAttributeValue("maximumentitieswithactivesla", value);
            }
        }

        /// <summary>
		/// Maximum number of SLA KPI per active SLA allowed for entity in online
		/// </summary>
        [AttributeLogicalName("maximumslakpiperentitywithactivesla")]
        public int? MaximumSLAKPIPerEntityWithActiveSLA
        {
            get
            {
                return GetAttributeValue<int?>("maximumslakpiperentitywithactivesla");
            }
            set
            {
                SetAttributeValue("maximumslakpiperentitywithactivesla", value);
            }
        }

        /// <summary>
		/// Maximum tracking number before recycling takes place.
		/// </summary>
        [AttributeLogicalName("maximumtrackingnumber")]
        public int? MaximumTrackingNumber
        {
            get
            {
                return GetAttributeValue<int?>("maximumtrackingnumber");
            }
            set
            {
                SetAttributeValue("maximumtrackingnumber", value);
            }
        }

        /// <summary>
		/// Restrict the maximum no of items in a bundle
		/// </summary>
        [AttributeLogicalName("maxproductsinbundle")]
        public int? MaxProductsInBundle
        {
            get
            {
                return GetAttributeValue<int?>("maxproductsinbundle");
            }
            set
            {
                SetAttributeValue("maxproductsinbundle", value);
            }
        }

        /// <summary>
		/// Maximum number of records that will be exported to a static Microsoft Office Excel worksheet when exporting from the grid.
		/// </summary>
        [AttributeLogicalName("maxrecordsforexporttoexcel")]
        public int? MaxRecordsForExportToExcel
        {
            get
            {
                return GetAttributeValue<int?>("maxrecordsforexporttoexcel");
            }
            set
            {
                SetAttributeValue("maxrecordsforexporttoexcel", value);
            }
        }

        /// <summary>
		/// Maximum number of lookup and picklist records that can be selected by user for filtering.
		/// </summary>
        [AttributeLogicalName("maxrecordsforlookupfilters")]
        public int? MaxRecordsForLookupFilters
        {
            get
            {
                return GetAttributeValue<int?>("maxrecordsforlookupfilters");
            }
            set
            {
                SetAttributeValue("maxrecordsforlookupfilters", value);
            }
        }

        /// <summary>
		/// Maximum Rollup Fields Per Entity
		/// </summary>
        [AttributeLogicalName("maxrollupfieldsperentity")]
        public int? MaxRollupFieldsPerEntity
        {
            get
            {
                return GetAttributeValue<int?>("maxrollupfieldsperentity");
            }
            set
            {
                SetAttributeValue("maxrollupfieldsperentity", value);
            }
        }

        /// <summary>
		/// Maximum Rollup Fields Per Organization
		/// </summary>
        [AttributeLogicalName("maxrollupfieldsperorg")]
        public int? MaxRollupFieldsPerOrg
        {
            get
            {
                return GetAttributeValue<int?>("maxrollupfieldsperorg");
            }
            set
            {
                SetAttributeValue("maxrollupfieldsperorg", value);
            }
        }

        
        [AttributeLogicalName("maxslaitemspersla")]
        public int? MaxSLAItemsPerSLA
        {
            get
            {
                return GetAttributeValue<int?>("maxslaitemspersla");
            }
            set
            {
                SetAttributeValue("maxslaitemspersla", value);
            }
        }

        /// <summary>
		/// The maximum version of IE to run browser emulation for in Outlook client
		/// </summary>
        [AttributeLogicalName("maxsupportedinternetexplorerversion")]
        public int? MaxSupportedInternetExplorerVersion
        {
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
            get
            {
                return GetAttributeValue<int?>("maxuploadfilesize");
            }
            set
            {
                SetAttributeValue("maxuploadfilesize", value);
            }
        }

        /// <summary>
		/// Maximum number of mailboxes that can be toggled for verbose logging
		/// </summary>
        [AttributeLogicalName("maxverboseloggingmailbox")]
        public int? MaxVerboseLoggingMailbox
        {
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
            get
            {
                return GetAttributeValue<string?>("microsoftflowenvironment");
            }
            set
            {
                SetAttributeValue("microsoftflowenvironment", value);
            }
        }

        /// <summary>
		/// Normal polling frequency used for address book synchronization in Microsoft Office Outlook.
		/// </summary>
        [AttributeLogicalName("minaddressbooksyncinterval")]
        public int? MinAddressBookSyncInterval
        {
            get
            {
                return GetAttributeValue<int?>("minaddressbooksyncinterval");
            }
            set
            {
                SetAttributeValue("minaddressbooksyncinterval", value);
            }
        }

        /// <summary>
		/// Normal polling frequency used for background offline synchronization in Microsoft Office Outlook.
		/// </summary>
        [AttributeLogicalName("minofflinesyncinterval")]
        public int? MinOfflineSyncInterval
        {
            get
            {
                return GetAttributeValue<int?>("minofflinesyncinterval");
            }
            set
            {
                SetAttributeValue("minofflinesyncinterval", value);
            }
        }

        /// <summary>
		/// Minimum allowed time between scheduled Outlook synchronizations.
		/// </summary>
        [AttributeLogicalName("minoutlooksyncinterval")]
        public int? MinOutlookSyncInterval
        {
            get
            {
                return GetAttributeValue<int?>("minoutlooksyncinterval");
            }
            set
            {
                SetAttributeValue("minoutlooksyncinterval", value);
            }
        }

        /// <summary>
		/// Minimum number of user license required for mobile offline service by production/preview organization
		/// </summary>
        [AttributeLogicalName("mobileofflineminlicenseprod")]
        public int? MobileOfflineMinLicenseProd
        {
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
            get
            {
                return GetAttributeValue<int?>("mobileofflinesyncinterval");
            }
            set
            {
                SetAttributeValue("mobileofflinesyncinterval", value);
            }
        }

        /// <summary>
		/// Flag to indicate if the modern advanced find filtering on all tables in a model-driven app is enabled
		/// </summary>
        [AttributeLogicalName("modernadvancedfindfiltering")]
        public bool? ModernAdvancedFindFiltering
        {
            get
            {
                return GetAttributeValue<bool?>("modernadvancedfindfiltering");
            }
            set
            {
                SetAttributeValue("modernadvancedfindfiltering", value);
            }
        }

        /// <summary>
		/// Indicates whether coauthoring is enabled in modern app designer
		/// </summary>
        [AttributeLogicalName("modernappdesignercoauthoringenabled")]
        public bool? ModernAppDesignerCoauthoringEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("modernappdesignercoauthoringenabled");
            }
            set
            {
                SetAttributeValue("modernappdesignercoauthoringenabled", value);
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the organization.
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
		/// Date and time when the organization was last modified.
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
		/// Unique identifier of the delegate user who last modified the organization.
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
		/// Show the sort by button on views
		/// </summary>
        [AttributeLogicalName("multicolumnsortenabled")]
        public int? MultiColumnSortEnabled
        {
            get
            {
                return GetAttributeValue<int?>("multicolumnsortenabled");
            }
            set
            {
                SetAttributeValue("multicolumnsortenabled", value);
            }
        }

        /// <summary>
		/// Name of the organization. The name is set when Microsoft CRM is installed and should not be changed.
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
		/// Enables Natural Language Assist Filter.
		/// </summary>
        [AttributeLogicalName("naturallanguageassistfilter")]
        public bool? NaturalLanguageAssistFilter
        {
            get
            {
                return GetAttributeValue<bool?>("naturallanguageassistfilter");
            }
            set
            {
                SetAttributeValue("naturallanguageassistfilter", value);
            }
        }

        /// <summary>
		/// Information that specifies how negative currency numbers are displayed throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("negativecurrencyformatcode")]
        public int? NegativeCurrencyFormatCode
        {
            get
            {
                return GetAttributeValue<int?>("negativecurrencyformatcode");
            }
            set
            {
                SetAttributeValue("negativecurrencyformatcode", value);
            }
        }

        /// <summary>
		/// Information that specifies how negative numbers are displayed throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("negativeformatcode")]
        public OptionSetValue? NegativeFormatCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("negativeformatcode");
            }
            set
            {
                SetAttributeValue("negativeformatcode", value);
            }
        }

        /// <summary>
		/// Indicates whether an organization has enabled the new Relevance search experience (released in Oct 2020) for the organization
		/// </summary>
        [AttributeLogicalName("newsearchexperienceenabled")]
        public bool? NewSearchExperienceEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("newsearchexperienceenabled");
            }
            set
            {
                SetAttributeValue("newsearchexperienceenabled", value);
            }
        }

        /// <summary>
		/// Next token to be placed on the subject line of an email message.
		/// </summary>
        [AttributeLogicalName("nexttrackingnumber")]
        public int? NextTrackingNumber
        {
            get
            {
                return GetAttributeValue<int?>("nexttrackingnumber");
            }
            set
            {
                SetAttributeValue("nexttrackingnumber", value);
            }
        }

        /// <summary>
		/// Indicates whether mailbox owners will be notified of email server profile level alerts.
		/// </summary>
        [AttributeLogicalName("notifymailboxownerofemailserverlevelalerts")]
        public bool? NotifyMailboxOwnerOfEmailServerLevelAlerts
        {
            get
            {
                return GetAttributeValue<bool?>("notifymailboxownerofemailserverlevelalerts");
            }
            set
            {
                SetAttributeValue("notifymailboxownerofemailserverlevelalerts", value);
            }
        }

        /// <summary>
		/// Specification of how numbers are displayed throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("numberformat")]
        public string? NumberFormat
        {
            get
            {
                return GetAttributeValue<string?>("numberformat");
            }
            set
            {
                SetAttributeValue("numberformat", value);
            }
        }

        /// <summary>
		/// Specifies how numbers are grouped in Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("numbergroupformat")]
        public string? NumberGroupFormat
        {
            get
            {
                return GetAttributeValue<string?>("numbergroupformat");
            }
            set
            {
                SetAttributeValue("numbergroupformat", value);
            }
        }

        /// <summary>
		/// Symbol used for number separation in Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("numberseparator")]
        public string? NumberSeparator
        {
            get
            {
                return GetAttributeValue<string?>("numberseparator");
            }
            set
            {
                SetAttributeValue("numberseparator", value);
            }
        }

        /// <summary>
		/// Indicates whether the Office Apps auto deployment is enabled for the organization.
		/// </summary>
        [AttributeLogicalName("officeappsautodeploymentenabled")]
        public bool? OfficeAppsAutoDeploymentEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("officeappsautodeploymentenabled");
            }
            set
            {
                SetAttributeValue("officeappsautodeploymentenabled", value);
            }
        }

        /// <summary>
		/// The url to open the Delve for the organization.
		/// </summary>
        [AttributeLogicalName("officegraphdelveurl")]
        public string? OfficeGraphDelveUrl
        {
            get
            {
                return GetAttributeValue<string?>("officegraphdelveurl");
            }
            set
            {
                SetAttributeValue("officegraphdelveurl", value);
            }
        }

        /// <summary>
		/// Enable OOB pricing calculation logic for Opportunity, Quote, Order and Invoice entities.
		/// </summary>
        [AttributeLogicalName("oobpricecalculationenabled")]
        public bool? OOBPriceCalculationEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("oobpricecalculationenabled");
            }
            set
            {
                SetAttributeValue("oobpricecalculationenabled", value);
            }
        }

        /// <summary>
		/// Indicates if this organization will opt-out from automatically enabling schema v2 on the organization.
		/// </summary>
        [AttributeLogicalName("optoutschemav2enabledbydefault")]
        public bool? OptOutSchemaV2EnabledByDefault
        {
            get
            {
                return GetAttributeValue<bool?>("optoutschemav2enabledbydefault");
            }
            set
            {
                SetAttributeValue("optoutschemav2enabledbydefault", value);
            }
        }

        /// <summary>
		/// Prefix to use for all orders throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("orderprefix")]
        public string? OrderPrefix
        {
            get
            {
                return GetAttributeValue<string?>("orderprefix");
            }
            set
            {
                SetAttributeValue("orderprefix", value);
            }
        }

        /// <summary>
		/// Indicates the organization lifecycle state
		/// </summary>
        [AttributeLogicalName("organizationstate")]
        public OptionSetValue? OrganizationState
        {
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
            get
            {
                return GetAttributeValue<string?>("orgdborgsettings");
            }
            set
            {
                SetAttributeValue("orgdborgsettings", value);
            }
        }

        /// <summary>
		/// Select whether to turn on OrgInsights for the organization.
		/// </summary>
        [AttributeLogicalName("orginsightsenabled")]
        public bool? OrgInsightsEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("orginsightsenabled");
            }
            set
            {
                SetAttributeValue("orginsightsenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether Preview feature has been enabled for the organization.
		/// </summary>
        [AttributeLogicalName("paipreviewscenarioenabled")]
        public bool? PaiPreviewScenarioEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("paipreviewscenarioenabled");
            }
            set
            {
                SetAttributeValue("paipreviewscenarioenabled", value);
            }
        }

        /// <summary>
		/// Prefix used for parsed table columns.
		/// </summary>
        [AttributeLogicalName("parsedtablecolumnprefix")]
        public string? ParsedTableColumnPrefix
        {
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
            get
            {
                return GetAttributeValue<int?>("pastexpansionwindow");
            }
            set
            {
                SetAttributeValue("pastexpansionwindow", value);
            }
        }

        /// <summary>
		/// Leave empty to use default setting. Set to on/off to enable/disable replacement of default grids with modern ones in model-driven apps.
		/// </summary>
        [AttributeLogicalName("pcfdatasetgridenabled")]
        public string? PcfDatasetGridEnabled
        {
            get
            {
                return GetAttributeValue<string?>("pcfdatasetgridenabled");
            }
            set
            {
                SetAttributeValue("pcfdatasetgridenabled", value);
            }
        }

        /// <summary>
		/// This setting contains the date time before an ACT sync can execute.
		/// </summary>
        [AttributeLogicalName("performactsyncafter")]
        public DateTime? PerformACTSyncAfter
        {
            get
            {
                return GetAttributeValue<DateTime?>("performactsyncafter");
            }
            set
            {
                SetAttributeValue("performactsyncafter", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("picture")]
        public string? Picture
        {
            get
            {
                return GetAttributeValue<string?>("picture");
            }
            set
            {
                SetAttributeValue("picture", value);
            }
        }

        
        [AttributeLogicalName("pinpointlanguagecode")]
        public int? PinpointLanguageCode
        {
            get
            {
                return GetAttributeValue<int?>("pinpointlanguagecode");
            }
            set
            {
                SetAttributeValue("pinpointlanguagecode", value);
            }
        }

        /// <summary>
		/// Plug-in Trace Log Setting for the Organization.
		/// </summary>
        [AttributeLogicalName("plugintracelogsetting")]
        public OptionSetValue? PluginTraceLogSetting
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("plugintracelogsetting");
            }
            set
            {
                SetAttributeValue("plugintracelogsetting", value);
            }
        }

        /// <summary>
		/// PM designator to use throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("pmdesignator")]
        public string? PMDesignator
        {
            get
            {
                return GetAttributeValue<string?>("pmdesignator");
            }
            set
            {
                SetAttributeValue("pmdesignator", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("postmessagewhitelistdomains")]
        public string? PostMessageWhitelistDomains
        {
            get
            {
                return GetAttributeValue<string?>("postmessagewhitelistdomains");
            }
            set
            {
                SetAttributeValue("postmessagewhitelistdomains", value);
            }
        }

        /// <summary>
		/// Indicates whether bot for makers is enabled.
		/// </summary>
        [AttributeLogicalName("powerappsmakerbotenabled")]
        public bool? PowerAppsMakerBotEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("powerappsmakerbotenabled");
            }
            set
            {
                SetAttributeValue("powerappsmakerbotenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether cross region operations are allowed for the organization
		/// </summary>
        [AttributeLogicalName("powerbiallowcrossregionoperations")]
        public bool? PowerBIAllowCrossRegionOperations
        {
            get
            {
                return GetAttributeValue<bool?>("powerbiallowcrossregionoperations");
            }
            set
            {
                SetAttributeValue("powerbiallowcrossregionoperations", value);
            }
        }

        /// <summary>
		/// Indicates whether automatic permissions assignment to Power BI has been enabled for the organization
		/// </summary>
        [AttributeLogicalName("powerbiautomaticpermissionsassignment")]
        public bool? PowerBIAutomaticPermissionsAssignment
        {
            get
            {
                return GetAttributeValue<bool?>("powerbiautomaticpermissionsassignment");
            }
            set
            {
                SetAttributeValue("powerbiautomaticpermissionsassignment", value);
            }
        }

        /// <summary>
		/// Indicates whether creation of Power BI components has been enabled for the organization
		/// </summary>
        [AttributeLogicalName("powerbicomponentscreate")]
        public bool? PowerBIComponentsCreate
        {
            get
            {
                return GetAttributeValue<bool?>("powerbicomponentscreate");
            }
            set
            {
                SetAttributeValue("powerbicomponentscreate", value);
            }
        }

        /// <summary>
		/// Indicates whether the Power BI feature should be enabled for the organization.
		/// </summary>
        [AttributeLogicalName("powerbifeatureenabled")]
        public bool? PowerBiFeatureEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("powerbifeatureenabled");
            }
            set
            {
                SetAttributeValue("powerbifeatureenabled", value);
            }
        }

        /// <summary>
		/// Number of decimal places that can be used for prices.
		/// </summary>
        [AttributeLogicalName("pricingdecimalprecision")]
        public int? PricingDecimalPrecision
        {
            get
            {
                return GetAttributeValue<int?>("pricingdecimalprecision");
            }
            set
            {
                SetAttributeValue("pricingdecimalprecision", value);
            }
        }

        /// <summary>
		/// Privacy Statement URL
		/// </summary>
        [AttributeLogicalName("privacystatementurl")]
        public string? PrivacyStatementUrl
        {
            get
            {
                return GetAttributeValue<string?>("privacystatementurl");
            }
            set
            {
                SetAttributeValue("privacystatementurl", value);
            }
        }

        /// <summary>
		/// Unique identifier of the default privilege for users in the organization.
		/// </summary>
        [AttributeLogicalName("privilegeusergroupid")]
        public Guid? PrivilegeUserGroupId
        {
            get
            {
                return GetAttributeValue<Guid?>("privilegeusergroupid");
            }
            set
            {
                SetAttributeValue("privilegeusergroupid", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("privreportinggroupid")]
        public Guid? PrivReportingGroupId
        {
            get
            {
                return GetAttributeValue<Guid?>("privreportinggroupid");
            }
            set
            {
                SetAttributeValue("privreportinggroupid", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("privreportinggroupname")]
        public string? PrivReportingGroupName
        {
            get
            {
                return GetAttributeValue<string?>("privreportinggroupname");
            }
            set
            {
                SetAttributeValue("privreportinggroupname", value);
            }
        }

        /// <summary>
		/// Select whether to turn on product recommendations for the organization.
		/// </summary>
        [AttributeLogicalName("productrecommendationsenabled")]
        public bool? ProductRecommendationsEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("productrecommendationsenabled");
            }
            set
            {
                SetAttributeValue("productrecommendationsenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether prompt should be shown for new Qualify Lead Experience
		/// </summary>
        [AttributeLogicalName("qualifyleadadditionaloptions")]
        public string? QualifyLeadAdditionalOptions
        {
            get
            {
                return GetAttributeValue<string?>("qualifyleadadditionaloptions");
            }
            set
            {
                SetAttributeValue("qualifyleadadditionaloptions", value);
            }
        }

        /// <summary>
		/// Flag to indicate if the feature to use quick action to open records in search side pane is enabled
		/// </summary>
        [AttributeLogicalName("quickactiontoopenrecordsinsidepaneenabled")]
        public bool? QuickActionToOpenRecordsInSidePaneEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("quickactiontoopenrecordsinsidepaneenabled");
            }
            set
            {
                SetAttributeValue("quickactiontoopenrecordsinsidepaneenabled", value);
            }
        }

        /// <summary>
		/// Indicates whether a quick find record limit should be enabled for this organization (allows for faster Quick Find queries but prevents overly broad searches).
		/// </summary>
        [AttributeLogicalName("quickfindrecordlimitenabled")]
        public bool? QuickFindRecordLimitEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("quickfindrecordlimitenabled");
            }
            set
            {
                SetAttributeValue("quickfindrecordlimitenabled", value);
            }
        }

        /// <summary>
		/// Prefix to use for all quotes throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("quoteprefix")]
        public string? QuotePrefix
        {
            get
            {
                return GetAttributeValue<string?>("quoteprefix");
            }
            set
            {
                SetAttributeValue("quoteprefix", value);
            }
        }

        /// <summary>
		/// Indicates whether SLA Recalculation has been enabled for the organization
		/// </summary>
        [AttributeLogicalName("recalculatesla")]
        public bool? RecalculateSLA
        {
            get
            {
                return GetAttributeValue<bool?>("recalculatesla");
            }
            set
            {
                SetAttributeValue("recalculatesla", value);
            }
        }

        /// <summary>
		/// Specifies the default value for number of occurrences field in the recurrence dialog.
		/// </summary>
        [AttributeLogicalName("recurrencedefaultnumberofoccurrences")]
        public int? RecurrenceDefaultNumberOfOccurrences
        {
            get
            {
                return GetAttributeValue<int?>("recurrencedefaultnumberofoccurrences");
            }
            set
            {
                SetAttributeValue("recurrencedefaultnumberofoccurrences", value);
            }
        }

        /// <summary>
		/// Specifies the interval (in seconds) for pausing expansion job.
		/// </summary>
        [AttributeLogicalName("recurrenceexpansionjobbatchinterval")]
        public int? RecurrenceExpansionJobBatchInterval
        {
            get
            {
                return GetAttributeValue<int?>("recurrenceexpansionjobbatchinterval");
            }
            set
            {
                SetAttributeValue("recurrenceexpansionjobbatchinterval", value);
            }
        }

        /// <summary>
		/// Specifies the value for number of instances created in on demand job in one shot.
		/// </summary>
        [AttributeLogicalName("recurrenceexpansionjobbatchsize")]
        public int? RecurrenceExpansionJobBatchSize
        {
            get
            {
                return GetAttributeValue<int?>("recurrenceexpansionjobbatchsize");
            }
            set
            {
                SetAttributeValue("recurrenceexpansionjobbatchsize", value);
            }
        }

        /// <summary>
		/// Specifies the maximum number of instances to be created synchronously after creating a recurring appointment.
		/// </summary>
        [AttributeLogicalName("recurrenceexpansionsynchcreatemax")]
        public int? RecurrenceExpansionSynchCreateMax
        {
            get
            {
                return GetAttributeValue<int?>("recurrenceexpansionsynchcreatemax");
            }
            set
            {
                SetAttributeValue("recurrenceexpansionsynchcreatemax", value);
            }
        }

        /// <summary>
		/// XML string that defines the navigation structure for the application. This is the site map from the previously upgraded build and is used in a 3-way merge during upgrade.
		/// </summary>
        [AttributeLogicalName("referencesitemapxml")]
        public string? ReferenceSiteMapXml
        {
            get
            {
                return GetAttributeValue<string?>("referencesitemapxml");
            }
            set
            {
                SetAttributeValue("referencesitemapxml", value);
            }
        }

        /// <summary>
		/// Current orgnization release cadence value
		/// </summary>
        [AttributeLogicalName("releasecadence")]
        public int? ReleaseCadence
        {
            get
            {
                return GetAttributeValue<int?>("releasecadence");
            }
            set
            {
                SetAttributeValue("releasecadence", value);
            }
        }

        /// <summary>
		/// Model app refresh channel
		/// </summary>
        [AttributeLogicalName("releasechannel")]
        public OptionSetValue? ReleaseChannel
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("releasechannel");
            }
            set
            {
                SetAttributeValue("releasechannel", value);
            }
        }

        /// <summary>
		/// Release Wave Applied to Environment.
		/// </summary>
        [AttributeLogicalName("releasewavename")]
        public string? ReleaseWaveName
        {
            get
            {
                return GetAttributeValue<string?>("releasewavename");
            }
            set
            {
                SetAttributeValue("releasewavename", value);
            }
        }

        /// <summary>
		/// Indicates whether relevance search was enabled for the environment as part of Dataverse's relevance search on-by-default sweep
		/// </summary>
        [AttributeLogicalName("relevancesearchenabledbyplatform")]
        public bool? RelevanceSearchEnabledByPlatform
        {
            get
            {
                return GetAttributeValue<bool?>("relevancesearchenabledbyplatform");
            }
            set
            {
                SetAttributeValue("relevancesearchenabledbyplatform", value);
            }
        }

        /// <summary>
		/// This setting contains the last modified date for relevance search setting that appears as a toggle in PPAC.
		/// </summary>
        [AttributeLogicalName("relevancesearchmodifiedon")]
        public DateTime? RelevanceSearchModifiedOn
        {
            get
            {
                return GetAttributeValue<DateTime?>("relevancesearchmodifiedon");
            }
            set
            {
                SetAttributeValue("relevancesearchmodifiedon", value);
            }
        }

        /// <summary>
		/// Flag to render the body of email in the Web form in an IFRAME with the security='restricted' attribute set. This is additional security but can cause a credentials prompt.
		/// </summary>
        [AttributeLogicalName("rendersecureiframeforemail")]
        public bool? RenderSecureIFrameForEmail
        {
            get
            {
                return GetAttributeValue<bool?>("rendersecureiframeforemail");
            }
            set
            {
                SetAttributeValue("rendersecureiframeforemail", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("reportinggroupid")]
        public Guid? ReportingGroupId
        {
            get
            {
                return GetAttributeValue<Guid?>("reportinggroupid");
            }
            set
            {
                SetAttributeValue("reportinggroupid", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("reportinggroupname")]
        public string? ReportingGroupName
        {
            get
            {
                return GetAttributeValue<string?>("reportinggroupname");
            }
            set
            {
                SetAttributeValue("reportinggroupname", value);
            }
        }

        /// <summary>
		/// Picklist for selecting the organization preference for reporting scripting errors.
		/// </summary>
        [AttributeLogicalName("reportscripterrors")]
        public OptionSetValue? ReportScriptErrors
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("reportscripterrors");
            }
            set
            {
                SetAttributeValue("reportscripterrors", value);
            }
        }

        /// <summary>
		/// Indicates whether Send As Other User privilege is enabled.
		/// </summary>
        [AttributeLogicalName("requireapprovalforqueueemail")]
        public bool? RequireApprovalForQueueEmail
        {
            get
            {
                return GetAttributeValue<bool?>("requireapprovalforqueueemail");
            }
            set
            {
                SetAttributeValue("requireapprovalforqueueemail", value);
            }
        }

        /// <summary>
		/// Indicates whether Send As Other User privilege is enabled.
		/// </summary>
        [AttributeLogicalName("requireapprovalforuseremail")]
        public bool? RequireApprovalForUserEmail
        {
            get
            {
                return GetAttributeValue<bool?>("requireapprovalforuseremail");
            }
            set
            {
                SetAttributeValue("requireapprovalforuseremail", value);
            }
        }

        /// <summary>
		/// Apply same email address to all unresolved matches when you manually resolve it for one
		/// </summary>
        [AttributeLogicalName("resolvesimilarunresolvedemailaddress")]
        public bool? ResolveSimilarUnresolvedEmailAddress
        {
            get
            {
                return GetAttributeValue<bool?>("resolvesimilarunresolvedemailaddress");
            }
            set
            {
                SetAttributeValue("resolvesimilarunresolvedemailaddress", value);
            }
        }

        /// <summary>
		/// Information that specifies whether guest user restriction is enabled
		/// </summary>
        [AttributeLogicalName("restrictGuestUserAccess")]
        public bool? RestrictGuestUserAccess
        {
            get
            {
                return GetAttributeValue<bool?>("restrictGuestUserAccess");
            }
            set
            {
                SetAttributeValue("restrictGuestUserAccess", value);
            }
        }

        /// <summary>
		/// Flag to restrict Update on incident.
		/// </summary>
        [AttributeLogicalName("restrictstatusupdate")]
        public bool? RestrictStatusUpdate
        {
            get
            {
                return GetAttributeValue<bool?>("restrictstatusupdate");
            }
            set
            {
                SetAttributeValue("restrictstatusupdate", value);
            }
        }

        /// <summary>
		/// Information that specifies Reverse Proxy IP addresses from which requests have to be allowed.
		/// </summary>
        [AttributeLogicalName("reverseproxyipaddresses")]
        public string? ReverseProxyIpAddresses
        {
            get
            {
                return GetAttributeValue<string?>("reverseproxyipaddresses");
            }
            set
            {
                SetAttributeValue("reverseproxyipaddresses", value);
            }
        }

        /// <summary>
		/// Error status of Relationship Insights provisioning.
		/// </summary>
        [AttributeLogicalName("rierrorstatus")]
        public int? RiErrorStatus
        {
            get
            {
                return GetAttributeValue<int?>("rierrorstatus");
            }
            set
            {
                SetAttributeValue("rierrorstatus", value);
            }
        }

        /// <summary>
		/// Samesite mode for Session Cookie 0 is Default, 1 is None, 2 is Lax , 3 is Strict
		/// </summary>
        [AttributeLogicalName("samesitemodeforsessioncookie")]
        public OptionSetValue? SameSiteModeForSessionCookie
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("samesitemodeforsessioncookie");
            }
            set
            {
                SetAttributeValue("samesitemodeforsessioncookie", value);
            }
        }

        /// <summary>
		/// Unique identifier of the sample data import job.
		/// </summary>
        [AttributeLogicalName("sampledataimportid")]
        public Guid? SampleDataImportId
        {
            get
            {
                return GetAttributeValue<Guid?>("sampledataimportid");
            }
            set
            {
                SetAttributeValue("sampledataimportid", value);
            }
        }

        /// <summary>
		/// Default time to live in minutes for new Power Automate savings events records in flow aggregation.
		/// </summary>
        [AttributeLogicalName("savingeventsttlinminutes")]
        public int? SavingEventsTTLInMinutes
        {
            get
            {
                return GetAttributeValue<int?>("savingeventsttlinminutes");
            }
            set
            {
                SetAttributeValue("savingeventsttlinminutes", value);
            }
        }

        /// <summary>
		/// Prefix used for custom entities and attributes.
		/// </summary>
        [AttributeLogicalName("schemanameprefix")]
        public string? SchemaNamePrefix
        {
            get
            {
                return GetAttributeValue<string?>("schemanameprefix");
            }
            set
            {
                SetAttributeValue("schemanameprefix", value);
            }
        }

        /// <summary>
		/// Indicates whether Send Bulk Email in UCI is enabled for the org.
		/// </summary>
        [AttributeLogicalName("sendbulkemailinuci")]
        public bool? SendBulkEmailInUCI
        {
            get
            {
                return GetAttributeValue<bool?>("sendbulkemailinuci");
            }
            set
            {
                SetAttributeValue("sendbulkemailinuci", value);
            }
        }

        /// <summary>
		/// Serve Static Content From CDN
		/// </summary>
        [AttributeLogicalName("servestaticresourcesfromazurecdn")]
        public bool? ServeStaticResourcesFromAzureCDN
        {
            get
            {
                return GetAttributeValue<bool?>("servestaticresourcesfromazurecdn");
            }
            set
            {
                SetAttributeValue("servestaticresourcesfromazurecdn", value);
            }
        }

        /// <summary>
		/// Enable the session recording feature to record user sessions in UCI
		/// </summary>
        [AttributeLogicalName("sessionrecordingenabled")]
        public bool? SessionRecordingEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("sessionrecordingenabled");
            }
            set
            {
                SetAttributeValue("sessionrecordingenabled", value);
            }
        }

        /// <summary>
		/// Information that specifies whether session timeout is enabled
		/// </summary>
        [AttributeLogicalName("sessiontimeoutenabled")]
        public bool? SessionTimeoutEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("sessiontimeoutenabled");
            }
            set
            {
                SetAttributeValue("sessiontimeoutenabled", value);
            }
        }

        /// <summary>
		/// Session timeout in minutes
		/// </summary>
        [AttributeLogicalName("sessiontimeoutinmins")]
        public int? SessionTimeoutInMins
        {
            get
            {
                return GetAttributeValue<int?>("sessiontimeoutinmins");
            }
            set
            {
                SetAttributeValue("sessiontimeoutinmins", value);
            }
        }

        /// <summary>
		/// Session timeout reminder in minutes
		/// </summary>
        [AttributeLogicalName("sessiontimeoutreminderinmins")]
        public int? SessionTimeoutReminderInMins
        {
            get
            {
                return GetAttributeValue<int?>("sessiontimeoutreminderinmins");
            }
            set
            {
                SetAttributeValue("sessiontimeoutreminderinmins", value);
            }
        }

        /// <summary>
		/// Indicates which SharePoint deployment type is configured for Server to Server. (Online or On-Premises)
		/// </summary>
        [AttributeLogicalName("sharepointdeploymenttype")]
        public OptionSetValue? SharePointDeploymentType
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("sharepointdeploymenttype");
            }
            set
            {
                SetAttributeValue("sharepointdeploymenttype", value);
            }
        }

        /// <summary>
		/// Information that specifies whether to share to previous owner on assign.
		/// </summary>
        [AttributeLogicalName("sharetopreviousowneronassign")]
        public bool? ShareToPreviousOwnerOnAssign
        {
            get
            {
                return GetAttributeValue<bool?>("sharetopreviousowneronassign");
            }
            set
            {
                SetAttributeValue("sharetopreviousowneronassign", value);
            }
        }

        /// <summary>
		/// Select whether to display a KB article deprecation notification to the user.
		/// </summary>
        [AttributeLogicalName("showkbarticledeprecationnotification")]
        public bool? ShowKBArticleDeprecationNotification
        {
            get
            {
                return GetAttributeValue<bool?>("showkbarticledeprecationnotification");
            }
            set
            {
                SetAttributeValue("showkbarticledeprecationnotification", value);
            }
        }

        /// <summary>
		/// Information that specifies whether to display the week number in calendar displays throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("showweeknumber")]
        public bool? ShowWeekNumber
        {
            get
            {
                return GetAttributeValue<bool?>("showweeknumber");
            }
            set
            {
                SetAttributeValue("showweeknumber", value);
            }
        }

        /// <summary>
		/// CRM for Outlook Download URL
		/// </summary>
        [AttributeLogicalName("signupoutlookdownloadfwlink")]
        public string? SignupOutlookDownloadFWLink
        {
            get
            {
                return GetAttributeValue<string?>("signupoutlookdownloadfwlink");
            }
            set
            {
                SetAttributeValue("signupoutlookdownloadfwlink", value);
            }
        }

        /// <summary>
		/// XML string that defines the navigation structure for the application.
		/// </summary>
        [AttributeLogicalName("sitemapxml")]
        public string? SiteMapXml
        {
            get
            {
                return GetAttributeValue<string?>("sitemapxml");
            }
            set
            {
                SetAttributeValue("sitemapxml", value);
            }
        }

        /// <summary>
		/// Contains the on hold case status values.
		/// </summary>
        [AttributeLogicalName("slapausestates")]
        public string? SlaPauseStates
        {
            get
            {
                return GetAttributeValue<string?>("slapausestates");
            }
            set
            {
                SetAttributeValue("slapausestates", value);
            }
        }

        /// <summary>
		/// Flag for whether the organization is using Social Insights.
		/// </summary>
        [AttributeLogicalName("socialinsightsenabled")]
        public bool? SocialInsightsEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("socialinsightsenabled");
            }
            set
            {
                SetAttributeValue("socialinsightsenabled", value);
            }
        }

        /// <summary>
		/// Identifier for the Social Insights instance for the organization.
		/// </summary>
        [AttributeLogicalName("socialinsightsinstance")]
        public string? SocialInsightsInstance
        {
            get
            {
                return GetAttributeValue<string?>("socialinsightsinstance");
            }
            set
            {
                SetAttributeValue("socialinsightsinstance", value);
            }
        }

        /// <summary>
		/// Flag for whether the organization has accepted the Social Insights terms of use.
		/// </summary>
        [AttributeLogicalName("socialinsightstermsaccepted")]
        public bool? SocialInsightsTermsAccepted
        {
            get
            {
                return GetAttributeValue<bool?>("socialinsightstermsaccepted");
            }
            set
            {
                SetAttributeValue("socialinsightstermsaccepted", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("sortid")]
        public int? SortId
        {
            get
            {
                return GetAttributeValue<int?>("sortid");
            }
            set
            {
                SetAttributeValue("sortid", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("sqlaccessgroupid")]
        public Guid? SqlAccessGroupId
        {
            get
            {
                return GetAttributeValue<Guid?>("sqlaccessgroupid");
            }
            set
            {
                SetAttributeValue("sqlaccessgroupid", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("sqlaccessgroupname")]
        public string? SqlAccessGroupName
        {
            get
            {
                return GetAttributeValue<string?>("sqlaccessgroupname");
            }
            set
            {
                SetAttributeValue("sqlaccessgroupname", value);
            }
        }

        /// <summary>
		/// Setting for SQM data collection, 0 no, 1 yes enabled
		/// </summary>
        [AttributeLogicalName("sqmenabled")]
        public bool? SQMEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("sqmenabled");
            }
            set
            {
                SetAttributeValue("sqmenabled", value);
            }
        }

        /// <summary>
		/// Unique identifier of the support user for the organization.
		/// </summary>
        [AttributeLogicalName("supportuserid")]
        public Guid? SupportUserId
        {
            get
            {
                return GetAttributeValue<Guid?>("supportuserid");
            }
            set
            {
                SetAttributeValue("supportuserid", value);
            }
        }

        /// <summary>
		/// Indicates whether SLA is suppressed.
		/// </summary>
        [AttributeLogicalName("suppresssla")]
        public bool? SuppressSLA
        {
            get
            {
                return GetAttributeValue<bool?>("suppresssla");
            }
            set
            {
                SetAttributeValue("suppresssla", value);
            }
        }

        /// <summary>
		/// Leave empty to use default setting. Set to on/off to enable/disable Admin emails when Solution Checker validation fails.
		/// </summary>
        [AttributeLogicalName("suppressvalidationemails")]
        public bool? SuppressValidationEmails
        {
            get
            {
                return GetAttributeValue<bool?>("suppressvalidationemails");
            }
            set
            {
                SetAttributeValue("suppressvalidationemails", value);
            }
        }

        /// <summary>
		/// Number of records to update per operation in Sync Bulk Pause/Resume/Cancel
		/// </summary>
        [AttributeLogicalName("syncbulkoperationbatchsize")]
        public int? SyncBulkOperationBatchSize
        {
            get
            {
                return GetAttributeValue<int?>("syncbulkoperationbatchsize");
            }
            set
            {
                SetAttributeValue("syncbulkoperationbatchsize", value);
            }
        }

        /// <summary>
		/// Max total number of records to update in database for Sync Bulk Pause/Resume/Cancel
		/// </summary>
        [AttributeLogicalName("syncbulkoperationmaxlimit")]
        public int? SyncBulkOperationMaxLimit
        {
            get
            {
                return GetAttributeValue<int?>("syncbulkoperationmaxlimit");
            }
            set
            {
                SetAttributeValue("syncbulkoperationmaxlimit", value);
            }
        }

        /// <summary>
		/// Indicates the selection to use the dynamics 365 azure sync framework or server side sync.
		/// </summary>
        [AttributeLogicalName("syncoptinselection")]
        public bool? SyncOptInSelection
        {
            get
            {
                return GetAttributeValue<bool?>("syncoptinselection");
            }
            set
            {
                SetAttributeValue("syncoptinselection", value);
            }
        }

        /// <summary>
		/// Indicates the status of the opt-in or opt-out operation for dynamics 365 azure sync.
		/// </summary>
        [AttributeLogicalName("syncoptinselectionstatus")]
        public OptionSetValue? SyncOptInSelectionStatus
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("syncoptinselectionstatus");
            }
            set
            {
                SetAttributeValue("syncoptinselectionstatus", value);
            }
        }

        /// <summary>
		/// Unique identifier of the system user for the organization.
		/// </summary>
        [AttributeLogicalName("systemuserid")]
        public Guid? SystemUserId
        {
            get
            {
                return GetAttributeValue<Guid?>("systemuserid");
            }
            set
            {
                SetAttributeValue("systemuserid", value);
            }
        }

        /// <summary>
		/// Controls the appearance of option to search over a single DV search indexed table in model-driven apps’ global search in the header.
		/// </summary>
        [AttributeLogicalName("tablescopeddvsearchinapps")]
        public bool? TableScopedDVSearchInApps
        {
            get
            {
                return GetAttributeValue<bool?>("tablescopeddvsearchinapps");
            }
            set
            {
                SetAttributeValue("tablescopeddvsearchinapps", value);
            }
        }

        /// <summary>
		/// Maximum number of aggressive polling cycles executed for email auto-tagging when a new email is received.
		/// </summary>
        [AttributeLogicalName("tagmaxaggressivecycles")]
        public int? TagMaxAggressiveCycles
        {
            get
            {
                return GetAttributeValue<int?>("tagmaxaggressivecycles");
            }
            set
            {
                SetAttributeValue("tagmaxaggressivecycles", value);
            }
        }

        /// <summary>
		/// Normal polling frequency used for email receive auto-tagging in outlook.
		/// </summary>
        [AttributeLogicalName("tagpollingperiod")]
        public int? TagPollingPeriod
        {
            get
            {
                return GetAttributeValue<int?>("tagpollingperiod");
            }
            set
            {
                SetAttributeValue("tagpollingperiod", value);
            }
        }

        /// <summary>
		/// Select whether to turn on task flows for the organization.
		/// </summary>
        [AttributeLogicalName("taskbasedflowenabled")]
        public bool? TaskBasedFlowEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("taskbasedflowenabled");
            }
            set
            {
                SetAttributeValue("taskbasedflowenabled", value);
            }
        }

        /// <summary>
		/// Information on whether Teams Chat Data Sync is enabled.
		/// </summary>
        [AttributeLogicalName("teamschatdatasync")]
        public bool? TeamsChatDataSync
        {
            get
            {
                return GetAttributeValue<bool?>("teamschatdatasync");
            }
            set
            {
                SetAttributeValue("teamschatdatasync", value);
            }
        }

        /// <summary>
		/// Instrumentation key for Application Insights used to log plugins telemetry.
		/// </summary>
        [AttributeLogicalName("telemetryinstrumentationkey")]
        public string? TelemetryInstrumentationKey
        {
            set
            {
                SetAttributeValue("telemetryinstrumentationkey", value);
            }
        }

        /// <summary>
		/// Select whether to turn on text analytics for the organization.
		/// </summary>
        [AttributeLogicalName("textanalyticsenabled")]
        public bool? TextAnalyticsEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("textanalyticsenabled");
            }
            set
            {
                SetAttributeValue("textanalyticsenabled", value);
            }
        }

        /// <summary>
		/// Information that specifies how the time is displayed throughout Microsoft CRM.
		/// </summary>
        [AttributeLogicalName("timeformatcode")]
        public OptionSetValue? TimeFormatCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("timeformatcode");
            }
            set
            {
                SetAttributeValue("timeformatcode", value);
            }
        }

        /// <summary>
		/// Text for how time is displayed in Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("timeformatstring")]
        public string? TimeFormatString
        {
            get
            {
                return GetAttributeValue<string?>("timeformatstring");
            }
            set
            {
                SetAttributeValue("timeformatstring", value);
            }
        }

        /// <summary>
		/// Text for how the time separator is displayed throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("timeseparator")]
        public string? TimeSeparator
        {
            get
            {
                return GetAttributeValue<string?>("timeseparator");
            }
            set
            {
                SetAttributeValue("timeseparator", value);
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
		/// Duration used for token expiration.
		/// </summary>
        [AttributeLogicalName("tokenexpiry")]
        public int? TokenExpiry
        {
            get
            {
                return GetAttributeValue<int?>("tokenexpiry");
            }
            set
            {
                SetAttributeValue("tokenexpiry", value);
            }
        }

        /// <summary>
		/// Token key.
		/// </summary>
        [AttributeLogicalName("tokenkey")]
        public string? TokenKey
        {
            set
            {
                SetAttributeValue("tokenkey", value);
            }
        }

        /// <summary>
		/// Tracelog record maximum age in days
		/// </summary>
        [AttributeLogicalName("tracelogmaximumageindays")]
        public int? TraceLogMaximumAgeInDays
        {
            get
            {
                return GetAttributeValue<int?>("tracelogmaximumageindays");
            }
            set
            {
                SetAttributeValue("tracelogmaximumageindays", value);
            }
        }

        /// <summary>
		/// History list of tracking token prefixes.
		/// </summary>
        [AttributeLogicalName("trackingprefix")]
        public string? TrackingPrefix
        {
            get
            {
                return GetAttributeValue<string?>("trackingprefix");
            }
            set
            {
                SetAttributeValue("trackingprefix", value);
            }
        }

        /// <summary>
		/// Base number used to provide separate tracking token identifiers to users belonging to different deployments.
		/// </summary>
        [AttributeLogicalName("trackingtokenidbase")]
        public int? TrackingTokenIdBase
        {
            get
            {
                return GetAttributeValue<int?>("trackingtokenidbase");
            }
            set
            {
                SetAttributeValue("trackingtokenidbase", value);
            }
        }

        /// <summary>
		/// Number of digits used to represent a tracking token identifier.
		/// </summary>
        [AttributeLogicalName("trackingtokeniddigits")]
        public int? TrackingTokenIdDigits
        {
            get
            {
                return GetAttributeValue<int?>("trackingtokeniddigits");
            }
            set
            {
                SetAttributeValue("trackingtokeniddigits", value);
            }
        }

        /// <summary>
		/// Number of characters appended to invoice, quote, and order numbers.
		/// </summary>
        [AttributeLogicalName("uniquespecifierlength")]
        public int? UniqueSpecifierLength
        {
            get
            {
                return GetAttributeValue<int?>("uniquespecifierlength");
            }
            set
            {
                SetAttributeValue("uniquespecifierlength", value);
            }
        }

        /// <summary>
		/// Indicates whether email address should be unresolved if multiple matches are found
		/// </summary>
        [AttributeLogicalName("unresolveemailaddressifmultiplematch")]
        public bool? UnresolveEmailAddressIfMultipleMatch
        {
            get
            {
                return GetAttributeValue<bool?>("unresolveemailaddressifmultiplematch");
            }
            set
            {
                SetAttributeValue("unresolveemailaddressifmultiplematch", value);
            }
        }

        /// <summary>
		/// Flag indicates whether to Use Inbuilt Rule For DefaultPricelist.
		/// </summary>
        [AttributeLogicalName("useinbuiltrulefordefaultpricelistselection")]
        public bool? UseInbuiltRuleForDefaultPricelistSelection
        {
            get
            {
                return GetAttributeValue<bool?>("useinbuiltrulefordefaultpricelistselection");
            }
            set
            {
                SetAttributeValue("useinbuiltrulefordefaultpricelistselection", value);
            }
        }

        /// <summary>
		/// Select whether to use legacy form rendering.
		/// </summary>
        [AttributeLogicalName("uselegacyrendering")]
        public bool? UseLegacyRendering
        {
            get
            {
                return GetAttributeValue<bool?>("uselegacyrendering");
            }
            set
            {
                SetAttributeValue("uselegacyrendering", value);
            }
        }

        /// <summary>
		/// Use position hierarchy
		/// </summary>
        [AttributeLogicalName("usepositionhierarchy")]
        public bool? UsePositionHierarchy
        {
            get
            {
                return GetAttributeValue<bool?>("usepositionhierarchy");
            }
            set
            {
                SetAttributeValue("usepositionhierarchy", value);
            }
        }

        /// <summary>
		/// Indicates whether searching in a grid should use the Quick Find view for the entity.
		/// </summary>
        [AttributeLogicalName("usequickfindviewforgridsearch")]
        public bool? UseQuickFindViewForGridSearch
        {
            get
            {
                return GetAttributeValue<bool?>("usequickfindviewforgridsearch");
            }
            set
            {
                SetAttributeValue("usequickfindviewforgridsearch", value);
            }
        }

        /// <summary>
		/// The interval at which user access is checked for auditing.
		/// </summary>
        [AttributeLogicalName("useraccessauditinginterval")]
        public int? UserAccessAuditingInterval
        {
            get
            {
                return GetAttributeValue<int?>("useraccessauditinginterval");
            }
            set
            {
                SetAttributeValue("useraccessauditinginterval", value);
            }
        }

        /// <summary>
		/// Indicates whether the read-optimized form should be enabled for this organization.
		/// </summary>
        [AttributeLogicalName("usereadform")]
        public bool? UseReadForm
        {
            get
            {
                return GetAttributeValue<bool?>("usereadform");
            }
            set
            {
                SetAttributeValue("usereadform", value);
            }
        }

        /// <summary>
		/// Unique identifier of the default group of users in the organization.
		/// </summary>
        [AttributeLogicalName("usergroupid")]
        public Guid? UserGroupId
        {
            get
            {
                return GetAttributeValue<Guid?>("usergroupid");
            }
            set
            {
                SetAttributeValue("usergroupid", value);
            }
        }

        /// <summary>
		/// Enable the user rating feature to show the NSAT score and comment to maker
		/// </summary>
        [AttributeLogicalName("userratingenabled")]
        public bool? UserRatingEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("userratingenabled");
            }
            set
            {
                SetAttributeValue("userratingenabled", value);
            }
        }

        /// <summary>
		/// Indicates default protocol selected for organization.
		/// </summary>
        [AttributeLogicalName("useskypeprotocol")]
        public bool? UseSkypeProtocol
        {
            get
            {
                return GetAttributeValue<bool?>("useskypeprotocol");
            }
            set
            {
                SetAttributeValue("useskypeprotocol", value);
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

        /// <summary>
		/// Hash of the V3 callout configuration file.
		/// </summary>
        [AttributeLogicalName("v3calloutconfighash")]
        public string? V3CalloutConfigHash
        {
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
            get
            {
                return GetAttributeValue<OptionSetValue?>("validationmode");
            }
            set
            {
                SetAttributeValue("validationmode", value);
            }
        }

        /// <summary>
		/// Version number of the organization.
		/// </summary>
        [AttributeLogicalName("versionnumber")]
        public long? VersionNumber
        {
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
            get
            {
                return GetAttributeValue<string?>("webresourcehash");
            }
            set
            {
                SetAttributeValue("webresourcehash", value);
            }
        }

        /// <summary>
		/// Designated first day of the week throughout Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("weekstartdaycode")]
        public OptionSetValue? WeekStartDayCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("weekstartdaycode");
            }
            set
            {
                SetAttributeValue("weekstartdaycode", value);
            }
        }

        /// <summary>
		/// For Internal use only.
		/// </summary>
        [AttributeLogicalName("widgetproperties")]
        public string? WidgetProperties
        {
            get
            {
                return GetAttributeValue<string?>("widgetproperties");
            }
            set
            {
                SetAttributeValue("widgetproperties", value);
            }
        }

        /// <summary>
		/// Denotes the Yammer group ID
		/// </summary>
        [AttributeLogicalName("yammergroupid")]
        public int? YammerGroupId
        {
            get
            {
                return GetAttributeValue<int?>("yammergroupid");
            }
            set
            {
                SetAttributeValue("yammergroupid", value);
            }
        }

        /// <summary>
		/// Denotes the Yammer network permalink
		/// </summary>
        [AttributeLogicalName("yammernetworkpermalink")]
        public string? YammerNetworkPermalink
        {
            get
            {
                return GetAttributeValue<string?>("yammernetworkpermalink");
            }
            set
            {
                SetAttributeValue("yammernetworkpermalink", value);
            }
        }

        /// <summary>
		/// Denotes whether the OAuth access token for Yammer network has expired
		/// </summary>
        [AttributeLogicalName("yammeroauthaccesstokenexpired")]
        public bool? YammerOAuthAccessTokenExpired
        {
            get
            {
                return GetAttributeValue<bool?>("yammeroauthaccesstokenexpired");
            }
            set
            {
                SetAttributeValue("yammeroauthaccesstokenexpired", value);
            }
        }

        /// <summary>
		/// Internal Use Only
		/// </summary>
        [AttributeLogicalName("yammerpostmethod")]
        public OptionSetValue? YammerPostMethod
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("yammerpostmethod");
            }
            set
            {
                SetAttributeValue("yammerpostmethod", value);
            }
        }

        /// <summary>
		/// Information that specifies how the first week of the year is specified in Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("yearstartweekcode")]
        public int? YearStartWeekCode
        {
            get
            {
                return GetAttributeValue<int?>("yearstartweekcode");
            }
            set
            {
                SetAttributeValue("yearstartweekcode", value);
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
            get
            {
                return GetRelatedEntities<DocumentTemplate>("lk_documenttemplatebase_organization", null);
            }
            set
            {
                SetRelatedEntities("lk_documenttemplatebase_organization", null, value);
            }
        }

        /// <summary>
        /// 1:N Organization_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("Organization_AsyncOperations")]
        public IEnumerable<AsyncOperation> OrganizationAsyncOperations
        {
            get
            {
                return GetRelatedEntities<AsyncOperation>("Organization_AsyncOperations", null);
            }
            set
            {
                SetRelatedEntities("Organization_AsyncOperations", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_business_units
        /// </summary>
        [RelationshipSchemaName("organization_business_units")]
        public IEnumerable<BusinessUnit> OrganizationBusinessUnits
        {
            get
            {
                return GetRelatedEntities<BusinessUnit>("organization_business_units", null);
            }
            set
            {
                SetRelatedEntities("organization_business_units", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_calendars
        /// </summary>
        [RelationshipSchemaName("organization_calendars")]
        public IEnumerable<Calendar> OrganizationCalendars
        {
            get
            {
                return GetRelatedEntities<Calendar>("organization_calendars", null);
            }
            set
            {
                SetRelatedEntities("organization_calendars", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_entitydataprovider
        /// </summary>
        [RelationshipSchemaName("organization_entitydataprovider")]
        public IEnumerable<EntityDataProvider> OrganizationEntitydataprovider
        {
            get
            {
                return GetRelatedEntities<EntityDataProvider>("organization_entitydataprovider", null);
            }
            set
            {
                SetRelatedEntities("organization_entitydataprovider", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_importjob
        /// </summary>
        [RelationshipSchemaName("organization_importjob")]
        public IEnumerable<ImportJob> OrganizationImportjob
        {
            get
            {
                return GetRelatedEntities<ImportJob>("organization_importjob", null);
            }
            set
            {
                SetRelatedEntities("organization_importjob", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_pluginassembly
        /// </summary>
        [RelationshipSchemaName("organization_pluginassembly")]
        public IEnumerable<PluginAssembly> OrganizationPluginassembly
        {
            get
            {
                return GetRelatedEntities<PluginAssembly>("organization_pluginassembly", null);
            }
            set
            {
                SetRelatedEntities("organization_pluginassembly", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_pluginpackage
        /// </summary>
        [RelationshipSchemaName("organization_pluginpackage")]
        public IEnumerable<PluginPackage> OrganizationPluginpackage
        {
            get
            {
                return GetRelatedEntities<PluginPackage>("organization_pluginpackage", null);
            }
            set
            {
                SetRelatedEntities("organization_pluginpackage", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_plugintype
        /// </summary>
        [RelationshipSchemaName("organization_plugintype")]
        public IEnumerable<PluginType> OrganizationPlugintype
        {
            get
            {
                return GetRelatedEntities<PluginType>("organization_plugintype", null);
            }
            set
            {
                SetRelatedEntities("organization_plugintype", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_publisher
        /// </summary>
        [RelationshipSchemaName("organization_publisher")]
        public IEnumerable<Publisher> OrganizationPublisher
        {
            get
            {
                return GetRelatedEntities<Publisher>("organization_publisher", null);
            }
            set
            {
                SetRelatedEntities("organization_publisher", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_queues
        /// </summary>
        [RelationshipSchemaName("organization_queues")]
        public IEnumerable<Queue> OrganizationQueues
        {
            get
            {
                return GetRelatedEntities<Queue>("organization_queues", null);
            }
            set
            {
                SetRelatedEntities("organization_queues", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_roles
        /// </summary>
        [RelationshipSchemaName("organization_roles")]
        public IEnumerable<Role> OrganizationRoles
        {
            get
            {
                return GetRelatedEntities<Role>("organization_roles", null);
            }
            set
            {
                SetRelatedEntities("organization_roles", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_routingruleitems
        /// </summary>
        [RelationshipSchemaName("organization_routingruleitems")]
        public IEnumerable<RoutingRuleItem> OrganizationRoutingruleitems
        {
            get
            {
                return GetRelatedEntities<RoutingRuleItem>("organization_routingruleitems", null);
            }
            set
            {
                SetRelatedEntities("organization_routingruleitems", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_RoutingRules
        /// </summary>
        [RelationshipSchemaName("organization_RoutingRules")]
        public IEnumerable<RoutingRule> OrganizationRoutingRules
        {
            get
            {
                return GetRelatedEntities<RoutingRule>("organization_RoutingRules", null);
            }
            set
            {
                SetRelatedEntities("organization_RoutingRules", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_saved_queries
        /// </summary>
        [RelationshipSchemaName("organization_saved_queries")]
        public IEnumerable<SavedQuery> OrganizationSavedQueries
        {
            get
            {
                return GetRelatedEntities<SavedQuery>("organization_saved_queries", null);
            }
            set
            {
                SetRelatedEntities("organization_saved_queries", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_sdkmessage
        /// </summary>
        [RelationshipSchemaName("organization_sdkmessage")]
        public IEnumerable<SdkMessage> OrganizationSdkmessage
        {
            get
            {
                return GetRelatedEntities<SdkMessage>("organization_sdkmessage", null);
            }
            set
            {
                SetRelatedEntities("organization_sdkmessage", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_sdkmessagefilter
        /// </summary>
        [RelationshipSchemaName("organization_sdkmessagefilter")]
        public IEnumerable<SdkMessageFilter> OrganizationSdkmessagefilter
        {
            get
            {
                return GetRelatedEntities<SdkMessageFilter>("organization_sdkmessagefilter", null);
            }
            set
            {
                SetRelatedEntities("organization_sdkmessagefilter", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_sdkmessageprocessingstep
        /// </summary>
        [RelationshipSchemaName("organization_sdkmessageprocessingstep")]
        public IEnumerable<SdkMessageProcessingStep> OrganizationSdkmessageprocessingstep
        {
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStep>("organization_sdkmessageprocessingstep", null);
            }
            set
            {
                SetRelatedEntities("organization_sdkmessageprocessingstep", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_sdkmessageprocessingstepimage
        /// </summary>
        [RelationshipSchemaName("organization_sdkmessageprocessingstepimage")]
        public IEnumerable<SdkMessageProcessingStepImage> OrganizationSdkmessageprocessingstepimage
        {
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStepImage>("organization_sdkmessageprocessingstepimage", null);
            }
            set
            {
                SetRelatedEntities("organization_sdkmessageprocessingstepimage", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_sdkmessageprocessingstepsecureconfig
        /// </summary>
        [RelationshipSchemaName("organization_sdkmessageprocessingstepsecureconfig")]
        public IEnumerable<SdkMessageProcessingStepSecureConfig> OrganizationSdkmessageprocessingstepsecureconfig
        {
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStepSecureConfig>("organization_sdkmessageprocessingstepsecureconfig", null);
            }
            set
            {
                SetRelatedEntities("organization_sdkmessageprocessingstepsecureconfig", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_solution
        /// </summary>
        [RelationshipSchemaName("organization_solution")]
        public IEnumerable<Solution> OrganizationSolution
        {
            get
            {
                return GetRelatedEntities<Solution>("organization_solution", null);
            }
            set
            {
                SetRelatedEntities("organization_solution", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_system_users
        /// </summary>
        [RelationshipSchemaName("organization_system_users")]
        public IEnumerable<SystemUser> OrganizationSystemUsers
        {
            get
            {
                return GetRelatedEntities<SystemUser>("organization_system_users", null);
            }
            set
            {
                SetRelatedEntities("organization_system_users", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_systemforms
        /// </summary>
        [RelationshipSchemaName("organization_systemforms")]
        public IEnumerable<SystemForm> OrganizationSystemforms
        {
            get
            {
                return GetRelatedEntities<SystemForm>("organization_systemforms", null);
            }
            set
            {
                SetRelatedEntities("organization_systemforms", null, value);
            }
        }

        /// <summary>
        /// 1:N organization_teams
        /// </summary>
        [RelationshipSchemaName("organization_teams")]
        public IEnumerable<Team> OrganizationTeams
        {
            get
            {
                return GetRelatedEntities<Team>("organization_teams", null);
            }
            set
            {
                SetRelatedEntities("organization_teams", null, value);
            }
        }

        /// <summary>
        /// 1:N webresource_organization
        /// </summary>
        [RelationshipSchemaName("webresource_organization")]
        public IEnumerable<WebResource> WebresourceOrganization
        {
            get
            {
                return GetRelatedEntities<WebResource>("webresource_organization", null);
            }
            set
            {
                SetRelatedEntities("webresource_organization", null, value);
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
            public struct DesktopFlowRunActionLogsCustomUrlEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
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
            public struct EnableEmailMention
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
            public struct IsDesktopFlowRemoteMonitoringControlEnabled
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
            public struct PowerBIAllowCrossRegionOperations
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct PowerBIAutomaticPermissionsAssignment
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct PowerBIComponentsCreate
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
            public const string DesktopFlowRunActionLogsCustomUrl = "desktopflowrunactionlogscustomurl";
            public const string DesktopFlowRunActionLogsCustomUrlEnabled = "desktopflowrunactionlogscustomurlenabled";
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
            public const string EnableEmailMention = "enableemailmention";
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
            public const string IsDesktopFlowRemoteMonitoringControlEnabled = "isdesktopflowremotemonitoringcontrolenabled";
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
            public const string PowerBIAllowCrossRegionOperations = "powerbiallowcrossregionoperations";
            public const string PowerBIAutomaticPermissionsAssignment = "powerbiautomaticpermissionsassignment";
            public const string PowerBIComponentsCreate = "powerbicomponentscreate";
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
                public const string OrganizationControlconfiguration = "organization_controlconfiguration";
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
                public const string OrganizationEntitystorageprofile = "organization_entitystorageprofile";
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
                public const string OrganizationMobileofflineprofilesuggestion = "organization_mobileofflineprofilesuggestion";
                public const string OrganizationMobileofflineprofilesuggestionimpactedtable = "organization_mobileofflineprofilesuggestionimpactedtable";
                public const string OrganizationMos3management = "organization_mos3management";
                public const string OrganizationMsdynAppinsightsmetadata = "organization_msdyn_appinsightsmetadata";
                public const string OrganizationMsdynEvalassertion = "organization_msdyn_evalassertion";
                public const string OrganizationMsdynEvaldataset = "organization_msdyn_evaldataset";
                public const string OrganizationMsdynEvalprompt = "organization_msdyn_evalprompt";
                public const string OrganizationMsdynEvalresult = "organization_msdyn_evalresult";
                public const string OrganizationMsdynEvalrun = "organization_msdyn_evalrun";
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
                public const string OrganizationPolicycriterion = "organization_policycriterion";
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
                public const string OrganizationSkillchangesetreviewer = "organization_skillchangesetreviewer";
                public const string OrganizationSkillrolemapping = "organization_skillrolemapping";
                public const string OrganizationSocialinsightsconfiguration = "organization_socialinsightsconfiguration";
                public const string OrganizationSolution = "organization_solution";
                public const string OrganizationSolutioncomponentattributeconfiguration = "organization_solutioncomponentattributeconfiguration";
                public const string OrganizationSolutioncomponentconfiguration = "organization_solutioncomponentconfiguration";
                public const string OrganizationSolutioncomponentrelationshipconfiguration = "organization_solutioncomponentrelationshipconfiguration";
                public const string OrganizationSourcecontroloperationtracking = "organization_sourcecontroloperationtracking";
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
