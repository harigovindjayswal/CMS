using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AppEntities;

namespace Persistence.Configurations;

public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> entity)
    {
        
entity.HasKey(e => e.ItemId).HasName("PK__InvoiceI__727E83EBC97016ED");
entity.Property(e => e.ItemId).HasColumnName("ItemID");
entity.Property(e => e.Description).HasMaxLength(200);
entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
entity.Property(e => e.Quantity).HasDefaultValue(1);
entity.Property(e => e.Total).HasComputedColumnSql("([Quantity]*[UnitPrice])", true).HasColumnType("decimal(21, 2)");
entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

    }
}