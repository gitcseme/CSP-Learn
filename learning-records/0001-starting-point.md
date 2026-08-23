# Starting point: strong .NET, no browser-security model

Kawsarul has ~5 years as a .NET developer and maintains a codebase that already ships a nonce-based CSP (a
CSP-nonce middleware, security-header setup, per-environment policy config) — so the ASP.NET Core middleware
pipeline, config binding, and React/webpack build are known ground and need no teaching. What is missing is the
browser side: what the header means, how the browser evaluates it, and why existing directives are shaped the way
they are.

**Implications:** teach browser behaviour, not C#. Every lesson can assume middleware fluency and use a generic,
invented policy as the worked example — not any specific employer's real codebase or production policy (see
[[0002-mission-pivot-to-generic-examples]]). The zone of proximal development starts at "read a policy and predict
blocks", not at "what is an HTTP header".
