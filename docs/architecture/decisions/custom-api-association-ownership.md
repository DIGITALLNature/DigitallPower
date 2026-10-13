# Custom API Association Ownership

## Context

A Custom API is handled by a plugin type, but API records are shared and provisioned outside plugin push.

## Decision

Push reconciles handler associations, never API records. An explicit declaration links the API even when another target currently handles it. Omission unlinks only associations handled by types owned by the target, including the superseded standalone assembly. Omission is computed across a whole package.

## Rationale

Records and parameters are external prerequisites, so deleting them would exceed what a plugin artifact owns. Authority over the handler link lets declarations move a handler between targets. Package-wide omission prevents a handler move between package assemblies from producing both a link and an unlink.

## Consequences

Duplicate declarations, missing APIs, and ambiguous unique names fail before artifact writes. Clearing a link does not propagate through managed solution import: downstream environments keep the old link, and removing the type or assembly later fails on import until the link is cleared manually. Platform-created steps and package asset removal keep conservative guards until their lifecycle is verified.