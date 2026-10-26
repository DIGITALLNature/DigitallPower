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
Keep the change active until a stable GitHub release containing the implementation has been
published from \`main\`. Merging into \`beta\` (including beta preparation for a later main release)
does not permit archiving. Merging into \`main\` without a published stable release, or publishing
a beta/prerelease, is also insufficient.

Before any archive-related writes, verify that the implementation PR for this change is merged
into \`main\`, and that the published stable release includes that PR:
1. Check the PR with \`gh pr view <number-or-url> --repo DIGITALLNature/DigitallPower --json state,mergedAt,baseRefName,mergeCommit,url\`. Require \`state: MERGED\`, non-null \`mergedAt\`, and \`baseRefName: main\`. Confirm it contains this change.
2. Check the corresponding GitHub release with \`gh release view <tag> --repo DIGITALLNature/DigitallPower --json isDraft,isPrerelease,publishedAt,tagName\`. Require a published, non-draft, non-prerelease release.
3. Fetch current \`main\` and the release tag, then verify the PR merge commit is an ancestor of the release tag and the release tag commit is reachable from \`origin/main\`.

Do not use an unrelated PR or release, or infer release status from the current branch. If the PR, release, or commit ancestry cannot be verified, report the blocker and stop without archiving; confirmation or optional guidance cannot bypass this gate.

After implementation, report "Implementation complete; awaiting stable release on main" rather
than offering immediate archival. Archive only as a separate post-release follow-up using a
checkout containing the released implementation, never during beta preparation or in the
implementation PR.
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