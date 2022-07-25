namespace HBDStack.Results;

public interface IError
{
    public string? Code { get; init; }
    public string Message { get; init; }
    public List<string>Reasons { get; set; }
    public IDictionary<string, object> MetaData { get; }
}

internal class Error : IError
{
    public string? Code { get; init; }
    public string Message { get; init; } = default!;
    public List<string> Reasons { get; set; } = new();
    
    public IDictionary<string, object> MetaData { get; } = new Dictionary<string, object>();
}