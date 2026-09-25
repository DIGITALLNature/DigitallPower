# Review Feedback Triage

Automated pull-request review comments can refer to an earlier commit after follow-up fixes have
been pushed. Assess each comment against the current branch head, not its original diff hunk or
the reviewer's severity label.

For `plugin push`, validate a claimed reconciliation risk across the complete pipeline:

1. local reflection and registration-attribute filtering;
2. planner comparisons and deployment plans;
3. executor ordering; and
4. tests covering the relevant upgrade or dry-run path.

This avoids treating intentionally excluded manually managed plugin types, already-corrected
package ownership filtering, and stale implementation details as active defects.
