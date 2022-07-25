using HBDStack.MediatR.DDD;
using MediatR;

namespace HBD.MediatR.DDD.Tests.Data;

public class TestQuery: IQuery<TestQueryResult>
{
    public Guid Id { get; set; }
}

public class TestQueryResult
{
    public Guid Id { get; set; }= default!;
    public string Name { get; set; } = default!;
}

internal class TestQueryHandler : IRequestHandler<TestQuery, TestQueryResult?>
{
    private readonly TestDbContext _dbContext;

    public TestQueryHandler(TestDbContext dbContext) => _dbContext = dbContext;
    
    public async Task<TestQueryResult?> Handle(TestQuery request, CancellationToken cancellationToken)
    {
        var item =await _dbContext.FindAsync<TestEntity>(request.Id);
        
        return item != null ? new TestQueryResult
        {
            Id = item.Id,
            Name = item.Name
        } : null;
    }
}