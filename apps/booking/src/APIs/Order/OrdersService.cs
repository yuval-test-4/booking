using Booking.Infrastructure;

namespace Booking.APIs;

public class OrdersService : OrdersServiceBase
{
    public OrdersService(BookingDbContext context)
        : base(context) { }
}
