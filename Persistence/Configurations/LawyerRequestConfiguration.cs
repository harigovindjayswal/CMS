using Domain.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class LawyerRequestConfiguration : IEntityTypeConfiguration<LawyerRequest>
{
    public void Configure(EntityTypeBuilder<LawyerRequest> entity)
    {
        entity.ToTable("LawyerRequests");

        entity.HasKey(e => e.LawyerRequestId);

        entity.Property(e => e.LawyerRequestId).HasColumnName("LawyerRequestID");
        entity.Property(e => e.ClientId).HasColumnName("ClientID");
        entity.Property(e => e.LawyerId).HasColumnName("LawyerID");
        entity.Property(e => e.CaseTypeId).HasColumnName("CaseTypeID");
        entity.Property(e => e.StateId).HasColumnName("StateID");
        entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
        entity.Property(e => e.CityId).HasColumnName("CityID");
        entity.Property(e => e.CaseDescription).HasMaxLength(2000);
        entity.Property(e => e.LawyerRemark).HasMaxLength(2000);
        entity.Property(e => e.Status).HasConversion<int>();
        entity.Property(e => e.IsActive).HasDefaultValue(true);

        entity.Property(e => e.CreatedBy).HasMaxLength(450);
        entity.Property(e => e.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("(getdate())");
        entity.Property(e => e.UpdatedBy).HasMaxLength(450);
        entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

        entity.HasOne<Client>()
            .WithMany()
            .HasForeignKey(e => e.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<Lawyer>()
            .WithMany()
            .HasForeignKey(e => e.LawyerId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<CaseTypeMst>()
            .WithMany()
            .HasForeignKey(e => e.CaseTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<StateMst>()
            .WithMany()
            .HasForeignKey(e => e.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<DistrictMst>()
            .WithMany()
            .HasForeignKey(e => e.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<CityMst>()
            .WithMany()
            .HasForeignKey(e => e.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

