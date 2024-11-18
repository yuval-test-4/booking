using Booking.APIs.Common;
using Booking.APIs.Dtos;

namespace Booking.APIs;

public interface ICustomersService
{
    /// <summary>
    /// Create one customer
    /// </summary>
    public Task<Customer> CreateCustomer(CustomerCreateInput customer);

    /// <summary>
    /// Delete one customer
    /// </summary>
    public Task DeleteCustomer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many customers
    /// </summary>
    public Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about customer records
    /// </summary>
    public Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one customer
    /// </summary>
    public Task<Customer> Customer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one customer
    /// </summary>
    public Task UpdateCustomer(CustomerWhereUniqueInput uniqueId, CustomerUpdateInput updateDto);

    /// <summary>
    /// Connect multiple bookings records to customer
    /// </summary>
    public Task ConnectBookings(
        CustomerWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] bookingsId
    );

    /// <summary>
    /// Disconnect multiple bookings records from customer
    /// </summary>
    public Task DisconnectBookings(
        CustomerWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] bookingsId
    );

    /// <summary>
    /// Find multiple bookings records for customer
    /// </summary>
    public Task<List<Booking>> FindBookings(
        CustomerWhereUniqueInput uniqueId,
        BookingFindManyArgs BookingFindManyArgs
    );

    /// <summary>
    /// Update multiple bookings records for customer
    /// </summary>
    public Task UpdateBookings(
        CustomerWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] bookingsId
    );
}
