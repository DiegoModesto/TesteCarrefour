using DM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DM.Infrastructure.Configurations;

public sealed class EntryConfiguration : IEntityTypeConfiguration<Entry>
{
    public void Configure(EntityTypeBuilder<Entry> builder)
    {
        builder.ToTable("entry");
        
        builder.HasKey(x => x.Id);
        
        builder
            .Property(x => x.Balance)
            .IsRequired();

        builder
            .Property(x => x.Type)
            .IsRequired();
        
        builder
            .Property(x => x.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_DATE");
    }
}