namespace MagniseFinAPI.Services
{
    public interface IFintachartsService
    {
        public Task<string> GetBearerTokenAsync();

        public Task UpdateMarketAssets();
    }
}
