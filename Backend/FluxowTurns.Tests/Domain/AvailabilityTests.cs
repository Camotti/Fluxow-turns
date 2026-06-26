using FluxowTurns.Domain.Entities;
using FluxowTurns.Domain.Enums;

namespace FluxowTurns.Tests.Domain;

public class AvailabilityTests
{
    [Fact]
    public void Create_WithValidTimes_ShouldBeAvailable()
    {
        var startAt= new DateTime(2026,7,10,9,0,0);
        var endAt= new DateTime(2026,7,10,10,0,0);

        var availability= new Availability(startAt,endAt);

        Assert.NotEqual(Guid.Empty, availability.Id);
        Assert.Equal(startAt, availability.StartAt);
        Assert.Equal(endAt, availability.EndAt);
        Assert.Equal(AvailabilityStatus.Available,availability.Status);
    }

    [Fact]
    public void Create_WhenStartIsAfterEnd_ShouldThrowArgumentException()
    {
        var starAt= new DateTime(2026, 7, 10, 10, 0, 0);
        var endAt= new DateTime(2026, 7, 10, 9, 0, 0);

        var exception= Assert.Throws<ArgumentException>(
            ()=> new Availability(starAt,endAt)
        );

        Assert.Equal("startAt", exception.ParamName);
    }

    [Fact]
    public void Create_WhenStartEqualsEnd_ShouldThrowArgumentException()
    {
        var date = new DateTime(2026, 7, 10, 9, 0, 0);

        Assert.Throws<ArgumentException>(
            () => new Availability(date, date));
    }

    [Fact]
    public void Reserve_WhenAvailabilityIsAvailable_ShouldSetReservedStatus()
    {
        var availability = CreateValidAvailability();

        availability.Reserve();

        Assert.Equal(AvailabilityStatus.Reserved, availability.Status);
    }


    [Fact]
    public void Reserve_WhenAvailabilityIsAlreadyReserved_ShouldThrowInvalidOperationException()
    {
        var availability= CreateValidAvailability();
        availability.Reserve();

        Assert.Throws<InvalidOperationException>( ()=> availability.Reserve() );    
    }

    [Fact]
    public void Release_WhenAvailabilityisReserved_ShouldSetAvailabilityStatus()
    {
        var availability= CreateValidAvailability();
        availability.Reserve();
        availability.Release();
        Assert.Equal(AvailabilityStatus.Available, availability.Status);

    }

    [Fact]
    public void  Release_WhenAvailabilityIsAvailable_ShouldThrowInvalidOperationException()
    {
        var availability= CreateValidAvailability();
        Assert.Throws<InvalidOperationException>( ()=> availability.Release() );   
    }

    [Fact]
    public void Disable_WhenAvailabilityIsAvailable_ShouldSetDisabledStatus()
    {
        var availability= CreateValidAvailability();
        availability.Disable();

        Assert.Equal(AvailabilityStatus.Disabled, availability.Status);
    }

    [Fact]
    public void  Disable_WhenAvailabilityIsReserved_ShouldThrowInvalidOperationException()
    {
        var availability= CreateValidAvailability();
        availability.Reserve();

        Assert.Throws<InvalidOperationException>( ()=> availability.Disable());
    }

    private static Availability CreateValidAvailability()
    {
        return new Availability(
            new DateTime(2026,7,10,9,0,0),
            new DateTime(2026,7,10,10,0,0)
        );
    }

}