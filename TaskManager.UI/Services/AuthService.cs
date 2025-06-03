using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace TaskManager.UI.Services
{
    public class AuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JwtAuthenticationStateProvider _authProvider;
        private readonly ProtectedSessionStorage _storage;

        public AuthService(IHttpClientFactory clientFactory,
                           JwtAuthenticationStateProvider authProvider,
                           ProtectedSessionStorage storage)
        {
            _clientFactory = clientFactory;
            _authProvider = authProvider;
            _storage = storage;
        }

        public async Task<bool> Login(string username, string password)
        {
            var client = _clientFactory.CreateClient("ApiClient");
            var loginData = new { username, password };
            var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:5094/api/auth/login", content);

            if (!response.IsSuccessStatusCode)
                return false;

            var token = await response.Content.ReadAsStringAsync();
            token = token.Replace("\"", "");

            if (string.IsNullOrWhiteSpace(token))
                return false;

            await _storage.SetAsync("authToken", token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            await _authProvider.MarkUserAsAuthenticated(token);
            return true;
        }

        public async Task Logout()
        {
            await _authProvider.MarkUserAsLoggedOut();
        }

        public async Task<string?> GetToken()
        {
            var result = await _storage.GetAsync<string>("authToken");
            return result.Success ? result.Value : null;
        }
    }
}