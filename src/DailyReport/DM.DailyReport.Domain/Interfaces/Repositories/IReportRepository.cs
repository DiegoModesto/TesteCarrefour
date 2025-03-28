using DM.Domain.Entities;
using DM.Domain.Interfaces.Repositories.Basis;

namespace DM.Domain.Interfaces.Repositories;

public interface IReportRepository :
    ICreateRepository<Report>{ }