using AutoMapper;
using HBDStack.Results;
using MediatR;

namespace HBDStack.MediatR.DDD.AutoMappers;

public interface IAutoMapMediator : IMediator
{
    Task<TResponse?> Send<TResponse>(object request, CancellationToken cancellationToken = default) where TResponse : class;
}

internal class AutoMapMediator : IAutoMapMediator
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AutoMapMediator(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<TResponse?> Send<TResponse>(object request, CancellationToken cancellationToken = default)
        where TResponse : class
    {
        var result = await _mediator.Send(request, cancellationToken);
        if (result == null || request.GetType() == typeof(TResponse)) return request as TResponse;
        
        if (result is IResult rs)
        {
            if (!rs.IsSuccess) throw new InvalidDataException("There is no data available for failed IResult.");
            result = ((dynamic)rs).Value;
        }
        
        return _mapper.Map<TResponse>(result);
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        => _mediator.Send(request, cancellationToken);

    public Task<object?> Send(object request, CancellationToken cancellationToken = default)
        => _mediator.Send(request, cancellationToken);

    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request,
        CancellationToken cancellationToken = default)
        => _mediator.CreateStream(request, cancellationToken);

    public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
        => _mediator.CreateStream(request, cancellationToken);

    public Task Publish(object notification, CancellationToken cancellationToken = default)
        => _mediator.Publish(notification, cancellationToken);

    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
        => _mediator.Publish(notification, cancellationToken);
}