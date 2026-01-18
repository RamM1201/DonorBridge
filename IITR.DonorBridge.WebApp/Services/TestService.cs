using IITR.DonorBridge.WebApp.Models;

namespace IITR.DonorBridge.WebApp.Services
{
    public class TestService
    {
        private readonly HttpClient _httpClient;
        public TestService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }
        public async Task<TestModel> GetDataAsync()
        {
            var response = await _httpClient.GetAsync($"Test/all");
            response.EnsureSuccessStatusCode();
            var course = await response.Content.ReadFromJsonAsync<TestModel>();
            if (course is null)
                throw new InvalidOperationException("Course not found or response was null.");
            return course;
        }
    }
}
