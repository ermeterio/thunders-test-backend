using Thunders.Test.Task.Manager.Domain.Enum;

namespace Thunders.Test.Task.Manager.Domain.DataTransferObjects;
public class TaskSearchDto
{
    public int? Id { get; set; }
    public EStatus? Status { get; set; }
    public string? Description { get; set; }
    public string? Responsible { get; set; }
}