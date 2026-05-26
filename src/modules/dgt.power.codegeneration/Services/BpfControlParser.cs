// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Model;
using dgt.power.dataverse;
using System.Text.Json;

namespace dgt.power.codegeneration.Services
{
    public static class BpfControlParser
    {
        /// <summary>
        /// Parse all the bpf controls into control detail entity list for a given entity name and workflow
        /// </summary>
        /// <param name="worflow"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public static List<BpfControlDetail> ParseBpfClientDetail(Workflow worflow, string entityName)
        {
            var clientDetail = worflow.GetAttributeValue<string>(Workflow.LogicalNames.ClientData);
            var doc = JsonSerializer.Deserialize<BpfClientData>(clientDetail);
            if (doc?.Steps == null)
            {
                return [];
            }

            var entitySteps = FilterRelevantSteps(doc.Steps?.StepList.ToList() ?? new List<BpfClientDataStep>());

            return entitySteps
                .Where(x => !string.IsNullOrWhiteSpace(x.ClassId) && !string.IsNullOrWhiteSpace(x.DataFieldName))
                .Select(x => MapControlStepToControlDetail(x, worflow.Name ?? string.Empty, entityName))
                .ToList();
        }

        /// <summary>
        /// Filter to only pick the control relevant steps from the bp flow
        /// </summary>
        /// <param name="listOfSteps"></param>
        /// <returns></returns>
        private static List<BpfClientDataStep> FilterRelevantSteps(List<BpfClientDataStep> listOfSteps)
        {
            return listOfSteps
                .Where(x => x.Class.StartsWith("EntityStep", StringComparison.InvariantCulture))
                .SelectMany(x => x.Steps == null ? [] : RecurringFilterRelevantSteps(x.Steps.StepList, x.Description))
                .ToList();
        }

        /// <summary>
        /// Map control step dat into the output control detail
        /// </summary>
        /// <param name="clientStep"></param>
        /// <param name="workflowName"></param>
        /// <param name="mainEntityName"></param>
        /// <returns></returns>
        private static BpfControlDetail MapControlStepToControlDetail(BpfClientDataStep clientStep, string workflowName, string mainEntityName)
        {
            return new BpfControlDetail
            {
                WorkflowName = workflowName,
                EntityName = string.IsNullOrWhiteSpace(clientStep.Description) ? mainEntityName : clientStep.Description,
                ClassId = clientStep.ClassId?.ToUpperInvariant() ?? string.Empty,
                DataFieldName = clientStep.DataFieldName ?? string.Empty
            };
        }

        /// <summary>
        /// Filter the bp client data step and select only the control relavant steps
        /// </summary>
        /// <param name="clientDataSteps"></param>
        /// <param name="entityStepEntityName"></param>
        /// <returns></returns>
        private static List<BpfClientDataStep> RecurringFilterRelevantSteps(List<BpfClientDataStep> clientDataSteps, string entityStepEntityName)
        {
            var controlStepClass = "ControlStep";
            var recurringStepClass = new[] { "PageStep", "StageStep", "StepStep" };

            return clientDataSteps
                .SelectMany(x => {
                    if (recurringStepClass.Any(rt => x.Class.StartsWith(rt, StringComparison.InvariantCulture)))
                    {
                        return x.Steps == null ? [] : RecurringFilterRelevantSteps(x.Steps.StepList, entityStepEntityName);
                    }
                    // Copy parent entity step name to the control step for easier mapping as multiple different entity steps
                    // might exist in a bpf for a primary entity name
                    x.Description = entityStepEntityName;
                    if (x.Steps == null)
                    {
                        return [x];
                    }
                    return x.Steps.StepList.Prepend(x);
                })
                .Where(x => x.Class.StartsWith(controlStepClass, StringComparison.InvariantCulture))
                .ToList();
        }
    }
}
