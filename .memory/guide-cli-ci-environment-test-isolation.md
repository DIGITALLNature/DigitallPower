# Guide: CLI CI Environment Test Isolation

`ExecutionEnvironmentTests` and `TelemetryConfigTests` both mutate the process-wide CI
environment variables exposed by `ExecutionEnvironment.CiEnvironmentVariables`.
Both classes use `[NotInParallel("CiEnvironmentVariables")]` to prevent overlapping
mutations while allowing unrelated tests to run concurrently. Class-specific keys
are insufficient: they serialize tests within each class, but not between the two classes.

To distinguish this race from a behavior regression, run the CLI test assembly with
`dotnet exec tests\dgt.power.cli.tests\bin\Debug\net10.0\dgt.power.cli.tests.dll --maximum-parallel-tests 1`,
or isolate the affected class with `--treenode-filter`.
Any additional CLI tests that mutate or depend on these variables must use the same
exclusion key rather than a class-specific key.
