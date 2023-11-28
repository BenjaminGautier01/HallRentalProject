using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using HallRentalModels.Dtos; // Assuming this is the correct namespace for your DTOs

namespace HallRentalProject.Services
{
    public class BookingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "api/bookings"; // Base URL for the bookings endpoint

        public BookingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetch all bookings
        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<BookingDto>>(_baseUrl) ?? Enumerable.Empty<BookingDto>();
        }

        // Fetch a single booking by ID
        public async Task<BookingDto?> GetBookingByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<BookingDto>($"{_baseUrl}/{id}");
        }

        // Create a new booking
        public async Task<BookingDto?> CreateBookingAsync(BookingDto newBooking)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, newBooking);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BookingDto>();
            }
            else
            {
                // Handle errors or throw an exception
                throw new HttpRequestException("Error creating booking");
            }
        }

        // Update an existing booking
        public async Task UpdateBookingAsync(int id, BookingDto updatedBooking)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", updatedBooking);
            if (!response.IsSuccessStatusCode)
            {
                // Handle errors or throw an exception
                throw new HttpRequestException("Error updating booking");
            }
        }

        // Delete a booking
        public async Task DeleteBookingAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                // Handle errors or throw an exception
                throw new HttpRequestException("Error deleting booking");
            }
        }
    }
}
