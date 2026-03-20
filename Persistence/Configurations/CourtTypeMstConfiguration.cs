using Domain.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CourtTypeMstConfiguration : IEntityTypeConfiguration<CourtTypeMst>
{
    public void Configure(EntityTypeBuilder<CourtTypeMst> entity)
    {
        entity.ToTable("CourtTypes");

        entity.HasKey(e => e.CourtTypeId);

        entity.Property(e => e.CourtTypeId).HasColumnName("CourtTypeID");
        entity.Property(e => e.TypeName).HasMaxLength(120).IsUnicode(false);
        entity.Property(e => e.IsActive).HasDefaultValue(true);

        entity.Property(e => e.CreatedBy).HasMaxLength(450);
        entity.Property(e => e.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("(getdate())");
        entity.Property(e => e.UpdatedBy).HasMaxLength(450);
        entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    }
}

