# Resource-Oriented Command Modules

## Context

The former combined `push` module deployed unrelated Dataverse resources through coupled command and service code. The current CLI also has focused capabilities and cross-cutting infrastructure that do not map one-to-one to a Dataverse resource.

## Decision

Use independent modules for cohesive business resources, with resource-first command paths such as `dgtp plugin push` and `dgtp webresource push`. Keep focused capabilities such as analysis and code generation, and infrastructure/operator capabilities such as connections and telemetry, as justified modules. Do not require an assembly for every CLI verb, Dataverse table, or technical operation. Keep shared infrastructure in `dgt.power.common`; keep resource-specific logic with its owning module.

## Rationale

Resource areas have independent behavior, validation rules, and tests. Separate modules avoid false shared abstractions and let each area evolve on its own. Cohesive capability and infrastructure modules avoid fragmenting the solution into assemblies without a meaningful ownership boundary.

## Consequences

Cross-module infrastructure remains shared, but module-local planners, repositories, DTOs, and executors stay owned by their feature module unless a real cross-module consumer requires sharing. Public type moves require compatibility review. Historical migration details belong in the version migration guide, not here.