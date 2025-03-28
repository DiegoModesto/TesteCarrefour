namespace DM.Domain.Interfaces.Repositories.Basis;

public interface IUpdateRepository<T> where T : class
{
    void Update(T entity);
}