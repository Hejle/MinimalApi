using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using MinimalApi.Auth;
using MinimalApi.Auth.ApiKeyAuth;
using MinimalApi.Auth.BasicAuth;
using MinimalApi.Converters;
using MinimalApi.Database;
using MinimalApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

//Register Services
builder.Services.AddMinimalApiDatabase(builder.Configuration);
builder.Services.AddMinimalApiLogic();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-UK");
});
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new DateOnlyConverter());
});


//Configure Authentication
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

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.MapType(typeof(DateOnly), () => new OpenApiSchema
    {
        Type = "string",
        Example = new OpenApiString(DateOnlyConverter.serializationFormat)
    });
});

//Build Api
var app = builder.Build();

app.UseRequestLocalization();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseBookEndpoint();
app.UseJokeEndpoint();

app.Run();