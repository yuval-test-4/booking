using Booking.APIs.Common;
using Booking.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class CustomerFindManyArgs : FindManyInput<Customer, CustomerWhereInput> { }
