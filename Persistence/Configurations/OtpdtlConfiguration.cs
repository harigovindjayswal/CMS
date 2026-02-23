using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AppEntities;

namespace Persistence.Configurations;

public class OtpdtlConfiguration : IEntityTypeConfiguration<Otpdtl>
{
    public void Configure(EntityTypeBuilder<Otpdtl> entity)
    {
        
entity.HasKey(e => e.Sno).HasName("PK_CommanOTPtbl");
entity.ToTable("OTPDtls");
entity.Property(e => e.Sno).HasColumnName("SNO");
entity.Property(e => e.ApplicationNo).HasMaxLength(50).IsUnicode(false);
entity.Property(e => e.EmailId).HasMaxLength(2500).IsUnicode(false);
entity.Property(e => e.Flag).HasMaxLength(50).IsUnicode(false);
entity.Property(e => e.InstDate).HasColumnType("datetime");
entity.Property(e => e.MobileNumber).HasMaxLength(250).IsUnicode(false);
entity.Property(e => e.Otp).HasMaxLength(250).HasColumnName("OTP");
entity.Property(e => e.UserId).HasMaxLength(50).IsUnicode(false).HasColumnName("UserID");

    }
}