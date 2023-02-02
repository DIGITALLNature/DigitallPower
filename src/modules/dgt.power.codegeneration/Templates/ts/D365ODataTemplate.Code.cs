namespace dgt.power.codegeneration.Templates.ts
{
  public partial class D365ODataTemplate : ITemplate
  {
    private readonly string TypingPath;

    public D365ODataTemplate(string typingPath)
    {
      TypingPath = typingPath;
    }

    public string GenerateTemplate()
    {
      return TransformText();
    }
  }
}