using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// A component dependency in CRM.
	/// </summary>
    [EntityLogicalName("dependency")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class Dependency : Entity
    {
        #region ctor
        public Dependency() : base(EntityLogicalName) { }

        public Dependency(Guid id) : base(EntityLogicalName, id) { }

        public Dependency(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public Dependency(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "dependency";
        public const int EntityTypeCode = 7105;
        #endregion

        #region Attributes
        [AttributeLogicalName("dependencyid")]
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
		/// Unique identifier of a dependency.
		/// </summary>
        [AttributeLogicalName("dependencyid")]
        public Guid? DependencyId
        {
            get
            {
                return GetAttributeValue<Guid?>("dependencyid");
            }
        }

        /// <summary>
		/// The dependency type of the dependency.
		/// </summary>
        [AttributeLogicalName("dependencytype")]
        public OptionSetValue? DependencyType
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("dependencytype");
            }
        }

        
        [AttributeLogicalName("dependentcomponentbasesolutionid")]
        public Guid? DependentComponentBaseSolutionId
        {
            get
            {
                return GetAttributeValue<Guid?>("dependentcomponentbasesolutionid");
            }
        }

        /// <summary>
		/// Unique identifier of the dependent component's node.
		/// </summary>
        [AttributeLogicalName("dependentcomponentnodeid")]
        public EntityReference? DependentComponentNodeId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("dependentcomponentnodeid");
            }
        }

        
        [AttributeLogicalName("dependentcomponentobjectid")]
        public Guid? DependentComponentObjectId
        {
            get
            {
                return GetAttributeValue<Guid?>("dependentcomponentobjectid");
            }
        }

        
        [AttributeLogicalName("dependentcomponentparentid")]
        public Guid? DependentComponentParentId
        {
            get
            {
                return GetAttributeValue<Guid?>("dependentcomponentparentid");
            }
        }

        
        [AttributeLogicalName("dependentcomponenttype")]
        public OptionSetValue? DependentComponentType
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("dependentcomponenttype");
            }
        }

        
        [AttributeLogicalName("requiredcomponentbasesolutionid")]
        public Guid? RequiredComponentBaseSolutionId
        {
            get
            {
                return GetAttributeValue<Guid?>("requiredcomponentbasesolutionid");
            }
        }

        
        [AttributeLogicalName("requiredcomponentintroducedversion")]
        public double? RequiredComponentIntroducedVersion
        {
            get
            {
                return GetAttributeValue<double?>("requiredcomponentintroducedversion");
            }
        }

        /// <summary>
		/// Unique identifier of the required component's node
		/// </summary>
        [AttributeLogicalName("requiredcomponentnodeid")]
        public EntityReference? RequiredComponentNodeId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("requiredcomponentnodeid");
            }
        }

        
        [AttributeLogicalName("requiredcomponentobjectid")]
        public Guid? RequiredComponentObjectId
        {
            get
            {
                return GetAttributeValue<Guid?>("requiredcomponentobjectid");
            }
        }

        
        [AttributeLogicalName("requiredcomponentparentid")]
        public Guid? RequiredComponentParentId
        {
            get
            {
                return GetAttributeValue<Guid?>("requiredcomponentparentid");
            }
        }

        
        [AttributeLogicalName("requiredcomponenttype")]
        public OptionSetValue? RequiredComponentType
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("requiredcomponenttype");
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
            public struct DependencyType
            {
                public const int None = 0;
                public const int SolutionInternal = 1;
                public const int Published = 2;
                public const int Unpublished = 4;
            }
            public struct DependentComponentType
            {
                public const int Entity = 1;
                public const int Attribute = 2;
                public const int Relationship = 3;
                public const int AttributePicklistValue = 4;
                public const int AttributeLookupValue = 5;
                public const int ViewAttribute = 6;
                public const int LocalizedLabel = 7;
                public const int RelationshipExtraCondition = 8;
                public const int OptionSet = 9;
                public const int EntityRelationship = 10;
                public const int EntityRelationshipRole = 11;
                public const int EntityRelationshipRelationships = 12;
                public const int ManagedProperty = 13;
                public const int EntityKey = 14;
                public const int Privilege = 16;
                public const int PrivilegeObjectTypeCode = 17;
                public const int Index = 18;
                public const int Role = 20;
                public const int RolePrivilege = 21;
                public const int DisplayString = 22;
                public const int DisplayStringMap = 23;
                public const int Form = 24;
                public const int Organization = 25;
                public const int SavedQuery = 26;
                public const int Workflow = 29;
                public const int Report = 31;
                public const int ReportEntity = 32;
                public const int ReportCategory = 33;
                public const int ReportVisibility = 34;
                public const int Attachment = 35;
                public const int EmailTemplate = 36;
                public const int ContractTemplate = 37;
                public const int KBArticleTemplate = 38;
                public const int MailMergeTemplate = 39;
                public const int DuplicateRule = 44;
                public const int DuplicateRuleCondition = 45;
                public const int EntityMap = 46;
                public const int AttributeMap = 47;
                public const int RibbonCommand = 48;
                public const int RibbonContextGroup = 49;
                public const int RibbonCustomization = 50;
                public const int RibbonRule = 52;
                public const int RibbonTabToCommandMap = 53;
                public const int RibbonDiff = 55;
                public const int SavedQueryVisualization = 59;
                public const int SystemForm = 60;
                public const int WebResource = 61;
                public const int SiteMap = 62;
                public const int ConnectionRole = 63;
                public const int ComplexControl = 64;
                public const int HierarchyRule = 65;
                public const int CustomControl = 66;
                public const int CustomControlDefaultConfig = 68;
                public const int FieldSecurityProfile = 70;
                public const int FieldPermission = 71;
                public const int PluginType = 90;
                public const int PluginAssembly = 91;
                public const int SDKMessageProcessingStep = 92;
                public const int SDKMessageProcessingStepImage = 93;
                public const int ServiceEndpoint = 95;
                public const int RoutingRule = 150;
                public const int RoutingRuleItem = 151;
                public const int SLA = 152;
                public const int SLAItem = 153;
                public const int ConvertRule = 154;
                public const int ConvertRuleItem = 155;
                public const int MobileOfflineProfile = 161;
                public const int MobileOfflineProfileItem = 162;
                public const int SimilarityRule = 165;
                public const int DataSourceMapping = 166;
                public const int SDKMessage = 201;
                public const int SDKMessageFilter = 202;
                public const int SdkMessagePair = 203;
                public const int SdkMessageRequest = 204;
                public const int SdkMessageRequestField = 205;
                public const int SdkMessageResponse = 206;
                public const int SdkMessageResponseField = 207;
                public const int ImportMap = 208;
                public const int WebWizard = 210;
                public const int CanvasApp = 300;
                public const int Connector = 371;
                public const int Connector_ = 372;
                public const int EnvironmentVariableDefinition = 380;
                public const int EnvironmentVariableValue = 381;
                public const int AIProjectType = 400;
                public const int AIProject = 401;
                public const int AIConfiguration = 402;
                public const int EntityAnalyticsConfiguration = 430;
                public const int AttributeImageConfiguration = 431;
                public const int EntityImageConfiguration = 432;
            }
            public struct RequiredComponentType
            {
                public const int Entity = 1;
                public const int Attribute = 2;
                public const int Relationship = 3;
                public const int AttributePicklistValue = 4;
                public const int AttributeLookupValue = 5;
                public const int ViewAttribute = 6;
                public const int LocalizedLabel = 7;
                public const int RelationshipExtraCondition = 8;
                public const int OptionSet = 9;
                public const int EntityRelationship = 10;
                public const int EntityRelationshipRole = 11;
                public const int EntityRelationshipRelationships = 12;
                public const int ManagedProperty = 13;
                public const int EntityKey = 14;
                public const int Privilege = 16;
                public const int PrivilegeObjectTypeCode = 17;
                public const int Index = 18;
                public const int Role = 20;
                public const int RolePrivilege = 21;
                public const int DisplayString = 22;
                public const int DisplayStringMap = 23;
                public const int Form = 24;
                public const int Organization = 25;
                public const int SavedQuery = 26;
                public const int Workflow = 29;
                public const int Report = 31;
                public const int ReportEntity = 32;
                public const int ReportCategory = 33;
                public const int ReportVisibility = 34;
                public const int Attachment = 35;
                public const int EmailTemplate = 36;
                public const int ContractTemplate = 37;
                public const int KBArticleTemplate = 38;
                public const int MailMergeTemplate = 39;
                public const int DuplicateRule = 44;
                public const int DuplicateRuleCondition = 45;
                public const int EntityMap = 46;
                public const int AttributeMap = 47;
                public const int RibbonCommand = 48;
                public const int RibbonContextGroup = 49;
                public const int RibbonCustomization = 50;
                public const int RibbonRule = 52;
                public const int RibbonTabToCommandMap = 53;
                public const int RibbonDiff = 55;
                public const int SavedQueryVisualization = 59;
                public const int SystemForm = 60;
                public const int WebResource = 61;
                public const int SiteMap = 62;
                public const int ConnectionRole = 63;
                public const int ComplexControl = 64;
                public const int HierarchyRule = 65;
                public const int CustomControl = 66;
                public const int CustomControlDefaultConfig = 68;
                public const int FieldSecurityProfile = 70;
                public const int FieldPermission = 71;
                public const int PluginType = 90;
                public const int PluginAssembly = 91;
                public const int SDKMessageProcessingStep = 92;
                public const int SDKMessageProcessingStepImage = 93;
                public const int ServiceEndpoint = 95;
                public const int RoutingRule = 150;
                public const int RoutingRuleItem = 151;
                public const int SLA = 152;
                public const int SLAItem = 153;
                public const int ConvertRule = 154;
                public const int ConvertRuleItem = 155;
                public const int MobileOfflineProfile = 161;
                public const int MobileOfflineProfileItem = 162;
                public const int SimilarityRule = 165;
                public const int DataSourceMapping = 166;
                public const int SDKMessage = 201;
                public const int SDKMessageFilter = 202;
                public const int SdkMessagePair = 203;
                public const int SdkMessageRequest = 204;
                public const int SdkMessageRequestField = 205;
                public const int SdkMessageResponse = 206;
                public const int SdkMessageResponseField = 207;
                public const int ImportMap = 208;
                public const int WebWizard = 210;
                public const int CanvasApp = 300;
                public const int Connector = 371;
                public const int Connector_ = 372;
                public const int EnvironmentVariableDefinition = 380;
                public const int EnvironmentVariableValue = 381;
                public const int AIProjectType = 400;
                public const int AIProject = 401;
                public const int AIConfiguration = 402;
                public const int EntityAnalyticsConfiguration = 430;
                public const int AttributeImageConfiguration = 431;
                public const int EntityImageConfiguration = 432;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string DependencyId = "dependencyid";
            public const string DependencyType = "dependencytype";
            public const string DependentComponentBaseSolutionId = "dependentcomponentbasesolutionid";
            public const string DependentComponentNodeId = "dependentcomponentnodeid";
            public const string DependentComponentObjectId = "dependentcomponentobjectid";
            public const string DependentComponentParentId = "dependentcomponentparentid";
            public const string DependentComponentType = "dependentcomponenttype";
            public const string RequiredComponentBaseSolutionId = "requiredcomponentbasesolutionid";
            public const string RequiredComponentIntroducedVersion = "requiredcomponentintroducedversion";
            public const string RequiredComponentNodeId = "requiredcomponentnodeid";
            public const string RequiredComponentObjectId = "requiredcomponentobjectid";
            public const string RequiredComponentParentId = "requiredcomponentparentid";
            public const string RequiredComponentType = "requiredcomponenttype";
            public const string VersionNumber = "versionnumber";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string UserentityinstancedataDependency = "userentityinstancedata_dependency";
            }

            public static partial class ManyToOne
            {
                public const string DependencynodeAncestorDependency = "dependencynode_ancestor_dependency";
                public const string DependencynodeDescendentDependency = "dependencynode_descendent_dependency";
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
        public IQueryable<Dependency> DependencySet
        {
            get
            {
                return CreateQuery<Dependency>();
            }
        }
    }
    #endregion
}
