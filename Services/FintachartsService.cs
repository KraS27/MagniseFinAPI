using MagniseFinAPI.DB;
using MagniseFinAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using static System.Formats.Asn1.AsnWriter;

namespace MagniseFinAPI.Services
{   
    public class FintachartsService : IFintachartsService
    {
        private readonly HttpClient _httpClient;  
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private static string _token = string.Empty;
        private DateTime _tokenExpiryTime = DateTime.MinValue;

        public FintachartsService(HttpClient httpClient, 
            IConfiguration configuration,
            IServiceProvider serviceProvider)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
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
            var incomingMarketAssets = await GetMarketAssets();

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var existingMarketAssets = await dbContext.MarketAssets
                    .Include(x => x.Mappings)
                    .ToListAsync();

                foreach (var incomingAsset in incomingMarketAssets)
                {
                    var existingAsset = existingMarketAssets.FirstOrDefault(e => e.Id == incomingAsset.Id);

                    if (existingAsset != null)
                    {
                        if (AssetHasChanged(existingAsset, incomingAsset))
                        {
                            UpdateAsset(existingAsset, incomingAsset);
                            dbContext.MarketAssets.Update(existingAsset);
                        }
                    }
                    else
                    {
                        dbContext.MarketAssets.Add(incomingAsset);
                    }
                }

                await dbContext.SaveChangesAsync();
            }
        }

        private async Task<IEnumerable<MarketAsset>> GetMarketAssets()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://platform.fintacharts.com/api/instruments/v1/instruments?size=100");
            var token = await GetBearerTokenAsync();
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(responseData);
            var data = doc.RootElement.GetProperty("data").GetRawText();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new MappingConverter() }
            };
            var marketAssets = JsonSerializer.Deserialize<List<MarketAsset>>(data, options);

            return marketAssets ?? new List<MarketAsset>();
        }

        private bool AssetHasChanged(MarketAsset existingAsset, MarketAsset incomingAsset)
        {            
            return existingAsset.Symbol != incomingAsset.Symbol ||
                   existingAsset.Kind != incomingAsset.Kind ||
                   existingAsset.Exchange != incomingAsset.Exchange ||
                   existingAsset.Description != incomingAsset.Description ||
                   existingAsset.TickSize != incomingAsset.TickSize ||
                   existingAsset.Currency != incomingAsset.Currency;
        }

        private void UpdateAsset(MarketAsset existingAsset, MarketAsset incomingAsset)
        {         
            existingAsset.Symbol = incomingAsset.Symbol;
            existingAsset.Kind = incomingAsset.Kind;
            existingAsset.Exchange = incomingAsset.Exchange;
            existingAsset.Description = incomingAsset.Description;
            existingAsset.TickSize = incomingAsset.TickSize;
            existingAsset.Currency = incomingAsset.Currency;
            existingAsset.Mappings = incomingAsset.Mappings;
        }
    }
}
