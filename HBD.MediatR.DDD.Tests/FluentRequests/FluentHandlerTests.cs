using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HBD.MediatR.DDD.Tests.FluentRequests;

public class FluentHandlerTests : IClassFixture<Fixture>
{
    private readonly Fixture _fixture;
    public FluentHandlerTests(Fixture fixture) => _fixture = fixture;
    
    [Fact]
    public async Task ShouldBe_Failed()
    {
        var m = _fixture.ServiceProvider.GetRequiredService<IMediator>();
        var rs = await m.Send(new FluentRequest());

        rs.IsSuccess.Should().BeFalse();
        rs.Value.Should().BeNull();
    }
    
    [Fact]
    public async Task ShouldBe_Success()
    {
        var m = _fixture.ServiceProvider.GetRequiredService<IMediator>();
        var rs = await m.Send(new FluentRequest{Name = "HBD"});

        rs.IsSuccess.Should().BeTrue();
        rs.Value.Should().NotBeNull();
    }
}