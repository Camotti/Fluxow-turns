using FluxowTurns.Domain.Common;
using FluxowTurns.Domain.Enums;

namespace FluxowTurns.Domain.Entities;

public sealed class Availability: BaseEntity
{
    public DateTime StartAt {get; private set;}
    public DateTime EndAt {get; private set;}
    public AvailabilityStatus Status {get; private set;}


   private Availability() {}

    public Availability(DateTime startAt, DateTime endAt) : base()
    {
        if (startAt >= endAt)
            throw new ArgumentException(
                "Start time must be earlier than end time.",nameof(startAt) 
            );

        StartAt= startAt;
        EndAt= endAt;
        Status= AvailabilityStatus.Available;
    }

    public void Reserve()
    {
        if (Status != AvailabilityStatus.Available)
            throw new InvalidOperationException("The Schedule is not available to book.");
        
        Status= AvailabilityStatus.Reserved;    
    }

    public void Release()
    {
        if (Status != AvailabilityStatus.Reserved)
            throw new InvalidOperationException("Only a reserved schedule can be released.");

        Status= AvailabilityStatus.Available;    
    }

    public void Disable()
    {
        if (Status != AvailabilityStatus.Reserved)
            throw new InvalidOperationException("An active reserved schedule cannot be disabled.");

        Status= AvailabilityStatus.Disabled;    
    }

}