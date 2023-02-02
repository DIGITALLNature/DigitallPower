namespace dgt.power.codegeneration.Templates.ts
{
  public partial class D365UtilsTemplate : ITemplate
  {
    private readonly string TypingPath;

    public D365UtilsTemplate(string typingPath)
    {
      TypingPath = typingPath;
    }

    public string GenerateTemplate()
    {
      return TransformText();
    }
  }
}