using Booking.Infrastructure;

namespace Booking.APIs;

public class BookingsService : BookingsServiceBase
{
    public BookingsService(BookingDbContext context)
        : base(context) { }
}
