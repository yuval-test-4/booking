using Microsoft.AspNetCore.Mvc;

namespace Booking.APIs;

[ApiController()]
public class OrdersController : OrdersControllerBase
{
    public OrdersController(IOrdersService service)
        : base(service) { }
}
