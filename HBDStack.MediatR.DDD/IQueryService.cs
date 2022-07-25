using MediatR;

namespace HBDStack.MediatR.DDD;

/// <summary>
/// THe interface for Query purposes in DDD design The EfCore auto save will ignore the Save change for this query requests.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IQuery<out TResponse> : IRequest<TResponse>
{
}