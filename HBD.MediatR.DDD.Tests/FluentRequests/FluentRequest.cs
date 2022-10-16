using HBDStack.MediatR.DDD;
using HBDStack.Results;

namespace HBD.MediatR.DDD.Tests.FluentRequests;

public class FluentRequest:IRequestFluent<FluentResponse>
{
    public string Name { get; set; } = default!;
}

public record FluentResponse
{
    
}

internal class FluentRequestHandler : IRequestFluentHandler<FluentRequest, FluentResponse>
{
    public async Task<IResult<FluentResponse>> Handle(FluentRequest request, CancellationToken cancellationToken)
        => string.IsNullOrEmpty(request.Name) ? Result.Fails<FluentResponse>("The name is invalid.") : Result.Ok(new FluentResponse());
}