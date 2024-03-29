using Microsoft.AspNetCore.DataProtection;

namespace Authentication;

public class AuthService
{
    private readonly IDataProtectionProvider _idp;
    private readonly IHttpContextAccessor _accessor;

    public AuthService(IDataProtectionProvider idp, IHttpContextAccessor accessor)
    {
        _idp = idp;
        _accessor = accessor;
    }

    public void SignIn()
    {
        var protector = _idp.CreateProtector("auth-cookie");
        _accessor.HttpContext.Response.Headers.TryAdd("set-cookie", $"auth={protector.Protect("usr:jane")}");
    }
}
