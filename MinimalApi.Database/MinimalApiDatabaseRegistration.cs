using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.Database.Context;
using MinimalApi.Database.Services;

namespace MinimalApi.Database
{
    public static class MinimalApiDatabaseRegistration
    {
        public static void AddMinimalApiDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<BookContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("MinimalApiDatabase"))
            );
            serviceCollection.AddScoped<IBookDataAccess, BookDataAccess>();
        }
    }
}
