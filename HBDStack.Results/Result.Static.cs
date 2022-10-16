using System.Diagnostics.CodeAnalysis;

namespace HBDStack.Results;

public partial class Result
{
    public static IResult Ok() => new Result();

    public static IResult Fails(string message)
        => Fails(null, message, Array.Empty<string>());

    public static IResult Fails(string message, string[] reasons)
        => Fails(null, message, reasons);

    public static IResult Fails(string? code, string message, string[] reasons)
        => Create().WithError(code, message, reasons);

    public static IResult Fails(IError error)
        => Create().WithError(error);

    private static IResult Create() => new Result();

    public static IResult OkIf(Action action)
    {
        try
        {
            action();
            return Ok();
        }
        catch (Exception ex)
        {
            return Create().WithError(ex);
        }
    }

    public static async Task<IResult> OkIf(Func<Task> action)
    {
        try
        {
            await action();
            return Ok();
        }
        catch (Exception ex)
        {
            return Create().WithError(ex);
        }
    }

    // public static async Task<IResult> OkIf(Func<Task<IResult>> action)
    // {
    //     try
    //     {
    //         var rs = await action();
    //         return rs;
    //     }
    //     catch (Exception ex)
    //     {
    //         return Fails().WithError(ex);
    //     }
    // }

    public static IResult<TResult> Ok<TResult>([DisallowNull] TResult value) => new Result<TResult>(value);

    public static IResult<TResult> Fails<TResult>(string message)
        => Fails<TResult>(null, message, Array.Empty<string>());

    public static IResult<TResult> Fails<TResult>(string message, string[] reasons)
        => Fails<TResult>(null, message, reasons);

    public static IResult<TResult> Fails<TResult>(string? code, string message, string[] reasons)
    {
        var error = new Error { Code = code, Message = message };
        error.Reasons.AddRange(reasons);
        return Fails<TResult>(error);
    }

    public static IResult<TResult> Fails<TResult>(IError error)
        => Create<TResult>().WithError<TResult>(error);

    private static IResult<TResult> Create<TResult>() => new Result<TResult>();

    public static IResult<TResult> OkIf<TResult>(Func<TResult> action)
    {
        try
        {
            var rs = action();
            return Ok(rs!);
        }
        catch (Exception ex)
        {
            return Create<TResult>().WithError(ex);
        }
    }

    public static async Task<IResult<TResult>> OkIf<TResult>(Func<Task<TResult>> action)
    {
        try
        {
            var rs = await action();
            return Ok(rs!);
        }
        catch (Exception ex)
        {
            return Create<TResult>().WithError(ex);
        }
    }
    
    // public static async Task<IResult<TResult>> OkIf<TResult>(Func<Task<IResult<TResult>>> action)
    // {
    //     try
    //     {
    //         var rs = await action();
    //         return rs;
    //     }
    //     catch (Exception ex)
    //     {
    //         return Fails<TResult>().WithError(ex);
    //     }
    // }
}