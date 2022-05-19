using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.Common.Models;
using MinimalApi.Logic.Services;
using MinimalApi.Logic.Validation;

namespace MinimalApi.Database;

public static class MinimalApiLogicRegistration
{
    public static void AddMinimalApiLogic(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped<IBookService, BookService>();
        serviceCollection.AddScoped<IValidator<Book>, BookValidator>();
    }
}