---
name: lesson5-report-only-rollout
description: Lesson 5 built on the Report-Only rollout; sandbox already supported dual headers, so the lab is a full four-step rollout. Lesson 6 is default-src as fallback.
metadata:
  type: project
---

Lesson 5 (`lessons/0005-shipping-a-policy-with-report-only.html`) teaches the
`Content-Security-Policy-Report-Only` rollout — the last unmet success criterion in `MISSION.md`.

**Sandbox needed no changes.** `Program.cs` already reads `CSPReportOnlyHeaders` from config, emits the second
header, and logs POSTs to `/csp-report`. The lab is therefore a pure `appsettings.json` exercise: set a loose
enforcing policy plus a strict candidate in report-only, read the reports, narrow the candidate on evidence, flip.

**Lab design choice worth keeping:** the candidate blocks probes B (cdn.jsdelivr.net script), F (placehold.co
image) and G (api.github.com fetch) — three violations with *three different correct answers* (widen the policy;
fix the app with a nonce/self-host; learner's judgement). The teaching point is the decision per violation, not the
mechanics of the header. Probe F is deliberately left open-ended.

**Grounded claims (checked against MDN, not memory):** Report-Only is a no-op without `report-uri`/`report-to`
("if not, the operation won't have any effect"); it is not supported in `<meta>`; `blocked-uri` is populated for
report-only violations too, so `disposition` is the only field that separates a real block from a prediction.
web.dev's strict-CSP guide supplied the production caveat about extension/malware noise in report streams.

**Housekeeping:** lessons 1-4 all carried a stale sandbox path (`D:\Projects\claude\CSP-Learn\sandbox`) in their
`dotnet run` block; corrected to the real workspace path across all files. `reference/0001-csp-cheatsheet.html`
gained a "Report-Only rollout" section and its footer now links lessons 1-5.

**Next (lesson 6):** `default-src` as a fallback, not a deny — reading a giant permissive `default-src` containing
`data:` and `blob:`, and working out which directives it does *not* cover. Last topic on `NOTES.md`'s working list.

**Rewritten for readability (2026-09-03).** Kawsarul flagged lesson 5 as "hard to understand" — the same
complaint as lesson 2, so the lesson-3 style rules were not strict enough. The lab, the four moves and the
grounded claims above are unchanged; the prose was cut by roughly a third. Concretely: merged the "why" section
down to two sentences, folded the standalone `<meta>` caveat into the 30-second bullets, dropped the second aside,
numbered the lab steps, and shortened every quiz explanation. Tightened rules now live in `NOTES.md`.

Also fixed: the `cd` path in lessons 1-5 pointed at `D:\Projects\claude\CSP-Learn` (and briefly at a
non-existent `D:\Personals\Shared\...`). All five now use the real workspace root, `D:\shared\OneDrive\Teach-Skill\CSP-Learn`.
