using Application.Interfaces;
using Application.Services;
using Application.ValidationPipeline;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddServices();
        services.AddApplicationCommandServices();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddScoped<ITaskMgmtService, TaskMgmtService>()
            .AddScoped<ITeamMgmtService, TeamMgmtService>()
            .AddScoped<IUserMgmtService, UserMgmtService>();
    
    private static IServiceCollection AddApplicationCommandServices(this IServiceCollection services) =>
        services
            .AddMediatR(cfg 
                => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
            .AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
}