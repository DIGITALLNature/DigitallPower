# Post-TSL Architecture Wave Decisions

## Context

After completing TSL stabilization (P0–P2), remaining quality work is driven by architecture-level static analysis findings and release-risk prioritization.

## Decisions

1. **VSTHRD200 in template callbacks**
   - Do not rename Liquid filter callback methods to `*Async`.
   - Reason: callback names are part of a stable DSL/API surface and should not be broken for analyzer cosmetics.
   - Action model: narrow, documented suppressions for callback-signature hotspots.

2. **VSTHRD002 sync-over-async**
   - Treat as release-relevant for production paths.
   - Preferred approach is async propagation end-to-end; sync boundaries need explicit architectural justification.

3. **S1067 / S3358 complexity**
   - Address in risk-based waves:
     - Wave A: highest-impact readability/maintainability hotspots first.
     - Wave B: broad cleanup pass in remaining modules.
   - Keep behavior unchanged; refactor for clarity only.

4. **S1135 / S125 hygiene**
   - `src/**`: findings are currently accepted as technical debt and tracked via baseline.
   - `tests/**`: TODOs may remain when tied to explicit follow-up tracking.

5. **S3717 not implemented**
   - For intentionally unsupported functionality, use `NotSupportedException` with actionable message.
   - Reserve `NotImplementedException` for truly in-progress, short-horizon implementation work.

## Prioritized Execution Order

1. VSTHRD002
2. S3717
3. S1135/S125 in `src/**`
4. S3358 + S1067 Wave A
5. VSTHRD200 callback-policy cleanup
6. S1067 Wave B

## Execution Note

- Execution was intentionally reordered to complete points 1, 2, 4, 5 and 6 first.
- `S1135`/`S125` in `src/**` were moved to baseline as explicit technical debt.
