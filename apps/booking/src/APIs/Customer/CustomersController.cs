using Microsoft.AspNetCore.Mvc;

namespace Booking.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
