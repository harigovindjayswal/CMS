using Domain.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class DistrictMstConfiguration : IEntityTypeConfiguration<DistrictMst>
{
    public void Configure(EntityTypeBuilder<DistrictMst> entity)
    {
        entity.ToTable("Districts");

        entity.HasKey(e => e.DistrictId);

        entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
        entity.Property(e => e.StateId).HasColumnName("StateID");
        entity.Property(e => e.Name).HasMaxLength(100).IsUnicode(false);
        entity.Property(e => e.IsActive).HasDefaultValue(true);

        entity.Property(e => e.CreatedBy).HasMaxLength(450);
        entity.Property(e => e.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("(getdate())");
        entity.Property(e => e.UpdatedBy).HasMaxLength(450);
        entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

        entity.HasOne<StateMst>()
            .WithMany()
            .HasForeignKey(e => e.StateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

