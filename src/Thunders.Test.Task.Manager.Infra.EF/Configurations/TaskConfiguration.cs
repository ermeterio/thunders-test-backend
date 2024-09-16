using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thunders.Test.Task.Manager.Infra.EF.Configurations;
public class TaskConfiguration : IEntityTypeConfiguration<Domain.Entity.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Task> builder)
    {
        builder.HasKey(consultant => consultant.Id);
        builder.Property(consultant => consultant.Description).HasMaxLength(100);
        builder.Property(consultant => consultant.Status);
        builder.Property(consultant => consultant.Responsible).HasMaxLength(60);
    }
}