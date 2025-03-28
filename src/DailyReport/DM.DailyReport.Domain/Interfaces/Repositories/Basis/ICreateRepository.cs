namespace DM.Domain.Interfaces.Repositories.Basis;

public interface ICreateRepository<T> where T : class
{
    void Create(T entity);
}