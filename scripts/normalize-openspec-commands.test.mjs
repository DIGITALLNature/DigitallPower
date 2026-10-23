// Copyright (c) DIGITALL Nature. All rights reserved
import assert from "node:assert/strict";
import { execFileSync } from "node:child_process";
import { copyFile, mkdir, mkdtemp, readFile, rm, writeFile } from "node:fs/promises";
import { tmpdir } from "node:os";
import { dirname, join } from "node:path";
import { test } from "node:test";

test("normalization preserves the merge gate across generated workflow refreshes", async () => {
    const root = await mkdtemp(join(tmpdir(), "digitallpower-openspec-"));
    try {
        await mkdir(join(root, "scripts"));
        const script = join(root, "scripts/normalize-openspec-commands.mjs");
        await copyFile(new URL("./normalize-openspec-commands.mjs", import.meta.url), script);
        const targets = [
            "agents/openspec.agent.md",
            "prompts/opsx-apply.prompt.md",
            "prompts/opsx-archive.prompt.md",
            "prompts/opsx-update.prompt.md",
            "skills/openspec-apply-change/SKILL.md",
            "skills/openspec-archive-change/SKILL.md",
            "skills/openspec-update-change/SKILL.md"
        ];
        const frontmatter = "---\ndescription: Test workflow\n---\n";
        const generated = `${frontmatter}\nRun \`openspec archive example\`.\n`;
        for (const target of [...targets, "prompts/unrelated.prompt.md"]) {
            const path = join(root, ".github", target);
            await mkdir(dirname(path), { recursive: true });
            await writeFile(path, generated);
        }

        execFileSync(process.execPath, [script]);
        const firstRun = [];
        for (const target of targets) {
            const content = await readFile(join(root, ".github", target), "utf8");
            firstRun.push(content);
            assert.ok(content.startsWith(frontmatter));
            assert.ok(content.includes("`pnpm exec openspec archive example`"));
            assert.ok(content.includes("`state: MERGED`"));
            assert.ok(content.includes("non-null `mergedAt`"));
            assert.ok(content.includes("`baseRefName` equal to `beta` or `main`"));
            assert.ok(content.includes("Confirm that this PR actually contains the selected change"));
            assert.ok(content.includes("report the blocker and stop without archiving"));
            assert.ok(content.includes("not in the implementation PR"));
            assert.ok(content.indexOf("## Repository archive gate") < content.indexOf("Run `"));
        }
        assert.equal(
            await readFile(join(root, ".github/prompts/unrelated.prompt.md"), "utf8"),
            generated.replace("openspec archive", "pnpm exec openspec archive")
        );

        execFileSync(process.execPath, [script]);
        for (const [index, target] of targets.entries()) {
            assert.equal(await readFile(join(root, ".github", target), "utf8"), firstRun[index]);
        }

        const archivePrompt = join(root, ".github/prompts/opsx-archive.prompt.md");
        await writeFile(archivePrompt, generated);
        execFileSync(process.execPath, [script]);
        assert.equal(
            await readFile(archivePrompt, "utf8"),
            firstRun[targets.indexOf("prompts/opsx-archive.prompt.md")]
        );
    } finally {
        await rm(root, { recursive: true, force: true });
    }
});
