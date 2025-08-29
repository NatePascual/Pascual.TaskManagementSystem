
using Pascual.TaskManagementSystem.Domain.Interfaces;
using Pascual.TaskManagementSystem.Infrastructure.Data;
using Pascual.TaskManagementSystem.Infrastructure.Repositories;

namespace Pascual.TaskManagementSystem.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private ITaskRepository? _taskRepository;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public ITaskRepository Tasks => _taskRepository ??= new TaskRepository(_context);

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
