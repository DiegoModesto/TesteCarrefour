using System.Data;
using DM.Domain.Interfaces;
using DM.Domain.Interfaces.Repositories;
using DM.Infrastructure.Persistence;
using DM.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DM.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppReportContext>(builder => builder.UseNpgsql(config.GetConnectionString("AppDB")));
        
        services.AddRepositories();
        
        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IReportRepository, ReportRepository>();

        services.AddScoped<IUnitOfWork>(f => f.GetRequiredService<AppReportContext>());
        services.AddScoped<IDbConnection>(f => f.GetRequiredService<AppReportContext>().Database.GetDbConnection());
    }
}