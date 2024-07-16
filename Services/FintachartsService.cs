
using Microsoft.AspNetCore.Server.HttpSys;
using System.Text.Json;

namespace MagniseFinAPI.Services
{
    public class FintachartsService : IFintachartsService
    {
        private readonly HttpClient _httpClient;

        public FintachartsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetBearerTokenAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://platform.fintacharts.com/identity/realms/:realm/protocol/openid-connect/token");

            request.Content = new StringContent(
                JsonSerializer
                .Serialize(
                    new
                    {
                        username = "r_test@fintatech.com",
                        password = "kisfiz-vUnvy9-sopnyv"
                    }));

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<string>(responseData);

            return null;
        }
    }
}
