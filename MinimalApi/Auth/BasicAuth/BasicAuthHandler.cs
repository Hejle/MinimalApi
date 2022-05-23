using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.DirectoryServices.AccountManagement;

namespace MinimalApi.Auth.BasicAuth;

public class BasicAuthHandler : AuthenticationHandler<BasicAuthSchemeOptions>
{
    public const string UserNameHeader = "UserName";
    public const string PasswordHeader = "Password";


    public BasicAuthHandler(IOptionsMonitor<BasicAuthSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(UserNameHeader) || !Request.Headers.ContainsKey(PasswordHeader))
        {
            return Task.FromResult(AuthenticateResult.Fail($"Missing {UserNameHeader} or {PasswordHeader} Header"));
        }

        var username = Request.Headers[UserNameHeader].ToString();
        var password = Request.Headers[PasswordHeader].ToString();

        PrincipalContext pc = new PrincipalContext(ContextType.Machine);
        bool isCredentialValid = pc.ValidateCredentials(username, password);

        if (!isCredentialValid)
        {
            return Task.FromResult(AuthenticateResult.Fail($"Invalid {UserNameHeader} or {PasswordHeader}"));
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Role, "AdminRole"),
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Basic");
        var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
