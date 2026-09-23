# Plugin package content idempotence

`plugin push` identifies plugin packages by the publisher-prefixed Dataverse name. Dataverse package
versions are immutable, so version is not a deployment comparison input.

The legacy `content` memo field is not the deployed package payload. Dataverse stores it in the
read-only `package` file column (with `package_name` providing its name). `FindByNameAsync` first
finds the package ID and file-column presence by name, then downloads `package` using
`InitializeFileBlocksDownloadRequest` and `DownloadBlockRequest`. It hashes the downloaded blocks
incrementally with SHA-256 and exposes that hash in `RemotePackage`.
`PackageComparison` represents:

- `Create` when no matching package exists.
- `Update` when the remote file hash differs from the local `.nupkg` SHA-256 hash.
- `Unchanged` when the hashes match, regardless of version.

An unchanged package skips `UpdateContentAsync` and emits no execution progress event. The command
therefore reports `No changes applied` when no other package-owned assembly/type operation occurs.
