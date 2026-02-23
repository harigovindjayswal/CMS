using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AppEntities;

namespace Persistence.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> entity)
    {
        
entity.HasKey(e => e.EventId).HasName("PK__Events__7944C87055661586");
entity.Property(e => e.EventId).HasColumnName("EventID");
entity.Property(e => e.CaseId).HasColumnName("CaseID");
entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
entity.Property(e => e.CreatedBy).HasMaxLength(450);
entity.Property(e => e.EventDate).HasColumnType("datetime");
entity.Property(e => e.EventType).HasMaxLength(20);
entity.Property(e => e.Reminder).HasDefaultValue(false);
entity.Property(e => e.Title).HasMaxLength(150);

    }
}