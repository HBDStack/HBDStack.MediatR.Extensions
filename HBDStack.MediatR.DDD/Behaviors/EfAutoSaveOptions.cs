namespace HBDStack.MediatR.DDD.Behaviors;

internal sealed class EfAutoSaveOptions
{
    public Type DbContextType { get; set; } = default!;
}