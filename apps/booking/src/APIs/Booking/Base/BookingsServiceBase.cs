using Booking.APIs;
using Booking.APIs.Common;
using Booking.APIs.Dtos;
using Booking.APIs.Errors;
using Booking.APIs.Extensions;
using Booking.Infrastructure;
using Booking.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.APIs;

public abstract class BookingsServiceBase : IBookingsService
{
    protected readonly BookingDbContext _context;

    public BookingsServiceBase(BookingDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one booking
    /// </summary>
    public async Task<Booking> CreateBooking(BookingCreateInput createDto)
    {
        var booking = new BookingDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            booking.Id = createDto.Id;
        }
        if (createDto.Customer != null)
        {
            booking.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<BookingDbModel>(booking.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one booking
    /// </summary>
    public async Task DeleteBooking(BookingWhereUniqueInput uniqueId)
    {
        var booking = await _context.Bookings.FindAsync(uniqueId.Id);
        if (booking == null)
        {
            throw new NotFoundException();
        }

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many bookings
    /// </summary>
    public async Task<List<Booking>> Bookings(BookingFindManyArgs findManyArgs)
    {
        var bookings = await _context
            .Bookings.Include(x => x.Customer)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return bookings.ConvertAll(booking => booking.ToDto());
    }

    /// <summary>
    /// Meta data about booking records
    /// </summary>
    public async Task<MetadataDto> BookingsMeta(BookingFindManyArgs findManyArgs)
    {
        var count = await _context.Bookings.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one booking
    /// </summary>
    public async Task<Booking> Booking(BookingWhereUniqueInput uniqueId)
    {
        var bookings = await this.Bookings(
            new BookingFindManyArgs { Where = new BookingWhereInput { Id = uniqueId.Id } }
        );
        var booking = bookings.FirstOrDefault();
        if (booking == null)
        {
            throw new NotFoundException();
        }

        return booking;
    }

    /// <summary>
    /// Update one booking
    /// </summary>
    public async Task UpdateBooking(BookingWhereUniqueInput uniqueId, BookingUpdateInput updateDto)
    {
        var booking = updateDto.ToModel(uniqueId);

        if (updateDto.Customer != null)
        {
            booking.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(booking).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Bookings.Any(e => e.Id == booking.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a customer record for booking
    /// </summary>
    public async Task<Customer> GetCustomer(BookingWhereUniqueInput uniqueId)
    {
        var booking = await _context
            .Bookings.Where(booking => booking.Id == uniqueId.Id)
            .Include(booking => booking.Customer)
            .FirstOrDefaultAsync();
        if (booking == null)
        {
            throw new NotFoundException();
        }
        return booking.Customer.ToDto();
    }
}
