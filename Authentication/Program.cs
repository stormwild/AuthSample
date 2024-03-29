using System.Security.Claims;

using Authentication;

using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddDataProtection();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

app.UseAuthentication();

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

