# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this repository is

Not a software project — a **teaching workspace** for one learner (Kawsarul, .NET developer) learning Content
Security Policy for ASP.NET Core + React. The deliverables are HTML lessons; `sandbox/` is a throwaway app that
exists only so lessons have something runnable.

The `teach` skill (`~/.claude/skills/teach`) defines the workflow and the file formats. Read it before producing
anything here. `MISSION.md` and `NOTES.md` are the two files that constrain every lesson — read both first.

## Running the lab

```
.\Teach-Skill\CSP-Learn\sandbox
dotnet run --urls http://localhost:5099
```

No tests, no build step, no lint. `net10.0` minimal-API app, ~90 lines in `sandbox/Program.cs`.

`appsettings.json` is watched, so **editing a policy takes effect on browser refresh — no restart.** That is the
whole design of the lab: labs are `appsettings.json` exercises, not code exercises. Only touch `Program.cs` if a
lesson needs a mechanism the middleware genuinely can't express yet.

## How the lab works

`sandbox/Program.cs` is deliberately shaped like a real nonce-based CSP middleware, so lessons can point at it:

- Per-request base64 nonce → `HttpContext.Items["CspScriptNonce"]`, exposed to the page as `__NONCE__`.
- Directives come from two config arrays: `CSPHeaders` (enforcing) and `CSPReportOnlyHeaders` (report-only).
  Either can be empty; an empty array means the header is not sent at all.
- `AppendNonce` appends `'nonce-…'` only to `script-src`, `script-src-elem`, `style-src`, `style-src-elem` — and
  only when the directive already has at least one source (it matches on `"script-src "` with a trailing space).
- `/csp-report` reads the raw body: the browser POSTs `application/csp-report`, which no model binder handles.
  Reports are logged as warnings, so **the terminal is part of the lesson UI** — labs tell the learner to watch it.
- `__POLICY__` in `lab.html` is replaced with the enforcing header as actually sent, so the page shows its own policy.

`sandbox/wwwroot/lab.html` holds probes A–I, each a single resource load that either happens or is blocked, with the
directive it needs printed next to it. Lessons refer to probes by letter. Adding a lesson usually means adding a
probe, not rewriting the page:

| | probe | needs |
|---|---|---|
| A | same-origin script | `script-src 'self'` |
| B | cross-origin script (cdn.jsdelivr.net) | `script-src cdn.jsdelivr.net` |
| C | inline script, no nonce | `script-src 'unsafe-inline'` |
| D | inline script with nonce | `script-src 'nonce-…'` |
| E | `eval()` | `script-src 'unsafe-eval'` |
| F | cross-origin image (placehold.co) | `img-src placehold.co` |
| G | `fetch()` to api.github.com | `connect-src api.github.com` |
| H | inline `<style>` block | `style-src 'unsafe-inline'` |
| I | runtime-injected style (CSS-in-JS shaped) | `style-src 'nonce-…'`, needs wiring |

Probes H and I detect blocking by reading `getComputedStyle(...).outlineColor` — the marker element keeps its
stylesheet colour when the style is blocked. Don't "fix" that indirection; it's how the page works without JS
that CSP would also block.

## Authoring lessons

Numbered `lessons/NNNN-dash-case-name.html`, incrementing. Every lesson links `../assets/course.css` and
`../assets/quiz.js` — **never inline CSS or JS a second lesson could reuse**; new reusable pieces go in `assets/`.

The house structure, followed by lessons 1–5, in order: `h1` → `p.subtitle` ("Lesson N · …") → **The win** →
`h2` "The 30-second version" (bullets) → one short "why" section → one `aside.note` trap → `hr` → `h2` "Hands-on"
with `div.step` blocks → `h2` "Check yourself" quizzes → `details.recall` → primary source → "Next" →
`footer.lesson-footer` with prev/reference links and the ask-your-teacher reminder.

Available classes in `assets/course.css`: `.subtitle`, `.step`, `.ok`, `.bad`, `aside.note`, `.quiz`/`.q`/`.opt`/
`.explain`, `.recall`, `footer.lesson-footer`. Quiz contract: `<div class="quiz" data-answer="N">` with
zero-indexed `.opt` buttons.

Hard constraints from `MISSION.md` / `NOTES.md`:

- **Generic examples only.** Never ground a lesson in the learner's employer's codebase or real production policy.
  Invent domains and policies shaped like real ones.
- **Writing style has been corrected twice** ("hard to understand", after lessons 2 and 5). The full ruleset is in
  `NOTES.md` and is binding: 1–3 sentence paragraphs, takeaway first, bullets over prose, plain words, one trap per
  lesson, numbered lab steps.
- **Never state a CSP fact from memory.** Every claim is cited to a source in `RESOURCES.md` (MDN first, then
  web.dev's strict-CSP guide), and the citation is a link in the lesson.
- Quiz options must be near-identical in length, so formatting leaks no clue.

## Workspace bookkeeping after each lesson

- `learning-records/NNNN-*.md` — one record per lesson: what was taught, design choices worth keeping, which claims
  were checked against which source, and what lesson N+1 should be. This is where the next session picks up.
- `RESOURCES.md` — add each source used, with a `**Status**` line naming the lesson that used it.
- `reference/0001-csp-cheatsheet.html` — the compressed, printable artifact the learner actually revisits. Extend it
  when a lesson adds durable knowledge, and keep its footer linking every lesson.
- `NOTES.md` — teaching preferences and the remaining topic list. Update when the learner gives feedback.

## Editing gotchas on this machine

Lesson files are **UTF-8 without BOM and full of em dashes and `·` separators**. Round-tripping them through
Windows PowerShell `Get-Content -Raw` / `Set-Content` corrupts every non-ASCII character and adds a BOM. Use the
Read/Edit/Write tools for these files. Backslashes in Windows paths are also unreliable through `sed`/`perl` in the
Bash tool — use Edit for path changes.
