using DM.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DM.Infrastructure.Persistence;

public class AppDailyContext(DbContextOptions<AppDailyContext> options) : DbContext(options), IUnitOfWork
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
            connectionString: "User ID=postgres;Password=postgres;Host=localhost;Port=5030;Database=daily_entry;Pooling=true;"
        );

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assemblyWithConfiguration = GetType().Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assemblyWithConfiguration);
    }
}