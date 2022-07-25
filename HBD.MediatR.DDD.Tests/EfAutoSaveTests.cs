using FluentAssertions;
using HBD.MediatR.DDD.Tests.Data;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HBD.MediatR.DDD.Tests;

public class EfAutoSaveTests:IClassFixture<Fixture>
{
    private readonly Fixture _fixture;
    public EfAutoSaveTests(Fixture fixture) => _fixture = fixture;

    [Fact]
    public async Task SaveChange_ShouldBe_Called()
    {
        TestDbContext.Called = false;
        
        var m = _fixture.ServiceProvider.GetRequiredService<IMediator>();
        var rs = await m.Send(new TestRequest { Name = "HBD" });

        rs.Should().NotBeEmpty();
        TestDbContext.Called.Should().BeTrue();
    }
    
    [Fact]
    public async Task SaveChange_ShouldNotBe_Called()
    {
        var m = _fixture.ServiceProvider.GetRequiredService<IMediator>();
        var id = await m.Send(new TestRequest { Name = "HBD" });
        
        TestDbContext.Called = false;
        var rs = await m.Send(new TestQuery { Id = id });

        rs.Should().NotBeNull();
        TestDbContext.Called.Should().BeFalse();
    }
}