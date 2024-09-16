using Microsoft.EntityFrameworkCore;
using Thunders.Test.Task.Manager.Infra.EF.Configurations;

namespace Thunders.Test.Task.Manager.Infra.EF;
public class ThundersDbContext : DbContext
{
    public DbSet<Domain.Entity.Task> Tasks
       => Set<Domain.Entity.Task>();

    public ThundersDbContext(DbContextOptions<ThundersDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
    }
}