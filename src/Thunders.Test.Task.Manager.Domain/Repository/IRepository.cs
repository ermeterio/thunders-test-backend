using Thunders.Test.Task.Manager.Domain.SeedWork;

namespace Thunders.Test.Task.Manager.Domain.Repository;
public interface IRepository<T> where T : IAggregateRoot
{
    Task<int> CreateAsync(T entity);
    System.Threading.Tasks.Task UpdateAsync(T entity);
    System.Threading.Tasks.Task DeleteAsync(int id);
}