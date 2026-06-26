using FluxowTurns.Domain.Entities;
using FluxowTurns.Domain.Enums;


namespace FluxowTurns.Tests.Domain;

public class BookingTests
{
    [Fact]
    public void Create_WithValidData_ShouldBeActive()
    {
        var clientId= Guid.NewGuid();
        var availabilityId= Guid.NewGuid();
        var createdAt= new DateTime(2026,7,10,8,30,0);

        var booking= new Booking(clientId,availabilityId,createdAt);

        Assert.NotEqual(Guid.Empty,booking.Id);
        Assert.Equal(clientId,booking.ClientId);
        Assert.Equal(availabilityId, booking.AvailabilityId);
        Assert.Equal(createdAt, booking.CreatedAt);
        Assert.Equal(BookingStatus.Active, booking.Status);
        Assert.Null(booking.CancelledAt);
    }

    [Fact]
    public void Create_WhenClientIdIsEmpty_ShouldThrowArgumentException()
    {
        var availabilityId= Guid.NewGuid();

        var exception = Assert.Throws<ArgumentException>(
            () => new Booking(Guid.Empty, availabilityId, DateTime.UtcNow));

        Assert.Equal("clientId", exception.ParamName);
    }

    [Fact]
    public void Create_WhenAvailabilityIdIsEmpty_ShouldThrowArgumentException()
    {
        var clientId= Guid.NewGuid();
        var exception= Assert.Throws<ArgumentException>( ()=> new Booking(clientId,Guid.Empty,DateTime.UtcNow));

        Assert.Equal("availabilityId", exception.ParamName);
    }

    [Fact]
    public void Cancel_WhenBookingIsActive_ShouldSetCancelledStatusAndDate()
    {
        var booking= CreateValidBooking();
        var cancelledAt= new DateTime(2026,7,10,9,15,0);

        booking.Cancel(cancelledAt);

        Assert.Equal(BookingStatus.Cancelled, booking.Status);
        Assert.Equal(cancelledAt, booking.CancelledAt);
    }

    [Fact]
    public void Cancel_WhenBookingIsAlreadyCancelled_ShouldThrowInvalidOperationException()
    {
        var booking= CreateValidBooking();
        booking.Cancel(DateTime.UtcNow);

        Assert.Throws<InvalidOperationException>( ()=> booking.Cancel(DateTime.UtcNow));
    }

    


    private static Booking CreateValidBooking()
    {
        return new Booking(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new DateTime(2026,7,10,8,30,0));
    }
}
