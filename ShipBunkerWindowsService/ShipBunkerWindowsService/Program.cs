using ShipBunkerWindowsService;
using Serilog;
using Microsoft.Extensions.Configuration.Json;

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
    {
        services.AddHostedService<Worker>();
    
    
    })
    .UseSerilog()
    .Build();

        await host.RunAsync();
    }
}