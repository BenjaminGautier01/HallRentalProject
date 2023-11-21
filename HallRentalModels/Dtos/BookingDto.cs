using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallRentalModels.Dtos
{
    public class BookingDto
    {
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public int HallID { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime RentalDate { get; set; }
        public int Duration { get; set; }
        public decimal TotalCost { get; set; }
    }
}
