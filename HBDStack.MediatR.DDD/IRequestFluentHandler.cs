using HBDStack.Results;
using MediatR;

namespace HBDStack.MediatR.DDD;

public interface IRequestFluentHandler<in TRequest> : IRequestHandler<TRequest, IResult> where TRequest : IRequest<IResult>
{
}

public interface IRequestFluentHandler<in TRequest, TResponse> : IRequestHandler<TRequest, IResult<TResponse>> where TRequest : IRequest<IResult<TResponse>>
{
}