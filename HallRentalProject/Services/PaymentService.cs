using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using HallRentalModels.Dtos; // Replace with the correct namespace for your Payment DTO

namespace HallRentalProject.Services
{
    public class PaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "api/payments"; // Base URL for the payments endpoint

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetch all payments
        public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<PaymentDto>>(_baseUrl) ?? Enumerable.Empty<PaymentDto>(); ;
        }

        // Fetch a single payment by ID
        public async Task<PaymentDto?> GetPaymentByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PaymentDto>($"{_baseUrl}/{id}");
        }

        // Create a new payment
        public async Task<PaymentDto?> CreatePaymentAsync(PaymentDto newPayment)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, newPayment);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PaymentDto>();
            }
            else
            {
                // Handle errors or throw an exception
                throw new HttpRequestException("Error creating payment");
            }
        }

        // Update an existing payment
        public async Task UpdatePaymentAsync(int id, PaymentDto updatedPayment)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", updatedPayment);
            if (!response.IsSuccessStatusCode)
            {
                // Handle errors or throw an exception
                throw new HttpRequestException("Error updating payment");
            }
        }

        // Delete a payment
        public async Task DeletePaymentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                // Handle errors or throw an exception
                throw new HttpRequestException("Error deleting payment");
            }
        }
    }
}
