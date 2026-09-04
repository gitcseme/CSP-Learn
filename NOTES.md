# Notes

## Learner
- .NET developer, ~5 years. Works with ASP.NET Core + React/webpack (multi-tenant healthcare domain).
- Comfortable: C#, middleware pipeline, EF, React/Redux, webpack.
- New to: browser security model, CSP internals.

## Teaching preferences
- **Hands-on over prose.** Every lesson must have something to run and break.
- Sandbox app, not a real repo — throwaway code in `./sandbox/`, fully self-contained.
- **Generic examples only** (as of 2026-08-16, see learning-records/0002) — no employer-specific codebase or real
  production policy as the reference point. Invent domains/policies shaped like real-world ones instead.
- **Writing style — corrected after lesson 2 (2026-08-16) and AGAIN after lesson 5 (2026-09-03).** Same words both
  times: "hard to understand, make the writing easy to understand and short". The lesson-3 rules were not enough.
  Standing rules for every lesson from now on:
  - Paragraphs of 1-3 sentences. One idea per sentence.
  - Lead every section with its one-line takeaway, then explain.
  - Bullets over prose wherever the content is a list.
  - Plain words over precise-but-heavy ones. Cut any clause that is only there for completeness.
  - One trap per lesson (one `aside.note`), not three.
  - Numbered lab steps with short imperative headings (`1 · Loose policy enforcing, strict candidate reporting`).
  - The "why this lesson exists" section gets two sentences, not five.
  - Don't retroactively rewrite older lessons unless asked — apply forward.

## Working notes
- **The original topic list is exhausted as of lesson 6 (2026-09-04).** All five topics are taught: style nonces
  vs script nonces (3), bypassable host allowlists (4), the weak `'unsafe-eval' 'unsafe-inline'` baseline (4),
  the Report-Only rollout (5), `default-src` as a fallback rather than a deny (6). Every `MISSION.md` success
  criterion has a lesson behind it — see learning-records/0005.
- **Nothing has interleaved yet.** Lessons 1-6 each tested only their own topic, so retrieval is cued: he has
  never had to identify a weakness without knowing which lesson it came from. Fix this before adding new
  material — proposed lesson 7 is a cold-read drill over four unfamiliar policies.
- Candidate directions after that, all needing his agreement (they extend or change `MISSION.md`):
  - Trusted Types / `require-trusted-types-for` — explicitly out of scope today.
  - `'strict-dynamic'` in depth: it is on the cheat sheet and in web.dev's recommended policy, but no lesson has
    made him reason about what it does to a host allowlist.
  - Hash-based policies (`'sha256-…'`) for the no-nonce case — mentioned on the cheat sheet, never practised.
  - Reading a real policy of his own choosing against csp-evaluator, as a graduation exercise.
- Spaced repetition is currently carried by each lesson's `details.recall` block, which pulls two or three items
  from earlier lessons. Keep doing that; it is the only spacing mechanism in the workspace.
- Style/UX: Tufte-ish, printable, one shared stylesheet in `./assets/course.css`.
