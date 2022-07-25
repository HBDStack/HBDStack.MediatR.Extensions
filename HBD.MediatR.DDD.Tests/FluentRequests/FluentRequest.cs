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
    {
        if (string.IsNullOrEmpty(request.Name))
            return Result.Fails<FluentResponse>("The name is invalid.");
        return Result.Ok(new FluentResponse());
    }
}