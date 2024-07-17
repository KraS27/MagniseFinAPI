using MagniseFinAPI.DB;
using MagniseFinAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagniseFinAPI.Services
{
    public class MarketAssetsService : IMarketAssetsService
    {
        private readonly AppDbContext _dbContext;

        public MarketAssetsService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MarketAsset>> GetAllAsync(Pagination<MarketAsset> pagination)
        {
            var assets = _dbContext.MarketAssets
                .Include(x => x.Mappings)
                .AsQueryable();
            
            return await pagination.Apply(assets).ToListAsync();
        }
    }
}
