namespace dgt.power.codegeneration.Model;

public class WfAction
{
    public WfAction(string logicalName)
    {
        LogicalName = logicalName;
        InParameters = new List<WfParameter>();
        OutParameters = new List<WfParameter>();  
    }

    public string LogicalName { get; private set; }

    public List<WfParameter> InParameters { get; set; }

    public List<WfParameter> OutParameters { get; set; }

}