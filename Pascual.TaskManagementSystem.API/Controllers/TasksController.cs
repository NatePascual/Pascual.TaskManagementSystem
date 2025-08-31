
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pascual.TaskManagementSystem.Application.DTOs;
using Pascual.TaskManagementSystem.Application.Interfaces;

namespace Pascual.TaskManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController:ControllerBase
{
    private readonly ITaskService _service;
    public TasksController(ITaskService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTasks(int page=1, int pageSize=10)
    {
        var tasks = await _service.GetTasksAsync(page, pageSize);
        return Ok(tasks);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        var task = await _service.GetTaskByIdAsync(id);
        if (task is null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTask([FromBody] TaskDTO task)
    {
        var createdTask = await _service.CreateTaskAsync(task);
        return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskDTO task)
    {
        if (id != task.Id)
        {
            return BadRequest("ID mismatch");
        }
        await _service.UpdateTaskAsync(task);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        await _service.DeleteTaskAsync(id);
        return NoContent();
    }
}
