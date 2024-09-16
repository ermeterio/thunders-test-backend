using Microsoft.EntityFrameworkCore;
using Thunders.Test.Task.Manager.Domain.Repository;

namespace Thunders.Test.Task.Manager.Infra.EF.Repositories;
public class TaskManagerRepository : ITaskManagerRepository
{
    private readonly ThundersDbContext _dbContext;
    public TaskManagerRepository(ThundersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CreateAsync(Domain.Entity.Task task)
    {
        try
        {

            await _dbContext.Tasks.AddAsync(task);

            await _dbContext.SaveChangesAsync();
        }
        catch
        {
            throw;
        }

        return task.Id;
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        try
        {
            var data = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (data is null)
                throw new KeyNotFoundException("A chave não foi encontrada");

            if (data is { Id: > 0 })
                _dbContext.Tasks.Remove(data);

            await _dbContext.SaveChangesAsync();
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Domain.Entity.Task>> GetByFiltersAsync(Domain.DataTransferObjects.TaskSearchDto filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        IQueryable<Domain.Entity.Task> data = _dbContext.Tasks.AsQueryable();

        if (filter is { Status: > 0})
            data = data.Where(d => d.Status.Equals(filter.Status));

        if (!string.IsNullOrWhiteSpace(filter.Responsible))
            data = data.Where(d => d.Responsible.Equals(filter.Responsible));

        if (!string.IsNullOrWhiteSpace(filter.Description))
            data = data.Where(d => d.Description.Contains(filter.Description));

        return await System.Threading.Tasks.Task.FromResult(data);
    }

    public async System.Threading.Tasks.Task UpdateAsync(Domain.Entity.Task task)
    {
        try
        {
            _dbContext.Tasks.Update(task);

            await _dbContext.SaveChangesAsync();
        }
        catch
        {
            throw;
        }
    }
}