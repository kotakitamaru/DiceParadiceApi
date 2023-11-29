using AutoMapper;
using DiceParadiceApi;
using DiceParadiceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.AddSerilog(logger);

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureRepository();
builder.Services.ConfigureResponseCashing();
builder.Services.ConfigureHttpCacheHeaders();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAuthentication();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler(logger);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCaching();
app.UseHttpCacheHeaders();
app.MapControllers();

app.Run();
