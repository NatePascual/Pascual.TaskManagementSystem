
namespace Pascual.TaskManagementSystem.Domain.Interfaces;

public interface IUnitOfWork
{
    ITaskRepository Tasks { get; }
    Task<int> CompleteAsync();
}
