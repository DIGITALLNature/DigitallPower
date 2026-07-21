# Qodana Fix Patterns: Telemetry Lifetime, XML Docs, and Test Hygiene

## Scope

Reusable patterns for analyzer findings that appeared in `dgt.power` telemetry/bootstrap code and related tests.

## 1. Avoid disposed-capture warnings for shared telemetry providers

When a `TracerProvider` is used both:
- in global exception handlers (`AppDomain.CurrentDomain.UnhandledException`, etc.), and
- at normal process shutdown,

avoid direct multi-site dispose calls on the same captured variable.

Use a single helper that atomically swaps the reference and disposes exactly once:

```csharp
void FlushAndDisposeTelemetryProvider()
{
    var provider = Interlocked.Exchange(ref tracerProvider, null);
    if (provider is null) return;
    provider.ForceFlush(5000);
    provider.Dispose();
}
```

This prevents closure/lifetime analyzer warnings and guarantees idempotent cleanup.

## 2. XML doc references across assembly boundaries

If a type is internal/inaccessible from the current assembly, avoid unresolved `<see cref="..."/>` references in XML comments.

Use `<c>TypeName</c>` text instead. This preserves documentation intent without broken symbol links.

## 3. Regex static field naming and literals

For static readonly regex fields, follow repository naming conventions (`s_` prefix), and remove unnecessary verbatim string literals (`@"..."`) when no escaping is needed.

## 4. Test-only analyzer hygiene

- Remove redundant `using` directives in tests.
- If helper members in test bases are unused, remove them unless they are part of an intentional test API surface.
- Prefer private/static fields for fixture exceptions and constants when values are not externally configured.
