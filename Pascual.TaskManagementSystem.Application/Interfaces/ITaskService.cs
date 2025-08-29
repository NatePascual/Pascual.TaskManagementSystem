using Pascual.TaskManagementSystem.Application.DTOs;

namespace Pascual.TaskManagementSystem.Application.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDTO>> GetTasksAsync(int page, int pageSize);
    Task<TaskDTO?> GetTaskByIdAsync(Guid id);
    Task<TaskDTO> CreateTaskAsync(TaskDTO task);
    Task UpdateTaskAsync(TaskDTO task);
    Task DeleteTaskAsync(Guid id);
}
