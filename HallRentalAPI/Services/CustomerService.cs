using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using HallRentalModels.Dtos;    // DTO namespace

namespace HallRentalClient.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "api/customers"; // Base URL for the customers endpoint

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetch all customers
        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<CustomerDto>>(_baseUrl) ?? Enumerable.Empty<CustomerDto>();
        }


        // Fetch a single customer by ID
        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CustomerDto>($"{_baseUrl}/{id}");
        }

        // Create a new customer
        public async Task<CustomerDto?> CreateCustomerAsync(CustomerDto newCustomer)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, newCustomer);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CustomerDto>();
            }
            else
            {
                // Handle errors or throw an exception
                throw new HttpRequestException("Error creating customer");
            }
        }

        // Update an existing customer
        public async Task UpdateCustomerAsync(int id, CustomerDto updatedCustomer)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", updatedCustomer);
            if (!response.IsSuccessStatusCode)
            {
                // Handle errors or throw an exception
                throw new HttpRequestException("Error updating customer");
            }
        }

        // Delete a customer
        public async Task DeleteCustomerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                // Handle errors or throw an exception
                throw new HttpRequestException("Error deleting customer");
            }
        }
    }
}
