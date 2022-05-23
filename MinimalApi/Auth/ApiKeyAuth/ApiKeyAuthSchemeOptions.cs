using Microsoft.AspNetCore.Authentication;

namespace MinimalApi.Auth.ApiKeyAuth;

public class ApiKeyAuthSchemeOptions : AuthenticationSchemeOptions
{
    public string ApiKey { get; set; } = "DefaultKey";
}
