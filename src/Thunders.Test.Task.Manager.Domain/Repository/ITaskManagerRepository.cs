
namespace Thunders.Test.Task.Manager.Domain.Repository;
public interface ITaskManagerRepository : IRepository<Entity.Task>
{
    Task<IEnumerable<Entity.Task>> GetByFiltersAsync(DataTransferObjects.TaskSearchDto filter);
}