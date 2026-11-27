// Copyright (c) DIGITALL Nature. All rights reserved
import assert from "node:assert/strict";
import { execFileSync } from "node:child_process";
import { copyFile, mkdir, mkdtemp, readFile, rm, writeFile } from "node:fs/promises";
import { tmpdir } from "node:os";
import { dirname, join } from "node:path";
import { test } from "node:test";

test("normalization covers OpenSpec 2 skills and preserves the stable-release gate", async () => {
    const root = await mkdtemp(join(tmpdir(), "digitallpower-openspec-"));
    try {
        await mkdir(join(root, "scripts"));
        const script = join(root, "scripts", "normalize-openspec-commands.mjs");
        await copyFile(new URL("./normalize-openspec-commands.mjs", import.meta.url), script);
        const targets = [
            ".github/agents/openspec.agent.md",
            ".agents/skills/openspec/SKILL.md",
            ".agents/skills/openspec-apply-change/SKILL.md",
            ".agents/skills/openspec-archive-change/SKILL.md",
            ".agents/skills/openspec-bulk-archive-change/SKILL.md",
            ".agents/skills/openspec-update-change/SKILL.md"
        ];
        const fallback = ".agents/skills/openspec/references/without-skills.md";
        const ordinary = ".agents/skills/openspec-explore/SKILL.md";
        const unrelated = ".agents/skills/unrelated/SKILL.md";
        const frontmatter = "---\ndescription: Test workflow\n---\n";
        const generated = `${frontmatter}\nRun \`openspec archive example\`.\n`;
        for (const target of [...targets, ordinary, unrelated]) {
            const path = join(root, target);
            await mkdir(dirname(path), { recursive: true });
            await writeFile(path, generated);
        }
        await mkdir(dirname(join(root, fallback)), { recursive: true });
        await writeFile(join(root, fallback), "Run `openspec archive example`.\n");

        execFileSync(process.execPath, [script]);
        const firstRun = [];
        for (const target of targets) {
            const content = await readFile(join(root, target), "utf8");
            firstRun.push(content);
            assert.ok(content.startsWith(frontmatter));
            assert.ok(content.includes("`pnpm exec openspec archive example`"));
            assert.ok(content.includes("`state: MERGED`"));
            assert.ok(content.includes("non-null `mergedAt`"));
            assert.ok(content.includes("`baseRefName: main`"));
            assert.ok(content.includes("non-draft, non-prerelease release"));
            assert.ok(content.includes("ancestor of the release tag"));
            assert.ok(content.includes("report the blocker and stop without archiving"));
            assert.ok(content.indexOf("## Repository archive gate") < content.indexOf("Run `"));
        }
        const fallbackContent = await readFile(join(root, fallback), "utf8");
        assert.ok(fallbackContent.startsWith("<!-- digitallpower:post-merge-archive -->"));
        assert.ok(fallbackContent.includes("`pnpm exec openspec archive example`"));
        assert.equal(
            await readFile(join(root, ordinary), "utf8"),
            generated.replace("openspec archive", "pnpm exec openspec archive")
        );
        assert.equal(await readFile(join(root, unrelated), "utf8"), generated);

        execFileSync(process.execPath, [script]);
        for (const [index, target] of targets.entries()) {
            assert.equal(await readFile(join(root, target), "utf8"), firstRun[index]);
        }
        assert.equal(await readFile(join(root, fallback), "utf8"), fallbackContent);

        const archiveSkill = join(root, ".agents/skills/openspec-archive-change/SKILL.md");
        await writeFile(archiveSkill, generated);
        execFileSync(process.execPath, [script]);
        assert.equal(
            await readFile(archiveSkill, "utf8"),
            firstRun[targets.indexOf(".agents/skills/openspec-archive-change/SKILL.md")]
        );
    } finally {
        await rm(root, { recursive: true, force: true });
    }
});
