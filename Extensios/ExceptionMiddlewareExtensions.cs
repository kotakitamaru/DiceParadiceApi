using System.Net;
using DiceParadiceApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using Serilog.Core;

namespace DiceParadiceApi;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, Logger logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    logger.Error(contextFeature.Error,"Something went wrong: {ContextFeatureError}", contextFeature.Error);
                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal server error",
                    }.ToString());
                }
            });
        });
    }
}