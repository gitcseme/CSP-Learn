# CSP-Learn

**Learning Content Security Policy the hands-on way — one runnable lab, six short lessons, one branch each.**

This is not a software project. It is a *teaching workspace*: a small ASP.NET Core sandbox that serves a real
nonce-based CSP, plus a set of self-contained HTML lessons that use it. You read a policy, predict what the browser
will do, then break it on purpose and watch whether you were right.

Built with the [`teach` skill](https://claude.com/claude-code) for one learner — a .NET developer maintaining
ASP.NET Core + React apps that ship a CSP without a solid mental model of it.

---

## The mission

> Own the policy in any codebase you touch: read the header, predict what the browser will do with it, change it
> safely, and tighten it towards a strict CSP without breaking production.

Success is being able to, **without looking anything up**:

- Read any `Content-Security-Policy` header and say, directive by directive, what it permits and what it blocks.
- Take a violation report or DevTools error and name the directive at fault plus the *smallest* correct fix — not
  "add `'unsafe-inline'`".
- Explain how nonce-based CSP middleware works, and why `style-src` so often keeps `'unsafe-inline'` in a React/SPA
  stack.
- Say why `script-src 'self' 'unsafe-eval' 'unsafe-inline'` is weak, and describe the migration off it.
- Run a `Content-Security-Policy-Report-Only` rollout end to end: collect violations, read them, flip to enforcing.

Full statement of intent, constraints, and what is deliberately out of scope: [`MISSION.md`](MISSION.md).

---

## The course, lesson by lesson

Each lesson is one standalone HTML page: a stated win, a 30-second summary, one short "why", exactly one trap, a
numbered lab, self-marking quizzes, and a cited primary source. Twenty minutes each, in order.

| # | Lesson | The win |
|---|--------|---------|
| **1** | [What the browser does with your CSP](https://github.com/gitcseme/CSP-Learn/blob/lesson-1/lessons/0001-what-the-browser-does-with-your-csp.html) | For every resource on a page, say *allowed* or *blocked* — and name the directive that decided it. |
| **2** | [Reading a CSP violation report](https://github.com/gitcseme/CSP-Learn/blob/lesson-2/lessons/0002-reading-a-csp-violation-report.html) | Read the `csp-report` JSON and the console line, then write the smallest correct fix. |
| **3** | [Why `script-src` gets a nonce and `style-src` doesn't](https://github.com/gitcseme/CSP-Learn/blob/lesson-3/lessons/0003-why-script-src-gets-a-nonce-and-style-src-doesnt.html) | Explain why a nonce ships easily on scripts but fights you on styles in MUI / emotion / styled-components. |
| **4** | [Telling a weak policy from a strict one at a glance](https://github.com/gitcseme/CSP-Learn/blob/lesson-4/lessons/0004-telling-a-weak-policy-from-a-strict-one.html) | Point at the keyword doing the damage, prove it in the lab, and state the one-line migration. |
| **5** | [Shipping a tighter policy with Report-Only](https://github.com/gitcseme/CSP-Learn/blob/lesson-5/lessons/0005-shipping-a-policy-with-report-only.html) | Tighten a policy without an outage: ship report-only, read the reports, fix, flip. |
| **6** | [`default-src` is a fallback, not a deny](https://github.com/gitcseme/CSP-Learn/blob/lesson-6/lessons/0006-default-src-is-a-fallback-not-a-deny.html) | Name what one big `default-src` governs — and the hole it leaves that no probe table shows you. |

Everything durable from all six is compressed into one printable page:
**[the CSP cheatsheet](reference/0001-csp-cheatsheet.html)**. That is the artifact worth revisiting; the lessons are
the scaffolding that built it.

---

## Branch layout

The lessons are cumulative, and so are the branches. Each `lesson-N` branch holds the workspace **as it stood at the
end of lesson N** — lessons 1 through N and nothing later. `master` is the shared baseline.

| Branch | Lessons present |
|--------|-----------------|
| `master` | 1 – 4 (baseline) |
| `lesson-1` | 1 |
| `lesson-2` | 1 – 2 |
| `lesson-3` | 1 – 3 |
| `lesson-4` | 1 – 4 |
| `lesson-5` | 1 – 5 |
| `lesson-6` | 1 – 6 — the complete course |

Want the whole thing? `git checkout lesson-6`. Want to follow along as it was taught, one step at a time? Start at
`lesson-1` and walk forward.

Every branch carries the full sandbox, assets, and reference material, so the lab runs on all of them.

---

## Running the lab

```bash
cd sandbox
dotnet run --urls http://localhost:5099
```

Then open <http://localhost:5099/lab.html>.

Two things make this work as a teaching tool:

- **`appsettings.json` is watched.** Editing the policy takes effect on browser refresh — no restart. Every lab is a
  config exercise, not a code exercise.
- **The terminal is part of the UI.** Violation reports POST to `/csp-report` and are logged as warnings, so you
  watch blocks happen in two places at once.

### The probes

`sandbox/wwwroot/lab.html` is a table of nine probes. Each is a single resource load that either happens or gets
blocked, with the directive it needs printed beside it. Lessons refer to them by letter.

| | Probe | Needs |
|---|-------|-------|
| **A** | same-origin script | `script-src 'self'` |
| **B** | cross-origin script | `script-src cdn.jsdelivr.net` |
| **C** | inline script, no nonce | `script-src 'unsafe-inline'` |
| **D** | inline script with nonce | `script-src 'nonce-…'` |
| **E** | `eval()` | `script-src 'unsafe-eval'` |
| **F** | cross-origin image | `img-src placehold.co` |
| **G** | `fetch()` to an API | `connect-src api.github.com` |
| **H** | inline `<style>` block | `style-src 'unsafe-inline'` |
| **I** | runtime-injected style | `style-src 'nonce-…'` |

`sandbox/Program.cs` is ~90 lines of minimal API shaped deliberately like real middleware: a per-request base64
nonce, directives read from two config arrays (`CSPHeaders` enforcing, `CSPReportOnlyHeaders` report-only), and the
nonce appended only to the script and style directives. The page prints the policy it was actually served, so what
you edit and what you see never drift apart.

---

## What's in here

| Path | What it is |
|------|-----------|
| [`MISSION.md`](MISSION.md) | Why this topic, what success looks like, what is out of scope |
| [`lessons/`](lessons) | The lessons themselves — numbered, standalone HTML |
| [`reference/`](reference) | The compressed, printable cheatsheet |
| [`sandbox/`](sandbox) | The runnable CSP lab |
| [`assets/`](assets) | Shared stylesheet and quiz widget — every lesson links these |
| [`learning-records/`](learning-records) | One record per lesson: what was taught, what to teach next |
| [`RESOURCES.md`](RESOURCES.md) | Every source used, with the lesson that used it |
| [`NOTES.md`](NOTES.md) | Teaching preferences and the remaining topic list |

---

## Ground rules for the lessons

These are constraints on the teaching, not decoration:

1. **No CSP fact stated from memory.** Every claim is cited to a source in [`RESOURCES.md`](RESOURCES.md) — MDN
   first, then [web.dev's strict-CSP guide](https://web.dev/articles/strict-csp) — and the citation is a live link
   in the lesson.
2. **Generic examples only.** No employer's codebase or real production policy. Invented domains, shaped like real
   ones.
3. **Hands-on first.** Nothing gets taught that can't be broken in the lab.
4. **One win per lesson, one trap per lesson.** Working memory is small; lessons stay short on purpose.
5. **Storage strength over fluency.** Quizzes and recall blocks are there to make retrieval effortful — that is the
   point, not a rough edge.

---

## Primary sources

- [MDN — Content Security Policy guide](https://developer.mozilla.org/en-US/docs/Web/HTTP/Guides/CSP)
- [MDN — CSP directive reference index](https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy)
- [web.dev — Mitigate XSS with a strict CSP](https://web.dev/articles/strict-csp) (Google security team)
- [W3C — Content Security Policy Level 3](https://www.w3.org/TR/CSP3/) — the normative text, for when docs disagree
- [CSP Evaluator](https://csp-evaluator.withgoogle.com/) — paste a policy, get graded findings

---

> **Stuck on a lesson?** Ask your teacher. Run `claude` in this directory and say which step didn't land — the
> lessons are written to be interrogated, not just read.
