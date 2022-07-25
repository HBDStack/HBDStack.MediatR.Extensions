using Microsoft.EntityFrameworkCore;

namespace HBD.MediatR.DDD.Tests.Data;

public class TestDbContext:DbContext
{
    public static bool Called { get; set; } = false;
    
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
    }

    public virtual DbSet<TestEntity> Entities { get; set; } = default!;
    

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        Called = true;
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}