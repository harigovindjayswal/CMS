using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AppEntities;

namespace Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> entity)
    {
        entity.HasKey(e => e.ClientId).HasName("PK__Clients__E67E1A042F9D710B");

        entity.Property(e => e.ClientId).HasColumnName("ClientID");
        entity.Property(e => e.Address).HasMaxLength(255);
        entity.Property(e => e.City).HasMaxLength(50).IsUnicode(false);
        entity.Property(e => e.CreatedBy).HasMaxLength(450);
        entity.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.EmailId).HasMaxLength(250).IsUnicode(false);
        entity.Property(e => e.FirstName).HasMaxLength(50).IsUnicode(false);
        entity.Property(e => e.IsActive).HasDefaultValue(true);
        entity.Property(e => e.LastName).HasMaxLength(50).IsUnicode(false);
        entity.Property(e => e.MiddleName).HasMaxLength(50).IsUnicode(false);
        entity.Property(e => e.MobileNo).HasMaxLength(250).IsUnicode(false);
        entity.Property(e => e.PinCode).HasMaxLength(6).IsUnicode(false);
        entity.Property(e => e.UpdatedBy).HasMaxLength(450);
        entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        entity.Property(e => e.UserId).HasMaxLength(450).HasColumnName("UserID");
    }
}