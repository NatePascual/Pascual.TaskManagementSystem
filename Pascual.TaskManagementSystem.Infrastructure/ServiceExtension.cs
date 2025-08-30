
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pascual.TaskManagementSystem.Domain.Interfaces;

namespace Pascual.TaskManagementSystem.Infrastructure;

public static class ServiceExtension
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITaskRepository, Repositories.TaskRepository>();
        services.AddDbContext<Data.ApplicationDbContext>();
  
        return services;
    }
}
