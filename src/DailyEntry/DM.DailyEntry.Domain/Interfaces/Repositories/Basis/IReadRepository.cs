namespace DM.Domain.Interfaces.Repositories.Basis;

public interface IReadRepository<T> where T : class
{
    ICollection<T>? Get(T entity, CancellationToken cancellationToken = default);
    Task<T?> GetById(Guid id, CancellationToken cancellationToken = default);
}