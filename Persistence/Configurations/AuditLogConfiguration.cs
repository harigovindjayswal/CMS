using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AppEntities;

namespace Persistence.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> entity)
    {
        entity.HasKey(e => e.LogId).HasName("PK__AuditLog__5E5499A813DDEF67");

        entity.Property(e => e.LogId).HasColumnName("LogID");
        entity.Property(e => e.Action).HasMaxLength(200);
        entity.Property(e => e.Ipaddress).HasMaxLength(50).HasColumnName("IPAddress");
        entity.Property(e => e.RecordId).HasColumnName("RecordID");
        entity.Property(e => e.TableName).HasMaxLength(100);
        entity.Property(e => e.Timestamp)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.UserId).HasMaxLength(450).HasColumnName("UserID");
    }
}