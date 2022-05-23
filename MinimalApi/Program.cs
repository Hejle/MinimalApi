using Microsoft.AspNetCore.Localization;
using MinimalApi.Database;
using MinimalApi.Endpoints;
using MinimalApi.Auth.ApiKeyAuth;
using MinimalApi.Auth.BasicAuth;
using MinimalApi.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Negotiate;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMinimalApiDatabase(builder.Configuration);
builder.Services.AddMinimalApiLogic(builder.Configuration);

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-UK");
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = DefaultAuthScheme.SchemeName;
    options.DefaultChallengeScheme = DefaultAuthScheme.SchemeName;
})
    .AddScheme<ApiKeyAuthSchemeOptions, ApiKeyAuthHandler>(ApiKeySchemeConstants.SchemeName, _ => { })
    .AddScheme<BasicAuthSchemeOptions, BasicAuthHandler>(BasicSchemeConstants.SchemeName, _ => { })
    .AddNegotiate()
    .AddPolicyScheme(DefaultAuthScheme.SchemeName, DefaultAuthScheme.SchemeName, 
        options => DefaultAuthScheme.ChooseAuthScheme(options));

builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRequestLocalization();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseBookEndpoint(app.Configuration);

app.Run();