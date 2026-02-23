using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AppEntities;

namespace Persistence.Configurations;

public class DocumentDtlConfiguration : IEntityTypeConfiguration<DocumentDtl>
{
    public void Configure(EntityTypeBuilder<DocumentDtl> entity)
    {
        
entity.Property(e => e.Id).HasColumnName("ID");
entity.Property(e => e.ApplicationNo).HasMaxLength(50).IsUnicode(false);
entity.Property(e => e.DigiPath).HasMaxLength(500).IsUnicode(false).HasColumnName("Digi_Path");
entity.Property(e => e.DocId).HasColumnName("Doc_ID");
entity.Property(e => e.DocPath).HasMaxLength(500).IsUnicode(false).HasColumnName("Doc_Path");
entity.Property(e => e.Flag).HasMaxLength(25).IsUnicode(false).HasColumnName("flag");
entity.Property(e => e.IsDigiSign).HasColumnName("Is_DigiSign");
entity.Property(e => e.Uploaddatetime).HasColumnType("datetime").HasColumnName("uploaddatetime");

    }
}