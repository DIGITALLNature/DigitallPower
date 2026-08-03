// Copyright (c) DIGITALL Nature. All rights reserved

# Connection command test pattern

When testing `dgt.power.connection` commands, seed identities on the same `ProfileManager` instance passed into the command under test. `ProfileManager.Save()` must be called after seeding so the command sees the current identity state.

For MSAL-only branches, use a `TokenIdentity` and a fake `IXrmConnection`. That is the simplest way to exercise `ConnectionRefreshCommand` without depending on external auth.

Keep command-context helpers local to the test file; each test project needs its own `IRemainingArguments` stub for `CommandContext`.
