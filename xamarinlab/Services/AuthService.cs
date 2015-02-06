using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace xamarinlab.Services
{
    public class AuthService
    {
        private const string Url = "http://webapilab-tab.azurewebsites.net/";

        public async Task<string> Login(string userName, string password)
        {
            var data = "grant_type=password&username=" + userName + "&password=" + password;

            using (var client = new System.Net.Http.HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                var queryString = new StringContent(data);
                var response = client.PostAsync(Url + "token", queryString).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var tokenResponseModel = JsonConvert.DeserializeObject<TokenResponseModel>(responseBody);
                    return tokenResponseModel.AccessToken;
                }

                return string.Empty;
            }
        }

    }

    class TokenResponseModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("userName")]
        public string Username { get; set; }

        [JsonProperty(".issued")]
        public string IssuedAt { get; set; }

        [JsonProperty(".expires")]
        public string ExpiresAt { get; set; }
    }
}