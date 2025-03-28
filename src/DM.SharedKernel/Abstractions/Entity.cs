namespace DM.SharedKernel.Abstractions;

public abstract class Entity
{
    public DateTimeOffset CreatedDate { get; protected set; } = DateTimeOffset.Now;
    public DateTimeOffset? DeletedDate { get; protected set; }
}