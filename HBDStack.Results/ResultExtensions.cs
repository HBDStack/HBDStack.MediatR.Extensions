namespace HBDStack.Results;

public static class ResultExtensions
{
    public static IResult WithError(this IResult result, string message)
        => result.WithError(null, message, Array.Empty<string>());

    public static IResult WithError(this IResult result, string message, string[] reasons)
        => result.WithError(null, message, reasons);

    public static IResult WithError(this IResult result, string? code, string message, string[] reasons)
    {
        if (message == null) throw new ArgumentNullException(nameof(message));
        if (reasons == null) throw new ArgumentNullException(nameof(reasons));

        var error = new Error { Code = code, Message = message };
        error.Reasons.AddRange(reasons);
        return result.WithError(error);
    }

    public static IResult WithError(this IResult result, Exception ex)
    {
        var error = new Error { Message = ex.Message };
        if (ex.InnerException != null)
            error.Reasons.Add(ex.InnerException.Message);

        return result.WithError(error);
    }

    public static IResult WithErrors(this IResult result, params IError[] errors)
    {
        foreach (var error in errors)
            result.WithError(error);
        return result;
    }

    public static IResult<TResult> WithError<TResult>(this IResult<TResult> result, string message)
        => result.WithError(null, message, Array.Empty<string>());

    public static IResult<TResult> WithError<TResult>(this IResult<TResult> result, string message, string[] reasons)
        => result.WithError(null, message, reasons);

    public static IResult<TResult> WithError<TResult>(this IResult<TResult> result, string? code, string message, string[] reasons)
    {
        ((IResult)result).WithError(code, message, reasons);
        return result;
    }

    public static IResult<TResult> WithError<TResult>(this IResult<TResult> result, IError error)
    {
        result.WithError(error);
        return result;
    }

    public static IResult<TResult> WithError<TResult>(this IResult<TResult> result, Exception ex)
    {
        ((IResult)result).WithError(ex);
        return result;
    }

    public static IResult<TResult> WithErrors<TResult>(this IResult<TResult> result, params IError[] errors)
    {
        foreach (var error in errors)
            result.WithError(error);
        return result;
    }
}