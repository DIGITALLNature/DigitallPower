using System;
using dgt.registration;
using Newtonsoft.Json;

namespace dgt.power.push.tests.dependentsample
{
    [PluginRegistration(PluginExecutionMode.Synchronous, "create",PluginExecutionStage.PreValidation, PrimaryEntityName = "contact", ExecutionOrder = 10)]
    [PluginRegistration(PluginExecutionMode.Synchronous, "update", PluginExecutionStage.PreOperation, PrimaryEntityName = "contact", ExecutionOrder = 20)]
    [PluginRegistration(PluginExecutionMode.Asynchronous, "update", PluginExecutionStage.PostOperation, PrimaryEntityName = "contact", ExecutionOrder = 30)]
    public class SamplePluginDependent : PluginBase
    {
        public SamplePluginDependent(string unsecureConfiguration, string secureConfiguration)
            : base(typeof(SamplePluginDependent))
        {

        }

        // Entry point for custom business logic execution
        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            if (localPluginContext == null)
            {
                throw new ArgumentNullException(nameof(localPluginContext));
            }

            var context = localPluginContext.PluginExecutionContext;
            var json = JsonConvert.SerializeObject("Bind Json.Net");

            throw new NotImplementedException();
        }
    }
}
