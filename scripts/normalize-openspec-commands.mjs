import { readdir, readFile, writeFile } from "node:fs/promises";
import { dirname, join, resolve } from "node:path";
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
        const normalized = content
            .replace(commandPattern, "pnpm exec openspec ")
            .replaceAll(globalInstallFallback, localInstallGuidance);

        if (normalized !== content) {
            await writeFile(filePath, normalized);
            updatedCount++;
        }
    }
}

console.log(`Normalized local OpenSpec commands in ${updatedCount} generated file(s).`);