using System;
using dgt.registration;

namespace dgt.power.push.tests.sample
{
    [PluginRegistration(PluginExecutionMode.Synchronous, "create",PluginExecutionStage.PreValidation, PrimaryEntityName = "contact", ExecutionOrder = 10)]
    [PluginRegistration(PluginExecutionMode.Synchronous, "update", PluginExecutionStage.PreOperation, PrimaryEntityName = "contact", ExecutionOrder = 20)]
    [PluginRegistration(PluginExecutionMode.Asynchronous, "update", PluginExecutionStage.PostOperation, PrimaryEntityName = "contact", ExecutionOrder = 30)]
    public class SamplePlugin : PluginBase
    {
        public SamplePlugin(string unsecureConfiguration, string secureConfiguration)
            : base(typeof(SamplePlugin))
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

            throw new NotImplementedException();
        }
    }
}
