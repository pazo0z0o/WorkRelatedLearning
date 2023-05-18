namespace ShipBunkerWindowsService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;


        //TODO : instantiate and inject in the worker also
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}