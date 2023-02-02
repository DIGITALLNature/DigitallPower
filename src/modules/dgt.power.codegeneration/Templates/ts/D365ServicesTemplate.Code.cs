namespace dgt.power.codegeneration.Templates.ts
{
  public partial class D365ServicesTemplate : ITemplate
  {
    private readonly string TypingPath;

    public D365ServicesTemplate(string typingPath)
    {
      TypingPath = typingPath;
    }

    public string GenerateTemplate()
    {
      return TransformText();
    }
  }
}