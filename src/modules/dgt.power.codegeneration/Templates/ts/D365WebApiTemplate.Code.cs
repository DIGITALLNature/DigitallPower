namespace dgt.power.codegeneration.Templates.ts
{
  public partial class D365WebApiTemplate : ITemplate
  {
    private readonly string TypingPath;

    public D365WebApiTemplate(string typingPath)
    {
      TypingPath = typingPath;
    }

    public string GenerateTemplate()
    {
      return TransformText();
    }
  }
}