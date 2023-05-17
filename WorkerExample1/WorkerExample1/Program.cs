using WorkerExample1;
using Serilog;

internal class Program
{
    private static async Task Main(string[] args)
    {
        //Serilog Simplest Configuration -- Check each one for specifics
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.File(@"C:\Users\k.stamos\Desktop\WorkRelatedLearning\WorkerExample1\WorkerLog.txt")
            .CreateLogger();

        //try
        //{
        //    Log.Information("Starting up the service");
        //    return;
        //}
        //catch (Exception ex)
        //{
        //    Log.Fatal(ex, "Problem starting service");
        //    return;
        //}
        //finally
        //{
        //    Log.CloseAndFlush();
        //}
        IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    //Dependency Injection part where I can inject my classes to use and Interfaces
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        
    })
    .UseSerilog()
    .Build();
        await host.RunAsync();



    }
}