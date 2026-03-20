using Domain.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CityMstConfiguration : IEntityTypeConfiguration<CityMst>
{
    public void Configure(EntityTypeBuilder<CityMst> entity)
    {
        entity.ToTable("Cities");

        entity.HasKey(e => e.CityId);

        entity.Property(e => e.CityId).HasColumnName("CityID");
        entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
        entity.Property(e => e.Name).HasMaxLength(100).IsUnicode(false);
        entity.Property(e => e.IsActive).HasDefaultValue(true);

        entity.Property(e => e.CreatedBy).HasMaxLength(450);
        entity.Property(e => e.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("(getdate())");
        entity.Property(e => e.UpdatedBy).HasMaxLength(450);
        entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

        entity.HasOne<DistrictMst>()
            .WithMany()
            .HasForeignKey(e => e.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

