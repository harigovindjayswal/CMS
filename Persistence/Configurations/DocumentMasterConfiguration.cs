using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AppEntities;

namespace Persistence.Configurations;

public class DocumentMasterConfiguration : IEntityTypeConfiguration<DocumentMaster>
{
    public void Configure(EntityTypeBuilder<DocumentMaster> entity)
    {
        
entity.ToTable("Document_Master");
entity.Property(e => e.Id).HasColumnName("ID");
entity.Property(e => e.DigiDocName).HasMaxLength(100).IsUnicode(false).HasColumnName("DigiDoc_Name");
entity.Property(e => e.DigiMandatory).HasMaxLength(1).IsUnicode(false).IsFixedLength().HasColumnName("Digi_Mandatory");
entity.Property(e => e.DocDesc).HasMaxLength(150).IsUnicode(false).HasColumnName("Doc_Desc");
entity.Property(e => e.DocId).HasColumnName("Doc_ID");
entity.Property(e => e.DocName).HasMaxLength(1000).HasColumnName("Doc_Name");
entity.Property(e => e.FileDirectory).HasMaxLength(150).IsUnicode(false).HasColumnName("File_Directory");
entity.Property(e => e.FileExtension).HasMaxLength(50).IsUnicode(false).HasColumnName("File_Extension");
entity.Property(e => e.FileSizeBytes).HasMaxLength(50).IsUnicode(false).HasColumnName("File_SizeBytes");
entity.Property(e => e.Flag).HasMaxLength(10).IsUnicode(false);
entity.Property(e => e.Mandatory).HasMaxLength(1).IsUnicode(false).IsFixedLength();

    }
}