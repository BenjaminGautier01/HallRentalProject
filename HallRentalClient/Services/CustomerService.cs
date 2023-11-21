using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using HallRentalModels.Dtos;    // Making sure to include the namespace where the DTOs are

namespace HallRentalClient.Services
{


    public class CustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

       
        }
    }
}
