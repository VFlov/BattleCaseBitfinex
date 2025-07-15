using BattleCaseBitfinex.Controllers;
using BattleCaseBitfinex.Data;
using BattleCaseBitfinex.Domain;
using BattleCaseBitfinex.Domain.Entities;
using BattleCaseBitfinex.Infrastructure.Clients;
using BattleCaseBitfinex.Infrastructure.Repositories;
using BattleCaseBitfinex.Quartz;
using BattleCaseBitfinex.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Quartz;
using RestSharp;
using Serilog;
using System;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<IBinanceClient, BinanceClient>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddScoped<IFuturesPriceService, FuturesPriceService>();
builder.Services.AddLogging(logging => logging.AddSerilog());
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    var jobKey = new JobKey("PriceDifferenceJob");
    q.AddJob<PriceDifferenceJob>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("PriceDifferenceTrigger")
        .WithSimpleSchedule(x => x.WithIntervalInHours(1).RepeatForever()));
});
builder.Services.AddHostedService<QuartzHostedService>();

var app = builder.Build();
app.UseRouting(); // Добавляем эту строку
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Добавляем эту строку
});
app.MapGet("/health", () => "OK");

await app.RunAsync();