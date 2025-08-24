using SimpleStocker.ProductApi.DTO;

namespace SimpleStocker.ProductApi.Factories
{
    public class HttpClientFactory
    {
        private readonly HttpClient _httpClient;

        public HttpClientFactory(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<T>> PostAsync<T>(string url, object body)
        {
            var response = await _httpClient.PostAsJsonAsync(url, body);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiResponse<T>>();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return new ApiResponse<T>(error);
            }
        }
    }
}
