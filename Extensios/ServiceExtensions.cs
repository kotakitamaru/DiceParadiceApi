using System.Configuration;
using DiceParadiceApi.Models;
using DiceParadiceApi.Repository;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace DiceParadiceApi;

public static class ServiceExtensions
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration config)
    {
        var dbConfig = config.GetRequiredSection("DbConfig").Get<DbOptions>();
        services.AddDbContext<RepositoryContext>(options =>
            options.UseCosmos(
                dbConfig!.DbEndpoint,
                dbConfig.DbKey,
                databaseName: "Board Games Store"));
    }

    public static void ConfigureRepository(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    public static void ConfigureResponseCashing(this IServiceCollection services) =>
        services.AddResponseCaching();
    
    public static void ConfigureHttpCacheHeaders(this IServiceCollection services) => 
        services.AddHttpCacheHeaders();
    
}