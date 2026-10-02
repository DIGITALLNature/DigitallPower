// Copyright (c) DIGITALL Nature. All rights reserved

# Connection command test pattern

`ConnectionTestsBase` creates a temporary `DgtpHome`, a file-backed `ConnectionStore`, and a fake
`ISecretStore`. Seed connections with `ConnectionStore.Upsert` and use the same store instance as
the command under test. This exercises the current JSON-backed persistence without touching a
developer's real connection data.

For status and refresh command tests, use a fake `IDataverseConnection` rather than invoking Azure
Identity or opening a browser. Authentication-record and credential-construction behavior should
be tested separately with injected stores/fakes. Keep command-context helpers local to the test
file; each test project may need its own `IRemainingArguments` stub for `CommandContext`.
