using MagniseFinAPI.Models;

namespace MagniseFinAPI.Services
{
    public interface IMarketAssetsService
    {
        public Task<IEnumerable<MarketAsset>> GetAllAsync(Pagination<MarketAsset> pagination);
        
    }
}
