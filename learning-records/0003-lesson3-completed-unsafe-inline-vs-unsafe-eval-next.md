---
name: lesson3-completed-unsafe-inline-vs-unsafe-eval-next
description: Lesson 3 (style nonce wiring) completed; lesson 4 built around unsafe-inline vs unsafe-eval and the nonce-neutralizes-unsafe-inline subtlety
metadata:
  type: project
---

Kawsarul completed lesson 3 (wiring a nonce through to `style-src` by hand in the sandbox). Lesson 4 follows the
"Next" lesson 3 already promised: distinguishing a weak `script-src 'self' 'unsafe-eval' 'unsafe-inline'` policy
from a strict one — matching the mission's success criterion 4 in `MISSION.md` almost verbatim.

While building the lab exercise, found probe C in `sandbox/wwwroot/lab.html` was mislabeled — comment said "inline
script, NO nonce" but the markup carried `nonce="__NONCE__"` same as probe D, making it indistinguishable from D and
useless for testing `'unsafe-inline'` in isolation. Removed the nonce attribute so probe C now genuinely tests
`'unsafe-inline'` alone.

**Key teaching insight used in lesson 4**: because `Program.cs`'s `AppendNonce` unconditionally stamps a nonce onto
any `script-src` line, this sandbox can never produce a nonce-free header — so `'unsafe-inline'` can never be shown
"working" here. Turned that constraint into the lesson's core point instead of fighting it: modern browsers ignore
`'unsafe-inline'` whenever a nonce/hash is present on the same directive (already documented in
`reference/0001-csp-cheatsheet.html`'s source-expression table), so the real lab proof is that adding
`'unsafe-eval'` flips probe E immediately while adding `'unsafe-inline'` next to the auto-appended nonce changes
nothing for probe C.

**Implications:** future lessons can rely on probe C being a clean, nonce-free `'unsafe-inline'` test. Next planned
lesson (5) is the `Content-Security-Policy-Report-Only` rollout, per `NOTES.md`'s working topic list.
