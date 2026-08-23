# Mission: Content Security Policy (CSP) for ASP.NET Core + React

## Why
Kawsarul is a .NET developer who works on ASP.NET Core + React applications that ship a CSP, and maintains that kind of code without a solid mental model of it. The goal is to own the policy in any codebase he touches: read the header, predict what the browser will do with it, change it safely, and tighten it towards a strict CSP without breaking production.

## Success looks like
- Read any `Content-Security-Policy` header and say, directive by directive, which resource loads it permits and which it blocks — without looking anything up.
- Given a browser CSP violation report or DevTools error, name the exact directive at fault and the smallest correct fix (not "add 'unsafe-inline'").
- Explain how a typical nonce-based CSP middleware works — per-request random nonce, appended to `script-src` — and why `style-src` frequently keeps `'unsafe-inline'` in React/SPA stacks (runtime style injection from libraries like MUI/emotion).
- Justify why a policy like `script-src 'self' 'unsafe-eval' 'unsafe-inline'` is weak, and describe a concrete migration to a nonce/hash-based policy.
- Run a `Content-Security-Policy-Report-Only` rollout: collect real violations, read them, and flip to enforcing with evidence.

## Constraints
- .NET developer, 5 years experience. Comfortable with ASP.NET Core middleware, C#, React/webpack. No prior depth in browser security.
- Hands-on first: every lesson pairs with something runnable in `./sandbox/` (a throwaway, self-contained ASP.NET Core app), not prose.
- **Generic examples only.** Do not ground lessons in any specific employer's codebase or real production policy — use invented domains/policies shaped like real-world ones instead. (Changed 2026-08-16 — see [[0002-mission-pivot-to-generic-examples]].)
- Short lessons. One tangible win per session.

## Out of scope (for now)
- Trusted Types, `require-trusted-types-for`.
- CORS, SameSite cookies, HSTS, and the rest of the security-header family — except where they get confused with CSP.
- XSS exploitation technique for its own sake; only enough to see why a directive exists.
