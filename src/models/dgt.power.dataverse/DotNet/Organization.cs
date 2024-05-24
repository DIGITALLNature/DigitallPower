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
	/// <summary>
	/// Top level of the Microsoft Dynamics 365 business hierarchy. The organization can be a specific business, holding company, or corporation.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("organization")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
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
		public Organization(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Organization(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
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
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "organization";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 1019;
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
		[AttributeLogicalNameAttribute("organizationid")]
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
                OnPropertyChanging(nameof(ACIWebEndpointUrl));
                SetAttributeValue("aciwebendpointurl", value);
                OnPropertyChanged(nameof(ACIWebEndpointUrl));
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
                OnPropertyChanging(nameof(AcknowledgementTemplateId));
                SetAttributeValue("acknowledgementtemplateid", value);
                OnPropertyChanged(nameof(AcknowledgementTemplateId));
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
                OnPropertyChanging(nameof(ActivityTypeFilter));
                SetAttributeValue("activitytypefilter", value);
                OnPropertyChanged(nameof(ActivityTypeFilter));
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
                OnPropertyChanging(nameof(ActivityTypeFilterV2));
                SetAttributeValue("activitytypefilterv2", value);
                OnPropertyChanged(nameof(ActivityTypeFilterV2));
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
                OnPropertyChanging(nameof(AdvancedColumnEditorEnabled));
                SetAttributeValue("advancedcolumneditorenabled", value);
                OnPropertyChanged(nameof(AdvancedColumnEditorEnabled));
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
                OnPropertyChanging(nameof(AdvancedColumnFilteringEnabled));
                SetAttributeValue("advancedcolumnfilteringenabled", value);
                OnPropertyChanged(nameof(AdvancedColumnFilteringEnabled));
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
                OnPropertyChanging(nameof(AdvancedFilteringEnabled));
                SetAttributeValue("advancedfilteringenabled", value);
                OnPropertyChanged(nameof(AdvancedFilteringEnabled));
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
                OnPropertyChanging(nameof(AdvancedLookupEnabled));
                SetAttributeValue("advancedlookupenabled", value);
                OnPropertyChanged(nameof(AdvancedLookupEnabled));
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
                OnPropertyChanging(nameof(AdvancedLookupInEditFilter));
                SetAttributeValue("advancedlookupineditfilter", value);
                OnPropertyChanged(nameof(AdvancedLookupInEditFilter));
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
                OnPropertyChanging(nameof(AllowAddressBookSyncs));
                SetAttributeValue("allowaddressbooksyncs", value);
                OnPropertyChanged(nameof(AllowAddressBookSyncs));
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
                OnPropertyChanging(nameof(AllowApplicationUserAccess));
                SetAttributeValue("allowapplicationuseraccess", value);
                OnPropertyChanged(nameof(AllowApplicationUserAccess));
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
                OnPropertyChanging(nameof(AllowAutoResponseCreation));
                SetAttributeValue("allowautoresponsecreation", value);
                OnPropertyChanged(nameof(AllowAutoResponseCreation));
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
                OnPropertyChanging(nameof(AllowAutoUnsubscribe));
                SetAttributeValue("allowautounsubscribe", value);
                OnPropertyChanged(nameof(AllowAutoUnsubscribe));
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
                OnPropertyChanging(nameof(AllowAutoUnsubscribeAcknowledgement));
                SetAttributeValue("allowautounsubscribeacknowledgement", value);
                OnPropertyChanged(nameof(AllowAutoUnsubscribeAcknowledgement));
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
                OnPropertyChanging(nameof(AllowClientMessageBarAd));
                SetAttributeValue("allowclientmessagebarad", value);
                OnPropertyChanged(nameof(AllowClientMessageBarAd));
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
                OnPropertyChanging(nameof(AllowedIpRangeForFirewall));
                SetAttributeValue("allowediprangeforfirewall", value);
                OnPropertyChanged(nameof(AllowedIpRangeForFirewall));
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
                OnPropertyChanging(nameof(AllowedIpRangeForStorageAccessSignatures));
                SetAttributeValue("allowediprangeforstorageaccesssignatures", value);
                OnPropertyChanged(nameof(AllowedIpRangeForStorageAccessSignatures));
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
                OnPropertyChanging(nameof(AllowedMimeTypes));
                SetAttributeValue("allowedmimetypes", value);
                OnPropertyChanged(nameof(AllowedMimeTypes));
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
                OnPropertyChanging(nameof(AllowEntityOnlyAudit));
                SetAttributeValue("allowentityonlyaudit", value);
                OnPropertyChanged(nameof(AllowEntityOnlyAudit));
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
                OnPropertyChanging(nameof(AllowLeadingWildcardsInGridSearch));
                SetAttributeValue("allowleadingwildcardsingridsearch", value);
                OnPropertyChanged(nameof(AllowLeadingWildcardsInGridSearch));
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
                OnPropertyChanging(nameof(AllowLeadingWildcardsInQuickFind));
                SetAttributeValue("allowleadingwildcardsinquickfind", value);
                OnPropertyChanged(nameof(AllowLeadingWildcardsInQuickFind));
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
                OnPropertyChanging(nameof(AllowLegacyClientExperience));
                SetAttributeValue("allowlegacyclientexperience", value);
                OnPropertyChanged(nameof(AllowLegacyClientExperience));
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
                OnPropertyChanging(nameof(AllowLegacyDialogsEmbedding));
                SetAttributeValue("allowlegacydialogsembedding", value);
                OnPropertyChanged(nameof(AllowLegacyDialogsEmbedding));
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
                OnPropertyChanging(nameof(AllowMarketingEmailExecution));
                SetAttributeValue("allowmarketingemailexecution", value);
                OnPropertyChanged(nameof(AllowMarketingEmailExecution));
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
                OnPropertyChanging(nameof(AllowMicrosoftTrustedServiceTags));
                SetAttributeValue("allowmicrosofttrustedservicetags", value);
                OnPropertyChanged(nameof(AllowMicrosoftTrustedServiceTags));
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
                OnPropertyChanging(nameof(AllowOfflineScheduledSyncs));
                SetAttributeValue("allowofflinescheduledsyncs", value);
                OnPropertyChanged(nameof(AllowOfflineScheduledSyncs));
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
                OnPropertyChanging(nameof(AllowOutlookScheduledSyncs));
                SetAttributeValue("allowoutlookscheduledsyncs", value);
                OnPropertyChanged(nameof(AllowOutlookScheduledSyncs));
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
                OnPropertyChanging(nameof(AllowRedirectAdminSettingsToModernUI));
                SetAttributeValue("allowredirectadminsettingstomodernui", value);
                OnPropertyChanged(nameof(AllowRedirectAdminSettingsToModernUI));
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
                OnPropertyChanging(nameof(AllowUnresolvedPartiesOnEmailSend));
                SetAttributeValue("allowunresolvedpartiesonemailsend", value);
                OnPropertyChanged(nameof(AllowUnresolvedPartiesOnEmailSend));
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
                OnPropertyChanging(nameof(AllowUserFormModePreference));
                SetAttributeValue("allowuserformmodepreference", value);
                OnPropertyChanged(nameof(AllowUserFormModePreference));
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
                OnPropertyChanging(nameof(AllowUsersHidingSystemViews));
                SetAttributeValue("allowusershidingsystemviews", value);
                OnPropertyChanged(nameof(AllowUsersHidingSystemViews));
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
                OnPropertyChanging(nameof(AllowUsersSeeAppdownloadMessage));
                SetAttributeValue("allowusersseeappdownloadmessage", value);
                OnPropertyChanged(nameof(AllowUsersSeeAppdownloadMessage));
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
                OnPropertyChanging(nameof(AllowWebExcelExport));
                SetAttributeValue("allowwebexcelexport", value);
                OnPropertyChanged(nameof(AllowWebExcelExport));
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
                OnPropertyChanging(nameof(AMDesignator));
                SetAttributeValue("amdesignator", value);
                OnPropertyChanged(nameof(AMDesignator));
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
                OnPropertyChanging(nameof(AppDesignerExperienceEnabled));
                SetAttributeValue("appdesignerexperienceenabled", value);
                OnPropertyChanged(nameof(AppDesignerExperienceEnabled));
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
                OnPropertyChanging(nameof(AppointmentRichEditorExperience));
                SetAttributeValue("appointmentricheditorexperience", value);
                OnPropertyChanged(nameof(AppointmentRichEditorExperience));
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
                OnPropertyChanging(nameof(AppointmentWithTeamsMeeting));
                SetAttributeValue("appointmentwithteamsmeeting", value);
                OnPropertyChanged(nameof(AppointmentWithTeamsMeeting));
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
                OnPropertyChanging(nameof(AppointmentWithTeamsMeetingV2));
                SetAttributeValue("appointmentwithteamsmeetingv2", value);
                OnPropertyChanged(nameof(AppointmentWithTeamsMeetingV2));
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
                OnPropertyChanging(nameof(AuditRetentionPeriod));
                SetAttributeValue("auditretentionperiod", value);
                OnPropertyChanged(nameof(AuditRetentionPeriod));
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
                OnPropertyChanging(nameof(AuditRetentionPeriodV2));
                SetAttributeValue("auditretentionperiodv2", value);
                OnPropertyChanged(nameof(AuditRetentionPeriodV2));
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
                OnPropertyChanging(nameof(AutoApplyDefaultonCaseCreate));
                SetAttributeValue("autoapplydefaultoncasecreate", value);
                OnPropertyChanged(nameof(AutoApplyDefaultonCaseCreate));
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
                OnPropertyChanging(nameof(AutoApplyDefaultonCaseUpdate));
                SetAttributeValue("autoapplydefaultoncaseupdate", value);
                OnPropertyChanged(nameof(AutoApplyDefaultonCaseUpdate));
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
                OnPropertyChanging(nameof(AutoApplySLA));
                SetAttributeValue("autoapplysla", value);
                OnPropertyChanged(nameof(AutoApplySLA));
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
                OnPropertyChanging(nameof(AzureSchedulerJobCollectionName));
                SetAttributeValue("azureschedulerjobcollectionname", value);
                OnPropertyChanged(nameof(AzureSchedulerJobCollectionName));
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
                OnPropertyChanging(nameof(BaseCurrencyId));
                SetAttributeValue("basecurrencyid", value);
                OnPropertyChanged(nameof(BaseCurrencyId));
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
                OnPropertyChanging(nameof(BingMapsApiKey));
                SetAttributeValue("bingmapsapikey", value);
                OnPropertyChanged(nameof(BingMapsApiKey));
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
                OnPropertyChanging(nameof(BlockedAttachments));
                SetAttributeValue("blockedattachments", value);
                OnPropertyChanged(nameof(BlockedAttachments));
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
                OnPropertyChanging(nameof(BlockedMimeTypes));
                SetAttributeValue("blockedmimetypes", value);
                OnPropertyChanged(nameof(BlockedMimeTypes));
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
                OnPropertyChanging(nameof(BoundDashboardDefaultCardExpanded));
                SetAttributeValue("bounddashboarddefaultcardexpanded", value);
                OnPropertyChanged(nameof(BoundDashboardDefaultCardExpanded));
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
                OnPropertyChanging(nameof(BulkOperationPrefix));
                SetAttributeValue("bulkoperationprefix", value);
                OnPropertyChanged(nameof(BulkOperationPrefix));
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
                OnPropertyChanging(nameof(BusinessCardOptions));
                SetAttributeValue("businesscardoptions", value);
                OnPropertyChanged(nameof(BusinessCardOptions));
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
                OnPropertyChanging(nameof(BusinessClosureCalendarId));
                SetAttributeValue("businessclosurecalendarid", value);
                OnPropertyChanged(nameof(BusinessClosureCalendarId));
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
                OnPropertyChanging(nameof(CalendarType));
                SetAttributeValue("calendartype", value);
                OnPropertyChanged(nameof(CalendarType));
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
                OnPropertyChanging(nameof(CampaignPrefix));
                SetAttributeValue("campaignprefix", value);
                OnPropertyChanged(nameof(CampaignPrefix));
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
                OnPropertyChanging(nameof(CanOptOutNewSearchExperience));
                SetAttributeValue("canoptoutnewsearchexperience", value);
                OnPropertyChanged(nameof(CanOptOutNewSearchExperience));
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
                OnPropertyChanging(nameof(CascadeStatusUpdate));
                SetAttributeValue("cascadestatusupdate", value);
                OnPropertyChanged(nameof(CascadeStatusUpdate));
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
                OnPropertyChanging(nameof(CasePrefix));
                SetAttributeValue("caseprefix", value);
                OnPropertyChanged(nameof(CasePrefix));
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
                OnPropertyChanging(nameof(CategoryPrefix));
                SetAttributeValue("categoryprefix", value);
                OnPropertyChanged(nameof(CategoryPrefix));
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
                OnPropertyChanging(nameof(ClientFeatureSet));
                SetAttributeValue("clientfeatureset", value);
                OnPropertyChanged(nameof(ClientFeatureSet));
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
                OnPropertyChanging(nameof(ContentSecurityPolicyConfiguration));
                SetAttributeValue("contentsecuritypolicyconfiguration", value);
                OnPropertyChanged(nameof(ContentSecurityPolicyConfiguration));
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
                OnPropertyChanging(nameof(ContentSecurityPolicyConfigurationForCanvas));
                SetAttributeValue("contentsecuritypolicyconfigurationforcanvas", value);
                OnPropertyChanged(nameof(ContentSecurityPolicyConfigurationForCanvas));
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
                OnPropertyChanging(nameof(ContentSecurityPolicyReportUri));
                SetAttributeValue("contentsecuritypolicyreporturi", value);
                OnPropertyChanged(nameof(ContentSecurityPolicyReportUri));
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
                OnPropertyChanging(nameof(ContractPrefix));
                SetAttributeValue("contractprefix", value);
                OnPropertyChanged(nameof(ContractPrefix));
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
                OnPropertyChanging(nameof(CopresenceRefreshRate));
                SetAttributeValue("copresencerefreshrate", value);
                OnPropertyChanged(nameof(CopresenceRefreshRate));
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
                OnPropertyChanging(nameof(CortanaProactiveExperienceEnabled));
                SetAttributeValue("cortanaproactiveexperienceenabled", value);
                OnPropertyChanged(nameof(CortanaProactiveExperienceEnabled));
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
                OnPropertyChanging(nameof(CreateProductsWithoutParentInActiveState));
                SetAttributeValue("createproductswithoutparentinactivestate", value);
                OnPropertyChanged(nameof(CreateProductsWithoutParentInActiveState));
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
                OnPropertyChanging(nameof(CurrencyDecimalPrecision));
                SetAttributeValue("currencydecimalprecision", value);
                OnPropertyChanged(nameof(CurrencyDecimalPrecision));
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
                OnPropertyChanging(nameof(CurrencyDisplayOption));
                SetAttributeValue("currencydisplayoption", value);
                OnPropertyChanged(nameof(CurrencyDisplayOption));
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
                OnPropertyChanging(nameof(CurrencyFormatCode));
                SetAttributeValue("currencyformatcode", value);
                OnPropertyChanged(nameof(CurrencyFormatCode));
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
                OnPropertyChanging(nameof(CurrencySymbol));
                SetAttributeValue("currencysymbol", value);
                OnPropertyChanged(nameof(CurrencySymbol));
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
                OnPropertyChanging(nameof(CurrentBulkOperationNumber));
                SetAttributeValue("currentbulkoperationnumber", value);
                OnPropertyChanged(nameof(CurrentBulkOperationNumber));
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
                OnPropertyChanging(nameof(CurrentCampaignNumber));
                SetAttributeValue("currentcampaignnumber", value);
                OnPropertyChanged(nameof(CurrentCampaignNumber));
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
                OnPropertyChanging(nameof(CurrentCaseNumber));
                SetAttributeValue("currentcasenumber", value);
                OnPropertyChanged(nameof(CurrentCaseNumber));
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
                OnPropertyChanging(nameof(CurrentCategoryNumber));
                SetAttributeValue("currentcategorynumber", value);
                OnPropertyChanged(nameof(CurrentCategoryNumber));
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
                OnPropertyChanging(nameof(CurrentContractNumber));
                SetAttributeValue("currentcontractnumber", value);
                OnPropertyChanged(nameof(CurrentContractNumber));
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
                OnPropertyChanging(nameof(CurrentInvoiceNumber));
                SetAttributeValue("currentinvoicenumber", value);
                OnPropertyChanged(nameof(CurrentInvoiceNumber));
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
                OnPropertyChanging(nameof(CurrentKaNumber));
                SetAttributeValue("currentkanumber", value);
                OnPropertyChanged(nameof(CurrentKaNumber));
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
                OnPropertyChanging(nameof(CurrentKbNumber));
                SetAttributeValue("currentkbnumber", value);
                OnPropertyChanged(nameof(CurrentKbNumber));
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
                OnPropertyChanging(nameof(CurrentOrderNumber));
                SetAttributeValue("currentordernumber", value);
                OnPropertyChanged(nameof(CurrentOrderNumber));
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
                OnPropertyChanging(nameof(CurrentQuoteNumber));
                SetAttributeValue("currentquotenumber", value);
                OnPropertyChanged(nameof(CurrentQuoteNumber));
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
                OnPropertyChanging(nameof(DateFormatCode));
                SetAttributeValue("dateformatcode", value);
                OnPropertyChanged(nameof(DateFormatCode));
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
                OnPropertyChanging(nameof(DateFormatString));
                SetAttributeValue("dateformatstring", value);
                OnPropertyChanged(nameof(DateFormatString));
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
                OnPropertyChanging(nameof(DateSeparator));
                SetAttributeValue("dateseparator", value);
                OnPropertyChanged(nameof(DateSeparator));
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
                OnPropertyChanging(nameof(DaysBeforeInactiveTeamsChatSyncDisabled));
                SetAttributeValue("daysbeforeinactiveteamschatsyncdisabled", value);
                OnPropertyChanged(nameof(DaysBeforeInactiveTeamsChatSyncDisabled));
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
                OnPropertyChanging(nameof(DecimalSymbol));
                SetAttributeValue("decimalsymbol", value);
                OnPropertyChanged(nameof(DecimalSymbol));
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
                OnPropertyChanging(nameof(DefaultCountryCode));
                SetAttributeValue("defaultcountrycode", value);
                OnPropertyChanged(nameof(DefaultCountryCode));
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
                OnPropertyChanging(nameof(DefaultCrmCustomName));
                SetAttributeValue("defaultcrmcustomname", value);
                OnPropertyChanged(nameof(DefaultCrmCustomName));
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
                OnPropertyChanging(nameof(DefaultEmailServerProfileId));
                SetAttributeValue("defaultemailserverprofileid", value);
                OnPropertyChanged(nameof(DefaultEmailServerProfileId));
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
                OnPropertyChanging(nameof(DefaultEmailSettings));
                SetAttributeValue("defaultemailsettings", value);
                OnPropertyChanged(nameof(DefaultEmailSettings));
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
                OnPropertyChanging(nameof(DefaultMobileOfflineProfileId));
                SetAttributeValue("defaultmobileofflineprofileid", value);
                OnPropertyChanged(nameof(DefaultMobileOfflineProfileId));
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
                OnPropertyChanging(nameof(DefaultRecurrenceEndRangeType));
                SetAttributeValue("defaultrecurrenceendrangetype", value);
                OnPropertyChanged(nameof(DefaultRecurrenceEndRangeType));
            }
        }

		
		[AttributeLogicalName("defaultrecurrenceendrangetypename")]
        public string? DefaultRecurrenceEndRangeTypeName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("defaultrecurrenceendrangetypename");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DefaultRecurrenceEndRangeTypeName));
                SetAttributeValue("defaultrecurrenceendrangetypename", value);
                OnPropertyChanged(nameof(DefaultRecurrenceEndRangeTypeName));
            }
        }

		/// <summary>
		/// Indicates whether the default teams linked chat title is the record name
		/// </summary>
		[AttributeLogicalName("defaultteamschattitlerecordname")]
        public bool? DefaultTeamsChatTitleRecordName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("defaultteamschattitlerecordname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DefaultTeamsChatTitleRecordName));
                SetAttributeValue("defaultteamschattitlerecordname", value);
                OnPropertyChanged(nameof(DefaultTeamsChatTitleRecordName));
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
                OnPropertyChanging(nameof(DefaultThemeData));
                SetAttributeValue("defaultthemedata", value);
                OnPropertyChanged(nameof(DefaultThemeData));
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
                OnPropertyChanging(nameof(DelegatedAdminUserId));
                SetAttributeValue("delegatedadminuserid", value);
                OnPropertyChanged(nameof(DelegatedAdminUserId));
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
                OnPropertyChanging(nameof(DisableSocialCare));
                SetAttributeValue("disablesocialcare", value);
                OnPropertyChanged(nameof(DisableSocialCare));
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
                OnPropertyChanging(nameof(DiscountCalculationMethod));
                SetAttributeValue("discountcalculationmethod", value);
                OnPropertyChanged(nameof(DiscountCalculationMethod));
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
                OnPropertyChanging(nameof(DisplayNavigationTour));
                SetAttributeValue("displaynavigationtour", value);
                OnPropertyChanged(nameof(DisplayNavigationTour));
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
                OnPropertyChanging(nameof(EmailConnectionChannel));
                SetAttributeValue("emailconnectionchannel", value);
                OnPropertyChanged(nameof(EmailConnectionChannel));
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
                OnPropertyChanging(nameof(EmailCorrelationEnabled));
                SetAttributeValue("emailcorrelationenabled", value);
                OnPropertyChanged(nameof(EmailCorrelationEnabled));
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
                OnPropertyChanging(nameof(EmailSendPollingPeriod));
                SetAttributeValue("emailsendpollingperiod", value);
                OnPropertyChanged(nameof(EmailSendPollingPeriod));
            }
        }

		/// <summary>
		/// Indicates the selected default view in the enhanced insert e-mail template experience..
		/// </summary>
		[AttributeLogicalName("emailtemplatedefaultview")]
        public OptionSetValue? EmailTemplateDefaultView
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("emailtemplatedefaultview");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(EmailTemplateDefaultView));
                SetAttributeValue("emailtemplatedefaultview", value);
                OnPropertyChanged(nameof(EmailTemplateDefaultView));
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
                OnPropertyChanging(nameof(EnableAsyncMergeAPIForUCI));
                SetAttributeValue("enableasyncmergeapiforuci", value);
                OnPropertyChanged(nameof(EnableAsyncMergeAPIForUCI));
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
                OnPropertyChanging(nameof(EnableBingMapsIntegration));
                SetAttributeValue("enablebingmapsintegration", value);
                OnPropertyChanged(nameof(EnableBingMapsIntegration));
            }
        }

		/// <summary>
		/// Indicates whether to Allow calendar export import with SLA.
		/// </summary>
		[AttributeLogicalName("enablecalendarimportexport")]
        public bool? EnableCalendarImportExport
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("enablecalendarimportexport");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(EnableCalendarImportExport));
                SetAttributeValue("enablecalendarimportexport", value);
                OnPropertyChanged(nameof(EnableCalendarImportExport));
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
                OnPropertyChanging(nameof(EnableCanvasAppsInSolutionsByDefault));
                SetAttributeValue("enablecanvasappsinsolutionsbydefault", value);
                OnPropertyChanged(nameof(EnableCanvasAppsInSolutionsByDefault));
            }
        }

		/// <summary>
		/// Indicates whether to Allow email template views in Enhanced Email Template.
		/// </summary>
		[AttributeLogicalName("enableemailtemplateviews")]
        public bool? EnableEmailTemplateViews
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("enableemailtemplateviews");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(EnableEmailTemplateViews));
                SetAttributeValue("enableemailtemplateviews", value);
                OnPropertyChanged(nameof(EnableEmailTemplateViews));
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
                OnPropertyChanging(nameof(EnableFlowsInSolutionByDefault));
                SetAttributeValue("enableflowsinsolutionbydefault", value);
                OnPropertyChanged(nameof(EnableFlowsInSolutionByDefault));
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
                OnPropertyChanging(nameof(EnableImmersiveSkypeIntegration));
                SetAttributeValue("enableimmersiveskypeintegration", value);
                OnPropertyChanged(nameof(EnableImmersiveSkypeIntegration));
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
                OnPropertyChanging(nameof(EnableIpBasedCookieBinding));
                SetAttributeValue("enableipbasedcookiebinding", value);
                OnPropertyChanged(nameof(EnableIpBasedCookieBinding));
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
                OnPropertyChanging(nameof(EnableIpBasedFirewallRule));
                SetAttributeValue("enableipbasedfirewallrule", value);
                OnPropertyChanged(nameof(EnableIpBasedFirewallRule));
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
                OnPropertyChanging(nameof(EnableIpBasedStorageAccessSignatureRule));
                SetAttributeValue("enableipbasedstorageaccesssignaturerule", value);
                OnPropertyChanged(nameof(EnableIpBasedStorageAccessSignatureRule));
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
                OnPropertyChanging(nameof(EnableLivePersonaCardUCI));
                SetAttributeValue("enablelivepersonacarduci", value);
                OnPropertyChanged(nameof(EnableLivePersonaCardUCI));
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
                OnPropertyChanging(nameof(EnableLivePersonCardIntegrationInOffice));
                SetAttributeValue("enablelivepersoncardintegrationinoffice", value);
                OnPropertyChanged(nameof(EnableLivePersonCardIntegrationInOffice));
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
                OnPropertyChanging(nameof(EnableLPAuthoring));
                SetAttributeValue("enablelpauthoring", value);
                OnPropertyChanged(nameof(EnableLPAuthoring));
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
                OnPropertyChanging(nameof(EnableMakerSwitchToClassic));
                SetAttributeValue("enablemakerswitchtoclassic", value);
                OnPropertyChanged(nameof(EnableMakerSwitchToClassic));
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
                OnPropertyChanging(nameof(EnableMicrosoftFlowIntegration));
                SetAttributeValue("enablemicrosoftflowintegration", value);
                OnPropertyChanged(nameof(EnableMicrosoftFlowIntegration));
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
                OnPropertyChanging(nameof(EnablePricingOnCreate));
                SetAttributeValue("enablepricingoncreate", value);
                OnPropertyChanged(nameof(EnablePricingOnCreate));
            }
        }

		/// <summary>
		/// Indicates whether privacy and sensitivity attributes for new team creation has been enabled
		/// </summary>
		[AttributeLogicalName("enablesensitivitylabelsforteamscollab")]
        public bool? EnableSensitivityLabelsForTeamsCollab
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("enablesensitivitylabelsforteamscollab");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(EnableSensitivityLabelsForTeamsCollab));
                SetAttributeValue("enablesensitivitylabelsforteamscollab", value);
                OnPropertyChanged(nameof(EnableSensitivityLabelsForTeamsCollab));
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
                OnPropertyChanging(nameof(EnableSmartMatching));
                SetAttributeValue("enablesmartmatching", value);
                OnPropertyChanged(nameof(EnableSmartMatching));
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
                OnPropertyChanging(nameof(EnableUnifiedClientCDN));
                SetAttributeValue("enableunifiedclientcdn", value);
                OnPropertyChanged(nameof(EnableUnifiedClientCDN));
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
                OnPropertyChanging(nameof(EnableUnifiedInterfaceShellRefresh));
                SetAttributeValue("enableunifiedinterfaceshellrefresh", value);
                OnPropertyChanged(nameof(EnableUnifiedInterfaceShellRefresh));
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
                OnPropertyChanging(nameof(EnforceReadOnlyPlugins));
                SetAttributeValue("enforcereadonlyplugins", value);
                OnPropertyChanged(nameof(EnforceReadOnlyPlugins));
            }
        }

		/// <summary>
		/// Indicates whether validation enforcement has been enabled for this organization's apps.
		/// </summary>
		[AttributeLogicalName("enforcevalidations")]
        public bool? EnforceValidations
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("enforcevalidations");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(EnforceValidations));
                SetAttributeValue("enforcevalidations", value);
                OnPropertyChanged(nameof(EnforceValidations));
            }
        }

		/// <summary>
		/// JSON string containing settings for enhanced add products experience in Sales
		/// </summary>
		[AttributeLogicalName("enhancedoqoiaddproductssettings")]
        public string? EnhancedOQOIAddProductsSettings
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("enhancedoqoiaddproductssettings");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(EnhancedOQOIAddProductsSettings));
                SetAttributeValue("enhancedoqoiaddproductssettings", value);
                OnPropertyChanged(nameof(EnhancedOQOIAddProductsSettings));
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
                OnPropertyChanging(nameof(EntityImage));
                SetAttributeValue("entityimage", value);
                OnPropertyChanged(nameof(EntityImage));
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
                OnPropertyChanging(nameof(ExpireChangeTrackingInDays));
                SetAttributeValue("expirechangetrackingindays", value);
                OnPropertyChanged(nameof(ExpireChangeTrackingInDays));
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
                OnPropertyChanging(nameof(ExpireSubscriptionsInDays));
                SetAttributeValue("expiresubscriptionsindays", value);
                OnPropertyChanged(nameof(ExpireSubscriptionsInDays));
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
                OnPropertyChanging(nameof(ExternalBaseUrl));
                SetAttributeValue("externalbaseurl", value);
                OnPropertyChanged(nameof(ExternalBaseUrl));
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
                OnPropertyChanging(nameof(ExternalPartyCorrelationKeys));
                SetAttributeValue("externalpartycorrelationkeys", value);
                OnPropertyChanged(nameof(ExternalPartyCorrelationKeys));
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
                OnPropertyChanging(nameof(ExternalPartyEntitySettings));
                SetAttributeValue("externalpartyentitysettings", value);
                OnPropertyChanged(nameof(ExternalPartyEntitySettings));
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
                OnPropertyChanging(nameof(FeatureSet));
                SetAttributeValue("featureset", value);
                OnPropertyChanged(nameof(FeatureSet));
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
                OnPropertyChanging(nameof(FiscalCalendarStart));
                SetAttributeValue("fiscalcalendarstart", value);
                OnPropertyChanged(nameof(FiscalCalendarStart));
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
                OnPropertyChanging(nameof(FiscalPeriodFormat));
                SetAttributeValue("fiscalperiodformat", value);
                OnPropertyChanged(nameof(FiscalPeriodFormat));
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
                OnPropertyChanging(nameof(FiscalPeriodFormatPeriod));
                SetAttributeValue("fiscalperiodformatperiod", value);
                OnPropertyChanged(nameof(FiscalPeriodFormatPeriod));
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
                OnPropertyChanging(nameof(FiscalPeriodType));
                SetAttributeValue("fiscalperiodtype", value);
                OnPropertyChanged(nameof(FiscalPeriodType));
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
                OnPropertyChanging(nameof(FiscalYearDisplayCode));
                SetAttributeValue("fiscalyeardisplaycode", value);
                OnPropertyChanged(nameof(FiscalYearDisplayCode));
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
                OnPropertyChanging(nameof(FiscalYearFormat));
                SetAttributeValue("fiscalyearformat", value);
                OnPropertyChanged(nameof(FiscalYearFormat));
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
                OnPropertyChanging(nameof(FiscalYearFormatPrefix));
                SetAttributeValue("fiscalyearformatprefix", value);
                OnPropertyChanged(nameof(FiscalYearFormatPrefix));
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
                OnPropertyChanging(nameof(FiscalYearFormatSuffix));
                SetAttributeValue("fiscalyearformatsuffix", value);
                OnPropertyChanged(nameof(FiscalYearFormatSuffix));
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
                OnPropertyChanging(nameof(FiscalYearFormatYear));
                SetAttributeValue("fiscalyearformatyear", value);
                OnPropertyChanged(nameof(FiscalYearFormatYear));
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
                OnPropertyChanging(nameof(FiscalYearPeriodConnect));
                SetAttributeValue("fiscalyearperiodconnect", value);
                OnPropertyChanged(nameof(FiscalYearPeriodConnect));
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
                OnPropertyChanging(nameof(FullNameConventionCode));
                SetAttributeValue("fullnameconventioncode", value);
                OnPropertyChanged(nameof(FullNameConventionCode));
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
                OnPropertyChanging(nameof(FutureExpansionWindow));
                SetAttributeValue("futureexpansionwindow", value);
                OnPropertyChanged(nameof(FutureExpansionWindow));
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
                OnPropertyChanging(nameof(GenerateAlertsForErrors));
                SetAttributeValue("generatealertsforerrors", value);
                OnPropertyChanged(nameof(GenerateAlertsForErrors));
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
                OnPropertyChanging(nameof(GenerateAlertsForInformation));
                SetAttributeValue("generatealertsforinformation", value);
                OnPropertyChanged(nameof(GenerateAlertsForInformation));
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
                OnPropertyChanging(nameof(GenerateAlertsForWarnings));
                SetAttributeValue("generatealertsforwarnings", value);
                OnPropertyChanged(nameof(GenerateAlertsForWarnings));
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
                OnPropertyChanging(nameof(GetStartedPaneContentEnabled));
                SetAttributeValue("getstartedpanecontentenabled", value);
                OnPropertyChanged(nameof(GetStartedPaneContentEnabled));
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
                OnPropertyChanging(nameof(GlobalAppendUrlParametersEnabled));
                SetAttributeValue("globalappendurlparametersenabled", value);
                OnPropertyChanged(nameof(GlobalAppendUrlParametersEnabled));
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
                OnPropertyChanging(nameof(GlobalHelpUrl));
                SetAttributeValue("globalhelpurl", value);
                OnPropertyChanged(nameof(GlobalHelpUrl));
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
                OnPropertyChanging(nameof(GlobalHelpUrlEnabled));
                SetAttributeValue("globalhelpurlenabled", value);
                OnPropertyChanged(nameof(GlobalHelpUrlEnabled));
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
                OnPropertyChanging(nameof(GoalRollupExpiryTime));
                SetAttributeValue("goalrollupexpirytime", value);
                OnPropertyChanged(nameof(GoalRollupExpiryTime));
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
                OnPropertyChanging(nameof(GoalRollupFrequency));
                SetAttributeValue("goalrollupfrequency", value);
                OnPropertyChanged(nameof(GoalRollupFrequency));
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
                OnPropertyChanging(nameof(GrantAccessToNetworkService));
                SetAttributeValue("grantaccesstonetworkservice", value);
                OnPropertyChanged(nameof(GrantAccessToNetworkService));
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
                OnPropertyChanging(nameof(HashDeltaSubjectCount));
                SetAttributeValue("hashdeltasubjectcount", value);
                OnPropertyChanged(nameof(HashDeltaSubjectCount));
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
                OnPropertyChanging(nameof(HashFilterKeywords));
                SetAttributeValue("hashfilterkeywords", value);
                OnPropertyChanged(nameof(HashFilterKeywords));
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
                OnPropertyChanging(nameof(HashMaxCount));
                SetAttributeValue("hashmaxcount", value);
                OnPropertyChanged(nameof(HashMaxCount));
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
                OnPropertyChanging(nameof(HashMinAddressCount));
                SetAttributeValue("hashminaddresscount", value);
                OnPropertyChanged(nameof(HashMinAddressCount));
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
                OnPropertyChanging(nameof(HighContrastThemeData));
                SetAttributeValue("highcontrastthemedata", value);
                OnPropertyChanged(nameof(HighContrastThemeData));
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
                OnPropertyChanging(nameof(IgnoreInternalEmail));
                SetAttributeValue("ignoreinternalemail", value);
                OnPropertyChanged(nameof(IgnoreInternalEmail));
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
                OnPropertyChanging(nameof(ImproveSearchLoggingEnabled));
                SetAttributeValue("improvesearchloggingenabled", value);
                OnPropertyChanged(nameof(ImproveSearchLoggingEnabled));
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
                OnPropertyChanging(nameof(InactivityTimeoutEnabled));
                SetAttributeValue("inactivitytimeoutenabled", value);
                OnPropertyChanged(nameof(InactivityTimeoutEnabled));
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
                OnPropertyChanging(nameof(InactivityTimeoutInMins));
                SetAttributeValue("inactivitytimeoutinmins", value);
                OnPropertyChanged(nameof(InactivityTimeoutInMins));
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
                OnPropertyChanging(nameof(InactivityTimeoutReminderInMins));
                SetAttributeValue("inactivitytimeoutreminderinmins", value);
                OnPropertyChanged(nameof(InactivityTimeoutReminderInMins));
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
                OnPropertyChanging(nameof(IncomingEmailExchangeEmailRetrievalBatchSize));
                SetAttributeValue("incomingemailexchangeemailretrievalbatchsize", value);
                OnPropertyChanged(nameof(IncomingEmailExchangeEmailRetrievalBatchSize));
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
                OnPropertyChanging(nameof(InitialVersion));
                SetAttributeValue("initialversion", value);
                OnPropertyChanged(nameof(InitialVersion));
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
                OnPropertyChanging(nameof(IntegrationUserId));
                SetAttributeValue("integrationuserid", value);
                OnPropertyChanged(nameof(IntegrationUserId));
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
                OnPropertyChanging(nameof(InvoicePrefix));
                SetAttributeValue("invoiceprefix", value);
                OnPropertyChanged(nameof(InvoicePrefix));
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
                OnPropertyChanging(nameof(IpBasedStorageAccessSignatureMode));
                SetAttributeValue("ipbasedstorageaccesssignaturemode", value);
                OnPropertyChanged(nameof(IpBasedStorageAccessSignatureMode));
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
                OnPropertyChanging(nameof(IsActionCardEnabled));
                SetAttributeValue("isactioncardenabled", value);
                OnPropertyChanged(nameof(IsActionCardEnabled));
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
                OnPropertyChanging(nameof(IsActionSupportFeatureEnabled));
                SetAttributeValue("isactionsupportfeatureenabled", value);
                OnPropertyChanged(nameof(IsActionSupportFeatureEnabled));
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
                OnPropertyChanging(nameof(IsActivityAnalysisEnabled));
                SetAttributeValue("isactivityanalysisenabled", value);
                OnPropertyChanged(nameof(IsActivityAnalysisEnabled));
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
                OnPropertyChanging(nameof(IsAppMode));
                SetAttributeValue("isappmode", value);
                OnPropertyChanged(nameof(IsAppMode));
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
                OnPropertyChanging(nameof(IsAppointmentAttachmentSyncEnabled));
                SetAttributeValue("isappointmentattachmentsyncenabled", value);
                OnPropertyChanged(nameof(IsAppointmentAttachmentSyncEnabled));
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
                OnPropertyChanging(nameof(IsAssignedTasksSyncEnabled));
                SetAttributeValue("isassignedtaskssyncenabled", value);
                OnPropertyChanged(nameof(IsAssignedTasksSyncEnabled));
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
                OnPropertyChanging(nameof(IsAuditEnabled));
                SetAttributeValue("isauditenabled", value);
                OnPropertyChanged(nameof(IsAuditEnabled));
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
                OnPropertyChanging(nameof(IsAutoDataCaptureEnabled));
                SetAttributeValue("isautodatacaptureenabled", value);
                OnPropertyChanged(nameof(IsAutoDataCaptureEnabled));
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
                OnPropertyChanging(nameof(IsAutoDataCaptureV2Enabled));
                SetAttributeValue("isautodatacapturev2enabled", value);
                OnPropertyChanged(nameof(IsAutoDataCaptureV2Enabled));
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
                OnPropertyChanging(nameof(IsAutoSaveEnabled));
                SetAttributeValue("isautosaveenabled", value);
                OnPropertyChanged(nameof(IsAutoSaveEnabled));
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
                OnPropertyChanging(nameof(IsBaseCardStaticFieldDataEnabled));
                SetAttributeValue("isbasecardstaticfielddataenabled", value);
                OnPropertyChanged(nameof(IsBaseCardStaticFieldDataEnabled));
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
                OnPropertyChanging(nameof(IsBasicGeospatialIntegrationEnabled));
                SetAttributeValue("isbasicgeospatialintegrationenabled", value);
                OnPropertyChanged(nameof(IsBasicGeospatialIntegrationEnabled));
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
                OnPropertyChanging(nameof(IsBPFEntityCustomizationFeatureEnabled));
                SetAttributeValue("isbpfentitycustomizationfeatureenabled", value);
                OnPropertyChanged(nameof(IsBPFEntityCustomizationFeatureEnabled));
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
                OnPropertyChanging(nameof(IsCollaborationExperienceEnabled));
                SetAttributeValue("iscollaborationexperienceenabled", value);
                OnPropertyChanged(nameof(IsCollaborationExperienceEnabled));
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
                OnPropertyChanging(nameof(IsConflictDetectionEnabledForMobileClient));
                SetAttributeValue("isconflictdetectionenabledformobileclient", value);
                OnPropertyChanged(nameof(IsConflictDetectionEnabledForMobileClient));
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
                OnPropertyChanging(nameof(IsContactMailingAddressSyncEnabled));
                SetAttributeValue("iscontactmailingaddresssyncenabled", value);
                OnPropertyChanged(nameof(IsContactMailingAddressSyncEnabled));
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
                OnPropertyChanging(nameof(IsContentSecurityPolicyEnabled));
                SetAttributeValue("iscontentsecuritypolicyenabled", value);
                OnPropertyChanged(nameof(IsContentSecurityPolicyEnabled));
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
                OnPropertyChanging(nameof(IsContentSecurityPolicyEnabledForCanvas));
                SetAttributeValue("iscontentsecuritypolicyenabledforcanvas", value);
                OnPropertyChanged(nameof(IsContentSecurityPolicyEnabledForCanvas));
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
                OnPropertyChanging(nameof(IsContextualEmailEnabled));
                SetAttributeValue("iscontextualemailenabled", value);
                OnPropertyChanged(nameof(IsContextualEmailEnabled));
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
                OnPropertyChanging(nameof(IsContextualHelpEnabled));
                SetAttributeValue("iscontextualhelpenabled", value);
                OnPropertyChanged(nameof(IsContextualHelpEnabled));
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
                OnPropertyChanging(nameof(IsCustomControlsInCanvasAppsEnabled));
                SetAttributeValue("iscustomcontrolsincanvasappsenabled", value);
                OnPropertyChanged(nameof(IsCustomControlsInCanvasAppsEnabled));
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
                OnPropertyChanging(nameof(IsDefaultCountryCodeCheckEnabled));
                SetAttributeValue("isdefaultcountrycodecheckenabled", value);
                OnPropertyChanged(nameof(IsDefaultCountryCodeCheckEnabled));
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
                OnPropertyChanging(nameof(IsDelegateAccessEnabled));
                SetAttributeValue("isdelegateaccessenabled", value);
                OnPropertyChanged(nameof(IsDelegateAccessEnabled));
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
                OnPropertyChanging(nameof(IsDelveActionHubIntegrationEnabled));
                SetAttributeValue("isdelveactionhubintegrationenabled", value);
                OnPropertyChanged(nameof(IsDelveActionHubIntegrationEnabled));
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
                OnPropertyChanging(nameof(IsDesktopFlowSchemaV2Enabled));
                SetAttributeValue("isdesktopflowschemav2enabled", value);
                OnPropertyChanged(nameof(IsDesktopFlowSchemaV2Enabled));
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
                OnPropertyChanging(nameof(IsDuplicateDetectionEnabled));
                SetAttributeValue("isduplicatedetectionenabled", value);
                OnPropertyChanged(nameof(IsDuplicateDetectionEnabled));
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
                OnPropertyChanging(nameof(IsDuplicateDetectionEnabledForImport));
                SetAttributeValue("isduplicatedetectionenabledforimport", value);
                OnPropertyChanged(nameof(IsDuplicateDetectionEnabledForImport));
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
                OnPropertyChanging(nameof(IsDuplicateDetectionEnabledForOfflineSync));
                SetAttributeValue("isduplicatedetectionenabledforofflinesync", value);
                OnPropertyChanged(nameof(IsDuplicateDetectionEnabledForOfflineSync));
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
                OnPropertyChanging(nameof(IsDuplicateDetectionEnabledForOnlineCreateUpdate));
                SetAttributeValue("isduplicatedetectionenabledforonlinecreateupdate", value);
                OnPropertyChanged(nameof(IsDuplicateDetectionEnabledForOnlineCreateUpdate));
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
                OnPropertyChanging(nameof(IsEmailMonitoringAllowed));
                SetAttributeValue("isemailmonitoringallowed", value);
                OnPropertyChanged(nameof(IsEmailMonitoringAllowed));
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
                OnPropertyChanging(nameof(IsEmailServerProfileContentFilteringEnabled));
                SetAttributeValue("isemailserverprofilecontentfilteringenabled", value);
                OnPropertyChanged(nameof(IsEmailServerProfileContentFilteringEnabled));
            }
        }

		/// <summary>
		/// Indicates whether embed Teams collaboration has been enabled for the organization
		/// </summary>
		[AttributeLogicalName("isembedteamscollabenabled")]
        public bool? IsEmbedTeamsCollabEnabled
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isembedteamscollabenabled");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsEmbedTeamsCollabEnabled));
                SetAttributeValue("isembedteamscollabenabled", value);
                OnPropertyChanged(nameof(IsEmbedTeamsCollabEnabled));
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
                OnPropertyChanging(nameof(IsEnabledForAllRoles));
                SetAttributeValue("isenabledforallroles", value);
                OnPropertyChanged(nameof(IsEnabledForAllRoles));
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
                OnPropertyChanging(nameof(IsExternalFileStorageEnabled));
                SetAttributeValue("isexternalfilestorageenabled", value);
                OnPropertyChanged(nameof(IsExternalFileStorageEnabled));
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
                OnPropertyChanging(nameof(IsExternalSearchIndexEnabled));
                SetAttributeValue("isexternalsearchindexenabled", value);
                OnPropertyChanged(nameof(IsExternalSearchIndexEnabled));
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
                OnPropertyChanging(nameof(IsFiscalPeriodMonthBased));
                SetAttributeValue("isfiscalperiodmonthbased", value);
                OnPropertyChanged(nameof(IsFiscalPeriodMonthBased));
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
                OnPropertyChanging(nameof(IsFolderAutoCreatedonSP));
                SetAttributeValue("isfolderautocreatedonsp", value);
                OnPropertyChanged(nameof(IsFolderAutoCreatedonSP));
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
                OnPropertyChanging(nameof(IsFolderBasedTrackingEnabled));
                SetAttributeValue("isfolderbasedtrackingenabled", value);
                OnPropertyChanged(nameof(IsFolderBasedTrackingEnabled));
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
                OnPropertyChanging(nameof(IsFullTextSearchEnabled));
                SetAttributeValue("isfulltextsearchenabled", value);
                OnPropertyChanged(nameof(IsFullTextSearchEnabled));
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
                OnPropertyChanging(nameof(IsGeospatialAzureMapsIntegrationEnabled));
                SetAttributeValue("isgeospatialazuremapsintegrationenabled", value);
                OnPropertyChanged(nameof(IsGeospatialAzureMapsIntegrationEnabled));
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
                OnPropertyChanging(nameof(IsHierarchicalSecurityModelEnabled));
                SetAttributeValue("ishierarchicalsecuritymodelenabled", value);
                OnPropertyChanged(nameof(IsHierarchicalSecurityModelEnabled));
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
                OnPropertyChanging(nameof(IsIdeasDataCollectionEnabled));
                SetAttributeValue("isideasdatacollectionenabled", value);
                OnPropertyChanged(nameof(IsIdeasDataCollectionEnabled));
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
                OnPropertyChanging(nameof(IsLUISEnabledforD365Bot));
                SetAttributeValue("isluisenabledford365bot", value);
                OnPropertyChanged(nameof(IsLUISEnabledforD365Bot));
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
                OnPropertyChanging(nameof(IsMailboxForcedUnlockingEnabled));
                SetAttributeValue("ismailboxforcedunlockingenabled", value);
                OnPropertyChanged(nameof(IsMailboxForcedUnlockingEnabled));
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
                OnPropertyChanging(nameof(IsMailboxInactiveBackoffEnabled));
                SetAttributeValue("ismailboxinactivebackoffenabled", value);
                OnPropertyChanged(nameof(IsMailboxInactiveBackoffEnabled));
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
                OnPropertyChanging(nameof(IsManualSalesForecastingEnabled));
                SetAttributeValue("ismanualsalesforecastingenabled", value);
                OnPropertyChanged(nameof(IsManualSalesForecastingEnabled));
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
                OnPropertyChanging(nameof(IsMobileClientOnDemandSyncEnabled));
                SetAttributeValue("ismobileclientondemandsyncenabled", value);
                OnPropertyChanged(nameof(IsMobileClientOnDemandSyncEnabled));
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
                OnPropertyChanging(nameof(IsMobileOfflineEnabled));
                SetAttributeValue("ismobileofflineenabled", value);
                OnPropertyChanged(nameof(IsMobileOfflineEnabled));
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
                OnPropertyChanging(nameof(IsModelDrivenAppsInMSTeamsEnabled));
                SetAttributeValue("ismodeldrivenappsinmsteamsenabled", value);
                OnPropertyChanged(nameof(IsModelDrivenAppsInMSTeamsEnabled));
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
                OnPropertyChanging(nameof(IsMSTeamsCollaborationEnabled));
                SetAttributeValue("ismsteamscollaborationenabled", value);
                OnPropertyChanged(nameof(IsMSTeamsCollaborationEnabled));
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
                OnPropertyChanging(nameof(IsMSTeamsEnabled));
                SetAttributeValue("ismsteamsenabled", value);
                OnPropertyChanged(nameof(IsMSTeamsEnabled));
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
                OnPropertyChanging(nameof(IsMSTeamsSettingChangedByUser));
                SetAttributeValue("ismsteamssettingchangedbyuser", value);
                OnPropertyChanged(nameof(IsMSTeamsSettingChangedByUser));
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
                OnPropertyChanging(nameof(IsMSTeamsUserSyncEnabled));
                SetAttributeValue("ismsteamsusersyncenabled", value);
                OnPropertyChanged(nameof(IsMSTeamsUserSyncEnabled));
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
                OnPropertyChanging(nameof(IsNewAddProductExperienceEnabled));
                SetAttributeValue("isnewaddproductexperienceenabled", value);
                OnPropertyChanged(nameof(IsNewAddProductExperienceEnabled));
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
                OnPropertyChanging(nameof(IsNotesAnalysisEnabled));
                SetAttributeValue("isnotesanalysisenabled", value);
                OnPropertyChanged(nameof(IsNotesAnalysisEnabled));
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
                OnPropertyChanging(nameof(IsOfficeGraphEnabled));
                SetAttributeValue("isofficegraphenabled", value);
                OnPropertyChanged(nameof(IsOfficeGraphEnabled));
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
                OnPropertyChanging(nameof(IsOneDriveEnabled));
                SetAttributeValue("isonedriveenabled", value);
                OnPropertyChanged(nameof(IsOneDriveEnabled));
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
                OnPropertyChanging(nameof(IsPAIEnabled));
                SetAttributeValue("ispaienabled", value);
                OnPropertyChanged(nameof(IsPAIEnabled));
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
                OnPropertyChanging(nameof(IsPDFGenerationEnabled));
                SetAttributeValue("ispdfgenerationenabled", value);
                OnPropertyChanged(nameof(IsPDFGenerationEnabled));
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
                OnPropertyChanging(nameof(IsPlaybookEnabled));
                SetAttributeValue("isplaybookenabled", value);
                OnPropertyChanged(nameof(IsPlaybookEnabled));
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
                OnPropertyChanging(nameof(IsPresenceEnabled));
                SetAttributeValue("ispresenceenabled", value);
                OnPropertyChanged(nameof(IsPresenceEnabled));
            }
        }

		
		[AttributeLogicalName("ispresenceenabledname")]
        public string? IsPresenceEnabledName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ispresenceenabledname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsPresenceEnabledName));
                SetAttributeValue("ispresenceenabledname", value);
                OnPropertyChanged(nameof(IsPresenceEnabledName));
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
                OnPropertyChanging(nameof(IsPreviewEnabledForActionCard));
                SetAttributeValue("ispreviewenabledforactioncard", value);
                OnPropertyChanged(nameof(IsPreviewEnabledForActionCard));
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
                OnPropertyChanging(nameof(IsPreviewForAutoCaptureEnabled));
                SetAttributeValue("ispreviewforautocaptureenabled", value);
                OnPropertyChanged(nameof(IsPreviewForAutoCaptureEnabled));
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
                OnPropertyChanging(nameof(IsPreviewForEmailMonitoringAllowed));
                SetAttributeValue("ispreviewforemailmonitoringallowed", value);
                OnPropertyChanged(nameof(IsPreviewForEmailMonitoringAllowed));
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
                OnPropertyChanging(nameof(IsPriceListMandatory));
                SetAttributeValue("ispricelistmandatory", value);
                OnPropertyChanged(nameof(IsPriceListMandatory));
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
                OnPropertyChanging(nameof(IsQuickCreateEnabledForOpportunityClose));
                SetAttributeValue("isquickcreateenabledforopportunityclose", value);
                OnPropertyChanged(nameof(IsQuickCreateEnabledForOpportunityClose));
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
                OnPropertyChanging(nameof(IsReadAuditEnabled));
                SetAttributeValue("isreadauditenabled", value);
                OnPropertyChanged(nameof(IsReadAuditEnabled));
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
                OnPropertyChanging(nameof(IsRelationshipInsightsEnabled));
                SetAttributeValue("isrelationshipinsightsenabled", value);
                OnPropertyChanged(nameof(IsRelationshipInsightsEnabled));
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
                OnPropertyChanging(nameof(IsResourceBookingExchangeSyncEnabled));
                SetAttributeValue("isresourcebookingexchangesyncenabled", value);
                OnPropertyChanged(nameof(IsResourceBookingExchangeSyncEnabled));
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
                OnPropertyChanging(nameof(IsRichTextNotesEnabled));
                SetAttributeValue("isrichtextnotesenabled", value);
                OnPropertyChanged(nameof(IsRichTextNotesEnabled));
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
                OnPropertyChanging(nameof(IsRpaAutoscaleAadJoinEnabled));
                SetAttributeValue("isrpaautoscaleaadjoinenabled", value);
                OnPropertyChanged(nameof(IsRpaAutoscaleAadJoinEnabled));
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
                OnPropertyChanging(nameof(IsRpaAutoscaleEnabled));
                SetAttributeValue("isrpaautoscaleenabled", value);
                OnPropertyChanged(nameof(IsRpaAutoscaleEnabled));
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
                OnPropertyChanging(nameof(IsRpaBoxEnabled));
                SetAttributeValue("isrpaboxenabled", value);
                OnPropertyChanged(nameof(IsRpaBoxEnabled));
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
                OnPropertyChanging(nameof(IsRpaUnattendedEnabled));
                SetAttributeValue("isrpaunattendedenabled", value);
                OnPropertyChanged(nameof(IsRpaUnattendedEnabled));
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
                OnPropertyChanging(nameof(IsSalesAssistantEnabled));
                SetAttributeValue("issalesassistantenabled", value);
                OnPropertyChanged(nameof(IsSalesAssistantEnabled));
            }
        }

		/// <summary>
		/// Indicates whether Sales Mobile Preview has been enabled for the organization
		/// </summary>
		[AttributeLogicalName("issalesmobilepreviewenabled")]
        public bool? IsSalesMobilePreviewEnabled
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("issalesmobilepreviewenabled");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsSalesMobilePreviewEnabled));
                SetAttributeValue("issalesmobilepreviewenabled", value);
                OnPropertyChanged(nameof(IsSalesMobilePreviewEnabled));
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
                OnPropertyChanging(nameof(IsSharingInOrgAllowed));
                SetAttributeValue("issharinginorgallowed", value);
                OnPropertyChanged(nameof(IsSharingInOrgAllowed));
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
                OnPropertyChanging(nameof(IsSOPIntegrationEnabled));
                SetAttributeValue("issopintegrationenabled", value);
                OnPropertyChanged(nameof(IsSOPIntegrationEnabled));
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
                OnPropertyChanging(nameof(IsTextWrapEnabled));
                SetAttributeValue("istextwrapenabled", value);
                OnPropertyChanged(nameof(IsTextWrapEnabled));
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
                OnPropertyChanging(nameof(IsUserAccessAuditEnabled));
                SetAttributeValue("isuseraccessauditenabled", value);
                OnPropertyChanged(nameof(IsUserAccessAuditEnabled));
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
                OnPropertyChanging(nameof(ISVIntegrationCode));
                SetAttributeValue("isvintegrationcode", value);
                OnPropertyChanged(nameof(ISVIntegrationCode));
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
                OnPropertyChanging(nameof(IsWriteInProductsAllowed));
                SetAttributeValue("iswriteinproductsallowed", value);
                OnPropertyChanged(nameof(IsWriteInProductsAllowed));
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
                OnPropertyChanging(nameof(KaPrefix));
                SetAttributeValue("kaprefix", value);
                OnPropertyChanged(nameof(KaPrefix));
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
                OnPropertyChanging(nameof(KbPrefix));
                SetAttributeValue("kbprefix", value);
                OnPropertyChanged(nameof(KbPrefix));
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
                OnPropertyChanging(nameof(KMSettings));
                SetAttributeValue("kmsettings", value);
                OnPropertyChanged(nameof(KMSettings));
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
                OnPropertyChanging(nameof(LanguageCode));
                SetAttributeValue("languagecode", value);
                OnPropertyChanged(nameof(LanguageCode));
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
                OnPropertyChanging(nameof(LocaleId));
                SetAttributeValue("localeid", value);
                OnPropertyChanged(nameof(LocaleId));
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
                OnPropertyChanging(nameof(LongDateFormatCode));
                SetAttributeValue("longdateformatcode", value);
                OnPropertyChanged(nameof(LongDateFormatCode));
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
                OnPropertyChanging(nameof(LookupCharacterCountBeforeResolve));
                SetAttributeValue("lookupcharactercountbeforeresolve", value);
                OnPropertyChanged(nameof(LookupCharacterCountBeforeResolve));
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
                OnPropertyChanging(nameof(LookupResolveDelayMS));
                SetAttributeValue("lookupresolvedelayms", value);
                OnPropertyChanged(nameof(LookupResolveDelayMS));
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
                OnPropertyChanging(nameof(MailboxIntermittentIssueMinRange));
                SetAttributeValue("mailboxintermittentissueminrange", value);
                OnPropertyChanged(nameof(MailboxIntermittentIssueMinRange));
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
                OnPropertyChanging(nameof(MailboxPermanentIssueMinRange));
                SetAttributeValue("mailboxpermanentissueminrange", value);
                OnPropertyChanged(nameof(MailboxPermanentIssueMinRange));
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
                OnPropertyChanging(nameof(MaxActionStepsInBPF));
                SetAttributeValue("maxactionstepsinbpf", value);
                OnPropertyChanged(nameof(MaxActionStepsInBPF));
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
                OnPropertyChanging(nameof(MaxAllowedPendingRollupJobCount));
                SetAttributeValue("maxallowedpendingrollupjobcount", value);
                OnPropertyChanged(nameof(MaxAllowedPendingRollupJobCount));
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
                OnPropertyChanging(nameof(MaxAllowedPendingRollupJobPercentage));
                SetAttributeValue("maxallowedpendingrollupjobpercentage", value);
                OnPropertyChanged(nameof(MaxAllowedPendingRollupJobPercentage));
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
                OnPropertyChanging(nameof(MaxAppointmentDurationDays));
                SetAttributeValue("maxappointmentdurationdays", value);
                OnPropertyChanged(nameof(MaxAppointmentDurationDays));
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
                OnPropertyChanging(nameof(MaxConditionsForMobileOfflineFilters));
                SetAttributeValue("maxconditionsformobileofflinefilters", value);
                OnPropertyChanged(nameof(MaxConditionsForMobileOfflineFilters));
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
                OnPropertyChanging(nameof(MaxDepthForHierarchicalSecurityModel));
                SetAttributeValue("maxdepthforhierarchicalsecuritymodel", value);
                OnPropertyChanged(nameof(MaxDepthForHierarchicalSecurityModel));
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
                OnPropertyChanging(nameof(MaxFolderBasedTrackingMappings));
                SetAttributeValue("maxfolderbasedtrackingmappings", value);
                OnPropertyChanged(nameof(MaxFolderBasedTrackingMappings));
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
                OnPropertyChanging(nameof(MaximumActiveBusinessProcessFlowsAllowedPerEntity));
                SetAttributeValue("maximumactivebusinessprocessflowsallowedperentity", value);
                OnPropertyChanged(nameof(MaximumActiveBusinessProcessFlowsAllowedPerEntity));
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
                OnPropertyChanging(nameof(MaximumDynamicPropertiesAllowed));
                SetAttributeValue("maximumdynamicpropertiesallowed", value);
                OnPropertyChanged(nameof(MaximumDynamicPropertiesAllowed));
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
                OnPropertyChanging(nameof(MaximumEntitiesWithActiveSLA));
                SetAttributeValue("maximumentitieswithactivesla", value);
                OnPropertyChanged(nameof(MaximumEntitiesWithActiveSLA));
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
                OnPropertyChanging(nameof(MaximumSLAKPIPerEntityWithActiveSLA));
                SetAttributeValue("maximumslakpiperentitywithactivesla", value);
                OnPropertyChanged(nameof(MaximumSLAKPIPerEntityWithActiveSLA));
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
                OnPropertyChanging(nameof(MaximumTrackingNumber));
                SetAttributeValue("maximumtrackingnumber", value);
                OnPropertyChanged(nameof(MaximumTrackingNumber));
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
                OnPropertyChanging(nameof(MaxProductsInBundle));
                SetAttributeValue("maxproductsinbundle", value);
                OnPropertyChanged(nameof(MaxProductsInBundle));
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
                OnPropertyChanging(nameof(MaxRecordsForExportToExcel));
                SetAttributeValue("maxrecordsforexporttoexcel", value);
                OnPropertyChanged(nameof(MaxRecordsForExportToExcel));
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
                OnPropertyChanging(nameof(MaxRecordsForLookupFilters));
                SetAttributeValue("maxrecordsforlookupfilters", value);
                OnPropertyChanged(nameof(MaxRecordsForLookupFilters));
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
                OnPropertyChanging(nameof(MaxRollupFieldsPerEntity));
                SetAttributeValue("maxrollupfieldsperentity", value);
                OnPropertyChanged(nameof(MaxRollupFieldsPerEntity));
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
                OnPropertyChanging(nameof(MaxRollupFieldsPerOrg));
                SetAttributeValue("maxrollupfieldsperorg", value);
                OnPropertyChanged(nameof(MaxRollupFieldsPerOrg));
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
                OnPropertyChanging(nameof(MaxSLAItemsPerSLA));
                SetAttributeValue("maxslaitemspersla", value);
                OnPropertyChanged(nameof(MaxSLAItemsPerSLA));
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
                OnPropertyChanging(nameof(MaxUploadFileSize));
                SetAttributeValue("maxuploadfilesize", value);
                OnPropertyChanged(nameof(MaxUploadFileSize));
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
                OnPropertyChanging(nameof(MicrosoftFlowEnvironment));
                SetAttributeValue("microsoftflowenvironment", value);
                OnPropertyChanged(nameof(MicrosoftFlowEnvironment));
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
                OnPropertyChanging(nameof(MinAddressBookSyncInterval));
                SetAttributeValue("minaddressbooksyncinterval", value);
                OnPropertyChanged(nameof(MinAddressBookSyncInterval));
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
                OnPropertyChanging(nameof(MinOfflineSyncInterval));
                SetAttributeValue("minofflinesyncinterval", value);
                OnPropertyChanged(nameof(MinOfflineSyncInterval));
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
                OnPropertyChanging(nameof(MinOutlookSyncInterval));
                SetAttributeValue("minoutlooksyncinterval", value);
                OnPropertyChanged(nameof(MinOutlookSyncInterval));
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
                OnPropertyChanging(nameof(MobileOfflineSyncInterval));
                SetAttributeValue("mobileofflinesyncinterval", value);
                OnPropertyChanged(nameof(MobileOfflineSyncInterval));
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
                OnPropertyChanging(nameof(ModernAdvancedFindFiltering));
                SetAttributeValue("modernadvancedfindfiltering", value);
                OnPropertyChanged(nameof(ModernAdvancedFindFiltering));
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
                OnPropertyChanging(nameof(ModernAppDesignerCoauthoringEnabled));
                SetAttributeValue("modernappdesignercoauthoringenabled", value);
                OnPropertyChanged(nameof(ModernAppDesignerCoauthoringEnabled));
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
                OnPropertyChanging(nameof(MultiColumnSortEnabled));
                SetAttributeValue("multicolumnsortenabled", value);
                OnPropertyChanged(nameof(MultiColumnSortEnabled));
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
                OnPropertyChanging(nameof(Name));
                SetAttributeValue("name", value);
                OnPropertyChanged(nameof(Name));
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
                OnPropertyChanging(nameof(NaturalLanguageAssistFilter));
                SetAttributeValue("naturallanguageassistfilter", value);
                OnPropertyChanged(nameof(NaturalLanguageAssistFilter));
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
                OnPropertyChanging(nameof(NegativeCurrencyFormatCode));
                SetAttributeValue("negativecurrencyformatcode", value);
                OnPropertyChanged(nameof(NegativeCurrencyFormatCode));
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
                OnPropertyChanging(nameof(NegativeFormatCode));
                SetAttributeValue("negativeformatcode", value);
                OnPropertyChanged(nameof(NegativeFormatCode));
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
                OnPropertyChanging(nameof(NewSearchExperienceEnabled));
                SetAttributeValue("newsearchexperienceenabled", value);
                OnPropertyChanged(nameof(NewSearchExperienceEnabled));
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
                OnPropertyChanging(nameof(NextTrackingNumber));
                SetAttributeValue("nexttrackingnumber", value);
                OnPropertyChanged(nameof(NextTrackingNumber));
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
                OnPropertyChanging(nameof(NotifyMailboxOwnerOfEmailServerLevelAlerts));
                SetAttributeValue("notifymailboxownerofemailserverlevelalerts", value);
                OnPropertyChanged(nameof(NotifyMailboxOwnerOfEmailServerLevelAlerts));
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
                OnPropertyChanging(nameof(NumberFormat));
                SetAttributeValue("numberformat", value);
                OnPropertyChanged(nameof(NumberFormat));
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
                OnPropertyChanging(nameof(NumberGroupFormat));
                SetAttributeValue("numbergroupformat", value);
                OnPropertyChanged(nameof(NumberGroupFormat));
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
                OnPropertyChanging(nameof(NumberSeparator));
                SetAttributeValue("numberseparator", value);
                OnPropertyChanged(nameof(NumberSeparator));
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
                OnPropertyChanging(nameof(OfficeAppsAutoDeploymentEnabled));
                SetAttributeValue("officeappsautodeploymentenabled", value);
                OnPropertyChanged(nameof(OfficeAppsAutoDeploymentEnabled));
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
                OnPropertyChanging(nameof(OfficeGraphDelveUrl));
                SetAttributeValue("officegraphdelveurl", value);
                OnPropertyChanged(nameof(OfficeGraphDelveUrl));
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
                OnPropertyChanging(nameof(OOBPriceCalculationEnabled));
                SetAttributeValue("oobpricecalculationenabled", value);
                OnPropertyChanged(nameof(OOBPriceCalculationEnabled));
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
                OnPropertyChanging(nameof(OrderPrefix));
                SetAttributeValue("orderprefix", value);
                OnPropertyChanged(nameof(OrderPrefix));
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
                OnPropertyChanging(nameof(OrgDbOrgSettings));
                SetAttributeValue("orgdborgsettings", value);
                OnPropertyChanged(nameof(OrgDbOrgSettings));
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
                OnPropertyChanging(nameof(OrgInsightsEnabled));
                SetAttributeValue("orginsightsenabled", value);
                OnPropertyChanged(nameof(OrgInsightsEnabled));
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
                OnPropertyChanging(nameof(PaiPreviewScenarioEnabled));
                SetAttributeValue("paipreviewscenarioenabled", value);
                OnPropertyChanged(nameof(PaiPreviewScenarioEnabled));
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
                OnPropertyChanging(nameof(PastExpansionWindow));
                SetAttributeValue("pastexpansionwindow", value);
                OnPropertyChanged(nameof(PastExpansionWindow));
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
                OnPropertyChanging(nameof(PcfDatasetGridEnabled));
                SetAttributeValue("pcfdatasetgridenabled", value);
                OnPropertyChanged(nameof(PcfDatasetGridEnabled));
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
                OnPropertyChanging(nameof(Picture));
                SetAttributeValue("picture", value);
                OnPropertyChanged(nameof(Picture));
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
                OnPropertyChanging(nameof(PinpointLanguageCode));
                SetAttributeValue("pinpointlanguagecode", value);
                OnPropertyChanged(nameof(PinpointLanguageCode));
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
                OnPropertyChanging(nameof(PluginTraceLogSetting));
                SetAttributeValue("plugintracelogsetting", value);
                OnPropertyChanged(nameof(PluginTraceLogSetting));
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
                OnPropertyChanging(nameof(PMDesignator));
                SetAttributeValue("pmdesignator", value);
                OnPropertyChanged(nameof(PMDesignator));
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
                OnPropertyChanging(nameof(PostMessageWhitelistDomains));
                SetAttributeValue("postmessagewhitelistdomains", value);
                OnPropertyChanged(nameof(PostMessageWhitelistDomains));
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
                OnPropertyChanging(nameof(PowerAppsMakerBotEnabled));
                SetAttributeValue("powerappsmakerbotenabled", value);
                OnPropertyChanged(nameof(PowerAppsMakerBotEnabled));
            }
        }

		/// <summary>
		/// Indicates whether cross region operations are allowed for the organization
		/// </summary>
		[AttributeLogicalName("powerbiallowcrossregionoperations")]
        public bool? PowerBIAllowCrossRegionOperations
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("powerbiallowcrossregionoperations");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PowerBIAllowCrossRegionOperations));
                SetAttributeValue("powerbiallowcrossregionoperations", value);
                OnPropertyChanged(nameof(PowerBIAllowCrossRegionOperations));
            }
        }

		/// <summary>
		/// Indicates whether automatic permissions assignment to Power BI has been enabled for the organization
		/// </summary>
		[AttributeLogicalName("powerbiautomaticpermissionsassignment")]
        public bool? PowerBIAutomaticPermissionsAssignment
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("powerbiautomaticpermissionsassignment");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PowerBIAutomaticPermissionsAssignment));
                SetAttributeValue("powerbiautomaticpermissionsassignment", value);
                OnPropertyChanged(nameof(PowerBIAutomaticPermissionsAssignment));
            }
        }

		/// <summary>
		/// Indicates whether creation of Power BI components has been enabled for the organization
		/// </summary>
		[AttributeLogicalName("powerbicomponentscreate")]
        public bool? PowerBIComponentsCreate
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("powerbicomponentscreate");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PowerBIComponentsCreate));
                SetAttributeValue("powerbicomponentscreate", value);
                OnPropertyChanged(nameof(PowerBIComponentsCreate));
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
                OnPropertyChanging(nameof(PowerBiFeatureEnabled));
                SetAttributeValue("powerbifeatureenabled", value);
                OnPropertyChanged(nameof(PowerBiFeatureEnabled));
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
                OnPropertyChanging(nameof(PricingDecimalPrecision));
                SetAttributeValue("pricingdecimalprecision", value);
                OnPropertyChanged(nameof(PricingDecimalPrecision));
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
                OnPropertyChanging(nameof(PrivacyStatementUrl));
                SetAttributeValue("privacystatementurl", value);
                OnPropertyChanged(nameof(PrivacyStatementUrl));
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
                OnPropertyChanging(nameof(PrivilegeUserGroupId));
                SetAttributeValue("privilegeusergroupid", value);
                OnPropertyChanged(nameof(PrivilegeUserGroupId));
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
                OnPropertyChanging(nameof(PrivReportingGroupId));
                SetAttributeValue("privreportinggroupid", value);
                OnPropertyChanged(nameof(PrivReportingGroupId));
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
                OnPropertyChanging(nameof(PrivReportingGroupName));
                SetAttributeValue("privreportinggroupname", value);
                OnPropertyChanged(nameof(PrivReportingGroupName));
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
                OnPropertyChanging(nameof(ProductRecommendationsEnabled));
                SetAttributeValue("productrecommendationsenabled", value);
                OnPropertyChanged(nameof(ProductRecommendationsEnabled));
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
                OnPropertyChanging(nameof(QualifyLeadAdditionalOptions));
                SetAttributeValue("qualifyleadadditionaloptions", value);
                OnPropertyChanged(nameof(QualifyLeadAdditionalOptions));
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
                OnPropertyChanging(nameof(QuickActionToOpenRecordsInSidePaneEnabled));
                SetAttributeValue("quickactiontoopenrecordsinsidepaneenabled", value);
                OnPropertyChanged(nameof(QuickActionToOpenRecordsInSidePaneEnabled));
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
                OnPropertyChanging(nameof(QuickFindRecordLimitEnabled));
                SetAttributeValue("quickfindrecordlimitenabled", value);
                OnPropertyChanged(nameof(QuickFindRecordLimitEnabled));
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
                OnPropertyChanging(nameof(QuotePrefix));
                SetAttributeValue("quoteprefix", value);
                OnPropertyChanged(nameof(QuotePrefix));
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
                OnPropertyChanging(nameof(RecalculateSLA));
                SetAttributeValue("recalculatesla", value);
                OnPropertyChanged(nameof(RecalculateSLA));
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
                OnPropertyChanging(nameof(RecurrenceDefaultNumberOfOccurrences));
                SetAttributeValue("recurrencedefaultnumberofoccurrences", value);
                OnPropertyChanged(nameof(RecurrenceDefaultNumberOfOccurrences));
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
                OnPropertyChanging(nameof(RecurrenceExpansionJobBatchInterval));
                SetAttributeValue("recurrenceexpansionjobbatchinterval", value);
                OnPropertyChanged(nameof(RecurrenceExpansionJobBatchInterval));
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
                OnPropertyChanging(nameof(RecurrenceExpansionJobBatchSize));
                SetAttributeValue("recurrenceexpansionjobbatchsize", value);
                OnPropertyChanged(nameof(RecurrenceExpansionJobBatchSize));
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
                OnPropertyChanging(nameof(RecurrenceExpansionSynchCreateMax));
                SetAttributeValue("recurrenceexpansionsynchcreatemax", value);
                OnPropertyChanged(nameof(RecurrenceExpansionSynchCreateMax));
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
                OnPropertyChanging(nameof(ReferenceSiteMapXml));
                SetAttributeValue("referencesitemapxml", value);
                OnPropertyChanged(nameof(ReferenceSiteMapXml));
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
                OnPropertyChanging(nameof(ReleaseCadence));
                SetAttributeValue("releasecadence", value);
                OnPropertyChanged(nameof(ReleaseCadence));
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
                OnPropertyChanging(nameof(ReleaseChannel));
                SetAttributeValue("releasechannel", value);
                OnPropertyChanged(nameof(ReleaseChannel));
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
                OnPropertyChanging(nameof(ReleaseWaveName));
                SetAttributeValue("releasewavename", value);
                OnPropertyChanged(nameof(ReleaseWaveName));
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
                OnPropertyChanging(nameof(RelevanceSearchEnabledByPlatform));
                SetAttributeValue("relevancesearchenabledbyplatform", value);
                OnPropertyChanged(nameof(RelevanceSearchEnabledByPlatform));
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
                OnPropertyChanging(nameof(RelevanceSearchModifiedOn));
                SetAttributeValue("relevancesearchmodifiedon", value);
                OnPropertyChanged(nameof(RelevanceSearchModifiedOn));
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
                OnPropertyChanging(nameof(RenderSecureIFrameForEmail));
                SetAttributeValue("rendersecureiframeforemail", value);
                OnPropertyChanged(nameof(RenderSecureIFrameForEmail));
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
                OnPropertyChanging(nameof(ReportingGroupId));
                SetAttributeValue("reportinggroupid", value);
                OnPropertyChanged(nameof(ReportingGroupId));
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
                OnPropertyChanging(nameof(ReportingGroupName));
                SetAttributeValue("reportinggroupname", value);
                OnPropertyChanged(nameof(ReportingGroupName));
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
                OnPropertyChanging(nameof(ReportScriptErrors));
                SetAttributeValue("reportscripterrors", value);
                OnPropertyChanged(nameof(ReportScriptErrors));
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
                OnPropertyChanging(nameof(RequireApprovalForQueueEmail));
                SetAttributeValue("requireapprovalforqueueemail", value);
                OnPropertyChanged(nameof(RequireApprovalForQueueEmail));
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
                OnPropertyChanging(nameof(RequireApprovalForUserEmail));
                SetAttributeValue("requireapprovalforuseremail", value);
                OnPropertyChanged(nameof(RequireApprovalForUserEmail));
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
                OnPropertyChanging(nameof(ResolveSimilarUnresolvedEmailAddress));
                SetAttributeValue("resolvesimilarunresolvedemailaddress", value);
                OnPropertyChanged(nameof(ResolveSimilarUnresolvedEmailAddress));
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
                OnPropertyChanging(nameof(RestrictStatusUpdate));
                SetAttributeValue("restrictstatusupdate", value);
                OnPropertyChanged(nameof(RestrictStatusUpdate));
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
                OnPropertyChanging(nameof(RiErrorStatus));
                SetAttributeValue("rierrorstatus", value);
                OnPropertyChanged(nameof(RiErrorStatus));
            }
        }

		/// <summary>
		/// Disable the option to quick create new records and activities in the Sales mobile application
		/// </summary>
		[AttributeLogicalName("salesmobilequickcreatedisabled")]
        public bool? SalesMobileQuickCreateDisabled
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("salesmobilequickcreatedisabled");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SalesMobileQuickCreateDisabled));
                SetAttributeValue("salesmobilequickcreatedisabled", value);
                OnPropertyChanged(nameof(SalesMobileQuickCreateDisabled));
            }
        }

		/// <summary>
		/// Indicates whether Sales Mobile should use UCI forms for create
		/// </summary>
		[AttributeLogicalName("salesmobileuseuciformsforcreate")]
        public bool? SalesMobileUseUCIFormsForCreate
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("salesmobileuseuciformsforcreate");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SalesMobileUseUCIFormsForCreate));
                SetAttributeValue("salesmobileuseuciformsforcreate", value);
                OnPropertyChanged(nameof(SalesMobileUseUCIFormsForCreate));
            }
        }

		/// <summary>
		/// Indicates whether Sales Mobile should use UCI forms for view
		/// </summary>
		[AttributeLogicalName("salesmobileuseuciformsforview")]
        public bool? SalesMobileUseUCIFormsForView
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("salesmobileuseuciformsforview");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SalesMobileUseUCIFormsForView));
                SetAttributeValue("salesmobileuseuciformsforview", value);
                OnPropertyChanged(nameof(SalesMobileUseUCIFormsForView));
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
                OnPropertyChanging(nameof(SampleDataImportId));
                SetAttributeValue("sampledataimportid", value);
                OnPropertyChanged(nameof(SampleDataImportId));
            }
        }

		/// <summary>
		/// Scheduling engine for Appointments and Service Activities
		/// </summary>
		[AttributeLogicalName("schedulingengine")]
        public OptionSetValue? SchedulingEngine
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("schedulingengine");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SchedulingEngine));
                SetAttributeValue("schedulingengine", value);
                OnPropertyChanged(nameof(SchedulingEngine));
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
                OnPropertyChanging(nameof(SchemaNamePrefix));
                SetAttributeValue("schemanameprefix", value);
                OnPropertyChanged(nameof(SchemaNamePrefix));
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
                OnPropertyChanging(nameof(SendBulkEmailInUCI));
                SetAttributeValue("sendbulkemailinuci", value);
                OnPropertyChanged(nameof(SendBulkEmailInUCI));
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
                OnPropertyChanging(nameof(ServeStaticResourcesFromAzureCDN));
                SetAttributeValue("servestaticresourcesfromazurecdn", value);
                OnPropertyChanged(nameof(ServeStaticResourcesFromAzureCDN));
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
                OnPropertyChanging(nameof(SessionRecordingEnabled));
                SetAttributeValue("sessionrecordingenabled", value);
                OnPropertyChanged(nameof(SessionRecordingEnabled));
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
                OnPropertyChanging(nameof(SessionTimeoutEnabled));
                SetAttributeValue("sessiontimeoutenabled", value);
                OnPropertyChanged(nameof(SessionTimeoutEnabled));
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
                OnPropertyChanging(nameof(SessionTimeoutInMins));
                SetAttributeValue("sessiontimeoutinmins", value);
                OnPropertyChanged(nameof(SessionTimeoutInMins));
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
                OnPropertyChanging(nameof(SessionTimeoutReminderInMins));
                SetAttributeValue("sessiontimeoutreminderinmins", value);
                OnPropertyChanged(nameof(SessionTimeoutReminderInMins));
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
                OnPropertyChanging(nameof(SharePointDeploymentType));
                SetAttributeValue("sharepointdeploymenttype", value);
                OnPropertyChanged(nameof(SharePointDeploymentType));
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
                OnPropertyChanging(nameof(ShareToPreviousOwnerOnAssign));
                SetAttributeValue("sharetopreviousowneronassign", value);
                OnPropertyChanged(nameof(ShareToPreviousOwnerOnAssign));
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
                OnPropertyChanging(nameof(ShowKBArticleDeprecationNotification));
                SetAttributeValue("showkbarticledeprecationnotification", value);
                OnPropertyChanged(nameof(ShowKBArticleDeprecationNotification));
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
                OnPropertyChanging(nameof(ShowWeekNumber));
                SetAttributeValue("showweeknumber", value);
                OnPropertyChanged(nameof(ShowWeekNumber));
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
                OnPropertyChanging(nameof(SignupOutlookDownloadFWLink));
                SetAttributeValue("signupoutlookdownloadfwlink", value);
                OnPropertyChanged(nameof(SignupOutlookDownloadFWLink));
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
                OnPropertyChanging(nameof(SiteMapXml));
                SetAttributeValue("sitemapxml", value);
                OnPropertyChanged(nameof(SiteMapXml));
            }
        }

		/// <summary>
		/// Indicates whether to Allow select record dialog in Enhanced Email Template.
		/// </summary>
		[AttributeLogicalName("skipselectrecorddialog")]
        public bool? SkipSelectRecordDialog
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("skipselectrecorddialog");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SkipSelectRecordDialog));
                SetAttributeValue("skipselectrecorddialog", value);
                OnPropertyChanged(nameof(SkipSelectRecordDialog));
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
                OnPropertyChanging(nameof(SlaPauseStates));
                SetAttributeValue("slapausestates", value);
                OnPropertyChanged(nameof(SlaPauseStates));
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
                OnPropertyChanging(nameof(SocialInsightsEnabled));
                SetAttributeValue("socialinsightsenabled", value);
                OnPropertyChanged(nameof(SocialInsightsEnabled));
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
                OnPropertyChanging(nameof(SocialInsightsInstance));
                SetAttributeValue("socialinsightsinstance", value);
                OnPropertyChanged(nameof(SocialInsightsInstance));
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
                OnPropertyChanging(nameof(SocialInsightsTermsAccepted));
                SetAttributeValue("socialinsightstermsaccepted", value);
                OnPropertyChanged(nameof(SocialInsightsTermsAccepted));
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
                OnPropertyChanging(nameof(SortId));
                SetAttributeValue("sortid", value);
                OnPropertyChanged(nameof(SortId));
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
                OnPropertyChanging(nameof(SqlAccessGroupId));
                SetAttributeValue("sqlaccessgroupid", value);
                OnPropertyChanged(nameof(SqlAccessGroupId));
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
                OnPropertyChanging(nameof(SqlAccessGroupName));
                SetAttributeValue("sqlaccessgroupname", value);
                OnPropertyChanged(nameof(SqlAccessGroupName));
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
                OnPropertyChanging(nameof(SQMEnabled));
                SetAttributeValue("sqmenabled", value);
                OnPropertyChanged(nameof(SQMEnabled));
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
                OnPropertyChanging(nameof(SupportUserId));
                SetAttributeValue("supportuserid", value);
                OnPropertyChanged(nameof(SupportUserId));
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
                OnPropertyChanging(nameof(SuppressSLA));
                SetAttributeValue("suppresssla", value);
                OnPropertyChanged(nameof(SuppressSLA));
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
                OnPropertyChanging(nameof(SuppressValidationEmails));
                SetAttributeValue("suppressvalidationemails", value);
                OnPropertyChanged(nameof(SuppressValidationEmails));
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
                OnPropertyChanging(nameof(SyncBulkOperationBatchSize));
                SetAttributeValue("syncbulkoperationbatchsize", value);
                OnPropertyChanged(nameof(SyncBulkOperationBatchSize));
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
                OnPropertyChanging(nameof(SyncBulkOperationMaxLimit));
                SetAttributeValue("syncbulkoperationmaxlimit", value);
                OnPropertyChanged(nameof(SyncBulkOperationMaxLimit));
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
                OnPropertyChanging(nameof(SyncOptInSelection));
                SetAttributeValue("syncoptinselection", value);
                OnPropertyChanged(nameof(SyncOptInSelection));
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
                OnPropertyChanging(nameof(SyncOptInSelectionStatus));
                SetAttributeValue("syncoptinselectionstatus", value);
                OnPropertyChanged(nameof(SyncOptInSelectionStatus));
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
                OnPropertyChanging(nameof(SystemUserId));
                SetAttributeValue("systemuserid", value);
                OnPropertyChanged(nameof(SystemUserId));
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
                OnPropertyChanging(nameof(TableScopedDVSearchInApps));
                SetAttributeValue("tablescopeddvsearchinapps", value);
                OnPropertyChanged(nameof(TableScopedDVSearchInApps));
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
                OnPropertyChanging(nameof(TagMaxAggressiveCycles));
                SetAttributeValue("tagmaxaggressivecycles", value);
                OnPropertyChanged(nameof(TagMaxAggressiveCycles));
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
                OnPropertyChanging(nameof(TagPollingPeriod));
                SetAttributeValue("tagpollingperiod", value);
                OnPropertyChanged(nameof(TagPollingPeriod));
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
                OnPropertyChanging(nameof(TaskBasedFlowEnabled));
                SetAttributeValue("taskbasedflowenabled", value);
                OnPropertyChanged(nameof(TaskBasedFlowEnabled));
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
                OnPropertyChanging(nameof(TeamsChatDataSync));
                SetAttributeValue("teamschatdatasync", value);
                OnPropertyChanged(nameof(TeamsChatDataSync));
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
                OnPropertyChanging(nameof(TelemetryInstrumentationKey));
                SetAttributeValue("telemetryinstrumentationkey", value);
                OnPropertyChanged(nameof(TelemetryInstrumentationKey));
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
                OnPropertyChanging(nameof(TextAnalyticsEnabled));
                SetAttributeValue("textanalyticsenabled", value);
                OnPropertyChanged(nameof(TextAnalyticsEnabled));
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
                OnPropertyChanging(nameof(TimeFormatCode));
                SetAttributeValue("timeformatcode", value);
                OnPropertyChanged(nameof(TimeFormatCode));
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
                OnPropertyChanging(nameof(TimeFormatString));
                SetAttributeValue("timeformatstring", value);
                OnPropertyChanged(nameof(TimeFormatString));
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
                OnPropertyChanging(nameof(TimeSeparator));
                SetAttributeValue("timeseparator", value);
                OnPropertyChanged(nameof(TimeSeparator));
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
                OnPropertyChanging(nameof(TokenExpiry));
                SetAttributeValue("tokenexpiry", value);
                OnPropertyChanged(nameof(TokenExpiry));
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
                OnPropertyChanging(nameof(TokenKey));
                SetAttributeValue("tokenkey", value);
                OnPropertyChanged(nameof(TokenKey));
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
                OnPropertyChanging(nameof(TraceLogMaximumAgeInDays));
                SetAttributeValue("tracelogmaximumageindays", value);
                OnPropertyChanged(nameof(TraceLogMaximumAgeInDays));
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
                OnPropertyChanging(nameof(TrackingPrefix));
                SetAttributeValue("trackingprefix", value);
                OnPropertyChanged(nameof(TrackingPrefix));
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
                OnPropertyChanging(nameof(TrackingTokenIdBase));
                SetAttributeValue("trackingtokenidbase", value);
                OnPropertyChanged(nameof(TrackingTokenIdBase));
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
                OnPropertyChanging(nameof(TrackingTokenIdDigits));
                SetAttributeValue("trackingtokeniddigits", value);
                OnPropertyChanged(nameof(TrackingTokenIdDigits));
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
                OnPropertyChanging(nameof(UniqueSpecifierLength));
                SetAttributeValue("uniquespecifierlength", value);
                OnPropertyChanged(nameof(UniqueSpecifierLength));
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
                OnPropertyChanging(nameof(UnresolveEmailAddressIfMultipleMatch));
                SetAttributeValue("unresolveemailaddressifmultiplematch", value);
                OnPropertyChanged(nameof(UnresolveEmailAddressIfMultipleMatch));
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
                OnPropertyChanging(nameof(UseInbuiltRuleForDefaultPricelistSelection));
                SetAttributeValue("useinbuiltrulefordefaultpricelistselection", value);
                OnPropertyChanged(nameof(UseInbuiltRuleForDefaultPricelistSelection));
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
                OnPropertyChanging(nameof(UseLegacyRendering));
                SetAttributeValue("uselegacyrendering", value);
                OnPropertyChanged(nameof(UseLegacyRendering));
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
                OnPropertyChanging(nameof(UsePositionHierarchy));
                SetAttributeValue("usepositionhierarchy", value);
                OnPropertyChanged(nameof(UsePositionHierarchy));
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
                OnPropertyChanging(nameof(UseQuickFindViewForGridSearch));
                SetAttributeValue("usequickfindviewforgridsearch", value);
                OnPropertyChanged(nameof(UseQuickFindViewForGridSearch));
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
                OnPropertyChanging(nameof(UserAccessAuditingInterval));
                SetAttributeValue("useraccessauditinginterval", value);
                OnPropertyChanged(nameof(UserAccessAuditingInterval));
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
                OnPropertyChanging(nameof(UseReadForm));
                SetAttributeValue("usereadform", value);
                OnPropertyChanged(nameof(UseReadForm));
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
                OnPropertyChanging(nameof(UserGroupId));
                SetAttributeValue("usergroupid", value);
                OnPropertyChanged(nameof(UserGroupId));
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
                OnPropertyChanging(nameof(UserRatingEnabled));
                SetAttributeValue("userratingenabled", value);
                OnPropertyChanged(nameof(UserRatingEnabled));
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
                OnPropertyChanging(nameof(UseSkypeProtocol));
                SetAttributeValue("useskypeprotocol", value);
                OnPropertyChanged(nameof(UseSkypeProtocol));
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
                OnPropertyChanging(nameof(ValidationMode));
                SetAttributeValue("validationmode", value);
                OnPropertyChanged(nameof(ValidationMode));
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
                OnPropertyChanging(nameof(WebResourceHash));
                SetAttributeValue("webresourcehash", value);
                OnPropertyChanged(nameof(WebResourceHash));
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
                OnPropertyChanging(nameof(WeekStartDayCode));
                SetAttributeValue("weekstartdaycode", value);
                OnPropertyChanged(nameof(WeekStartDayCode));
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
                OnPropertyChanging(nameof(WidgetProperties));
                SetAttributeValue("widgetproperties", value);
                OnPropertyChanged(nameof(WidgetProperties));
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
                OnPropertyChanging(nameof(YammerGroupId));
                SetAttributeValue("yammergroupid", value);
                OnPropertyChanged(nameof(YammerGroupId));
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
                OnPropertyChanging(nameof(YammerNetworkPermalink));
                SetAttributeValue("yammernetworkpermalink", value);
                OnPropertyChanged(nameof(YammerNetworkPermalink));
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
                OnPropertyChanging(nameof(YammerOAuthAccessTokenExpired));
                SetAttributeValue("yammeroauthaccesstokenexpired", value);
                OnPropertyChanged(nameof(YammerOAuthAccessTokenExpired));
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
                OnPropertyChanging(nameof(YammerPostMethod));
                SetAttributeValue("yammerpostmethod", value);
                OnPropertyChanged(nameof(YammerPostMethod));
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
                OnPropertyChanging(nameof(YearStartWeekCode));
                SetAttributeValue("yearstartweekcode", value);
                OnPropertyChanged(nameof(YearStartWeekCode));
            }
        }


		#endregion

		#region NavigationProperties
		/// <summary>
		/// 1:N lk_documenttemplatebase_organization
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_documenttemplatebase_organization")]
		public System.Collections.Generic.IEnumerable<DocumentTemplate> LkDocumenttemplatebaseOrganization
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DocumentTemplate>("lk_documenttemplatebase_organization", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkDocumenttemplatebaseOrganization");
				this.SetRelatedEntities<DocumentTemplate>("lk_documenttemplatebase_organization", null, value);
				this.OnPropertyChanged("LkDocumenttemplatebaseOrganization");
			}
		}

		/// <summary>
		/// 1:N Organization_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("Organization_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> OrganizationAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("Organization_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("Organization_AsyncOperations", null, value);
				this.OnPropertyChanged("OrganizationAsyncOperations");
			}
		}

		/// <summary>
		/// 1:N organization_business_units
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_business_units")]
		public System.Collections.Generic.IEnumerable<BusinessUnit> OrganizationBusinessUnits
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<BusinessUnit>("organization_business_units", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationBusinessUnits");
				this.SetRelatedEntities<BusinessUnit>("organization_business_units", null, value);
				this.OnPropertyChanged("OrganizationBusinessUnits");
			}
		}

		/// <summary>
		/// 1:N organization_calendars
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_calendars")]
		public System.Collections.Generic.IEnumerable<Calendar> OrganizationCalendars
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Calendar>("organization_calendars", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationCalendars");
				this.SetRelatedEntities<Calendar>("organization_calendars", null, value);
				this.OnPropertyChanged("OrganizationCalendars");
			}
		}

		/// <summary>
		/// 1:N organization_importjob
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_importjob")]
		public System.Collections.Generic.IEnumerable<ImportJob> OrganizationImportjob
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<ImportJob>("organization_importjob", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationImportjob");
				this.SetRelatedEntities<ImportJob>("organization_importjob", null, value);
				this.OnPropertyChanged("OrganizationImportjob");
			}
		}

		/// <summary>
		/// 1:N organization_pluginassembly
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_pluginassembly")]
		public System.Collections.Generic.IEnumerable<PluginAssembly> OrganizationPluginassembly
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<PluginAssembly>("organization_pluginassembly", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationPluginassembly");
				this.SetRelatedEntities<PluginAssembly>("organization_pluginassembly", null, value);
				this.OnPropertyChanged("OrganizationPluginassembly");
			}
		}

		/// <summary>
		/// 1:N organization_pluginpackage
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_pluginpackage")]
		public System.Collections.Generic.IEnumerable<PluginPackage> OrganizationPluginpackage
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<PluginPackage>("organization_pluginpackage", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationPluginpackage");
				this.SetRelatedEntities<PluginPackage>("organization_pluginpackage", null, value);
				this.OnPropertyChanged("OrganizationPluginpackage");
			}
		}

		/// <summary>
		/// 1:N organization_plugintype
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_plugintype")]
		public System.Collections.Generic.IEnumerable<PluginType> OrganizationPlugintype
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<PluginType>("organization_plugintype", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationPlugintype");
				this.SetRelatedEntities<PluginType>("organization_plugintype", null, value);
				this.OnPropertyChanged("OrganizationPlugintype");
			}
		}

		/// <summary>
		/// 1:N organization_publisher
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_publisher")]
		public System.Collections.Generic.IEnumerable<Publisher> OrganizationPublisher
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Publisher>("organization_publisher", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationPublisher");
				this.SetRelatedEntities<Publisher>("organization_publisher", null, value);
				this.OnPropertyChanged("OrganizationPublisher");
			}
		}

		/// <summary>
		/// 1:N organization_queues
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_queues")]
		public System.Collections.Generic.IEnumerable<Queue> OrganizationQueues
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Queue>("organization_queues", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationQueues");
				this.SetRelatedEntities<Queue>("organization_queues", null, value);
				this.OnPropertyChanged("OrganizationQueues");
			}
		}

		/// <summary>
		/// 1:N organization_roles
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_roles")]
		public System.Collections.Generic.IEnumerable<Role> OrganizationRoles
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Role>("organization_roles", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationRoles");
				this.SetRelatedEntities<Role>("organization_roles", null, value);
				this.OnPropertyChanged("OrganizationRoles");
			}
		}

		/// <summary>
		/// 1:N organization_routingruleitems
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_routingruleitems")]
		public System.Collections.Generic.IEnumerable<RoutingRuleItem> OrganizationRoutingruleitems
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRuleItem>("organization_routingruleitems", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationRoutingruleitems");
				this.SetRelatedEntities<RoutingRuleItem>("organization_routingruleitems", null, value);
				this.OnPropertyChanged("OrganizationRoutingruleitems");
			}
		}

		/// <summary>
		/// 1:N organization_RoutingRules
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_RoutingRules")]
		public System.Collections.Generic.IEnumerable<RoutingRule> OrganizationRoutingRules
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRule>("organization_RoutingRules", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationRoutingRules");
				this.SetRelatedEntities<RoutingRule>("organization_RoutingRules", null, value);
				this.OnPropertyChanged("OrganizationRoutingRules");
			}
		}

		/// <summary>
		/// 1:N organization_saved_queries
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_saved_queries")]
		public System.Collections.Generic.IEnumerable<SavedQuery> OrganizationSavedQueries
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SavedQuery>("organization_saved_queries", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationSavedQueries");
				this.SetRelatedEntities<SavedQuery>("organization_saved_queries", null, value);
				this.OnPropertyChanged("OrganizationSavedQueries");
			}
		}

		/// <summary>
		/// 1:N organization_sdkmessage
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_sdkmessage")]
		public System.Collections.Generic.IEnumerable<SdkMessage> OrganizationSdkmessage
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessage>("organization_sdkmessage", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationSdkmessage");
				this.SetRelatedEntities<SdkMessage>("organization_sdkmessage", null, value);
				this.OnPropertyChanged("OrganizationSdkmessage");
			}
		}

		/// <summary>
		/// 1:N organization_sdkmessagefilter
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_sdkmessagefilter")]
		public System.Collections.Generic.IEnumerable<SdkMessageFilter> OrganizationSdkmessagefilter
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageFilter>("organization_sdkmessagefilter", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationSdkmessagefilter");
				this.SetRelatedEntities<SdkMessageFilter>("organization_sdkmessagefilter", null, value);
				this.OnPropertyChanged("OrganizationSdkmessagefilter");
			}
		}

		/// <summary>
		/// 1:N organization_sdkmessageprocessingstep
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_sdkmessageprocessingstep")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStep> OrganizationSdkmessageprocessingstep
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStep>("organization_sdkmessageprocessingstep", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationSdkmessageprocessingstep");
				this.SetRelatedEntities<SdkMessageProcessingStep>("organization_sdkmessageprocessingstep", null, value);
				this.OnPropertyChanged("OrganizationSdkmessageprocessingstep");
			}
		}

		/// <summary>
		/// 1:N organization_sdkmessageprocessingstepimage
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_sdkmessageprocessingstepimage")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStepImage> OrganizationSdkmessageprocessingstepimage
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStepImage>("organization_sdkmessageprocessingstepimage", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationSdkmessageprocessingstepimage");
				this.SetRelatedEntities<SdkMessageProcessingStepImage>("organization_sdkmessageprocessingstepimage", null, value);
				this.OnPropertyChanged("OrganizationSdkmessageprocessingstepimage");
			}
		}

		/// <summary>
		/// 1:N organization_sdkmessageprocessingstepsecureconfig
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_sdkmessageprocessingstepsecureconfig")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStepSecureConfig> OrganizationSdkmessageprocessingstepsecureconfig
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStepSecureConfig>("organization_sdkmessageprocessingstepsecureconfig", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationSdkmessageprocessingstepsecureconfig");
				this.SetRelatedEntities<SdkMessageProcessingStepSecureConfig>("organization_sdkmessageprocessingstepsecureconfig", null, value);
				this.OnPropertyChanged("OrganizationSdkmessageprocessingstepsecureconfig");
			}
		}

		/// <summary>
		/// 1:N organization_solution
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_solution")]
		public System.Collections.Generic.IEnumerable<Solution> OrganizationSolution
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Solution>("organization_solution", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationSolution");
				this.SetRelatedEntities<Solution>("organization_solution", null, value);
				this.OnPropertyChanged("OrganizationSolution");
			}
		}

		/// <summary>
		/// 1:N organization_system_users
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_system_users")]
		public System.Collections.Generic.IEnumerable<SystemUser> OrganizationSystemUsers
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SystemUser>("organization_system_users", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationSystemUsers");
				this.SetRelatedEntities<SystemUser>("organization_system_users", null, value);
				this.OnPropertyChanged("OrganizationSystemUsers");
			}
		}

		/// <summary>
		/// 1:N organization_systemforms
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_systemforms")]
		public System.Collections.Generic.IEnumerable<SystemForm> OrganizationSystemforms
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SystemForm>("organization_systemforms", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationSystemforms");
				this.SetRelatedEntities<SystemForm>("organization_systemforms", null, value);
				this.OnPropertyChanged("OrganizationSystemforms");
			}
		}

		/// <summary>
		/// 1:N organization_teams
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_teams")]
		public System.Collections.Generic.IEnumerable<Team> OrganizationTeams
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Team>("organization_teams", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("OrganizationTeams");
				this.SetRelatedEntities<Team>("organization_teams", null, value);
				this.OnPropertyChanged("OrganizationTeams");
			}
		}

		/// <summary>
		/// 1:N webresource_organization
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("webresource_organization")]
		public System.Collections.Generic.IEnumerable<WebResource> WebresourceOrganization
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<WebResource>("webresource_organization", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("WebresourceOrganization");
				this.SetRelatedEntities<WebResource>("webresource_organization", null, value);
				this.OnPropertyChanged("WebresourceOrganization");
			}
		}

		#endregion

		#region Options
		public static class Options
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
                public struct DefaultTeamsChatTitleRecordName
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct DisableSocialCare
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
			    public struct EmailTemplateDefaultView
                {
					public const int TilesView = 1;
					public const int GridView = 2;
					public const int ListView = 3;
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
                public struct EnableCalendarImportExport
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct EnableCanvasAppsInSolutionsByDefault
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct EnableEmailTemplateViews
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct EnableFlowsInSolutionByDefault
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
                public struct EnableSensitivityLabelsForTeamsCollab
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
                public struct EnforceValidations
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
			    public struct FiscalPeriodFormatPeriod
                {
					public const int Quarter_0_ = 1;
					public const int Q_0_ = 2;
					public const int P_0_ = 3;
					public const int Month_0_ = 4;
					public const int M_0_ = 5;
					public const int Semester_0_ = 6;
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
					public const int LastName_FirstName = 0;
					public const int FirstName = 1;
					public const int LastName_FirstName_MiddleInitial = 2;
					public const int FirstName_MiddleInitial_LastName = 3;
					public const int LastName_FirstName_MiddleName = 4;
					public const int FirstName_MiddleName_LastName = 5;
					public const int LastName_Space_FirstName = 6;
					public const int LastName_NoSpace_FirstName = 7;
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
                public struct IsCollaborationExperienceEnabled
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
                public struct IsDesktopFlowSchemaV2Enabled
                {
                    public const bool No = false;
                    public const bool Yes = true;
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
                public struct IsEmbedTeamsCollabEnabled
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
                public struct IsSalesMobilePreviewEnabled
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
					public const int Web_OutlookWorkstationClient = 3;
					public const int OutlookLaptopClient = 4;
					public const int Web_OutlookLaptopClient = 5;
					public const int Outlook = 6;
					public const int All = 7;
                }
                public struct IsWriteInProductsAllowed
                {
                    public const bool No = false;
                    public const bool Yes = true;
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
					public const int SemiAnnualChannel = 0;
					public const int MonthlyChannel = 1;
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
                public struct RestrictStatusUpdate
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct SalesMobileQuickCreateDisabled
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct SalesMobileUseUCIFormsForCreate
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct SalesMobileUseUCIFormsForView
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
			    public struct SchedulingEngine
                {
					public const int LegacySchedulingEngine = 0;
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
                public struct SkipSelectRecordDialog
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
		public static class LogicalNames
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
				public const string AllowAddressBookSyncs = "allowaddressbooksyncs";
				public const string AllowApplicationUserAccess = "allowapplicationuseraccess";
				public const string AllowAutoResponseCreation = "allowautoresponsecreation";
				public const string AllowAutoUnsubscribe = "allowautounsubscribe";
				public const string AllowAutoUnsubscribeAcknowledgement = "allowautounsubscribeacknowledgement";
				public const string AllowClientMessageBarAd = "allowclientmessagebarad";
				public const string AllowedIpRangeForFirewall = "allowediprangeforfirewall";
				public const string AllowedIpRangeForStorageAccessSignatures = "allowediprangeforstorageaccesssignatures";
				public const string AllowedMimeTypes = "allowedmimetypes";
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
				public const string AllowWebExcelExport = "allowwebexcelexport";
				public const string AMDesignator = "amdesignator";
				public const string AppDesignerExperienceEnabled = "appdesignerexperienceenabled";
				public const string AppointmentRichEditorExperience = "appointmentricheditorexperience";
				public const string AppointmentWithTeamsMeeting = "appointmentwithteamsmeeting";
				public const string AppointmentWithTeamsMeetingV2 = "appointmentwithteamsmeetingv2";
				public const string AuditRetentionPeriod = "auditretentionperiod";
				public const string AuditRetentionPeriodV2 = "auditretentionperiodv2";
				public const string AutoApplyDefaultonCaseCreate = "autoapplydefaultoncasecreate";
				public const string AutoApplyDefaultonCaseUpdate = "autoapplydefaultoncaseupdate";
				public const string AutoApplySLA = "autoapplysla";
				public const string AzureSchedulerJobCollectionName = "azureschedulerjobcollectionname";
				public const string BaseCurrencyId = "basecurrencyid";
				public const string BaseCurrencyPrecision = "basecurrencyprecision";
				public const string BaseCurrencySymbol = "basecurrencysymbol";
				public const string BingMapsApiKey = "bingmapsapikey";
				public const string BlockedAttachments = "blockedattachments";
				public const string BlockedMimeTypes = "blockedmimetypes";
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
				public const string ContentSecurityPolicyReportUri = "contentsecuritypolicyreporturi";
				public const string ContractPrefix = "contractprefix";
				public const string CopresenceRefreshRate = "copresencerefreshrate";
				public const string CortanaProactiveExperienceEnabled = "cortanaproactiveexperienceenabled";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string CreateProductsWithoutParentInActiveState = "createproductswithoutparentinactivestate";
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
				public const string DaysBeforeInactiveTeamsChatSyncDisabled = "daysbeforeinactiveteamschatsyncdisabled";
				public const string DaysSinceRecordLastModifiedMaxValue = "dayssincerecordlastmodifiedmaxvalue";
				public const string DecimalSymbol = "decimalsymbol";
				public const string DefaultCountryCode = "defaultcountrycode";
				public const string DefaultCrmCustomName = "defaultcrmcustomname";
				public const string DefaultEmailServerProfileId = "defaultemailserverprofileid";
				public const string DefaultEmailSettings = "defaultemailsettings";
				public const string DefaultMobileOfflineProfileId = "defaultmobileofflineprofileid";
				public const string DefaultRecurrenceEndRangeType = "defaultrecurrenceendrangetype";
				public const string DefaultRecurrenceEndRangeTypeName = "defaultrecurrenceendrangetypename";
				public const string DefaultTeamsChatTitleRecordName = "defaultteamschattitlerecordname";
				public const string DefaultThemeData = "defaultthemedata";
				public const string DelegatedAdminUserId = "delegatedadminuserid";
				public const string DisabledReason = "disabledreason";
				public const string DisableSocialCare = "disablesocialcare";
				public const string DiscountCalculationMethod = "discountcalculationmethod";
				public const string DisplayNavigationTour = "displaynavigationtour";
				public const string EmailConnectionChannel = "emailconnectionchannel";
				public const string EmailCorrelationEnabled = "emailcorrelationenabled";
				public const string EmailSendPollingPeriod = "emailsendpollingperiod";
				public const string EmailTemplateDefaultView = "emailtemplatedefaultview";
				public const string EnableAsyncMergeAPIForUCI = "enableasyncmergeapiforuci";
				public const string EnableBingMapsIntegration = "enablebingmapsintegration";
				public const string EnableCalendarImportExport = "enablecalendarimportexport";
				public const string EnableCanvasAppsInSolutionsByDefault = "enablecanvasappsinsolutionsbydefault";
				public const string EnableEmailTemplateViews = "enableemailtemplateviews";
				public const string EnableFlowsInSolutionByDefault = "enableflowsinsolutionbydefault";
				public const string EnableImmersiveSkypeIntegration = "enableimmersiveskypeintegration";
				public const string EnableIpBasedCookieBinding = "enableipbasedcookiebinding";
				public const string EnableIpBasedFirewallRule = "enableipbasedfirewallrule";
				public const string EnableIpBasedStorageAccessSignatureRule = "enableipbasedstorageaccesssignaturerule";
				public const string EnableLivePersonaCardUCI = "enablelivepersonacarduci";
				public const string EnableLivePersonCardIntegrationInOffice = "enablelivepersoncardintegrationinoffice";
				public const string EnableLPAuthoring = "enablelpauthoring";
				public const string EnableMakerSwitchToClassic = "enablemakerswitchtoclassic";
				public const string EnableMicrosoftFlowIntegration = "enablemicrosoftflowintegration";
				public const string EnablePricingOnCreate = "enablepricingoncreate";
				public const string EnableSensitivityLabelsForTeamsCollab = "enablesensitivitylabelsforteamscollab";
				public const string EnableSmartMatching = "enablesmartmatching";
				public const string EnableUnifiedClientCDN = "enableunifiedclientcdn";
				public const string EnableUnifiedInterfaceShellRefresh = "enableunifiedinterfaceshellrefresh";
				public const string EnforceReadOnlyPlugins = "enforcereadonlyplugins";
				public const string EnforceValidations = "enforcevalidations";
				public const string EnhancedOQOIAddProductsSettings = "enhancedoqoiaddproductssettings";
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
				public const string IsAutoSaveEnabled = "isautosaveenabled";
				public const string IsBaseCardStaticFieldDataEnabled = "isbasecardstaticfielddataenabled";
				public const string IsBasicGeospatialIntegrationEnabled = "isbasicgeospatialintegrationenabled";
				public const string IsBPFEntityCustomizationFeatureEnabled = "isbpfentitycustomizationfeatureenabled";
				public const string IsCollaborationExperienceEnabled = "iscollaborationexperienceenabled";
				public const string IsConflictDetectionEnabledForMobileClient = "isconflictdetectionenabledformobileclient";
				public const string IsContactMailingAddressSyncEnabled = "iscontactmailingaddresssyncenabled";
				public const string IsContentSecurityPolicyEnabled = "iscontentsecuritypolicyenabled";
				public const string IsContentSecurityPolicyEnabledForCanvas = "iscontentsecuritypolicyenabledforcanvas";
				public const string IsContextualEmailEnabled = "iscontextualemailenabled";
				public const string IsContextualHelpEnabled = "iscontextualhelpenabled";
				public const string IsCustomControlsInCanvasAppsEnabled = "iscustomcontrolsincanvasappsenabled";
				public const string IsDefaultCountryCodeCheckEnabled = "isdefaultcountrycodecheckenabled";
				public const string IsDelegateAccessEnabled = "isdelegateaccessenabled";
				public const string IsDelveActionHubIntegrationEnabled = "isdelveactionhubintegrationenabled";
				public const string IsDesktopFlowSchemaV2Enabled = "isdesktopflowschemav2enabled";
				public const string IsDisabled = "isdisabled";
				public const string IsDuplicateDetectionEnabled = "isduplicatedetectionenabled";
				public const string IsDuplicateDetectionEnabledForImport = "isduplicatedetectionenabledforimport";
				public const string IsDuplicateDetectionEnabledForOfflineSync = "isduplicatedetectionenabledforofflinesync";
				public const string IsDuplicateDetectionEnabledForOnlineCreateUpdate = "isduplicatedetectionenabledforonlinecreateupdate";
				public const string IsEmailMonitoringAllowed = "isemailmonitoringallowed";
				public const string IsEmailServerProfileContentFilteringEnabled = "isemailserverprofilecontentfilteringenabled";
				public const string IsEmbedTeamsCollabEnabled = "isembedteamscollabenabled";
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
				public const string IsMSTeamsCollaborationEnabled = "ismsteamscollaborationenabled";
				public const string IsMSTeamsEnabled = "ismsteamsenabled";
				public const string IsMSTeamsSettingChangedByUser = "ismsteamssettingchangedbyuser";
				public const string IsMSTeamsUserSyncEnabled = "ismsteamsusersyncenabled";
				public const string IsNewAddProductExperienceEnabled = "isnewaddproductexperienceenabled";
				public const string IsNotesAnalysisEnabled = "isnotesanalysisenabled";
				public const string IsOfficeGraphEnabled = "isofficegraphenabled";
				public const string IsOneDriveEnabled = "isonedriveenabled";
				public const string IsPAIEnabled = "ispaienabled";
				public const string IsPDFGenerationEnabled = "ispdfgenerationenabled";
				public const string IsPlaybookEnabled = "isplaybookenabled";
				public const string IsPresenceEnabled = "ispresenceenabled";
				public const string IsPresenceEnabledName = "ispresenceenabledname";
				public const string IsPreviewEnabledForActionCard = "ispreviewenabledforactioncard";
				public const string IsPreviewForAutoCaptureEnabled = "ispreviewforautocaptureenabled";
				public const string IsPreviewForEmailMonitoringAllowed = "ispreviewforemailmonitoringallowed";
				public const string IsPriceListMandatory = "ispricelistmandatory";
				public const string IsQuickCreateEnabledForOpportunityClose = "isquickcreateenabledforopportunityclose";
				public const string IsReadAuditEnabled = "isreadauditenabled";
				public const string IsRelationshipInsightsEnabled = "isrelationshipinsightsenabled";
				public const string IsResourceBookingExchangeSyncEnabled = "isresourcebookingexchangesyncenabled";
				public const string IsRichTextNotesEnabled = "isrichtextnotesenabled";
				public const string IsRpaAutoscaleAadJoinEnabled = "isrpaautoscaleaadjoinenabled";
				public const string IsRpaAutoscaleEnabled = "isrpaautoscaleenabled";
				public const string IsRpaBoxEnabled = "isrpaboxenabled";
				public const string IsRpaUnattendedEnabled = "isrpaunattendedenabled";
				public const string IsSalesAssistantEnabled = "issalesassistantenabled";
				public const string IsSalesMobilePreviewEnabled = "issalesmobilepreviewenabled";
				public const string IsSharingInOrgAllowed = "issharinginorgallowed";
				public const string IsSOPIntegrationEnabled = "issopintegrationenabled";
				public const string IsTextWrapEnabled = "istextwrapenabled";
				public const string IsUserAccessAuditEnabled = "isuseraccessauditenabled";
				public const string ISVIntegrationCode = "isvintegrationcode";
				public const string IsWriteInProductsAllowed = "iswriteinproductsallowed";
				public const string KaPrefix = "kaprefix";
				public const string KbPrefix = "kbprefix";
				public const string KMSettings = "kmsettings";
				public const string LanguageCode = "languagecode";
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
				public const string OrderPrefix = "orderprefix";
				public const string OrganizationState = "organizationstate";
				public const string OrgDbOrgSettings = "orgdborgsettings";
				public const string OrgInsightsEnabled = "orginsightsenabled";
				public const string PaiPreviewScenarioEnabled = "paipreviewscenarioenabled";
				public const string ParsedTableColumnPrefix = "parsedtablecolumnprefix";
				public const string ParsedTablePrefix = "parsedtableprefix";
				public const string PastExpansionWindow = "pastexpansionwindow";
				public const string PcfDatasetGridEnabled = "pcfdatasetgridenabled";
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
				public const string RestrictStatusUpdate = "restrictstatusupdate";
				public const string RiErrorStatus = "rierrorstatus";
				public const string SalesMobileQuickCreateDisabled = "salesmobilequickcreatedisabled";
				public const string SalesMobileUseUCIFormsForCreate = "salesmobileuseuciformsforcreate";
				public const string SalesMobileUseUCIFormsForView = "salesmobileuseuciformsforview";
				public const string SampleDataImportId = "sampledataimportid";
				public const string SchedulingEngine = "schedulingengine";
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
				public const string SkipSelectRecordDialog = "skipselectrecorddialog";
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
        public static class Relations
        {
            public static class OneToMany
            {
				public const string ChannelpropertyOrganization = "channelproperty_organization";
				public const string ChannelpropertygroupOrganization = "channelpropertygroup_organization";
				public const string CustomcontrolOrganization = "customcontrol_organization";
				public const string CustomcontroldefaultconfigOrganization = "customcontroldefaultconfig_organization";
				public const string CustomcontrolresourceOrganization = "customcontrolresource_organization";
				public const string DynamicpropertyOrganization = "dynamicproperty_organization";
				public const string DynamicPropertyAssociationOrganization = "DynamicPropertyAssociation_organization";
				public const string DynamicPropertyOptionSetItemOrganization = "DynamicPropertyOptionSetItem_organization";
				public const string EntitlementchannelOrganization = "entitlementchannel_organization";
				public const string EntitlementtemplateOrganization = "entitlementtemplate_organization";
				public const string EntitlementtemplatechannelOrganization = "entitlementtemplatechannel_organization";
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
				public const string OrganizationAppaction = "organization_appaction";
				public const string OrganizationAppactionmigration = "organization_appactionmigration";
				public const string OrganizationAppactionrule = "organization_appactionrule";
				public const string OrganizationAppconfig = "organization_appconfig";
				public const string OrganizationAppconfiginstance = "organization_appconfiginstance";
				public const string OrganizationAppconfigmaster = "organization_appconfigmaster";
				public const string OrganizationAppelement = "organization_appelement";
				public const string OrganizationApplicationfile = "organization_applicationfile";
				public const string OrganizationAppmodule = "organization_appmodule";
				public const string OrganizationAppmodulecomponentedge = "organization_appmodulecomponentedge";
				public const string OrganizationAppmodulecomponentnode = "organization_appmodulecomponentnode";
				public const string OrganizationAppsetting = "organization_appsetting";
				public const string OrganizationAppusersetting = "organization_appusersetting";
				public const string OrganizationAsyncOperations = "Organization_AsyncOperations";
				public const string OrganizationAttributemap = "organization_attributemap";
				public const string OrganizationAzureserviceconnection = "organization_azureserviceconnection";
				public const string OrganizationBulkDeleteFailures = "Organization_BulkDeleteFailures";
				public const string OrganizationBusinessUnitNewsArticles = "organization_business_unit_news_articles";
				public const string OrganizationBusinessUnits = "organization_business_units";
				public const string OrganizationCalendars = "organization_calendars";
				public const string OrganizationCatalog = "organization_catalog";
				public const string OrganizationCatalogassignment = "organization_catalogassignment";
				public const string OrganizationCompetitors = "organization_competitors";
				public const string OrganizationComplexcontrols = "organization_complexcontrols";
				public const string OrganizationConnectionRoles = "organization_connection_roles";
				public const string OrganizationConstraintBasedGroups = "organization_constraint_based_groups";
				public const string OrganizationContractTemplates = "organization_contract_templates";
				public const string OrganizationCustomDisplaystrings = "organization_custom_displaystrings";
				public const string OrganizationDatalakeworkspace = "organization_datalakeworkspace";
				public const string OrganizationDatalakeworkspacepermission = "organization_datalakeworkspacepermission";
				public const string OrganizationDataprocessingconfiguration = "organization_dataprocessingconfiguration";
				public const string OrganizationDelveactionhub = "organization_delveactionhub";
				public const string OrganizationDiscountTypes = "organization_discount_types";
				public const string OrganizationEc4uGdprBpfCorrection = "organization_ec4u_gdpr_bpf_correction";
				public const string OrganizationEc4uGdprBpfDeletion = "organization_ec4u_gdpr_bpf_deletion";
				public const string OrganizationEc4uGdprBpfInformation = "organization_ec4u_gdpr_bpf_information";
				public const string OrganizationEc4uGdprConfigEntity = "organization_ec4u_gdpr_config_entity";
				public const string OrganizationEc4uGdprConfigField = "organization_ec4u_gdpr_config_field";
				public const string OrganizationEc4uGdprConfigHierarchy = "organization_ec4u_gdpr_config_hierarchy";
				public const string OrganizationEc4uLegalbasistype = "organization_ec4u_legalbasistype";
				public const string OrganizationEmailserverprofile = "organization_emailserverprofile";
				public const string OrganizationEntityanalyticsconfig = "organization_entityanalyticsconfig";
				public const string OrganizationEntitydataprovider = "organization_entitydataprovider";
				public const string OrganizationEntitydatasource = "organization_entitydatasource";
				public const string OrganizationEntitymap = "organization_entitymap";
				public const string OrganizationEntityrecordfilter = "organization_entityrecordfilter";
				public const string OrganizationEquipment = "organization_equipment";
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
				public const string OrganizationLeadtoopportunitysalesprocess = "organization_leadtoopportunitysalesprocess";
				public const string OrganizationLicenses = "organization_licenses";
				public const string OrganizationMailbox = "organization_mailbox";
				public const string OrganizationMailboxstatistics = "organization_mailboxstatistics";
				public const string OrganizationMailboxTrackingFolder = "Organization_MailboxTrackingFolder";
				public const string OrganizationMarketingformdisplayattributes = "organization_marketingformdisplayattributes";
				public const string OrganizationMetric = "organization_metric";
				public const string OrganizationMobileofflineprofileextension = "organization_mobileofflineprofileextension";
				public const string OrganizationMsdynAccountkpiitem = "organization_msdyn_accountkpiitem";
				public const string OrganizationMsdynActivityanalysiscleanupstate = "organization_msdyn_activityanalysiscleanupstate";
				public const string OrganizationMsdynActivityanalysisconfig = "organization_msdyn_activityanalysisconfig";
				public const string OrganizationMsdynAdaptivecardconfiguration = "organization_msdyn_adaptivecardconfiguration";
				public const string OrganizationMsdynAgentresourceforecasting = "organization_msdyn_agentresourceforecasting";
				public const string OrganizationMsdynAppinsightsmetadata = "organization_msdyn_appinsightsmetadata";
				public const string OrganizationMsdynAttributeinfluencestatistics = "organization_msdyn_attributeinfluencestatistics";
				public const string OrganizationMsdynCannedmessage = "organization_msdyn_cannedmessage";
				public const string OrganizationMsdynCaseenrichment = "organization_msdyn_caseenrichment";
				public const string OrganizationMsdynCasesuggestionrequestpayload = "organization_msdyn_casesuggestionrequestpayload";
				public const string OrganizationMsdynCasetopic = "organization_msdyn_casetopic";
				public const string OrganizationMsdynCasetopicIncident = "organization_msdyn_casetopic_incident";
				public const string OrganizationMsdynCasetopicsetting = "organization_msdyn_casetopicsetting";
				public const string OrganizationMsdynCasetopicsummary = "organization_msdyn_casetopicsummary";
				public const string OrganizationMsdynChannelcapability = "organization_msdyn_channelcapability";
				public const string OrganizationMsdynCiprovider = "organization_msdyn_ciprovider";
				public const string OrganizationMsdynConsoleapplicationtype = "organization_msdyn_consoleapplicationtype";
				public const string OrganizationMsdynConsoleappparameterdefinition = "organization_msdyn_consoleappparameterdefinition";
				public const string OrganizationMsdynContactkpiitem = "organization_msdyn_contactkpiitem";
				public const string OrganizationMsdynCustomeremailcommunication = "organization_msdyn_customeremailcommunication";
				public const string OrganizationMsdynDailyaccountkpiitem = "organization_msdyn_dailyaccountkpiitem";
				public const string OrganizationMsdynDailycontactkpiitem = "organization_msdyn_dailycontactkpiitem";
				public const string OrganizationMsdynDailyleadkpiitem = "organization_msdyn_dailyleadkpiitem";
				public const string OrganizationMsdynDailyopportunitykpiitem = "organization_msdyn_dailyopportunitykpiitem";
				public const string OrganizationMsdynDataanalyticsreportCsrmanager = "organization_msdyn_dataanalyticsreport_csrmanager";
				public const string OrganizationMsdynDataanalyticsreportForecast = "organization_msdyn_dataanalyticsreport_forecast";
				public const string OrganizationMsdynDataanalyticsreportKsinsights = "organization_msdyn_dataanalyticsreport_ksinsights";
				public const string OrganizationMsdynDataanalyticsreportSutreporting = "organization_msdyn_dataanalyticsreport_sutreporting";
				public const string OrganizationMsdynDatabaseversion = "organization_msdyn_databaseversion";
				public const string OrganizationMsdynDatahygienesettinginfo = "organization_msdyn_datahygienesettinginfo";
				public const string OrganizationMsdynDatainsightsandanalyticsfeature = "organization_msdyn_datainsightsandanalyticsfeature";
				public const string OrganizationMsdynDigitalsellingactivetask = "organization_msdyn_digitalsellingactivetask";
				public const string OrganizationMsdynDigitalsellingcompletedtask = "organization_msdyn_digitalsellingcompletedtask";
				public const string OrganizationMsdynDistributedlock = "organization_msdyn_distributedlock";
				public const string OrganizationMsdynDuplicatedetectionpluginrun = "organization_msdyn_duplicatedetectionpluginrun";
				public const string OrganizationMsdynFederatedarticleincident = "organization_msdyn_federatedarticleincident";
				public const string OrganizationMsdynForecastingcache = "organization_msdyn_forecastingcache";
				public const string OrganizationMsdynForecastpredictionstatus = "organization_msdyn_forecastpredictionstatus";
				public const string OrganizationMsdynForecastsettingsandsummary = "organization_msdyn_forecastsettingsandsummary";
				public const string OrganizationMsdynHelppage = "organization_msdyn_helppage";
				public const string OrganizationMsdynInboxconfiguration = "organization_msdyn_inboxconfiguration";
				public const string OrganizationMsdynInsightsstorevirtualentity = "organization_msdyn_insightsstorevirtualentity";
				public const string OrganizationMsdynIottocaseprocess = "organization_msdyn_iottocaseprocess";
				public const string OrganizationMsdynKbenrichment = "organization_msdyn_kbenrichment";
				public const string OrganizationMsdynKbkeywordsdescsuggestionsetting = "organization_msdyn_kbkeywordsdescsuggestionsetting";
				public const string OrganizationMsdynKmpersonalizationsetting = "organization_msdyn_kmpersonalizationsetting";
				public const string OrganizationMsdynKnowledgeconfiguration = "organization_msdyn_knowledgeconfiguration";
				public const string OrganizationMsdynLeadhygienesetting = "organization_msdyn_leadhygienesetting";
				public const string OrganizationMsdynLeadkpiitem = "organization_msdyn_leadkpiitem";
				public const string OrganizationMsdynLinkedentityattributevalidity = "organization_msdyn_linkedentityattributevalidity";
				public const string OrganizationMsdynMaskingrule = "organization_msdyn_maskingrule";
				public const string OrganizationMsdynMostcontacted = "organization_msdyn_mostcontacted";
				public const string OrganizationMsdynMostcontactedby = "organization_msdyn_mostcontactedby";
				public const string OrganizationMsdynOcchannelconfiguration = "organization_msdyn_occhannelconfiguration";
				public const string OrganizationMsdynOcchannelstateconfiguration = "organization_msdyn_occhannelstateconfiguration";
				public const string OrganizationMsdynOclocalizationdata = "organization_msdyn_oclocalizationdata";
				public const string OrganizationMsdynOcsystemmessage = "organization_msdyn_ocsystemmessage";
				public const string OrganizationMsdynOctag = "organization_msdyn_octag";
				public const string OrganizationMsdynOmnichannelconfiguration = "organization_msdyn_omnichannelconfiguration";
				public const string OrganizationMsdynOpportunitykpiitem = "organization_msdyn_opportunitykpiitem";
				public const string OrganizationMsdynPaneconfiguration = "organization_msdyn_paneconfiguration";
				public const string OrganizationMsdynPanetabconfiguration = "organization_msdyn_panetabconfiguration";
				public const string OrganizationMsdynPanetoolconfiguration = "organization_msdyn_panetoolconfiguration";
				public const string OrganizationMsdynPersonasecurityrolemapping = "organization_msdyn_personasecurityrolemapping";
				public const string OrganizationMsdynPostconfig = "organization_msdyn_postconfig";
				public const string OrganizationMsdynPostruleconfig = "organization_msdyn_postruleconfig";
				public const string OrganizationMsdynPredictivemodelscore = "organization_msdyn_predictivemodelscore";
				public const string OrganizationMsdynPredictivescore = "organization_msdyn_predictivescore";
				public const string OrganizationMsdynPresence = "organization_msdyn_presence";
				public const string OrganizationMsdynProvider = "organization_msdyn_provider";
				public const string OrganizationMsdynRecomputetracker = "organization_msdyn_recomputetracker";
				public const string OrganizationMsdynRecurringsalesaction = "organization_msdyn_recurringsalesaction";
				public const string OrganizationMsdynRoutingrulesetsetting = "organization_msdyn_routingrulesetsetting";
				public const string OrganizationMsdynSabackupdiagnostic = "organization_msdyn_sabackupdiagnostic";
				public const string OrganizationMsdynSabatchruninstance = "organization_msdyn_sabatchruninstance";
				public const string OrganizationMsdynSalesaccelerationinsight = "organization_msdyn_salesaccelerationinsight";
				public const string OrganizationMsdynSalesaccelerationsettings = "organization_msdyn_salesaccelerationsettings";
				public const string OrganizationMsdynSalesassignmentsetting = "organization_msdyn_salesassignmentsetting";
				public const string OrganizationMsdynSalesroutingdiagnostic = "organization_msdyn_salesroutingdiagnostic";
				public const string OrganizationMsdynSaruninstance = "organization_msdyn_saruninstance";
				public const string OrganizationMsdynSegmentationsetting = "organization_msdyn_segmentationsetting";
				public const string OrganizationMsdynSegmentattribute = "organization_msdyn_segmentattribute";
				public const string OrganizationMsdynSegmentcatalogue = "organization_msdyn_segmentcatalogue";
				public const string OrganizationMsdynSentimentanalysis = "organization_msdyn_sentimentanalysis";
				public const string OrganizationMsdynShareasconfiguration = "organization_msdyn_shareasconfiguration";
				public const string OrganizationMsdynSikeyvalueconfig = "organization_msdyn_sikeyvalueconfig";
				public const string OrganizationMsdynSimilarentitiesfeatureimportance = "organization_msdyn_similarentitiesfeatureimportance";
				public const string OrganizationMsdynSmartassistconfig = "organization_msdyn_smartassistconfig";
				public const string OrganizationMsdynSolutionhealthruleset = "organization_msdyn_solutionhealthruleset";
				public const string OrganizationMsdynSoundfile = "organization_msdyn_soundfile";
				public const string OrganizationMsdynSuggestioninteraction = "organization_msdyn_suggestioninteraction";
				public const string OrganizationMsdynSuggestionrequestpayload = "organization_msdyn_suggestionrequestpayload";
				public const string OrganizationMsdynSuggestionsmodelsummary = "organization_msdyn_suggestionsmodelsummary";
				public const string OrganizationMsdynSuggestionssetting = "organization_msdyn_suggestionssetting";
				public const string OrganizationMsdynTeamsdialeradminsettings = "organization_msdyn_teamsdialeradminsettings";
				public const string OrganizationMsdynTour = "organization_msdyn_tour";
				public const string OrganizationMsdynUnifiedroutingsetuptracker = "organization_msdyn_unifiedroutingsetuptracker";
				public const string OrganizationMsdynUpgraderun = "organization_msdyn_upgraderun";
				public const string OrganizationMsdynUpgradestep = "organization_msdyn_upgradestep";
				public const string OrganizationMsdynUpgradeversion = "organization_msdyn_upgradeversion";
				public const string OrganizationMsdynUsagemetric = "organization_msdyn_usagemetric";
				public const string OrganizationMsdynUsagereporting = "organization_msdyn_usagereporting";
				public const string OrganizationMsdynUsersetting = "organization_msdyn_usersetting";
				public const string OrganizationMsdynVivaentitysetting = "organization_msdyn_vivaentitysetting";
				public const string OrganizationMsdynVivaorgsetting = "organization_msdyn_vivaorgsetting";
				public const string OrganizationMsdynWallsavedquery = "organization_msdyn_wallsavedquery";
				public const string OrganizationMsdynWkwcolleaguesforcompany = "organization_msdyn_wkwcolleaguesforcompany";
				public const string OrganizationMsdynWkwcolleaguesforcontact = "organization_msdyn_wkwcolleaguesforcontact";
				public const string OrganizationMsdynWorkflowactionstatus = "organization_msdyn_workflowactionstatus";
				public const string OrganizationMsdynWorklistviewconfiguration = "organization_msdyn_worklistviewconfiguration";
				public const string OrganizationMsdynmktEventmetadataSdkmessageprocessingstep = "organization_msdynmkt_eventmetadata_sdkmessageprocessingstep";
				public const string OrganizationNavigationsetting = "organization_navigationsetting";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaT10050c3cae86 = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_t10_050c3cae86";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaT1005f8960225 = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_t10_05f8960225";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaT10175176e791 = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_t10_175176e791";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaT101ae9aba71b = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_t10_1ae9aba71b";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaT1043904e4236 = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_t10_43904e4236";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaT1046d78dc4c0 = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_t10_46d78dc4c0";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaT10840de5be5b = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_t10_840de5be5b";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaT10A65901ff6b = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_t10_a65901ff6b";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaT10Bbe8491a4c = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_t10_bbe8491a4c";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaT10C6216089e7 = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_t10_c6216089e7";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaTa80ab8691386 = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_ta8_0ab8691386";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaTa81359a424a7 = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_ta8_1359a424a7";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaTa840c23450ed = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_ta8_40c23450ed";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaTa890efed25db = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_ta8_90efed25db";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaTa898fb4824c8 = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_ta8_98fb4824c8";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaTa8A797a12af6 = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_ta8_a797a12af6";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaTa8A944cae038 = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_ta8_a944cae038";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaTa8F28d1abf40 = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_ta8_f28d1abf40";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaTa8F9956aedb7 = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_ta8_f9956aedb7";
				public const string OrganizationNewSystemDonotuseentityRp53fd1p1ekxpaTa8Feedd2fd0e = "organization_new_system_donotuseentity_rp53fd1p1ekxpa_ta8_feedd2fd0e";
				public const string OrganizationNewprocess = "organization_newprocess";
				public const string OrganizationOfficegraphdocument = "organization_officegraphdocument";
				public const string OrganizationOpportunitysalesprocess = "organization_opportunitysalesprocess";
				public const string OrganizationOrganizationdatasyncstate = "organization_organizationdatasyncstate";
				public const string OrganizationOrganizationdatasyncsubscription = "organization_organizationdatasyncsubscription";
				public const string OrganizationOrganizationdatasyncsubscriptionentity = "organization_organizationdatasyncsubscriptionentity";
				public const string OrganizationOrganizationsetting = "organization_organizationsetting";
				public const string OrganizationOrginsightsmetric = "organization_orginsightsmetric";
				public const string OrganizationOrginsightsnotification = "organization_orginsightsnotification";
				public const string OrganizationPackage = "organization_package";
				public const string OrganizationPhonetocaseprocess = "organization_phonetocaseprocess";
				public const string OrganizationPluginassembly = "organization_pluginassembly";
				public const string OrganizationPluginpackage = "organization_pluginpackage";
				public const string OrganizationPlugintype = "organization_plugintype";
				public const string OrganizationPlugintypestatistic = "organization_plugintypestatistic";
				public const string OrganizationPosition = "organization_position";
				public const string OrganizationPost = "organization_post";
				public const string OrganizationPostComment = "organization_PostComment";
				public const string OrganizationPostlike = "organization_postlike";
				public const string OrganizationPostrole = "organization_postrole";
				public const string OrganizationPriceLevels = "organization_price_levels";
				public const string OrganizationPrivilegesremovalsetting = "organization_privilegesremovalsetting";
				public const string OrganizationProductAssociation = "organization_ProductAssociation";
				public const string OrganizationProducts = "organization_products";
				public const string OrganizationProductSubstitute = "organization_ProductSubstitute";
				public const string OrganizationPublisher = "organization_publisher";
				public const string OrganizationQueueitems = "organization_queueitems";
				public const string OrganizationQueues = "organization_queues";
				public const string OrganizationRecommendeddocument = "organization_recommendeddocument";
				public const string OrganizationRecordfilter = "organization_recordfilter";
				public const string OrganizationRelationshipRoles = "organization_relationship_roles";
				public const string OrganizationRelationshipattribute = "organization_relationshipattribute";
				public const string OrganizationResourceGroups = "organization_resource_groups";
				public const string OrganizationResourceSpecs = "organization_resource_specs";
				public const string OrganizationResources = "organization_resources";
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
				public const string OrganizationSalesLiterature = "organization_sales_literature";
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
				public const string OrganizationSearchrelationshipsettings = "organization_searchrelationshipsettings";
				public const string OrganizationServiceendpoint = "organization_serviceendpoint";
				public const string OrganizationServices = "organization_services";
				public const string OrganizationSettingdefinition = "organization_settingdefinition";
				public const string OrganizationSharedlinksetting = "organization_sharedlinksetting";
				public const string OrganizationSharedobject = "organization_sharedobject";
				public const string OrganizationSharedworkspace = "organization_sharedworkspace";
				public const string OrganizationSharepointdata = "organization_sharepointdata";
				public const string OrganizationSharepointdocument = "organization_sharepointdocument";
				public const string OrganizationSimilarityrule = "organization_similarityrule";
				public const string OrganizationSitemap = "organization_sitemap";
				public const string OrganizationSites = "organization_sites";
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
				public const string OrganizationTopicmodel = "organization_topicmodel";
				public const string OrganizationTopicmodelconfiguration = "organization_topicmodelconfiguration";
				public const string OrganizationTopicmodelexecutionhistory = "organization_topicmodelexecutionhistory";
				public const string OrganizationTraceassociation = "organization_traceassociation";
				public const string OrganizationTracelog = "organization_tracelog";
				public const string OrganizationTransactioncurrencies = "organization_transactioncurrencies";
				public const string OrganizationTranslationprocess = "organization_translationprocess";
				public const string OrganizationUofSchedules = "organization_uof_schedules";
				public const string OrganizationUserMapping = "organization_UserMapping";
				public const string OrganizationUsermobileofflineprofilemembership = "organization_usermobileofflineprofilemembership";
				public const string OrganizationUserrating = "organization_userrating";
				public const string OrganizationVirtualentitymetadata = "organization_virtualentitymetadata";
				public const string OrganizationWebwizard = "organization_webwizard";
				public const string OrganizationWizardaccessprivilege = "organization_wizardaccessprivilege";
				public const string OrganizationWizardpage = "organization_wizardpage";
				public const string UserentityinstancedataOrganization = "userentityinstancedata_organization";
				public const string WebresourceOrganization = "webresource_organization";
            }

            public static class ManyToOne
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

            public static class ManyToMany
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
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static Organization Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("organization", id, columnSet).ToEntity<Organization>();
        }

        public Organization GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  Organization(Id) {Attributes = attr };
            }
            return this;
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
