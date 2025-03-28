using DM.Domain.Entities;
using DM.Domain.Interfaces.Repositories;
using DM.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DM.Infrastructure.Repositories;

public sealed class EntryRepository(AppDailyContext dbContext) : IEntryRepository
{
    public bool Exist(Guid id)
    {
        var entity = dbContext.Set<Entry>().FirstOrDefault(x => x.Id == id);

        return entity is not null;
    }
    
    public void Create(Entry entity)
    {
        dbContext.Set<Entry>().Add(entity);
    }

    public ICollection<Entry>? Get(Entry entity, CancellationToken cancellationToken = default)
    {
        return dbContext.Set<Entry>().AsNoTracking().ToList();
    }

    public async Task<Entry?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Set<Entry>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public void Update(Entry entity)
    {
        dbContext.Set<Entry>().Update(entity);
    }
}