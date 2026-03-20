using Domain.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CaseTypeMstConfiguration : IEntityTypeConfiguration<CaseTypeMst>
{
    public void Configure(EntityTypeBuilder<CaseTypeMst> entity)
    {
        entity.ToTable("CaseTypes");

        entity.HasKey(e => e.CaseTypeId);

        entity.Property(e => e.CaseTypeId).HasColumnName("CaseTypeID");
        entity.Property(e => e.TypeName).HasMaxLength(120).IsUnicode(false);
        entity.Property(e => e.IsActive).HasDefaultValue(true);

        entity.Property(e => e.CreatedBy).HasMaxLength(450);
        entity.Property(e => e.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("(getdate())");
        entity.Property(e => e.UpdatedBy).HasMaxLength(450);
        entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    }
}

