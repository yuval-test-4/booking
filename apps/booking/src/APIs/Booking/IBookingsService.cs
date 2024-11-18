using Booking.APIs.Common;
using Booking.APIs.Dtos;

namespace Booking.APIs;

public interface IBookingsService
{
    /// <summary>
    /// Create one booking
    /// </summary>
    public Task<Booking> CreateBooking(BookingCreateInput booking);

    /// <summary>
    /// Delete one booking
    /// </summary>
    public Task DeleteBooking(BookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many bookings
    /// </summary>
    public Task<List<Booking>> Bookings(BookingFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about booking records
    /// </summary>
    public Task<MetadataDto> BookingsMeta(BookingFindManyArgs findManyArgs);

    /// <summary>
    /// Get one booking
    /// </summary>
    public Task<Booking> Booking(BookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one booking
    /// </summary>
    public Task UpdateBooking(BookingWhereUniqueInput uniqueId, BookingUpdateInput updateDto);

    /// <summary>
    /// Get a customer record for booking
    /// </summary>
    public Task<Customer> GetCustomer(BookingWhereUniqueInput uniqueId);
}
