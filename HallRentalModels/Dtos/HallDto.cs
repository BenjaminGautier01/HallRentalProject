using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallRentalModels.Dtos
{
    public class HallDto
    {
        public int HallID { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public int? Capacity { get; set; }
        public string? Amenities { get; set; }
    }
}
