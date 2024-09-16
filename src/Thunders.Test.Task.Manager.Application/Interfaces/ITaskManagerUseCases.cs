using Thunders.Test.Task.Manager.Application.DataTransferObject;

namespace Thunders.Test.Task.Manager.Application.Interfaces;
public interface ITaskManagerUseCases
{
    Task<IEnumerable<Domain.Entity.Task>> GetTasksByFilterAsync(Domain.DataTransferObjects.TaskSearchDto task);
    Task<int> CreateTaskAsync(TaskCreateDto task);
    System.Threading.Tasks.Task UpdateTaskAsync(Domain.Entity.Task task);
    System.Threading.Tasks.Task DeleteTaskAsync(int idTask);
}