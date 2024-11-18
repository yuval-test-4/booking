using Booking.APIs;
using Booking.APIs.Common;
using Booking.APIs.Dtos;
using Booking.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class BookingsControllerBase : ControllerBase
{
    protected readonly IBookingsService _service;

    public BookingsControllerBase(IBookingsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one booking
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Booking>> CreateBooking(BookingCreateInput input)
    {
        var booking = await _service.CreateBooking(input);

        return CreatedAtAction(nameof(Booking), new { id = booking.Id }, booking);
    }

    /// <summary>
    /// Delete one booking
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteBooking([FromRoute()] BookingWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteBooking(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many bookings
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Booking>>> Bookings(
        [FromQuery()] BookingFindManyArgs filter
    )
    {
        return Ok(await _service.Bookings(filter));
    }

    /// <summary>
    /// Meta data about booking records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> BookingsMeta(
        [FromQuery()] BookingFindManyArgs filter
    )
    {
        return Ok(await _service.BookingsMeta(filter));
    }

    /// <summary>
    /// Get one booking
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Booking>> Booking([FromRoute()] BookingWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Booking(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one booking
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateBooking(
        [FromRoute()] BookingWhereUniqueInput uniqueId,
        [FromQuery()] BookingUpdateInput bookingUpdateDto
    )
    {
        try
        {
            await _service.UpdateBooking(uniqueId, bookingUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a customer record for booking
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] BookingWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }
}
