# Plugin Secure Configuration Provisioning

Plugin-step configuration—both unsecure and secure—is environment-provisioned state. It must not
be stored in source code, registration attributes, ordinary configuration files, plans, or logs.

The future plugin module capability is a separate post-deployment operation, rather than an
option on `plugin push`. CI pipelines provide secret values through an environment-secret provider
or standard input; command-line arguments must not carry secret values. The operation resolves
steps by stable declarative identity, not Dataverse GUIDs.

The core `plugin push` upgrade flow must preserve existing unsecure and secure configuration for a
matching declared step by reassigning that step to the replacement plugin type and applying only
registration metadata. New steps receive no configuration unless the future post-deployment
operation explicitly provisions it. `plugin push` does not read registration-attribute
configuration values while the registration library removes that property independently.
