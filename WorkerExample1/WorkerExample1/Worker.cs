namespace WorkerExample1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient _client;
        
        
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        //
        public override Task StartAsync(CancellationToken cancellationToken)
        {   
            _client = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("The service has stopped");
            _client.Dispose();
            return base.StopAsync(cancellationToken);
        }

        
        
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await _client.GetAsync("https://www.youtube.com");
                if(result.IsSuccessStatusCode) 
                {
                    _logger.LogInformation("Youtube is up: Status code {StatusCode}", result.StatusCode);             
                }
                else
                {   //should in general take action, not only log the error
                    _logger.LogError("Website Is down. Status code {StatusCode}",result.StatusCode);
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}