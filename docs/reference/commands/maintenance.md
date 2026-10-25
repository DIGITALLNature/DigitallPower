# `maintenance` — Operational tasks

Day-to-day administrative actions against a live environment.

| Command | Description |
|---------|-------------|
| `maintenance bulkdelete` | Run a bulk delete job for a given FetchXML and wait for completion |
| `maintenance autonumber` | Set auto-number formats for columns from a JSON config |
| `maintenance protectfields` | Prevent all calculated fields from receiving an active layer |
| `maintenance carrierinfo` | Export carrier solutions metadata to JSON |
| `maintenance createworkflowstate` | Generate a workflow-state configuration file |
| `maintenance workflowstate` | Apply a workflow-state configuration |
| `maintenance removeredundantcomponents <SourceSolutions> <TargetSolution> [--dryrun] [--includeEntities]` | Remove components from `TargetSolution` that already exist in `SourceSolutions` (comma-separated) |
| `maintenance filterfxplugins` | Add message filtering for PowerFx plugin steps |

`maintenance bulkdelete` accepts `--poll-interval <seconds>` (default `5`) to set the interval
between asynchronous job status checks. The option is specific to this command.

```bash
dgtp maintenance bulkdelete --inline "<fetchxml>...</fetchxml>" --poll-interval 10
```
