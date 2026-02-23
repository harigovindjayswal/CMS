using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AppEntities;

namespace Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> entity)
    {
        
entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__D796AAD5ED8AE495");
entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
entity.Property(e => e.CaseId).HasColumnName("CaseID");
entity.Property(e => e.ClientId).HasColumnName("ClientID");
entity.Property(e => e.DueDate).HasColumnType("datetime");
entity.Property(e => e.IssuedAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
entity.Property(e => e.Status).HasMaxLength(20).HasDefaultValue("Pending");

    }
}