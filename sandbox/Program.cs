using System.Security.Cryptography;
using System.Text;

// CSP lab. A generic nonce-based CSP setup, shaped like a typical ASP.NET Core
// middleware pipeline:
//   1. per-request random nonce  -> HttpContext.Items
//   2. directives from config    -> joined with "; "
//   3. nonce appended to script-src / script-src-elem only
// appsettings.json is watched, so editing the policy takes effect on the next
// refresh -- no restart.

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    var nonce = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
    context.Items["CspScriptNonce"] = nonce;

    var directives = app.Configuration.GetSection("CSPHeaders").Get<string[]>() ?? [];
    var reportOnly = app.Configuration.GetSection("CSPReportOnlyHeaders").Get<string[]>() ?? [];

    if (directives.Length > 0)
    {
        context.Response.Headers.ContentSecurityPolicy = BuildCspHeaderValue(directives, nonce);
    }

    if (reportOnly.Length > 0)
    {
        context.Response.Headers["Content-Security-Policy-Report-Only"] = BuildCspHeaderValue(reportOnly, nonce);
    }

    await next();
});

app.UseStaticFiles();

app.MapGet("/", async context =>
{
    var nonce = context.Items["CspScriptNonce"] as string ?? string.Empty;
    var html = await File.ReadAllTextAsync(Path.Combine(app.Environment.WebRootPath, "lab.html"));

    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync(html
        .Replace("__NONCE__", nonce)
        .Replace("__POLICY__", context.Response.Headers.ContentSecurityPolicy.ToString()));
});

// The browser POSTs here when report-uri is in the policy. Content type is
// application/csp-report, which no model binder understands -- read the raw body.
app.MapPost("/csp-report", async context =>
{
    using var reader = new StreamReader(context.Request.Body, Encoding.UTF8);
    var body = await reader.ReadToEndAsync();
    app.Logger.LogWarning("CSP violation report: {Report}", body);
    context.Response.StatusCode = StatusCodes.Status204NoContent;
});

app.Run();

static string BuildCspHeaderValue(string[] directives, string nonce)
{
    if (string.IsNullOrEmpty(nonce))
    {
        return string.Join("; ", directives);
    }

    var nonceSource = $"'nonce-{nonce}'";
    return string.Join("; ", directives.Select(d => AppendNonce(d, nonceSource)));
}

static string AppendNonce(string directive, string nonceSource)
{
    var trimmed = directive.TrimStart();
    var takesNonce = trimmed.StartsWith("script-src ", StringComparison.OrdinalIgnoreCase)
        || trimmed.StartsWith("script-src-elem ", StringComparison.OrdinalIgnoreCase)
        || trimmed.StartsWith("style-src ", StringComparison.OrdinalIgnoreCase)
        || trimmed.StartsWith("style-src-elem ", StringComparison.OrdinalIgnoreCase);

    return takesNonce ? $"{directive} {nonceSource}" : directive;
}
