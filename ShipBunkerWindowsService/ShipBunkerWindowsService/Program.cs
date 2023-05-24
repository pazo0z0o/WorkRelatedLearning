using ShipBunkerWindowsService;
using Serilog;
using ShipBunkerWindowsService.Repos;
using ShipBunkerWindowsService.Models;

internal class Program
{
    private static async Task Main(string[] args)
    {
        //Logger Configuration --

#if DEBUG
        Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
           .Enrich.FromLogContext()
           .WriteTo.File(@"C:\Users\k.stamos\Desktop\WorkRelatedLearning\ShipBunkerWindowsService\WorkerLog\DebugLog.txt")
           .CreateLogger();
#else
    Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
           .Enrich.FromLogContext()
           .WriteTo.File(@"C:\Users\k.stamos\Desktop\WorkRelatedLearning\ShipBunkerWindowsService\WorkerLog\WorkerLog.txt")
           .CreateLogger();
#endif


        IHost host = Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .ConfigureServices((context,services) => {  
        services.AddHostedService<Worker>();
        services.AddSingleton<IEntityRepo<FinancialData>, ScrapingRepo>();
    })
    .UseSerilog()
    .Build();
        await host.RunAsync();
    }

}