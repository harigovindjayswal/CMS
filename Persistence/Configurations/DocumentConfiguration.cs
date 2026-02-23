using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AppEntities;

namespace Persistence.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> entity)
    {
        
entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF6F6F67F17C");
entity.Property(e => e.DocumentId).HasColumnName("DocumentID");
entity.Property(e => e.CaseId).HasColumnName("CaseID");
entity.Property(e => e.Category).HasMaxLength(50);
entity.Property(e => e.FilePath).HasMaxLength(255);
entity.Property(e => e.Title).HasMaxLength(150);
entity.Property(e => e.UploadedAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
entity.Property(e => e.UploadedBy).HasMaxLength(450);
entity.Property(e => e.Version).HasDefaultValue(1);

    }
}