# Spec Delta

## Purpose

Define how a `dgtp` command run is executed and observed: when Dataverse is contacted, what telemetry a run produces, and which exit codes callers can rely on.

## ADDED Requirements

### Requirement: Dataverse is contacted only when a command needs it

A command MUST NOT authenticate or open a Dataverse connection unless its execution requires Dataverse access. Connection options (`--connection`, `--connection-string`, `--non-interactive`) MUST be honored when the connection is first needed.

#### Scenario: A command without Dataverse access runs

- **WHEN** a command that does not use Dataverse, such as `connection list`, is executed
- **THEN** no authentication is attempted and no Dataverse request is sent

#### Scenario: A Dataverse command runs with an explicit connection

- **WHEN** a command that uses Dataverse is executed with `--connection <name>`
- **THEN** the named connection is used for every Dataverse request of that run

### Requirement: One connection per run

Within a single command run, all Dataverse access MUST share one authenticated connection, established no more than once even if first requested concurrently.

#### Scenario: Several components request Dataverse access

- **WHEN** multiple components of one run request Dataverse access, including at the same time
- **THEN** authentication and the connection verification request happen once

### Requirement: Each run produces one telemetry span with its result

When telemetry is enabled and not suppressed with `--no-telemetry`, a command run MUST produce exactly one span named after the command that records the run's result as success or failure. The span MUST be closed on every exit path, including exceptions.

#### Scenario: A command completes successfully

- **WHEN** a traced command finishes successfully
- **THEN** its span is marked successful and closed

#### Scenario: A command fails with an exception

- **WHEN** a traced command throws
- **THEN** the anonymized exception is recorded on that command's span and the span is closed with an error status

#### Scenario: Telemetry is suppressed

- **WHEN** `--no-telemetry` is passed
- **THEN** no span is produced

### Requirement: Each run is announced at start and end

A traced command run MUST print a start line and an end line that identify the command by the path the user typed and state the result and duration at the end. Telemetry settings MUST NOT affect these lines, and they MUST NOT be mixed into the command's data output.

#### Scenario: Consecutive commands in a script

- **WHEN** two commands run one after another
- **THEN** each run shows its own start line and an end line with result and duration, so the runs can be told apart in the log

#### Scenario: A command fails

- **WHEN** a command fails
- **THEN** the end line states the failure and the duration

#### Scenario: A command's output is redirected

- **WHEN** a command's data output is redirected to a file
- **THEN** the file contains no start or end lines

### Requirement: Exit code contract

A command MUST exit with `0` when the run succeeded, `1` when it failed (including a missing required configuration), and `2` when interactive authentication is required. Callers MUST NOT need to distinguish other failure kinds by exit code.

#### Scenario: A run fails

- **WHEN** a command fails for any reason other than required interactive authentication, including a missing required configuration
- **THEN** the process exits with `1`

#### Scenario: A run has nothing to do

- **WHEN** a command determines there is nothing to execute
- **THEN** the process exits with `0`

#### Scenario: Interactive sign-in is required

- **WHEN** a Dataverse command needs interactive sign-in and `--non-interactive` is set
- **THEN** the process prints the `AUTH_REQUIRED:` message and exits with `2`
