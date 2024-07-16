using MagniseFinAPI.Models;
using System.Text;
using System.Text.Json;

namespace MagniseFinAPI.Services
{   
    public class FintachartsService : IFintachartsService
    {
        private readonly HttpClient _httpClient;
        private readonly FintachartsLoginSettings _loginSettings;

        public FintachartsService(HttpClient httpClient, FintachartsLoginSettings loginSettings)
        {
            _httpClient = httpClient;
            _loginSettings = loginSettings;
        }

        public async Task<string> GetBearerTokenAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://platform.fintacharts.com/identity/realms/fintatech/protocol/openid-connect/token");
            var parameters = new Dictionary<string, string>
            {
                { "username", _loginSettings.Username },
                { "password", _loginSettings.Password },
                { "grant_type", _loginSettings.GrantType },
                { "client_id", _loginSettings.ClientId }
            };

            request.Content = new FormUrlEncodedContent(parameters);

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<JsonElement>(responseData).GetProperty("access_token").GetString();

            return token;
        }
    }
}
