using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Pascual.TaskManagementSystem.Application;
using Pascual.TaskManagementSystem.Infrastructure.Data;

namespace Pascual.TaskManagementSystem.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.ConfigureApplicationServices(builder.Configuration);
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
                           options.UseInMemoryDatabase("TaskDb")); 


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManagementSystem.API v1");
                //c.RoutePrefix = string.Empty;
            });
        }
        app.UseMiddleware<Middleware.ErrorHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
