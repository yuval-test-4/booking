using Booking.APIs.Dtos;
using Booking.Infrastructure.Models;

namespace Booking.APIs.Extensions;

public static class BookingsExtensions
{
    public static Booking ToDto(this BookingDbModel model)
    {
        return new Booking
        {
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static BookingDbModel ToModel(
        this BookingUpdateInput updateDto,
        BookingWhereUniqueInput uniqueId
    )
    {
        var booking = new BookingDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            booking.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            booking.CustomerId = updateDto.Customer;
        }
        if (updateDto.UpdatedAt != null)
        {
            booking.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return booking;
    }
}
