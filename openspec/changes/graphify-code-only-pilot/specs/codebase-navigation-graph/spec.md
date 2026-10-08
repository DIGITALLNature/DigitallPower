# Spec Delta

## Purpose

Provide contributors and AI agents a fresh, code-derived relationship graph for navigating across DigitallPower modules without making the generated graph a source of truth.

## ADDED Requirements

### Requirement: Code graph generation requires no semantic model calls

The pilot graph MUST be generated from supported code in the current checkout without requiring a semantic LLM backend.

#### Scenario: A local or cloud setup builds the graph

- **WHEN** the Graphify setup runs for a checkout
- **THEN** it creates the graph from supported code using local extraction
- **AND** it does not call a semantic model backend

### Requirement: Generated graph state is regenerable and untracked

Generated graph files, caches, and machine-specific paths MUST remain outside version control. A contributor or agent MUST be able to regenerate the graph from tracked source using the pinned Graphify version.

#### Scenario: A fresh clone prepares the graph

- **WHEN** a contributor or cloud agent runs the documented setup
- **THEN** the graph is generated for that clone's current source tree
- **AND** generated graph state does not appear as repository changes

### Requirement: The graph is navigation context, not project authority

Project AI instructions MUST recommend graph queries for cross-module and architecture questions, require verification of important findings against source or tests, and allow direct source exploration when the graph is absent, stale, or unhelpful.

#### Scenario: An agent investigates a cross-module question

- **WHEN** a relevant graph is available
- **THEN** the agent may use a scoped graph query to locate likely code paths
- **AND** it verifies conclusions against current source or tests

#### Scenario: The graph is unavailable or unhelpful

- **WHEN** graph generation or a graph query fails, or does not answer the question
- **THEN** the agent continues by searching source and tests
- **AND** it does not treat the graph as an authoritative requirement

### Requirement: Local graph refresh is best-effort

Local Git hooks MUST refresh code-derived graph state after relevant commits and branch changes where supported. A missing Graphify installation or refresh failure MUST NOT prevent the underlying Git operation from completing.

#### Scenario: A contributor commits or switches branches

- **WHEN** a Graphify-aware local hook runs
- **THEN** it requests a code-only graph refresh for the current checkout
- **AND** the Git operation remains successful if Graphify is unavailable or refresh fails

### Requirement: GitHub Actions validates code-only graph generation

The Graphify validation workflow MUST build the code-only graph from the checkout using the pinned Graphify version and MUST NOT commit or push generated graph files.

#### Scenario: A pull request changes graph inputs

- **WHEN** the Graphify validation workflow runs
- **THEN** it completes a code-only graph build from the pull-request checkout
- **AND** it fails visibly if graph generation fails
- **AND** it leaves generated graph state out of the pull request