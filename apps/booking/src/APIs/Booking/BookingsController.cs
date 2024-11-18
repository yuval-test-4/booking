using Microsoft.AspNetCore.Mvc;

namespace Booking.APIs;

[ApiController()]
public class BookingsController : BookingsControllerBase
{
    public BookingsController(IBookingsService service)
        : base(service) { }
}
