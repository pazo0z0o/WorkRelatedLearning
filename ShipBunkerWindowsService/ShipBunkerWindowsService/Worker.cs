using ShipBunkerWindowsService.Models;
using ShipBunkerWindowsService.Repos;

namespace ShipBunkerWindowsService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEntityRepo<FinancialData> _scraperRepo;
        
        

        //TODO : instantiate and inject in the worker also
        public Worker(ILogger<Worker> logger, IEntityRepo<FinancialData> scraper )
        {
            _scraperRepo = scraper;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
         
            _logger.LogInformation("The service has started at {initialTime}",DateTime.UtcNow);
            
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("The service has stopped at {endTime}", DateTime.UtcNow);

            return base.StopAsync(cancellationToken);
        }





        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //interval time set from appSettings
            while (!stoppingToken.IsCancellationRequested)
            {
                //TODO : try{Loading,Scraping}-catch{ex message}-finally{CsvOutput} 
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}