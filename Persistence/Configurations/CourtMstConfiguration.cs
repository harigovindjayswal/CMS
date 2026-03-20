using Domain.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CourtMstConfiguration : IEntityTypeConfiguration<CourtMst>
{
    public void Configure(EntityTypeBuilder<CourtMst> entity)
    {
        entity.ToTable("Courts");

        entity.HasKey(e => e.CourtId);

        entity.Property(e => e.CourtId).HasColumnName("CourtID");
        entity.Property(e => e.CourtTypeId).HasColumnName("CourtTypeID");
        entity.Property(e => e.CityId).HasColumnName("CityID");
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode(false);
        entity.Property(e => e.IsActive).HasDefaultValue(true);

        entity.Property(e => e.CreatedBy).HasMaxLength(450);
        entity.Property(e => e.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("(getdate())");
        entity.Property(e => e.UpdatedBy).HasMaxLength(450);
        entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

        entity.HasOne<CourtTypeMst>()
            .WithMany()
            .HasForeignKey(e => e.CourtTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<CityMst>()
            .WithMany()
            .HasForeignKey(e => e.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

