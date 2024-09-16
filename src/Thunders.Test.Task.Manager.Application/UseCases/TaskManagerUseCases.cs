using AutoMapper;
using Thunders.Test.Task.Manager.Application.DataTransferObject;
using Thunders.Test.Task.Manager.Application.Interfaces;
using Thunders.Test.Task.Manager.Domain.DataTransferObjects;
using Thunders.Test.Task.Manager.Domain.Repository;

namespace Thunders.Test.Task.Manager.Application.UseCases;
public class TaskManagerUseCases : ITaskManagerUseCases
{
    private readonly ITaskManagerRepository _taskManagerRepository;
    private readonly IMapper _mapper;
    public TaskManagerUseCases(ITaskManagerRepository taskManagerRepository, IMapper mapper)
    {
        _taskManagerRepository = taskManagerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Domain.Entity.Task>> GetTasksByFilterAsync(TaskSearchDto filter)
        => await _taskManagerRepository.GetByFiltersAsync(filter);

    public async Task<int> CreateTaskAsync(TaskCreateDto taskCreateDto)
    {
        var taskSsearchDb = await GetTasksByFilterAsync(_mapper.Map<TaskSearchDto>(taskCreateDto));
        if (taskSsearchDb.Any())
            throw new Exception("Já existe um registro com as mesmas informações");

        var task = _mapper.Map<Domain.Entity.Task>(taskCreateDto);

        task.Validate();

        return await _taskManagerRepository.CreateAsync(task);
    }       

    public async System.Threading.Tasks.Task UpdateTaskAsync(Domain.Entity.Task task)
    {
        if(task.Id == 0)
            throw new ArgumentException("O id deve ser informado", nameof(task));

        var taskSearchDb = await GetTasksByFilterAsync(_mapper.Map<TaskSearchDto>(task));
        if (taskSearchDb.Any(t => t.Id != task.Id))
            throw new Exception("Já existe um registro com as mesmas informações");

        _ = await _taskManagerRepository.GetByFiltersAsync(new TaskSearchDto() { Id = task.Id }) ?? throw new Exception("O registro informado não foi encontrado");

        task.Validate();

        await _taskManagerRepository.UpdateAsync(task);
    }

    public async System.Threading.Tasks.Task DeleteTaskAsync(int idTask)
    {
        if (idTask == 0)
            throw new ArgumentException("O id deve ser informado", nameof(idTask));

        var task = await _taskManagerRepository.GetByFiltersAsync(new TaskSearchDto() { Id = idTask });
        if(!task.Any())
            throw new Exception("O registro informado não foi encontrado");
        
        await _taskManagerRepository.DeleteAsync(idTask);
    }
}