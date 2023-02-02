namespace dgt.power.codegeneration.Templates.ts
{
  public partial class D365ModelTemplate : ITemplate
  {
    private readonly string TypingPath;

    public D365ModelTemplate(string typingPath)
    {
      TypingPath = typingPath;
    }

    public string GenerateTemplate()
    {
      return TransformText();
    }
  }
}