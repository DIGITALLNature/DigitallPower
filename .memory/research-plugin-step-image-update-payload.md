# Plugin Step Image Update Payload

`SdkMessageProcessingStepImageRepository.UpdateAsync` sends the existing image ID, parent step
reference (`sdkmessageprocessingstepid`), and attributes. It does not resend name, entity alias,
image type, or message property name. `PluginTypeDeploymentExecutor` supplies the actual step ID
returned by step execution, including when a step is migrated.

This avoids the former attributes-only update payload. A live Dataverse fault identified
`SdkMessageProcessingStepImageServiceInternal.Update` as the source of a null reference exception.
Sparse updates are normally valid SDK operations. The reported mitigation is to include the
parent step reference, not the full image registration. Keep the payload narrow so an
attribute-list update does not overwrite unrelated registration fields. Resolution of the
platform fault requires a live retry; local fakes cannot establish Dataverse's internal requirements.

Null and empty attribute lists both explicitly clear the attributes field, meaning all
attributes. Keep this behavior when including the parent step reference in update payloads.

Repository tests verify the parent reference on an initially sparse image record and clearing
the attributes field. An executor test changes image attributes on a second push and verifies
the same image and parent step IDs are retained while other registration fields remain untouched.
These fakes do not reproduce Dataverse's internal image-update service.
