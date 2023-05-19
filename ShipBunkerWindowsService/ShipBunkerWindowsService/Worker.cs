using Microsoft.Extensions.Configuration;
using ShipBunkerWindowsService.Models;
using ShipBunkerWindowsService.Repos;

namespace ShipBunkerWindowsService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEntityRepo<FinancialData> _scraperRepo;
        private readonly IConfiguration _config;


        
        public Worker(ILogger<Worker> logger, IEntityRepo<FinancialData> scraper, IConfiguration configuration)
        {
            _scraperRepo = scraper;
            _logger = logger;
            _config = configuration;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {

            _logger.LogInformation("The service has started at {initialTime}", DateTime.UtcNow);

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("The service has stopped at {endTime}", DateTime.UtcNow);

            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //very sus, there must be a better way
                var resources = _config.GetSection("ScrapingResources").Get<ScrapingResourses>();
                
                //TODO : try{Loading,Scraping}-catch{ex message}-finally{CsvOutput} 
                //TODO : Check how to do this with fully asynchronous calls
                //MGO scraping logic and Output

                var doc = _scraperRepo.DocumentLoader(resources.MgoUrl);
                var scrapList = _scraperRepo.ScrapingLogic(doc, resources.MgoXpath);
                _scraperRepo.CsvOutput(scrapList, resources.MgoCsvFile);

                //VLSFO scraping logic and Output

                doc = _scraperRepo.DocumentLoader(resources.VlsfoUrl);
                scrapList = _scraperRepo.ScrapingLogic(doc, resources.VlsfoXpath);
                _scraperRepo.CsvOutput(scrapList, resources.VlsfoCsvFile);

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(resources.IntervalTime, stoppingToken);
            }
        }
    }
}