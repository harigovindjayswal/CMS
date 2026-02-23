using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AppEntities;

namespace Persistence.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<CaseTask>
{
    public void Configure(EntityTypeBuilder<CaseTask> entity)
    {
        
entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949D14186CB16");
entity.Property(e => e.TaskId).HasColumnName("TaskID");
entity.Property(e => e.AssignedTo).HasMaxLength(450);
entity.Property(e => e.CaseId).HasColumnName("CaseID");
entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
entity.Property(e => e.DueDate).HasColumnType("datetime");
entity.Property(e => e.Status).HasMaxLength(20).HasDefaultValue("To-Do");
entity.Property(e => e.Title).HasMaxLength(150);

    }
}