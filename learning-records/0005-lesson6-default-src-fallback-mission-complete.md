---
name: lesson6-default-src-fallback-mission-complete
description: Lesson 6 taught default-src as a fallback via a new probe J (injected <base>). Every MISSION.md success criterion is now covered; lesson 7 should be an interleaved cold-read drill rather than new material.
metadata:
  type: project
---

Lesson 6 (`lessons/0006-default-src-is-a-fallback-not-a-deny.html`) teaches `default-src` as a fallback, not a
deny — the last topic on `NOTES.md`'s working list.

**Sandbox changed for the first time since lesson 3.** Added **probe J** to `sandbox/wwwroot/lab.html`: it appends
a `<base href="https://csp-lab.example/">` after load, reads `document.baseURI` to see whether the hijack took, then
removes the element. `base-uri` was the right choice for this lesson because it is the sharpest case of a directive
`default-src` cannot reach, and it needs no middleware change.

**Probe J deliberately inverts the table's colour convention** — green "RAN / LOADED" means the hijack *succeeded*,
which is the bad outcome. The lesson calls this out in step 3 rather than hiding it. Don't "fix" the inversion: the
table reports whether the probe's attempt happened, and for J the attempt is an attack.

**Lab design worth keeping — the three-beat structure:**
1. One giant permissive `default-src` → probes A–I all pass, proving the fallback does nine jobs from one line.
2. Add `img-src 'self'` → probe F breaks *even though `default-src` still lists `placehold.co`*. This is the beat
   that actually surprises people, and it's the one MDN sentence worth memorising.
3. Probe J is green, and **no violation report arrives**, because nothing was violated. The invisible-gap point
   lands far harder than any prose version of it.
4. Add `base-uri 'none'` → J flips to BLOCKED and a report with `effective-directive: "base-uri"` appears.

Note the giant `default-src` in step 1 must include `'unsafe-inline'`. Without it, the nonce'd results-renderer
script is blocked (the middleware only appends a nonce to directives it recognises by name, never to `default-src`),
so the lab page renders nothing and the whole lesson dies. That constraint is realistic anyway.

**Grounded claims (fetched, not recalled):** the 16-directive fallback list and "If there are other directives
specified, `default-src` does not influence them." from MDN's `default-src` page; "No. Not setting this allows
anything." from the spec summary tables on `base-uri`, `form-action` and `frame-ancestors`; the fetch / document /
navigation / reporting grouping and its two definitions from MDN's CSP header index; the strict-CSP header and its
`base-uri 'none'` rationale ("blocks the injection of `<base>` tags") from web.dev.

**Housekeeping:** `reference/0001-csp-cheatsheet.html` gained a "The three fallback rules" table and now links
lesson 6. `RESOURCES.md` gained the MDN `default-src` and `base-uri`/`form-action`/`frame-ancestors` entries; the
CSP directive index moved from "unread" to used.

**Mission status: every success criterion in `MISSION.md` is now covered by a lesson.** Read a header directive by
directive (1, 6), name the directive at fault from a report and the smallest fix (2), explain nonce middleware and
why `style-src` keeps `'unsafe-inline'` (3), justify why a weak policy is weak and migrate it (4), run a Report-Only
rollout (5).

**Next (lesson 7) — consolidation, not new material.** Proposed: a cold-read drill. Four unfamiliar invented
policies, no hints; for each, name the weakness and the smallest correct fix. This interleaves lessons 1–6, which
is the one thing the course has never done — every lesson so far tested only its own topic, so his retrieval is
almost certainly cued rather than durable. Confirm with him before building it; the alternative is extending the
mission into out-of-scope territory (Trusted Types), which needs his agreement per `MISSION.md`.
