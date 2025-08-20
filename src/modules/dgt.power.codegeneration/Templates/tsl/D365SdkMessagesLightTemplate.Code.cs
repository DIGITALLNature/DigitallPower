// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Templates.ts;

namespace dgt.power.codegeneration.Templates.tsl
{
    public partial class D365SdkMessagesLightTemplate(IEnumerable<(string Name, string Message)> SdkMessages) : ITemplate
    {
        public string GenerateTemplate() => TransformText();
    }
}
