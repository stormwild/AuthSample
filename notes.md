# Notes

```xml
    <!-- <PackageVersion Include="FastEndpoints" Version="5.23.0" />
    <PackageVersion Include="FastEndpoints.Swagger" Version="5.23.0" />
    <PackageVersion Include="LanguageExt.Core" Version="4.4.8" />
    <PackageVersion Include="Microsoft.AspNetCore.OpenApi" Version="8.0.3" />
    <PackageVersion Include="NSubstitute" Version="5.1.0" />
    <PackageVersion Include="Polly.Core" Version="8.3.1" />
    <PackageVersion Include="Swashbuckle.AspNetCore" Version="6.4.0" />
    <PackageVersion Include="FastEndpoints.Testing" Version="5.23.0" />
    <PackageVersion Include="FluentAssertions" Version="6.12.0" />
    <PackageVersion Include="Microsoft.NET.Test.Sdk" Version="17.9.0" />
    <PackageVersion Include="xunit" Version="2.7.0" />
    <PackageVersion Include="xunit.runner.visualstudio" Version="2.5.7" PrivateAssets="all"
    IncludeAssets="runtime; build; native; contentfiles; analyzers; buildtransitive" />
    <PackageVersion Include="coverlet.collector" Version="6.0.1" PrivateAssets="all"
    IncludeAssets="runtime; build; native; contentfiles; analyzers; buildtransitive" /> -->

```

```csharp
var protector = idp.CreateProtector("auth-cookie");
    ctx.Response.Headers.TryAdd("set-cookie", $"auth={protector.Protect("usr:jane")}");
```
