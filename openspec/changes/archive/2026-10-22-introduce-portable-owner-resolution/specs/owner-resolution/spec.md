# Spec Delta

## Purpose

Resolve portable logical owner references to existing Dataverse target principals without assigning records or orchestrating resource lifecycle.

## ADDED Requirements

### Requirement: Logical owners are separate from target identities

Owner intent SHALL refer to a logical key resolved through an explicit target-environment mapping. Missing mappings SHALL fail resolution. Resolution SHALL NOT infer an owner from the connection identity, the most common existing owner, or a display-name guess.

#### Scenario: Same intent across environments
- **WHEN** the same logical owner key has different valid target mappings in two environments
- **THEN** each environment resolves its own mapped principal without changing the resource intent

#### Scenario: Missing mapping
- **WHEN** a requested logical owner key has no mapping
- **THEN** resolution fails explicitly without using a default principal

### Requirement: Mapping files have a strict versioned contract

Mapping files SHALL require `schemaVersion: 1` and an `owners` object and MAY contain a root `$schema` URI. Logical keys SHALL be nonblank, ordinal case-sensitive, and free of leading/trailing whitespace. Unknown/duplicate properties, duplicate keys, null entries, invalid selectors, empty GUIDs, and unsupported versions SHALL fail local validation. Selector strings SHALL be nonblank without surrounding whitespace.

#### Scenario: Invalid unused entry
- **WHEN** an unused entry has an unknown property or invalid selector combination
- **THEN** local validation fails before any target lookup

#### Scenario: Duplicate JSON property
- **WHEN** a property or owner key occurs twice in the same JSON object
- **THEN** loading fails rather than keeping the first or last value

#### Scenario: Case-sensitive logical key
- **WHEN** only `salesOwner` is defined and `SalesOwner` is requested
- **THEN** resolution reports a missing key without normalizing its spelling

### Requirement: Mapping loading is explicit

Consumers SHALL use `--owner-mapping <File>` with relative paths resolved against the working directory. Supplied files SHALL always be structurally validated. Owner resolution and owner capture SHALL require mapping; state-only operations SHALL NOT require it. Loading SHALL NOT search default paths, select an environment automatically, or acquire a Dataverse connection.

#### Scenario: State-only operation
- **WHEN** a consumer requests no owner resolution and supplies no mapping
- **THEN** ownership infrastructure requires no mapping or target lookup

#### Scenario: Explicit invalid file
- **WHEN** a mapping path is explicitly supplied but is missing or invalid
- **THEN** loading fails even if no ownership change was requested

### Requirement: Principal selectors are explicit alternatives

A `user` entry SHALL contain exactly one of `entraObjectId` or `domainName`; an `applicationUser` SHALL contain `applicationId`. A `team` SHALL contain either `entraObjectId` with optional `businessUnitName` and `membershipType`, or `namePattern` with required `businessUnitName`. Other combinations and Dataverse user/team ID selectors SHALL be rejected. No lookup SHALL fall back to another selector.

#### Scenario: User supplies two selectors
- **WHEN** a user entry specifies both domain name and Entra object ID
- **THEN** local validation fails rather than treating either as a fallback

#### Scenario: Domain name lookup
- **WHEN** a user is selected by domain name
- **THEN** resolution matches `systemuser.domainname` rather than substituting email or another directory attribute

#### Scenario: Application identity
- **WHEN** an application user is selected by application/client ID
- **THEN** resolution matches the target Dataverse application identity

### Requirement: Team qualifiers constrain resolution

A supplied `businessUnitName` SHALL first resolve to exactly one BU by ordinal case-insensitive exact name without wildcards. All supplied team qualifiers SHALL combine with AND. Entra membership values SHALL be `membersAndGuests`, `members`, `owners`, or `guests`; membership filtering SHALL be allowed only with `entraObjectId`. The final team match SHALL be unique.

#### Scenario: Multiple teams share an Entra group
- **WHEN** an Entra group ID matches several teams and qualifiers select exactly one
- **THEN** that team is resolved

#### Scenario: Unqualified group is ambiguous
- **WHEN** the supplied group ID matches several teams and no qualifiers distinguish them
- **THEN** resolution fails as ambiguous

#### Scenario: Business unit name is ambiguous
- **WHEN** a BU name matches several business units
- **THEN** resolution fails even if only one team would otherwise match

### Requirement: Team glob matching has bounded literal semantics

Team name patterns SHALL match the entire name using ordinal case-insensitive comparison. Only `*` SHALL mean zero or more characters; every other character SHALL be literal. Domain and BU names SHALL use ordinal case-insensitive exact matching. Retrieval SHALL preserve these semantics across all candidate pages, independent of database collation, without truncation before matching.

#### Scenario: Prefix pattern
- **WHEN** `namePattern` is `Sales-*` within one resolved BU
- **THEN** names starting with `Sales-` match without case sensitivity
- **AND** names merely containing that prefix do not match

#### Scenario: Literal metacharacters
- **WHEN** a pattern contains `?`, `%`, `_`, or brackets
- **THEN** those characters are literal and do not expand the match

#### Scenario: Later page contains another match
- **WHEN** a second matching candidate occurs on a later retrieval page
- **THEN** resolution reports ambiguity rather than returning the first page's match

### Requirement: Principal kinds are explicit

Resolution SHALL cover regular users, application users, and owning teams, with an explicit kind per mapping. Wrong or unknown kinds and missing classification metadata SHALL fail. Owner, security-group, and Microsoft 365 group teams SHALL be supported; access teams SHALL be rejected. Results SHALL include principal reference, kind, and relevant eligibility metadata. Disabled users SHALL remain resolvable; consumers decide assignment admissibility.

