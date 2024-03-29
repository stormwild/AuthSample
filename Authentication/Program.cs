var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/username", (HttpContext ctx) =>
{
    var authCookie = ctx.Request.Headers.Cookie.FirstOrDefault(c => c.StartsWith("auth="));
    if (authCookie == null)
    {
        ctx.Response.StatusCode = 401;
        return Results.Unauthorized();
    }

    var value = authCookie.Split('=').Last().Split(':').Last();

    return Results.Ok(value);
});

app.MapGet("/login", (HttpContext ctx) =>
{
    ctx.Response.Headers.TryAdd("set-cookie", "auth=usr:jane");
    return Results.Ok("Logged in");
});

app.Run();

