using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.Common.Models;
using MinimalApi.Logic.Services;
using MinimalApi.Logic.Validation;

namespace MinimalApi.Database;

public static class MinimalApiLogicRegistration
{
    public static void AddMinimalApiLogic(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBookService, BookService>();
        serviceCollection.AddScoped<IJokeService, JokeService>();
        serviceCollection.AddScoped<IValidator<Book>, BookValidator>();
    }
}