# Registration Attributes Evaluation in Push Module

## Overview

The push module evaluates attributes from the `dgt.registration` NuGet package (and compatible namespaces) to automatically register plugin steps, Custom APIs, data providers, workflow activities, and managed identities.

## Supported Attributes

| Attribute | Target | Evaluated | PR |
|-----------|--------|-----------|-----|
| `PluginRegistrationAttribute` | Class | ✅ Yes — generates `SdkMessageProcessingStep` records | (original) |
| `CustomApiRegistrationAttribute` | Class | ✅ Yes — links PluginType to Custom API by message name | (original) |
| `CustomDataProviderRegistrationAttribute` | Class | ✅ Yes — generates MainOperation steps for virtual entity events | #152 |
| `WorkflowRegistrationAttribute` | Class | ✅ Yes — sets group/name metadata on workflow types | (original) |
| `ManagedIdentityRegistrationAttribute` | Assembly | ✅ Yes — creates/links `managedidentity` record | #153 |

## Known Namespaces

Attributes are matched by namespace membership:
- `D365.Extension.Registration`
- `DGT.Registrations`
- `dgt.registration`
- `Digitall.APower.Registration`
- `Digitall.Plugins.Registration`

## CustomDataProviderRegistrationAttribute

- **AllowMultiple:** true (one class can handle multiple events)
- **Constructor params:** `entityName` (string), `eventRegistration` (DataProviderEvent enum)
- **Generated step:** Stage=30 (MainOperation), Mode=Synchronous, PrimaryEntity=entityName
- **Event mapping:** 0=Retrieve, 1=RetrieveMultiple, 2=Create, 3=Update, 4=Delete
- **Graceful degradation:** If SDK message/filter not found in target env, prints warning and skips

## ManagedIdentityRegistrationAttribute

- **Target:** Assembly-level (`AttributeTargets.Assembly`)
- **Constructor params:** `clientId` (string)
- **Named params:** `TenantId` (string, optional)
- **Note:** Not yet in `dgt.registration` v1.0.1 — matched by type name string
- **Behavior:**
  1. Queries `managedidentity` table by `applicationid = clientId`
  2. Creates if not found (CredentialSource=2/ManagedIdentity, SubjectScope=1/Global)
  3. Updates `PluginAssembly.ManagedIdentityId` with EntityReference
- **Limitation:** Only for standalone assemblies; `PluginPackage` has no `ManagedIdentityId` field

## Architecture Notes

- Assembly-level attributes parsed in `ParseAssemblyLevelAttributes()` (called at start of `ParseAssembly()`)
- Class-level attributes parsed in the plugin type foreach loop
- `AssemblyModelBuilder` extracts attribute data into the model
- `AssemblyProcessor` performs the Dataverse CRUD operations
- `PushCommand` orchestrates the flow and decides when to call what
