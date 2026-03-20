using Domain.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class LawyerConfiguration : IEntityTypeConfiguration<Lawyer>
{
    public void Configure(EntityTypeBuilder<Lawyer> entity)
    {
        entity.ToTable("Lawyers");

        entity.HasKey(e => e.LawyerId);

        entity.Property(e => e.LawyerId).HasColumnName("LawyerID");
        entity.Property(e => e.UserId).HasMaxLength(450).HasColumnName("UserID");
        entity.Property(e => e.FirstName).HasMaxLength(50).IsUnicode(false);
        entity.Property(e => e.MiddleName).HasMaxLength(50).IsUnicode(false);
        entity.Property(e => e.LastName).HasMaxLength(50).IsUnicode(false);
        entity.Property(e => e.EmailId).HasMaxLength(250).IsUnicode(false);
        entity.Property(e => e.MobileNo).HasMaxLength(20).IsUnicode(false);
        entity.Property(e => e.Address).HasMaxLength(255);
        entity.Property(e => e.ProfileImagePath).HasMaxLength(400);
        entity.Property(e => e.BarLicenseNumber).HasMaxLength(80).IsUnicode(false);
        entity.Property(e => e.CourtDetails).HasMaxLength(400);
        entity.Property(e => e.RegisteredByUserId).HasMaxLength(450);
        entity.Property(e => e.IsActive).HasDefaultValue(true);

        entity.Property(e => e.CreatedBy).HasMaxLength(450);
        entity.Property(e => e.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("(getdate())");
        entity.Property(e => e.UpdatedBy).HasMaxLength(450);
        entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

        entity.HasIndex(e => e.UserId).IsUnique();

        entity.HasOne<StateMst>()
            .WithMany()
            .HasForeignKey(e => e.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<CityMst>()
            .WithMany()
            .HasForeignKey(e => e.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

