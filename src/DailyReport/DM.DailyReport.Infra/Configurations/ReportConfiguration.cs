using DM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DM.Infrastructure.Configurations;

public sealed class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("report");
        
        builder.HasKey(x => x.Id);
        
        builder
            .Property(x => x.Balance)
            .IsRequired();

        builder
            .Property(x => x.EntryName)
            .IsRequired();

        builder
            .Property(x => x.RegisterDate)
            .IsRequired();
        
        builder
            .Property(x => x.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_DATE");
    }
}