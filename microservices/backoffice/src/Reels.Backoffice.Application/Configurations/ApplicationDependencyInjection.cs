using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Reels.Backoffice.CrossCutting.Behaviors;
using Reels.Backoffice.CrossCutting.GlobalException;

namespace Reels.Backoffice.Application.Configurations;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(ApplicationDependencyInjection).Assembly);

            config.AddOpenBehavior(typeof(ValidatorPipelineBehavior<,>));

            config.AddOpenBehavior(typeof(GlobalExceptionPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(ApplicationDependencyInjection).Assembly);

        return services;
    }
}