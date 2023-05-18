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
           .WriteTo.File(@"C:\Users\k.stamos\Desktop\WorkRelatedLearning\ShipBunkerWindowsService\WorkerLog.txt")
           .CreateLogger();


















        IHost host = Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .ConfigureServices(services =>
    {// TODO : Dependency injections with the Interfaces -- Register the services as Singleton
        services.AddHostedService<Worker>();
        services.AddSingleton<IEntityRepo<FinancialData,ScrapingResourses>, ScrapingRepo>();
        
    })
    .UseSerilog()
    .Build();

        await host.RunAsync();
    }
}