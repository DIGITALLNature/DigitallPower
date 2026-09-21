# Plugin push dry-run reconciliation summary

`dgtp plugin push --dry-run` reports aggregate reconciliation results after processing
plugin types. The output includes counts for new, existing, and removed plugin types,
plus created, updated, unchanged, and deleted steps and images. Unchanged images are
counted from local images that produced no plan because the planner intentionally only
creates plans for image creates and updates.

The summary is produced by `PluginTypeReconciler` after all plans have been evaluated.
Dataverse writes remain guarded by the existing dry-run checks; the summary is
observational only and does not alter reconciliation behavior.
