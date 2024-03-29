using System.Security.Claims;

using Microsoft.AspNetCore.DataProtection;

namespace Authentication;

public static class AuthenticationMiddleware
{
    public static void UseAuthentication(this WebApplication app)
    {
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

    }
}
