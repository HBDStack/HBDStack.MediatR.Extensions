using HBDStack.Results;
using MediatR;

namespace HBDStack.MediatR.DDD;

public interface IRequestFluent : IRequest<IResult>
{
}

public interface IRequestFluent<out TResponse> : IRequest<IResult<TResponse>>
{
}