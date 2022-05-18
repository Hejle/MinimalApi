using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.Logic.Services;

namespace MinimalApi.Database
{
    public static class MinimalApiLogicRegistration
    {
        public static void AddMinimalApiLogic(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped<IBookService, BookService>();
        }
    }
}
