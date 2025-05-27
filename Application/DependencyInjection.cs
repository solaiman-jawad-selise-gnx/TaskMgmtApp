using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddServices();
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddScoped<ITaskMgmtService, TaskMgmtService>()
            .AddScoped<ITeamMgmtService, TeamMgmtService>()
            .AddScoped<IUserMgmtService, UserMgmtService>();
}