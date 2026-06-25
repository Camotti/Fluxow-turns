using FluxowTurns.Domain.Enums;
using FluxowTurns.Domain.Common;
using FluxdoowTurns.Domain.Enums;
using System.Net;

namespace FluxowTurns.Domain.Entities;

public sealed class Booking: BaseEntity
{
    public Guid ClientId {get; private set;}
    public Guid AvailabilityId {get; private set;}
    public BookingStatus Status {get; private set;}
    public DateTime CreatedAt {get; private set;}
    public DateTime CancelledAt {get; private set;}

    private Booking()
    {}

    public Booking (Guid clientId,Guid availabilityId, DateTime createdAt): base()
    {
        if (clientId == Guid.Empty)
            throw new ArgumentException("The customer is obligatory",nameof(clientId));

        if (availabilityId == Guid.Empty)
            throw new ArgumentException("Availability must be obligotory",nameof(availabilityId));


        ClientId= clientId;
        AvailabilityId=availabilityId;
        CreatedAt=createdAt;
        Status=BookingStatus.Active;

    }
}