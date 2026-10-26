# Spec Delta

## MODIFIED Requirements

### Requirement: Owner capture requires an unambiguous source mapping

Capture SHALL fully resolve its supplied source mapping, then map each captured owner reference to exactly one logical key by entity name and ID. Any mapping-resolution failure or ambiguous reverse match SHALL fail capture. A captured owner without a matching key SHALL be reported as a typed skip instead of a failure when it is a platform principal, or when the consumer explicitly requests skipping unmapped owners; otherwise it SHALL fail capture. A platform principal is a system user whose full name is exactly `SYSTEM` or `INTEGRATION` and that has no domain name, Entra object ID, or application ID. An explicit key match SHALL take precedence over platform-principal detection. No keys, default owners, raw-ID substitutes, or mapping files SHALL be invented. Shared capture SHALL return skipped principals with their reason and SHALL NOT write console output; consumers SHALL report skipped records and complete capture validation before writing configuration output.

#### Scenario: Unique capture mapping
- **WHEN** exactly one resolved key identifies the captured owner
- **THEN** capture represents that owner using that key

#### Scenario: Aliases for captured owner
- **WHEN** several keys identify a captured owner
- **THEN** capture fails rather than choosing a key, even when skipping unmapped owners is requested

#### Scenario: Owner has no key
- **WHEN** no key identifies a captured owner that is not a platform principal and skipping is not requested
- **THEN** capture fails without emitting a substitute owner identity

#### Scenario: Platform principal owns a captured record
- **WHEN** no key identifies a captured owner that is the SYSTEM or INTEGRATION platform principal
- **THEN** capture reports that principal as skipped with reason platform principal, regardless of the unmapped-owner handling

#### Scenario: User named like a platform principal
- **WHEN** an unmapped captured user is named SYSTEM but has a domain name, Entra object ID, or application ID
- **THEN** it is treated as an ordinary unmapped owner

#### Scenario: Skipping unmapped owners
- **WHEN** skipping unmapped owners is requested and no key identifies a captured owner
- **THEN** capture reports that principal as skipped with reason unmapped instead of failing

#### Scenario: Unused source mapping cannot resolve
- **WHEN** any source-mapping entry fails target resolution during owner capture
- **THEN** capture fails even if that entry would not identify a captured owner

#### Scenario: Aliases for an uncaptured principal
- **WHEN** several keys resolve to a principal not among the captured owners
- **THEN** those aliases do not make the captured owners ambiguous
