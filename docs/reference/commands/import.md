# `import` — Import Dataverse artifacts

Counterpart to [`export`](export.md). Reads the previously exported JSON files and applies them to the currently selected environment.

| Command | Description |
|---------|-------------|
| `import outlooktemplates` | Outlook templates |
| `import userroles` | User → security role assignments |
| `import queues` | Queues |
| `import teamtemplates` | Team templates |
| `import bulkdeletes` | Bulk delete jobs |
| `import documenttemplates` | Document templates |
| `import calendar` | Calendars |
| `import slaconfigs` | SLAs |
| `import routingruleconfigs` | Routing rules |

```bash
dgtp import outlooktemplates --filedir ./out/outlooktemplates
```
