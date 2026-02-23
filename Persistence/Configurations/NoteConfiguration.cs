using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AppEntities;

namespace Persistence.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> entity)
    {
        
entity.HasKey(e => e.NoteId).HasName("PK__Notes__EACE357FD65BA5B4");
entity.Property(e => e.NoteId).HasColumnName("NoteID");
entity.Property(e => e.CaseId).HasColumnName("CaseID");
entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
entity.Property(e => e.IsPrivate).HasDefaultValue(true);
entity.Property(e => e.UserId).HasMaxLength(450).HasColumnName("UserID");

    }
}