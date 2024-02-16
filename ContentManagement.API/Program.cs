using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;
using CSPortal.API;
using ContentManagement.Domain;
using ContentManagement.API;
using Hangfire;
using Microsoft.Extensions.Configuration;
using System;
using Hafiz.UI.BackgroudServices;
using HangfireBasicAuthenticationFilter;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

builder.Logging.ClearProviders();
builder.Host.UseNLog();

// Hangfire'nin connection stringine ulasiyor ve onu kullaniyor
builder.Services.AddHangfire(config => config.UseSqlServerStorage(builder.Configuration.GetConnectionString("PTConnectionString")));
builder.Services.AddHangfireServer(); // Hangfire serveri eklenmis

var app = builder.Build();

try
{
    using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<PTContext>();
        context.Database.Migrate();
    }
}
catch (System.Exception)
{
    throw;
}


ILoggerFactory loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
startup.Configure(app, loggerFactory, app.Environment);
//GetSection ile Hangfire settings icerisindeki username ve password alanlarinin keylerinin valuesini aliyor
var hangfireUserName = app.Configuration.GetSection("HangfireSettings:UserName").Value;
var hangfirePassword = app.Configuration.GetSection("HangfireSettings:Password").Value;
app.UseHangfireDashboard("/italycronjob", new DashboardOptions
{
    DashboardTitle = "İtalya - Cronjob",
    Authorization = new[]
{
            new HangfireCustomBasicAuthenticationFilter{
                User = hangfireUserName,
                Pass = hangfirePassword
            }
        }
});
// Hangfire dashboardinin basligi ve authorize olacak kullanici adi ve sifre tanimlanmasi yapilmasi
//RecurringJobs classi icerisinde calisacak olan metotlarin ve hangi saatte calisacaklarinin tanimlanmasi
#region CronJob
//RecurringJob.AddOrUpdate<RecurringJobs>("23 Yas Kadin", x => x.YirmiUcYasKadin(), "0 23 * * *");
//RecurringJob.AddOrUpdate<RecurringJobs>("21 Yas Erkek", x => x.YirmiBirYasErkek(), "0 23 * * *");
RecurringJob.AddOrUpdate<RecurringJobs>("Fatura Gonder", x => x.FaturaGonder(), "0 00 1 1 1");
#endregion
//GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 8 });
app.Run();
