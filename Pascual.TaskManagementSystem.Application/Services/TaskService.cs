
using Pascual.TaskManagementSystem.Application.DTOs;
using Pascual.TaskManagementSystem.Application.Interfaces;
using Pascual.TaskManagementSystem.Domain.Entities;
using Pascual.TaskManagementSystem.Domain.Enums;
using Pascual.TaskManagementSystem.Domain.Exceptions;
using Pascual.TaskManagementSystem.Domain.Interfaces;

namespace Pascual.TaskManagementSystem.Application.Services;
public sealed class TaskService : ITaskService
{
    private readonly IUnitOfWork _unit;

    public TaskService(IUnitOfWork unit)
    {
        _unit = unit;
    }
    public async Task<TaskDTO?> GetTaskByIdAsync(Guid id)
    {
        var task = await _unit.Tasks.GetTaskByIdAsync(id);
        return task is null ? null : MapToDto(task);
    }

    public async Task<IEnumerable<TaskDTO>> GetTasksAsync(int page, int pageSize)
    {
        var tasks = await _unit.Tasks.GetAllTasksAsync(page, pageSize);
        return tasks.Select(MapToDto);
    }

    public async Task<TaskDTO> CreateTaskAsync(TaskDTO task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
        { 
            throw new ValidateException(new Dictionary<string, string[]>
            {
               { nameof(task.Title), new[] { "Title is required." } }
            });
        }

        TaskItem newTask = new()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = (Domain.Enums.TaskStatus)Enum.Parse<Domain.Enums.TaskStatus>(task.Status),
            Priority = Enum.Parse<TaskPriority>(task.Priority),
            CreatedDate = task.CreatedDate,
            DueDate = task.DueDate
        };

        await _unit.Tasks.AddAsync(newTask);
        await _unit.CompleteAsync();
        return MapToDto(newTask);
    }
    public async Task UpdateTaskAsync(TaskDTO task)
    {
       var existingTask =  await _unit.Tasks.GetTaskByIdAsync(task.Id);
        if (existingTask is null)
        {
            throw new ValidateException(new Dictionary<string, string[]>
            {
               { nameof(task.Title), new[] { $"Task with Id {task.Id} not found." } }
            });
        }

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.Status = (Domain.Enums.TaskStatus)Enum.Parse<Domain.Enums.TaskStatus>(task.Status);
        existingTask.Priority = Enum.Parse<TaskPriority>(task.Priority);
        existingTask.DueDate = task.DueDate;

        await _unit.Tasks.UpdateAsync(existingTask);
        await _unit.CompleteAsync();
    }
    public async Task DeleteTaskAsync(Guid id)
    {
       await _unit.Tasks.DeleteAsync(id);
        await _unit.CompleteAsync();
    }

    private TaskDTO MapToDto(TaskItem task)
    {
        return new()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status.ToString(),
            Priority = task.Priority.ToString(),
            CreatedDate = task.CreatedDate,
            DueDate = task.DueDate
        };
    }
       
}
