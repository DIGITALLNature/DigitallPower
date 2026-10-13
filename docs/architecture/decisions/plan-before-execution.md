# Plan Before Execution

## Context

Deployment commands need a trustworthy preview, including dry-run, without making the renderer a second source of deployment decisions.

## Decision

Build one typed deployment plan and use that same plan for rendering and execution. Executors apply planned actions; they do not repeat reconciliation decisions.

## Rationale

Preview and execution must describe the same mutations. A shared plan makes that invariant explicit and allows planner, renderer, and executor behavior to be tested independently.

## Consequences

Planning validates remote state before writes. Dry-run stops after rendering. Progress is reported only after successful writes so partial execution remains visible without coupling domain execution to terminal output.