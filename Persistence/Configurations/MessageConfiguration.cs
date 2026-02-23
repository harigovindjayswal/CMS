using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AppEntities;

namespace Persistence.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> entity)
    {
        
entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C037C47261C2F");
entity.Property(e => e.MessageId).HasColumnName("MessageID");
entity.Property(e => e.CaseId).HasColumnName("CaseID");
entity.Property(e => e.FromUserId).HasMaxLength(450).HasColumnName("FromUserID");
entity.Property(e => e.SentAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
entity.Property(e => e.ToUserId).HasMaxLength(450).HasColumnName("ToUserID");

    }
}