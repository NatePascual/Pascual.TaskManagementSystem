
using System;

namespace Pascual.TaskManagementSystem.Application.DTOs;

public class TaskDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = "ToDo";
    public string Priority { get; set; } = "Low";
    public DateTime CreatedDate { get; set; }
    public DateTime? DueDate { get; set; }
}
