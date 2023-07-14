using System.Text;
using System.Text.Json;

namespace ConsoleFrontEnd
{
    internal class ApiClient
    {
        private readonly HttpClient httpClient;
        private string jwt;
        
        public ApiClient(string baseUri) 
        {
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUri)
            };
        }

        public async Task<bool> Authenticate(string userName, int pinCode)
        {
            using StringContent jsonContent = new StringContent(
                JsonSerializer.Serialize(new { userName, pinCode }), Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await httpClient.PostAsync(
                "api/auth/authenticate",
                jsonContent);

            if (response.IsSuccessStatusCode)
            {
                jwt = await response.Content.ReadAsStringAsync();
            }

            return response.IsSuccessStatusCode;
        } 
    }
}
