using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.JSInterop;
using Rummy.Shared.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Rummy.Client.Services
{
    public class AuthService
    {
        private readonly LocalStorage _storage;
        private readonly HttpClient _http;
        private AuthResponse auth;
        private readonly string authKey = "auth";

        public AuthService(LocalStorage storage, HttpClient http)
        {
            _storage = storage;
            _http = http;
            CheckLocalStorage();
        }

        public async Task<bool> TryLogin(User user)
        {
            var userContent = new StringContent(Json.Serialize(user), Encoding.UTF8, "application/json");
            var httpResponse = await _http.PostAsync("api/Auth/login", userContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                var auth = Json.Deserialize<AuthResponse>(await httpResponse.Content.ReadAsStringAsync());
                _storage.SetItem(authKey, auth);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> TryRegister(User user)
        {
            var userContent = new StringContent(Json.Serialize(user), Encoding.UTF8, "application/json");
            var httpResponse = await _http.PostAsync("api/Auth/register", userContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                var auth = Json.Deserialize<AuthResponse>(await httpResponse.Content.ReadAsStringAsync());
                _storage.SetItem(authKey, auth);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryGetUsername(out string username)
        {
            if (auth == null) { CheckLocalStorage(); }
            if (auth == null)
            {
                username = null;
                return false;
            }
            username = auth.Email;
            return true;
        }

        private void CheckLocalStorage()
        {
            auth = _storage.GetItem<AuthResponse>(authKey);
        }
    }
}
