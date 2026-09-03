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
- CSP teaching topics worth covering, in rough order of teaching value (genericized — previously framed around a
  specific employer's codebase):
  - A nonce appended only to `script-src` / `script-src-elem`, while `style-src` keeps `'unsafe-inline'` — because
    CSS-in-JS libraries (MUI/emotion, styled-components) inject styles at runtime. Good lesson on why style nonces
    are hard in a React SPA.
  - A `script-src` with no `'unsafe-inline'` but a host allowlist — exactly the shape web.dev calls bypassable.
  - `script-src 'self' 'unsafe-eval' 'unsafe-inline'` as a weak baseline — worth a lesson on why, and what a real fix
    costs.
  - A `Content-Security-Policy-Report-Only` rollout: collect real violations, read them, flip to enforcing.
  - `default-src` as a huge allowlist including `data:` and `blob:` — teachable moment on `default-src` as a
    fallback, not a default *deny*.
- Style/UX: Tufte-ish, printable, one shared stylesheet in `./assets/course.css`.
