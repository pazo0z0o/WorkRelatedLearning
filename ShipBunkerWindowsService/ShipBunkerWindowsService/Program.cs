using ShipBunkerWindowsService;
using Serilog;
using Microsoft.Extensions.Configuration.Json;
using ShipBunkerWindowsService.Repos;
using ShipBunkerWindowsService.Models;

internal class Program
{
    private static async Task Main(string[] args)
    {
        //Logger Configuration -- 
        Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
           .Enrich.FromLogContext()
           .WriteTo.File(@"C:\Users\k.stamos\Desktop\WorkRelatedLearning\ShipBunkerWindowsService\WorkerLog\WorkerLog.txt")
           .CreateLogger();



        IHost host = Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .ConfigureServices((context,services) =>
    {//Could context be removed?
        services.AddHostedService<Worker>();
        services.AddSingleton<IEntityRepo<FinancialData>, ScrapingRepo>();
        //services.Configure<ScrapingResourses>(context.Configuration.GetSection("ScrapingResources")); --> Needs to be an object to be grabbed in this way,in the appsettings.json!
    })
    .UseSerilog()
    .Build();

        await host.RunAsync();
    }
}