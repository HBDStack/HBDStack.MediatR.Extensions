using HBD.MediatR.DDD.Tests.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HBD.MediatR.DDD.Tests;

public class Fixture:IDisposable,IAsyncDisposable
{
    public ServiceProvider  ServiceProvider { get;  } 
    
    public Fixture()
    {
        ServiceProvider = new ServiceCollection()
            .AddDbContext<TestDbContext>(b => b.UseInMemoryDatabase(nameof(TestDbContext)))
            .AddMediatR(typeof(Fixture).Assembly)
            .AddMediatEfCoreExtensions<TestDbContext>()
            .BuildServiceProvider();

        var db = ServiceProvider.GetRequiredService<TestDbContext>();
        db.Database.EnsureCreated();
    }

    public void Dispose() => ServiceProvider.Dispose();

    public ValueTask DisposeAsync() => ServiceProvider.DisposeAsync();
}