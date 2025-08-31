using Pascual.TaskManagementSystem.Application.DTOs;

namespace Pascual.TaskManagementSystem.Blazor.Services;

public class TaskApiService
{
    readonly HttpClient _httpClient;
    public TaskApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TaskDTO>> GetAllTasksAsync(int page=1, int pageSize = 10)
    {
        var response = await _httpClient.GetFromJsonAsync<List<TaskDTO>>($"api/tasks?page={page}&pageSize={pageSize}");
        return response ?? new List<TaskDTO>();
    }

    public async Task<TaskDTO?> GetTaskByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<TaskDTO>($"api/tasks/{id}");
    }
    public async Task<TaskDTO> CreateTaskAsync(TaskDTO task)
    {
        var response = await _httpClient.PostAsJsonAsync("api/tasks", task);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TaskDTO>() ?? task;
    }

    public async Task UpdateTaskAsync(TaskDTO task)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/tasks/{task.Id}", task);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteTaskAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/tasks/{id}");
        response.EnsureSuccessStatusCode();
    }
}
