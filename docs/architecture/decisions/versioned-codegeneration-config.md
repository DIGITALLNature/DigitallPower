# Versioned Code-Generation Configuration

## Context

The legacy flat configuration mixed .NET-only, TypeScript-only, and shared options, so users carried settings irrelevant to their output target.

## Decision

Use V2 typed configurations selected by a required `type` discriminator, with shared scope and target-specific output settings. Keep V1 loading as a deprecated compatibility path through one configuration factory.

## Rationale

The output targets have distinct settings and are normally generated from separate configs. Explicit routing gives each target a coherent schema while avoiding an abrupt break for existing V1 users.

## Consequences

The factory owns version routing. V1 receives a deprecation warning; V2 schema and model changes must remain synchronized. V2 dispatch is manual because System.Text.Json polymorphic metadata conflicts with user-authored `$schema` properties.