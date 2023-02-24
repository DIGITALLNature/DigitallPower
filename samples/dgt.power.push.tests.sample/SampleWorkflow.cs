using System;
using System.Activities;
using dgt.registration;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Xrm.Sdk;

namespace dgt.power.push.tests.sample
{
    [WorkflowRegistration("Sample")] // Can not be imported via PowerTool currently
    public class SampleWorkflow : CodeActivity
    {
        [Input(nameof(Email))]
        [RequiredArgument]
        [ReferenceTarget("email")]
        public InArgument<EntityReference> Email { get; set; }

        protected override void Execute(CodeActivityContext context) => throw new NotImplementedException();
    }
}
