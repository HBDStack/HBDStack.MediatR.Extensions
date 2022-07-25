using HBDStack.MediatR.DDD.AutoMappers;
using HBDStack.MediatR.DDD.Behaviors;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class EfCoreSetup
{
    public static IServiceCollection AddMediatEfCoreExtensions<TDbContext>(this IServiceCollection serviceCollection)where TDbContext:DbContext =>
        serviceCollection.AddSingleton(new EfAutoSaveOptions { DbContextType = typeof(TDbContext) })
            .AddScoped<IAutoMapMediator,AutoMapMediator>()
            .AddScoped(typeof(IRequestPostProcessor<,>), typeof(EfAutoSavePostProcessor<,>));
}