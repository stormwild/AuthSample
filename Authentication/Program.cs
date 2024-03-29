using System.Security.Claims;

using Authentication;

using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddDataProtection();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

app.Use((ctx, next) =>
{
    var idp = ctx.RequestServices.GetRequiredService<IDataProtectionProvider>();

    var protector = idp.CreateProtector("auth-cookie");

    var authCookie = ctx.Request.Headers.Cookie.FirstOrDefault(c => c.StartsWith("auth="));
    if (authCookie is not null)
    {
        var protectedPayload = authCookie.Split('=').Last();
        var value = protector.Unprotect(protectedPayload).Split(':').Last();

        ctx.User = new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.Name, value)]));
    }

    return next();
});

app.MapGet("/username", (HttpContext ctx) =>
{
    var user = ctx.User.FindFirstValue(ClaimTypes.Name) ?? "Anonymous";
    return Results.Ok(user);
});

app.MapGet("/login", (AuthService auth) =>
{
    auth.SignIn();
    return Results.Ok("Logged in");
});

app.Run();

