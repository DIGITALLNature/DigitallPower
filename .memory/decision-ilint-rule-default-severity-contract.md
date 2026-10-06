# ILintRule Default Severity Contract

`ILintRule.DefaultSeverity` remains part of the public interface. `LintRuleCatalog.All` exposes
`ILintRule` publicly, so callers can inspect each rule's default severity even though the built-in
evaluation path currently reads the property through concrete rule implementations.

Qodana's `UnusedMemberInSuper.Global` finding is therefore treated as a false positive for this
public metadata contract. Keep a narrow suppression on the interface property rather than removing
it and breaking consumers; reconsider only as an intentional API change.