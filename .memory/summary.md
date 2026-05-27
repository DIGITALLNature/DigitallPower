# Project Summary

## Overview

**DigitallPower** — CLI tooling for Dataverse/Power Platform operations (export, import, push, maintenance, code generation, analysis, profiling).

## Architecture

- **Framework:** .NET 10, C# latest
- **CLI Host:** `dgt.power` (entry point)
- **Common Layer:** `dgt.power.common` (shared abstractions, extensions, fixtures)
- **Modules:** `dgt.power.{analyzer,codegeneration,export,import,maintenance,profile,push}`
- **Models:** `dgt.power.dataverse`, `dgt.power.dto`
- **Tests:** TUnit framework, one test project per module in `tests/`



## Current Status

No active tasks or ongoing work tracked.

## Key Decisions

_(None recorded yet — add `.memory/decision-*.md` files as decisions are made)_

## Known Caveats

_(None recorded yet — add findings here as they are discovered)_
