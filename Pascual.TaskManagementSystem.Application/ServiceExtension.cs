using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pascual.TaskManagementSystem.Infrastructure;
namespace Pascual.TaskManagementSystem.Application;

public static class ServiceExtension
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services,IConfiguration configuration )
    {
        services.AddScoped<Interfaces.ITaskService, Services.TaskService>();
        services.ConfigureInfrastructureServices(configuration);
        return services;
    }
}
