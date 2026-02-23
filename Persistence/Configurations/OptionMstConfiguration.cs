using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AppEntities;

namespace Persistence.Configurations;

public class OptionMstConfiguration : IEntityTypeConfiguration<OptionMst>
{
    public void Configure(EntityTypeBuilder<OptionMst> entity)
    {
        
entity.HasKey(e => e.SerialNo);
entity.ToTable("OptionMst");
entity.Property(e => e.IsActive).HasDefaultValue(true);
entity.Property(e => e.OptionCode).HasMaxLength(6).IsUnicode(false);
entity.Property(e => e.OptionDesc).HasMaxLength(100);
entity.Property(e => e.OptionDescHindi).HasMaxLength(200);
entity.Property(e => e.OptionName).HasMaxLength(50);

    }
}