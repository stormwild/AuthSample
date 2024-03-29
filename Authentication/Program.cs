using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataProtection();

var app = builder.Build();

app.MapGet("/username", (HttpContext ctx, IDataProtectionProvider idp) =>
{
    var protector = idp.CreateProtector("auth-cookie");

    var authCookie = ctx.Request.Headers.Cookie.FirstOrDefault(c => c.StartsWith("auth="));
    if (authCookie == null)
    {
        ctx.Response.StatusCode = 401;
        return Results.Unauthorized();
    }

    var protectedPayload = authCookie.Split('=').Last();
    var value = protector.Unprotect(protectedPayload).Split(':').Last();

    return Results.Ok(value);
});

app.MapGet("/login", (HttpContext ctx, IDataProtectionProvider idp) =>
{
    var protector = idp.CreateProtector("auth-cookie");
    ctx.Response.Headers.TryAdd("set-cookie", $"auth={protector.Protect("usr:jane")}");
    return Results.Ok("Logged in");
});

app.Run();

