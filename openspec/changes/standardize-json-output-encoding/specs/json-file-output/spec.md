# Spec Delta

## Purpose

Define how CLI commands encode and format the JSON files they create or replace, so generated configuration and export files are readable, reviewable, and consistent across modules.

## ADDED Requirements

### Requirement: Generated JSON files preserve readable Unicode text

JSON files created or replaced by CLI commands SHALL be written as UTF-8 without a byte order mark. Printable non-ASCII characters, including umlauts, accented letters, and no-break spaces, SHALL be written as literal characters instead of `\uXXXX` escapes. Only characters that JSON requires to be escaped (quotation mark, reverse solidus, and control characters) SHALL be escaped.

#### Scenario: Captured display name with umlauts
- **WHEN** a capture writes a rule or process named `Kontakte mit derselben geschäftlichen Telefonnummer`
- **THEN** the file contains `geschäftlichen` literally and no `\u00E4` escape

#### Scenario: Required escapes are kept
- **WHEN** a written value contains a quotation mark, backslash, tab, or newline
- **THEN** the value is escaped as JSON requires and the file parses back to the original string

### Requirement: Generated JSON output is consistent across commands

All commands that write JSON files intended for users or version control SHALL apply the same encoding and indentation rules, independent of the module that writes them. HTML-sensitive characters such as `<`, `>`, and `&` SHALL NOT be escaped because the files are not embedded in HTML.

#### Scenario: Same value written by different commands
- **WHEN** two different commands write the same string value
- **THEN** both files encode that value identically

### Requirement: Readers accept escaped and unescaped input

Commands that read JSON files SHALL accept both literal non-ASCII characters and equivalent `\uXXXX` escapes, so files written before this change remain valid input.

#### Scenario: Previously captured file
- **WHEN** apply reads a configuration file written with `\u00E4` escapes
- **THEN** it interprets the value identically to one written with literal `ä`
