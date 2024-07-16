
using Microsoft.AspNetCore.Server.HttpSys;
using System.Text;
using System.Text.Json;

namespace MagniseFinAPI.Services
{

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

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
            var loginRequestModel = new LoginRequest { Username = "r_test@fintatech.com", Password = "kisfiz-vUnvy9-sopnyv" };


            request.Content = new StringContent(
                JsonSerializer
                .Serialize(loginRequestModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<string>(responseData);

            return token;
        }
    }
}
