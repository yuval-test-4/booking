using Booking.Infrastructure;

namespace Booking.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(BookingDbContext context)
        : base(context) { }
}
