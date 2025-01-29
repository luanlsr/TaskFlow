using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Domain.Entities;

public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
{
    public void Configure(EntityTypeBuilder<WorkItem> builder)
    {
        builder.ToTable("tb_workitem");
        builder.HasKey(w => w.Id);
        builder.Property(w => w.Title).IsRequired().HasMaxLength(200);
        builder.Property(w => w.Description).HasMaxLength(1000);
        builder.Property(w => w.Status).HasConversion<string>();
        builder.Property(w => w.DueDate).HasColumnType("datetime");
    }
}
