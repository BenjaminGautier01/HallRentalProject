namespace HallRentalAPI.Entities
{
    public class Hall
    {

        public Hall()
        {
            Bookings = new List<Booking>();
        }

        public int HallID { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public int? Capacity { get; set; }
        public string? Amenities { get; set; }

        // Navigation property for related Bookings
        public List<Booking> Bookings { get; set; }
    }
}
