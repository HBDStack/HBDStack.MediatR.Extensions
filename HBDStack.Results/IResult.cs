using System.Diagnostics.CodeAnalysis;

namespace HBDStack.Results;

public interface IResult
{
    public IReadOnlyList<IError> Errors { get; }
    bool IsSuccess => !Errors.Any();

    IResult WithError(IError error);
}

partial class Result : IResult
{
    internal Result()
    {
    }

    public IReadOnlyList<IError> Errors => _errors;
    private readonly List<IError> _errors = new();

    public virtual IResult WithError(IError error)
    {
        _errors.Add(error);
        return this;
    }
}

public interface IResult<out TResult> : IResult
{
    public TResult? Value { get; }
}

internal sealed class Result<TResult> : Result, IResult<TResult>
{
    public TResult? Value { get; }
    
    internal Result([DisallowNull] TResult value) => Value = value ?? throw new ArgumentNullException(nameof(value));

    internal Result()
    {
    }

    private void CheckResult()
    {
        if (Value is not null) throw new InvalidOperationException("The Result is already provided.");
    }

    public override IResult WithError(IError error)
    {
        CheckResult();
        return base.WithError(error);
    }
}