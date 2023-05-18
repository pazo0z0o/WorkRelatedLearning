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
        

        //TODO : instantiate and inject in the worker also
        public Worker(ILogger<Worker> logger, IEntityRepo<FinancialData> scraper, IConfiguration configuration)
        {
            _scraperRepo = scraper;
            _logger = logger;
            _config = configuration;
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
                //very sus, there must be a better way
                int intervalTime = _config.GetValue<int>("IntervalTime");
                string? mgoUrl = _config.GetValue<string>("MgoUrl");
                string? vlsfoUrl = _config.GetValue<string>("VlsfoUrl");
                string? mgoXpath = _config.GetValue<string>("MgoXpath");
                string? vlsfoXpath = _config.GetValue<string>("VlsfoXpath");
                string? mgoCsv = _config.GetValue<string>("MgoCsvFile");
                string? vlsfoCsv = _config.GetValue<string>("VlsfoCsvFile");

                //TODO : Check how to do this with fully asynchronous calls
                //MGO scraping logic and Output
                var doc = _scraperRepo.DocumentLoader(mgoUrl);
                var scrapList = _scraperRepo.ScrapingLogic(doc, mgoXpath);
                _scraperRepo.CsvOutput(scrapList,mgoCsv);
                
                //VLSFO scraping logic and Output
                doc = _scraperRepo.DocumentLoader(vlsfoUrl);
                scrapList = _scraperRepo.ScrapingLogic(doc, vlsfoXpath);
                _scraperRepo.CsvOutput(scrapList, vlsfoCsv);

                //TODO : try{Loading,Scraping}-catch{ex message}-finally{CsvOutput} 
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(intervalTime, stoppingToken); //TODO : find a way to pass in there the appsettings
            }
        }
    }
}