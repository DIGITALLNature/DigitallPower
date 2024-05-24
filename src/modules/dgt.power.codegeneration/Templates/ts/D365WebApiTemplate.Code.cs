// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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