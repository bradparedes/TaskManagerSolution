using TaskManager.Core.Entities;

namespace TaskManager.UI.Services
{
    public class UserService
    {
        private readonly HttpClient _http;

        public UserService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("ApiClient");
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var response = await _http.GetFromJsonAsync<List<User>>("users");
            return response!;
        }

    }
}
