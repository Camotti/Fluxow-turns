namespace FluxowTurns.Domain.Common; 

public abstract class BaseEntity
{
   public Guid Id {get; protected set;}
   protected BaseEntity()
    { Id= Guid.NewGuid();} //it's for asigned an ID and avoid a testing error 

   protected BaseEntity(Guid id)
    {
        Id=id == Guid.Empty
            ? Guid.NewGuid()
            : id;
    } 
}