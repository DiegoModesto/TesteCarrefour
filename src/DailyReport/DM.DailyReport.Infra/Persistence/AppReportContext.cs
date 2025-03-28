using DM.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DM.Infrastructure.Persistence;

public class AppReportContext(DbContextOptions<AppReportContext> options) : DbContext(options), IUnitOfWork
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
            connectionString: "User ID=postgres;Password=postgres;Host=localhost;Port=5031;Database=daily_report;Pooling=true;"
        );

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assemblyWithConfiguration = GetType().Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assemblyWithConfiguration);
    }
}