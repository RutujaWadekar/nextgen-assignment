using System.Net.Http.Json;

using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Services
{
    public class CalculatorHttpService : ICalculatorHttpService
    {
        private readonly HttpClient _httpClient;
        public CalculatorHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<PostalCode>> GetPostalCodesAsync()
        {
            
            var response = await _httpClient.GetAsync("api/posta1code");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Cannot fetch postal codes, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<List<PostalCode>>() ?? [];
        }

        public async Task<List<CalculatorHistory>> GetHistoryAsync()
        {
            var response = await _httpClient.GetAsync("api/taxcalculator/history");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Cannot fetch history, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<List<CalculatorHistory>>() ?? new List<CalculatorHistory>();
        }

        public async Task<CalculateResult> CalculateTaxAsync(CalculateRequest calculationRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/taxcalculator/calculate-tax", calculationRequest);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Tax calculation failed, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<CalculateResult>()
                ?? throw new Exception("Invalid response from tax calculation API.");
        }
    }
}