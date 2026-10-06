# Plugin registration v3 cutoff

## Decision

`plugin push` recognizes only attributes in the `Digitall.Plugins.Registration` namespace. The
supported user-facing requirement is `Digitall.Plugins.Registration` 2.0.0 or later.

Historical registration namespaces (`D365.Extension.Registration`, `DGT.Registrations`,
`dgt.registration`, and `Digitall.APower.Registration`) are deliberately not recognized by the
resource-oriented v3 plugin command. Users maintaining such assemblies must use dgtp v2 or upgrade
their registration package before deploying with v3.

## Rationale

The current registration contract includes Custom API, Custom Data Provider, and managed-identity
attributes that are not consistently available in older packages. Retaining namespace aliases would
imply compatibility that cannot be guaranteed and would preserve a growing legacy matrix.

`plugin push` still has no compile-time reference to the registration package. It inspects supported
attribute names and namespaces through `MetadataLoadContext` metadata.

## Compatibility

The 2.x combined `push` command remains available only to users who stay on dgtp 2.x. In dgtp 3.x,
the combined command is removed, and plugin deployments use `plugin push` with the registration
requirements above.
