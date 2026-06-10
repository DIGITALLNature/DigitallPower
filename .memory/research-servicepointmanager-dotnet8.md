# Research: ServicePointManager is a no-op on .NET 8+

## Finding

`System.Net.ServicePointManager` is **completely inoperative** on .NET 8+ for all `HttpClient`-based connections.

- `ServicePointManager.SecurityProtocol` and `ServicePointManager.ServerCertificateValidationCallback` only affect the legacy `HttpWebRequest` stack.
- `HttpClient` (and its underlying `SocketsHttpHandler`) ignores `ServicePointManager` entirely.
- `Microsoft.PowerPlatform.Dataverse.Client` v1.2.10 (latest as of 2026-06) uses `HttpClient` internally.

## Consequence

The `--insecure` and `--security-protocol` CLI options in `dgt.power.profile` were **silently doing nothing** on .NET 8+.

## What Was Done

Both options were removed as a **breaking change** (see `decision-remove-insecure-protocol.md`).

## TLS Configuration on .NET 8+

To configure TLS/certificate validation in .NET 8+, you must:
1. Configure the `SocketsHttpHandler` directly when constructing `HttpClient`
2. Or use OS-level trust store configuration

`Microsoft.PowerPlatform.Dataverse.Client` v1.2.10 does **not** expose an `HttpClient` or `HttpMessageHandler` constructor — no injection point exists. Verified by inspecting XML docs and DLL strings.

## Tooling Warning

The compiler emits `SYSLIB0014` when `ServicePointManager` is referenced. This is an **obsolete API** warning.
