using Microsoft.AspNetCore.Localization;
using MinimalApi.Database;
using MinimalApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMinimalApiDatabase(builder.Configuration);
builder.Services.AddMinimalApiLogic(builder.Configuration);

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-UK");
});

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
//builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRequestLocalization();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

app.UseBookEndpoint(app.Configuration);

app.Run();