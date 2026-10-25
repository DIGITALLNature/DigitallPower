# `analyze` — Solution analysis

Static analysis of one or many Dataverse solutions.

| Command | Description |
|---------|-------------|
| `analyze entityallassets` | Scan solutions for entities containing all assets |
| `analyze noactivelayer` | Find unmanaged solution components without an active layer |
| `analyze activelayer` | Find managed solution components that already received an active layer |
| `analyze toplayer` | Find managed solution components where the given solution is not the top layer |
| `analyze redundantcomponents` | Find components contained in multiple solutions |
| `analyze redundantpatches` | Find patches that are no longer needed because their components are no longer top-layer |

```bash
dgtp analyze noactivelayer --inline solution1,solution2
```
