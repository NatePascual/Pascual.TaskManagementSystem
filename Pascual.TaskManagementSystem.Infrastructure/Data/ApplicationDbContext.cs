using Microsoft.EntityFrameworkCore;
using Pascual.TaskManagementSystem.Domain.Entities;

namespace Pascual.TaskManagementSystem.Infrastructure.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
}
