using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;

namespace TaskManager.UI.Services
{
    public class CustomAuthProvider : JwtAuthenticationStateProvider
    {
        private string _token = string.Empty;
        private readonly AuthService _auth;

        public void SetToken(string token)
        {
            _token = token;
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public CustomAuthProvider(HttpClient httpClient, ProtectedSessionStorage sessionStorage, AuthService auth)
            : base(httpClient, sessionStorage)
        {
            _auth = auth;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var handler = new JwtSecurityTokenHandler();
            var token = await _auth.GetToken();

            if (string.IsNullOrWhiteSpace(token))
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "UsuarioLogueado")
            }, "jwt");

            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public void NotifyUserAuthentication(string username)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            }, "jwt");

            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public string GetToken() => _token;

        public void NotifyUserLogout()
        {
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(nobody)));
        }
        public void Logout()
        {
            _token = string.Empty;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }
    }
}