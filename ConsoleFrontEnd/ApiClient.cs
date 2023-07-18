using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ConsoleFrontEnd
{
    internal class ApiClient
    {
        private readonly HttpClient httpClient;
        
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
                string jwt = await response.Content.ReadAsStringAsync();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<string?> Post(string route, object data)
        {
            using StringContent jsonContent = new StringContent(
                JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await httpClient.PostAsync(route, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return response.StatusCode.ToString();
        }

        public async Task<string?> Put(string route, object data)
        {
            using StringContent jsonContent = new StringContent(
                JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await httpClient.PutAsync(route, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return response.StatusCode.ToString();
        }

        public async Task<string?> Delete(string route)
        {
            using HttpResponseMessage response = await httpClient.DeleteAsync(route);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return response.StatusCode.ToString();
        }
    }
}
