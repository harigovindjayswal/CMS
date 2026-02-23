using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AppEntities;

namespace Persistence.Configurations;

public class CaseConfiguration : IEntityTypeConfiguration<Case>
{
    public void Configure(EntityTypeBuilder<Case> entity)
    {
        entity.HasKey(e => e.CaseId).HasName("PK__Cases__6CAE526C7FBA9E16");

        entity.Property(e => e.CaseId).HasColumnName("CaseID");
        entity.Property(e => e.AssignedTo).HasMaxLength(450);
        entity.Property(e => e.CaseType).HasMaxLength(100);
        entity.Property(e => e.ClientId).HasColumnName("ClientID");
        entity.Property(e => e.CourtName).HasMaxLength(150);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.Opponent).HasMaxLength(150);
        entity.Property(e => e.Stage).HasMaxLength(20).HasDefaultValue("Filed");
        entity.Property(e => e.Status).HasMaxLength(20).HasDefaultValue("Open");
        entity.Property(e => e.Title).HasMaxLength(200);
        entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
    }
}