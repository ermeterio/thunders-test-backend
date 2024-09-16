using Thunders.Test.Task.Manager.Domain.Enum;

namespace Thunders.Test.Task.Manager.Application.DataTransferObject;
public class TaskCreateDto
{
    public EStatus? Status { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Responsible { get; set; } = string.Empty;
}