#### Scenario: Application user is requested
- **WHEN** an application-user mapping resolves uniquely to a valid target application user
- **THEN** the result identifies that Dataverse user and classifies it as an application user

#### Scenario: Owning team is requested
- **WHEN** a team mapping resolves uniquely to a supported team capable of owning records
- **THEN** the result identifies that Dataverse team and its kind

#### Scenario: Regular user is requested
- **WHEN** a regular-user mapping resolves uniquely to a valid target regular user
- **THEN** the result identifies that Dataverse user and classifies it as a regular user

#### Scenario: Principal kind is wrong
- **WHEN** a mapping resolves to a principal of a different or unsupported kind
- **THEN** resolution fails rather than coercing the principal kind

#### Scenario: Disabled user
- **WHEN** a selector uniquely identifies a disabled user of the requested kind
- **THEN** resolution returns that principal with disabled status
- **AND** does not claim that the consuming resource can assign it

#### Scenario: Access team
- **WHEN** a selector uniquely identifies an access team
- **THEN** resolution fails because the team cannot own records

### Requirement: Target lookup is unambiguous

Every requested owner SHALL resolve to exactly one existing principal using its explicit supported selector. Zero or multiple matches SHALL fail with diagnostics identifying the logical key and failure category. Resolution SHALL NOT silently select the first match or fall back to another selector.

#### Scenario: Ambiguous target
- **WHEN** a mapped selector matches multiple target principals
- **THEN** resolution fails as ambiguous

#### Scenario: Missing target
- **WHEN** a mapped selector matches no target principal
- **THEN** resolution fails as not found

#### Scenario: Ineligible match does not hide ambiguity
- **WHEN** a team pattern matches both an owner team and an access team
- **THEN** resolution fails as ambiguous rather than selecting only the owner team

### Requirement: Forward resolution is limited to requested keys

After validating the whole mapping locally and checking requested keys exist, forward resolution SHALL query only requested entries. Unused structurally valid entries SHALL NOT cause target lookups. Different requested logical keys MAY resolve to the same principal. An empty mapping SHALL be valid when no keys are required.

#### Scenario: Unused target is absent
- **WHEN** an unused valid mapping identifies a principal absent from the target
- **THEN** forward resolution of other requested keys is unaffected

#### Scenario: Aliases in apply
- **WHEN** two requested keys resolve to the same principal
- **THEN** forward resolution succeeds for both keys

### Requirement: Owner capture requires an unambiguous source mapping

Capture SHALL fully resolve its supplied source mapping, then map each captured owner reference to exactly one logical key by entity name and ID. Any mapping-resolution failure or missing/ambiguous reverse match SHALL fail capture. No keys, default owners, raw-ID substitutes, or mapping files SHALL be invented. Consumers SHALL complete capture validation before writing configuration output.

#### Scenario: Unique capture mapping
- **WHEN** exactly one resolved key identifies the captured owner
- **THEN** capture represents that owner using that key

#### Scenario: Aliases for captured owner
- **WHEN** several keys identify a captured owner
- **THEN** capture fails rather than choosing a key

#### Scenario: Owner has no key
- **WHEN** no key identifies a captured owner
- **THEN** capture fails without emitting a substitute owner identity

#### Scenario: Unused source mapping cannot resolve
- **WHEN** any source-mapping entry fails target resolution during owner capture
- **THEN** capture fails even if that entry would not identify a captured owner

#### Scenario: Aliases for an uncaptured principal
- **WHEN** several keys resolve to a principal not among the captured owners
- **THEN** those aliases do not make the captured owners ambiguous

### Requirement: Failures remain explicit and typed

Shared resolution SHALL expose typed mapping/resolution errors with logical-key or captured-reference context and a distinguishable failure category. It SHALL NOT print console output, fabricate successful results, or convert authentication/service errors into missing-target results. Authentication/service failures and cancellation SHALL propagate. No application-level automatic retry or assignment recovery SHALL be introduced.

#### Scenario: Lookup service fails
- **WHEN** Dataverse rejects or fails a lookup
- **THEN** the service failure propagates rather than becoming a not-found owner

#### Scenario: Invalid selector
- **WHEN** local validation rejects a selector
- **THEN** the error identifies its mapping context and validation failure category without printing from shared code

### Requirement: Resolution has no resource side effects

The resolver SHALL only resolve principals and report resolution failures. It SHALL NOT assign records, publish or deactivate resources, provision principals, change permissions, or impersonate users. Consumer-specific assignment eligibility and lifecycle sequencing SHALL remain with the resource. Resolution SHALL NOT guarantee server-side assignment permission.

#### Scenario: Resource rejects an otherwise resolved owner
- **WHEN** a principal resolves but its kind is not admissible for a consuming resource
- **THEN** the consumer rejects the requested assignment before its writes
- **AND** the resolver has not modified the resource or principal

### Requirement: Resolution is scoped to the command target

Resolution SHALL use the command's shared target Dataverse connection without opening a separate directory-authentication flow. Construction SHALL NOT perform I/O. Cancellation SHALL propagate without producing a successful resolution or fallback principal.

#### Scenario: Cancelled resolution
- **WHEN** cancellation is observed during a target lookup
- **THEN** resolution stops and propagates cancellation rather than substituting an owner

#### Scenario: Constructing the resolver
- **WHEN** the resolver is constructed
- **THEN** it performs no network, authentication, or disk I/O
