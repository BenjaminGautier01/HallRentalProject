using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using HallRentalModels.Dtos; // Assuming this is the correct namespace for your DTOs

namespace HallRentalClient.Services
{
    public class HallService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "api/halls"; // Base URL for the halls endpoint

        public HallService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetch all halls
        public async Task<IEnumerable<HallDto?>> GetAllHallsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<HallDto>>(_baseUrl) ?? Enumerable.Empty<HallDto>();
        }

        // Fetch a single hall by ID
        public async Task<HallDto?> GetHallByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<HallDto>($"{_baseUrl}/{id}");
        }

        // Create a new hall
        public async Task<HallDto?> CreateHallAsync(HallDto newHall)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, newHall);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<HallDto>();
            }
            else
            {
                // Handle errors or throw an exception
                throw new HttpRequestException("Error creating hall");
            }
        }

        // Update an existing hall
        public async Task UpdateHallAsync(int id, HallDto updatedHall)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", updatedHall);
            if (!response.IsSuccessStatusCode)
            {
                // Handle errors or throw an exception
                throw new HttpRequestException("Error updating hall");
            }
        }

        // Delete a hall
        public async Task DeleteHallAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                // Handle errors or throw an exception
                throw new HttpRequestException("Error deleting hall");
            }
        }
    }
}
