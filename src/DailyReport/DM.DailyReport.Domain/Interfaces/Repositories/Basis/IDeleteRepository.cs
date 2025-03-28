namespace DM.Domain.Interfaces.Repositories.Basis;

public interface IDeleteRepository<in T> where T : class
{
    void Delete(T entity);
    void Delete(Guid id);
}