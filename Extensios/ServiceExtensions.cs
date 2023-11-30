using System.Configuration;
using System.Text;
using DiceParadiceApi.Models;
using DiceParadiceApi.Repository;
using Humanizer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    
    public static void ConfigureJWT(this IServiceCollection services, IConfiguration 
        configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings.GetSection("secret").ToString();
        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
    }
}