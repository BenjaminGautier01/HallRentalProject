namespace HallRentalAPI.Entities
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public int HallID { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime RentalDate { get; set; }
        public int Duration { get; set; }
        public decimal TotalCost { get; set; }

        // Navigation properties for related entities
        public Customer? Customer { get; set; }
        public Hall? Hall { get; set; }
        public Payment? Payment { get; set; }
    }
}
