using MagniseFinAPI.Models;
using System.Text.Json;

namespace MagniseFinAPI.Services
{   
    public class FintachartsService : IFintachartsService
    {
        private readonly HttpClient _httpClient;  
        private readonly IConfiguration _configuration;
        private static string _token = string.Empty;
        private DateTime _tokenExpiryTime = DateTime.MinValue;

        public FintachartsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GetBearerTokenAsync()
        {
            if (string.IsNullOrEmpty(_token) || DateTime.UtcNow >= _tokenExpiryTime)
            {
                await RefreshTokenAsync();
            }           

            return _token;
        }

        private async Task RefreshTokenAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://platform.fintacharts.com/identity/realms/fintatech/protocol/openid-connect/token");
            var loginSettings = _configuration.GetSection("FintachartsLoginSettings").Get<FintachartsLoginSettings>()!;
            var parameters = new Dictionary<string, string>
            {
                { "username", loginSettings.Username },
                { "password", loginSettings.Password },
                { "grant_type", loginSettings.GrantType },
                { "client_id", loginSettings.ClientId }
            };

            request.Content = new FormUrlEncodedContent(parameters);

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseData);

            _token = jsonResponse.GetProperty("access_token").GetString()!;
            var expiresIn = jsonResponse.GetProperty("expires_in").GetInt32();

            _tokenExpiryTime = DateTime.UtcNow.AddSeconds(expiresIn).AddMinutes(-1); 
        }

        public async Task UpdateMarketAssets()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://platform.fintacharts.com/api/instruments/v1/instruments");
            var token = await GetBearerTokenAsync();
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
        }
    }
}
