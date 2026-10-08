# Resource-Oriented Command Modules

## Context

The former combined `push` module deployed unrelated Dataverse resources through coupled command and service code.

## Decision

Keep plugin and webresource deployment in independent resource-named modules and expose resource-first command paths such as `dgtp plugin push` and `dgtp webresource push`.

## Rationale

The resource types have independent behavior and validation rules. Separate modules avoid a false shared abstraction and let each deployment flow evolve and be tested on its own.

## Consequences

Cross-module infrastructure remains shared, but module-local planners, repositories, and executors stay owned by their feature module. Historical migration details belong in the version migration guide, not here.