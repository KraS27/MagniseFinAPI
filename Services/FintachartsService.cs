
namespace MagniseFinAPI.Services
{
    public class FintachartsService : IFintachartsService
    {
        private readonly HttpClient _httpClient;

        public FintachartsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<string> GetBearerToken()
        {
            throw new NotImplementedException();
        }
    }
}
