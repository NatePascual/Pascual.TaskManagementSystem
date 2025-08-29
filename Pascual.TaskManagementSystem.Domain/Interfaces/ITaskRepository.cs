using Pascual.TaskManagementSystem.Domain.Entities;

namespace Pascual.TaskManagementSystem.Domain.Interfaces;
public interface ITaskRepository
{
    Task<TaskItem?> GetTaskByIdAsync(Guid id);
    Task<IEnumerable<TaskItem>> GetAllTasksAsync(int page, int pageSize);
    Task<TaskItem> AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(Guid id);
}
