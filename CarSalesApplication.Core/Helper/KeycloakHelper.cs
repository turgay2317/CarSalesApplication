using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarSalesApplication.Core.Helper;

// Artık JWT tokeni biz üretmiyoruz, Keycloak üzerinden ürettiriyoruz ve imzalıyoruz.
public class KeycloakHelper
{
    private readonly HttpClient _httpClient;

    public KeycloakHelper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetAccessTokenAsync(string username, string password)
    {
        var tokenEndpoint = "http://localhost:9500/realms/CarSales/protocol/openid-connect/token";

        // Request body oluşturuluyor
        var requestBody = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", "CarSales"),
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password)
        });

        var response = await _httpClient.PostAsync(tokenEndpoint, requestBody);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var jsonDocument = JsonDocument.Parse(responseContent);
        var accessToken = jsonDocument.RootElement.GetProperty("access_token").GetString();

        return accessToken;
    }
    
    // Kullanıcı Kayıt et
    public async Task<bool> CreateUserAsync(string email, string firstName, string lastName, string password)
    {
        var userEndpoint = "http://localhost:9500/admin/realms/CarSales/users";
        // Önce Admine giriş yaptık.
        var adminToken = await GetAccessTokenAsync("USERNAME", "PASSWORD");
        // Authorization header ekle
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminToken);

        // Kullanıcıyı oluşturduk
        var userPayload = new
        {
            enabled = true,
            email = email,
            firstName = firstName,
            lastName = lastName,
            emailVerified = true,
            credentials = new[]
            {
                new
                {
                    type = "password",
                    value = password,
                    temporary = false
                }
            }
        };
        
        var jsonPayload = JsonSerializer.Serialize(userPayload);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(userEndpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        
        return true;
    }
}
