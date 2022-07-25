using MediatR;

namespace HBD.MediatR.DDD.Tests.Data;

public class TestRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
}

internal sealed class TestRequestHandler : IRequestHandler<TestRequest, Guid>
{
    private readonly TestDbContext _dbContext;
    public TestRequestHandler(TestDbContext dbContext) => _dbContext = dbContext;

    public async Task<Guid> Handle(TestRequest request, CancellationToken cancellationToken)
    {
        var entity = new TestEntity { Name = request.Name };
        await _dbContext.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}