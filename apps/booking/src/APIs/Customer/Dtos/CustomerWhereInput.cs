namespace Booking.APIs.Dtos;

public class CustomerWhereInput
{
    public List<string>? Bookings { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
