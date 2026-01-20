// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Model
{
    public class BpfControlDetail
    {
        public required string EntityName { get; set; }
        public required string WorkflowName { get; set; }
        public required string ClassId { get; set; }

        public required string DataFieldName { get; set; }

        public string? DefinitelyTypedControlType { get; set; }
        public string? DefinitelyTypedAttributeType { get; set; }
    }
}
