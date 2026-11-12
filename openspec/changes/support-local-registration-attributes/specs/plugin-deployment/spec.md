# Spec Delta

## ADDED Requirements

### Requirement: Declarative registrations may be defined in the target plugin assembly

`dgtp plugin push` MUST recognize supported declarative registration attributes when they are defined either by the official `Digitall.Plugins.Registration` library or by the target plugin assembly being inspected.

#### Scenario: Registration attributes come from the official library

- **WHEN** a plugin type uses a supported registration attribute from `Digitall.Plugins.Registration`
- **THEN** `plugin push` recognizes the registration

#### Scenario: Registration attributes are defined in the target assembly

- **WHEN** a plugin type uses a supported registration attribute defined in the target plugin assembly
- **THEN** `plugin push` recognizes the registration

#### Scenario: Registration attributes come from an unrelated assembly

- **WHEN** a plugin type uses an attribute with a supported namespace and name that is defined by neither the official library nor the target plugin assembly
- **THEN** `plugin push` does not recognize it as a registration
