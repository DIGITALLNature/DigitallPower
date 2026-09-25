# Plugin Step Secure Configuration

`sdkmessageprocessingstep.configuration` is the unsecure configuration field.
`sdkmessageprocessingstepsecureconfig` is a separate entity referenced by
`sdkmessageprocessingstep.sdkmessageprocessingstepsecureconfigid`.

The plugin push step repository currently does not retrieve or write the secure-config lookup.
Both configuration fields are environment-provisioned state for the strict module contract and
must be preserved for matching existing steps, not applied from registration attributes.

For strict declarative reconciliation, matching declared steps should be reassigned to the
replacement plugin type and then updated with registration metadata only, preserving their step ID
and both configuration associations. New declared steps have no configuration unless a future
explicit CI provisioning mechanism supplies it; removed declared steps delete configuration with
the old step.
