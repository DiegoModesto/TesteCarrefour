using DM.Domain.Entities;
using DM.Domain.Interfaces.Repositories;
using DM.Infrastructure.Persistence;

namespace DM.Infrastructure.Repositories;

public sealed class ReportRepository(AppReportContext dbContext) : IReportRepository
{
    public void Create(Report entity)
    {
        dbContext.Set<Report>().Add(entity);
    }
}