namespace WebApplication3
{
    public class Booking
    {
            public int? BookingId { get; set; }
            public string? ParcelTypeId { get; set; }
            public double? Price { get; set; }
            public double? ShippingTime { get; set; }
            public int FromCity { get; set; }
            public int ToCity { get; set; }
            public string? Date { get; set; }
            public int? Height { get; set; }
            public int? Width { get; set; }
            public int? Length { get; set; }
            public int? Weight { get; set; }
            public List<Route>? TotalRoute { get; set; }
            public bool isBooked { get; set; }

    }
}
