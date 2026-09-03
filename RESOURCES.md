# Resources

## Primary

### MDN — Content Security Policy guide
- **URL**: https://developer.mozilla.org/en-US/docs/Web/HTTP/Guides/CSP
- **Type**: Reference documentation
- **Trust**: High — MDN, maintained with browser vendors
- **Covers**: What CSP is, directive syntax, fetch vs document/navigation directives, `default-src` fallback, source-expression keywords, nonces and hashes
- **Status**: used in lesson 0001

### MDN — CSP directive reference index
- **URL**: https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy
- **Type**: Reference
- **Trust**: High
- **Covers**: Every directive, one page each, with browser-support tables. The lookup table when a violation names a directive you do not recognise.
- **Status**: unread

### MDN — `report-uri` directive (violation report JSON shape)
- **URL**: https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy/report-uri
- **Type**: Reference
- **Trust**: High — MDN
- **Covers**: The exact `csp-report` JSON fields (`blocked-uri`, `disposition`, `effective-directive` vs `violated-directive`, `original-policy`, `script-sample` + `'report-sample'`), and the `report-uri` → `report-to` deprecation
- **Status**: used in lesson 0002

### MDN — `Content-Security-Policy-Report-Only` header
- **URL**: https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy-Report-Only
- **Type**: Reference
- **Trust**: High — MDN
- **Covers**: Report-only semantics (never blocks), the hard requirement for `report-to`/`report-uri` ("if not, the operation won't have any effect"), no `<meta>` support, and the modern `Reporting-Endpoints` + `report-to` example that replaces `report-uri`
- **Status**: used in lesson 0005

### web.dev — Mitigate XSS with a strict Content Security Policy
- **URL**: https://web.dev/articles/strict-csp
- **Type**: Article (Google security team)
- **Trust**: High — authors ran the CSP measurement study behind the recommendation
- **Covers**: Why host allowlists fail, nonce-based and hash-based strict policies, `'strict-dynamic'`, `object-src 'none'`, `base-uri 'none'`, report-only rollout
- **Status**: used in lessons 0001, 0004, 0005

### W3C — Content Security Policy Level 3
- **URL**: https://www.w3.org/TR/CSP3/
- **Type**: Specification
- **Trust**: Authoritative — this is the normative text
- **Covers**: Exact matching algorithms. Read when documentation disagrees or an edge case bites.
- **Status**: unread

## Secondary

### CSP Evaluator
- **URL**: https://csp-evaluator.withgoogle.com/
- **Type**: Tool
- **Trust**: High — Google
- **Covers**: Paste a policy, get graded findings on bypasses. Good for checking your own answer against a real policy.
- **Status**: unread

### "CSP Is Dead, Long Live CSP" (Weichselbaum et al., CCS 2016)
- **URL**: https://research.google/pubs/pub45542/
- **Type**: Research paper
- **Trust**: Authoritative — the empirical study of 1B+ hosts that killed allowlist CSP
- **Covers**: Measured bypass rates on real allowlists; the evidence behind `'strict-dynamic'`
- **Status**: unread — read once the mechanics are solid

## Community

### r/netsec and the OWASP Slack (#appsec channel)
- **URL**: https://www.reddit.com/r/netsec/ · https://owasp.org/slack/invite
- **Type**: Community
- **Covers**: Where to sanity-check a real policy against practitioners before shipping it to a healthcare product.
- **Status**: not joined
