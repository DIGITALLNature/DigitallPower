# Plugin Assembly Version Trains

Manual Plugin Registration Tool verification established the Dataverse assembly version behavior
used by `plugin push`:

- Changes within the same major/minor version train, including lower build/revision values, are
  valid in-place assembly updates.
- A major or minor version change cannot be updated in place and must be registered as a new
  assembly, irrespective of whether the target train is numerically higher or lower.

`plugin push` should therefore treat major/minor as the update-versus-replacement boundary without
adding a downgrade guard. When multiple same-name version trains coexist, planning must select the
local assembly's matching major/minor train for an in-place update; otherwise it creates the target
train and applies the replacement lifecycle.
