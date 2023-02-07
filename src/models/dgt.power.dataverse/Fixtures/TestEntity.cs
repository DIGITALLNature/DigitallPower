using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

// ReSharper disable All
namespace dgt.power.dataverse;

[DataContract]
[EntityLogicalName("testentity")]
public partial class TestEntity : Entity, INotifyPropertyChanging, INotifyPropertyChanged
{
    [DebuggerNonUserCode]
    public TestEntity() : this(false)
    {
    }

    [DebuggerNonUserCode]
    public TestEntity(bool trackChanges = false) : base(EntityLogicalName)
    {
        _trackChanges = trackChanges;
    }

    [DebuggerNonUserCode]
    public TestEntity(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
    {
        _trackChanges = trackChanges;
    }

    [DebuggerNonUserCode]
    public TestEntity(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
    {
        _trackChanges = trackChanges;
    }

    [DebuggerNonUserCode]
    public TestEntity(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
    {
        _trackChanges = trackChanges;
    }

    #region Attributes
    [AttributeLogicalNameAttribute("testentityid")]
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
            TestEntityId = value;
        }
    }

    [AttributeLogicalName("testentityid")]
    public Guid? TestEntityId
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<Guid?>("testentityid");
        }
        [DebuggerNonUserCode]
        set
        {
            OnPropertyChanging(nameof(TestEntityId));
            SetAttributeValue("testentityid", value);
            if (value.HasValue)
            {
                base.Id = value.Value;
            }
            else
            {
                base.Id = System.Guid.Empty;
            }
            OnPropertyChanged(nameof(TestEntityId));
        }
    }

    public const string EntityLogicalName = "testentity";
    public const int EntityTypeCode = 10000;

    private readonly bool _trackChanges;
    private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();

    public event PropertyChangingEventHandler? PropertyChanging;
    public event PropertyChangedEventHandler? PropertyChanged;

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

    [AttributeLogicalName("createdby")]
    public EntityReference CreatedBy
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<EntityReference>("createdby");
        }
    }

    [AttributeLogicalName("createdon")]
    public DateTime? CreatedOn
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<DateTime?>("createdon");
        }
    }

    [AttributeLogicalName("modifiedby")]
    public EntityReference ModifiedBy
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<EntityReference>("modifiedby");
        }
    }

    [AttributeLogicalName("modifiedon")]
    public DateTime? ModifiedOn
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<DateTime?>("modifiedon");
        }
    }

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

    [AttributeLogicalName("name")]
    public string Name
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<string>("name");
        }
        [DebuggerNonUserCode]
        set
        {
            OnPropertyChanging(nameof(Name));
            SetAttributeValue("name", value);
            OnPropertyChanged(nameof(Name));
        }
    }

    [AttributeLogicalName("webpage")]
    public string Webpage
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<string>("webpage");
        }
        [DebuggerNonUserCode]
        set
        {
            OnPropertyChanging(nameof(Webpage));
            SetAttributeValue("webpage", value);
            OnPropertyChanged(nameof(Webpage));
        }
    }

    [AttributeLogicalName("email")]
    public string Email
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<string>("email");
        }
        [DebuggerNonUserCode]
        set
        {
            OnPropertyChanging(nameof(Email));
            SetAttributeValue("email", value);
            OnPropertyChanged(nameof(Email));
        }
    }

    [AttributeLogicalName("phone")]
    public string Phone
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<string>("phone");
        }
        [DebuggerNonUserCode]
        set
        {
            OnPropertyChanging(nameof(Phone));
            SetAttributeValue("phone", value);
            OnPropertyChanged(nameof(Phone));
        }
    }

    [AttributeLogicalName("description")]
    public string Description
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<string>("description");
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
    /// Status of the duplicate detection rule.
    /// </summary>
    [AttributeLogicalName("statecode")]
    public OptionSetValue StateCode
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<OptionSetValue>("statecode");
        }
    }

    /// <summary>
    /// Reason for the status of the duplicate detection rule.
    /// </summary>
    [AttributeLogicalName("statuscode")]
    public OptionSetValue StatusCode
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<OptionSetValue>("statuscode");
        }
        [DebuggerNonUserCode]
        set
        {
            OnPropertyChanging(nameof(StatusCode));
            SetAttributeValue("statuscode", value);
            OnPropertyChanged(nameof(StatusCode));
        }
    }

    [AttributeLogicalName("test_number_calc")]
    public int? TestNumberCalc
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<int?>("test_number_calc");
        }
        [DebuggerNonUserCode]
        set
        {
            OnPropertyChanging(nameof(TestNumberCalc));
            SetAttributeValue("test_number_calc", value);
            OnPropertyChanged(nameof(TestNumberCalc));
        }
    }

    [AttributeLogicalName("test_money_calc")]
    public Money? TestMoneyCalc
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<Money?>("test_money_calc");
        }
        [DebuggerNonUserCode]
        set
        {
            OnPropertyChanging(nameof(TestMoneyCalc));
            SetAttributeValue("test_money_calc", value);
            OnPropertyChanged(nameof(TestMoneyCalc));
        }
    }

    [AttributeLogicalName("test_money_base")]
    public Money? TestMoneyCalcBase
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<Money?>("test_money_base");
        }
        [DebuggerNonUserCode]
        set
        {
            OnPropertyChanging(nameof(TestMoneyCalcBase));
            SetAttributeValue("test_money_base", value);
            OnPropertyChanged(nameof(TestMoneyCalcBase));
        }
    }

    [AttributeLogicalName("bpf_duration")]
    public int? BpfDuration
    {
        [DebuggerNonUserCode]
        get
        {
            return GetAttributeValue<int?>("bpf_duration");
        }
        [DebuggerNonUserCode]
        set
        {
            OnPropertyChanging(nameof(BpfDuration));
            SetAttributeValue("bpf_duration", value);
            OnPropertyChanged(nameof(BpfDuration));
        }
    }
    #endregion

    #region LogicalNames
    public static class LogicalNames
    {
        public const string TestEntityId = "testentityid";
        public const string CreatedBy = "createdby";
        public const string CreatedOn = "createdon";
        public const string ModifiedBy = "modifiedby";
        public const string ModifiedOn = "modifiedon";
        public const string OverriddenCreatedOn = "overriddencreatedon";
        public const string Name = "name";
        public const string Webpage = "webpage";
        public const string Email = "email";
        public const string Phone = "phone";
        public const string Description = "description";
        public const string StateCode = "statecode";
        public const string StatusCode = "statuscode";
        public const string TestNumberCalc = "test_number_calc";
        public const string TestMoneyCalc = "test_money_calc";
        public const string TestMoneyCalcBase = "test_money_base";
        public const string BpfDuration = "bpf_duration";
    }
    #endregion
}

public partial class DataContext
{
    public IQueryable<TestEntity> TestEntitySet
    {
        get
        {
            return CreateQuery<TestEntity>();
        }
    }
}
