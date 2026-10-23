import { readdir, readFile, writeFile } from "node:fs/promises";
import { dirname, join, relative, resolve } from "node:path";
import { fileURLToPath } from "node:url";

const repositoryRoot = resolve(dirname(fileURLToPath(import.meta.url)), "..");
const generatedDirectories = ["agents", "prompts", "skills"].map((directory) =>
    join(repositoryRoot, ".github", directory));
const commands = [
    "--version",
    "context",
    "new",
    "status",
    "instructions",
    "validate",
    "archive",
    "list",
    "show",
    "sync",
    "view",
    "doctor",
    "config",
    "schemas",
    "spec",
    "templates",
    "store",
    "init",
    "update"
];
const commandPattern = new RegExp(
    `(?<!pnpm exec )\\bopenspec (?=(?:${commands.join("|")})\\b)`,
    "g"
);
const globalInstallFallback =
    "If it is unavailable, install it with `npm install -g @fission-ai/openspec`.";
const localInstallGuidance =
    "Do not install OpenSpec globally; use the repository-local CLI through pnpm.";
const archiveGateFiles = new Set([
    ".github/agents/openspec.agent.md",
    ".github/prompts/opsx-apply.prompt.md",
    ".github/prompts/opsx-archive.prompt.md",
    ".github/prompts/opsx-update.prompt.md",
    ".github/skills/openspec-apply-change/SKILL.md",
    ".github/skills/openspec-archive-change/SKILL.md",
    ".github/skills/openspec-update-change/SKILL.md"
]);
const archiveGate = `<!-- digitallpower:post-merge-archive -->
## Repository archive gate (required)

This repository rule takes precedence over archive suggestions and completion examples below.
Keep the change active until its implementation PR has successfully merged into \`beta\` or
\`main\` (exact, case-sensitive branch names). Finished tasks, an open or approved PR, green CI,
a closed-but-unmerged PR, or a merge into another branch do not permit archiving.

Before any archive-related writes, verify the implementation PR for the selected change:
\`gh pr view <number-or-url> --repo DIGITALLNature/DigitallPower --json state,mergedAt,baseRefName,url\`.
Require \`state: MERGED\`, non-null \`mergedAt\`, and \`baseRefName\` equal to \`beta\` or \`main\`.
Confirm that this PR actually contains the selected change; do not use an unrelated merged PR
or infer merge success from the current branch. If the PR is unknown, ask for it. If verification
fails or is unavailable, report the blocker and stop without archiving; confirmation or optional
operation guidance cannot bypass this gate.

After implementation, report "Implementation complete; awaiting merge into beta or main" rather
than offering immediate archival. After a verified merge, archive as a separate post-merge
follow-up using a checkout containing the merged implementation, not in the implementation PR.
<!-- /digitallpower:post-merge-archive -->`;

async function findMarkdownFiles(directory) {
    let entries;
    try {
        entries = await readdir(directory, { withFileTypes: true });
    } catch (error) {
        if (error.code === "ENOENT") {
            return [];
        }

        throw error;
    }

    const files = [];
    for (const entry of entries) {
        const entryPath = join(directory, entry.name);
        if (entry.isDirectory()) {
            files.push(...await findMarkdownFiles(entryPath));
        } else if (entry.isFile() && entry.name.endsWith(".md")) {
            files.push(entryPath);
        }
    }

    return files;
}

let updatedCount = 0;
for (const directory of generatedDirectories) {
    for (const filePath of await findMarkdownFiles(directory)) {
        const content = await readFile(filePath, "utf8");
        let normalized = content
            .replace(commandPattern, "pnpm exec openspec ")
            .replaceAll(globalInstallFallback, localInstallGuidance);

        if (archiveGateFiles.has(relative(repositoryRoot, filePath).replaceAll("\\", "/"))) {
            normalized = normalized.replace(
                /<!-- digitallpower:post-merge-archive -->[\s\S]*?<!-- \/digitallpower:post-merge-archive -->\r?\n*/g,
                ""
            );
            const frontmatter = /^(---\r?\n[\s\S]*?\r?\n---\r?\n)\s*/;
            if (!frontmatter.test(normalized)) {
                throw new Error(`Missing YAML frontmatter in ${filePath}`);
            }
            normalized = normalized.replace(frontmatter, `$1\n${archiveGate}\n\n`);
        }

        if (normalized !== content) {
            await writeFile(filePath, normalized);
            updatedCount++;
        }
    }
}

console.log(`Normalized local OpenSpec commands in ${updatedCount} generated file(s).`);