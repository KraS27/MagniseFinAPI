namespace MagniseFinAPI.Services
{
    public class MarketAssetsUpdateService : BackgroundService
    {
        private readonly IFintachartsService _fintachartsService;
        private readonly ILogger<MarketAssetsUpdateService> _logger;
        private const int UPDATE_TIME_MIN = 1;

        public MarketAssetsUpdateService(IFintachartsService fintachartsService, ILogger<MarketAssetsUpdateService> logger)
        {
            _fintachartsService = fintachartsService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Market assets update service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Updating market assets from Fintacharts API.");
                    await _fintachartsService.UpdateMarketAssetsAsync();
                    _logger.LogInformation("Market assets updated successfully.");

                    await Task.Delay(TimeSpan.FromMinutes(UPDATE_TIME_MIN), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating market assets.");
                    await Task.Delay(TimeSpan.FromMinutes(UPDATE_TIME_MIN), stoppingToken);
                }
            }

            _logger.LogInformation("Market assets update service is stopping.");
        }
    }

}
