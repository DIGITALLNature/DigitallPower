# `export` — Export Dataverse artifacts

Exports configuration data from the currently selected environment into JSON files.

| Command | Description |
|---------|-------------|
| `export teamtemplates` | Team templates |
| `export bulkdeletes` | Bulk delete jobs |
| `export queues` | Queues |
| `export documenttemplates` | Document templates |
| `export calendars` | Calendars |
| `export slaconfigs` | SLAs |
| `export routingruleconfigs` | Routing rules |
| `export userroles` | User → security role assignments |
| `export outlooktemplates` | Outlook templates |

All export commands accept `--filedir <path>` to control the output directory.

```bash
dgtp export bulkdeletes --filedir ./out/bulkdeletes
```
