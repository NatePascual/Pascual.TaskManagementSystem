using Microsoft.EntityFrameworkCore;
using Pascual.TaskManagementSystem.Domain.Entities;
using Pascual.TaskManagementSystem.Domain.Interfaces;
using Pascual.TaskManagementSystem.Infrastructure.Data;
using System.Text;

namespace Pascual.TaskManagementSystem.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;
    public TaskRepository(ApplicationDbContext context)
    {
       _context = context;
    }
    public async Task<TaskItem> AddAsync(TaskItem task)
    {
        await _context.Tasks.AddAsync(task);
        return task;
    }

    public async Task DeleteAsync(Guid id)
    {
        var existingTask = await GetTaskByIdAsync(id);
        if (existingTask != null)
        {
            _context.Tasks.Remove(existingTask);
        }
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(TaskItem task)
    {
        _context.Tasks.Update(task);
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasksAsync(int page, int pageSize)
    {
        var tasks= await _context.Tasks.OrderByDescending(t=>t.CreatedDate)
                        .Skip((page -1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return tasks;
    }

    public async Task<TaskItem?> GetTaskByIdAsync(Guid id)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);  
        return task;
    }
}
