using Microsoft.AspNetCore.Mvc;
using Thunders.Test.Task.Manager.Application.DataTransferObject;
using Thunders.Test.Task.Manager.Application.Interfaces;

namespace Thunders.Test.Task.Manager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskManagerController : Controller
{
    private readonly ITaskManagerUseCases _taskManageruserCases;
    public TaskManagerController(ITaskManagerUseCases taskManagerUseCases)
    {
        _taskManageruserCases = taskManagerUseCases;
    }

    [HttpPost(nameof(GetTasksByFilterAsync))]
    public async Task<IActionResult> GetTasksByFilterAsync([FromBody] Domain.DataTransferObjects.TaskSearchDto task)
    {
        try
        {
            var tasks = await _taskManageruserCases.GetTasksByFilterAsync(task);
            if (tasks.Any())
                return Ok(tasks);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost(nameof(CreateTaskAsync))]
    public async Task<IActionResult> CreateTaskAsync([FromBody] TaskCreateDto task)
    {
        try
        {
            return Ok(await _taskManageruserCases.CreateTaskAsync(task));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut(nameof(UpdateTaskAsync))]
    public async Task<IActionResult> UpdateTaskAsync([FromBody] Domain.Entity.Task task)
    {
        try
        {
            await _taskManageruserCases.UpdateTaskAsync(task);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete(nameof(DeleteTaskAsync))]
    public async Task<IActionResult> DeleteTaskAsync(int idTask)
    {
        try
        {
            await _taskManageruserCases.DeleteTaskAsync(idTask);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}