using HBDStack.Results;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HBDStack.MediatR.DDD.Behaviors;

internal sealed class EfAutoSavePostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly EfAutoSaveOptions _options;
    private readonly IServiceProvider _serviceProvider;

    public EfAutoSavePostProcessor(EfAutoSaveOptions options, IServiceProvider serviceProvider)
    {
        _options = options;
        _serviceProvider = serviceProvider;
    }
    
    public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        if (response is null || request is IQuery<TResponse>) return;
        if (response is IResult { IsSuccess: false }) return;
        
        //Call DbContext Save Changes
        var dbContext = _serviceProvider.GetService(_options.DbContextType) as DbContext ??
                        _serviceProvider.GetService<DbContext>();

        if (dbContext != null)
            await dbContext.SaveChangesAsync(cancellationToken);
    }
}