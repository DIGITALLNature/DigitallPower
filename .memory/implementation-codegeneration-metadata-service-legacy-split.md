# MetadataService legacy split

## Context

`src\modules\dgt.power.codegeneration\Services\MetadataService.cs` had two static-analysis problems:

- **S104** because the file exceeded 1000 lines
- **S4136** because V1 `CodeGenerationConfig` overloads were separated from their V2 counterparts

## Implementation

- Kept the main implementation in `MetadataService.cs` and changed the type to `partial`
- Moved all V1 overload implementations that take `CodeGenerationConfig` into `MetadataService.Legacy.cs`
- Left shared helpers in the main file:
  - `_usedTokens`
  - `FetchSolution(string)`
- Reordered `IMetadataService` so each V1 overload sits directly below its V2 counterpart
- Left V1-only members (`RetrieveActions`, `RetrieveCustomApis`) at the end of the interface

## Caveats

- If a future V1 overload is reintroduced in the main file, `S4136` can return even though the class is partial
- `FetchSolution(string)` must stay outside the legacy partial because both V1 and V2 paths use it
