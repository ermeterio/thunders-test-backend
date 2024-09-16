using Thunders.Test.Task.Manager.Domain.Enum;
using Thunders.Test.Task.Manager.Domain.SeedWork;

namespace Thunders.Test.Task.Manager.Domain.Entity;
public class Task : SeedWork.Entity, IAggregateRoot
{
    public EStatus Status { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Responsible { get; set; } = string.Empty;

    public Task() { }

    public Task(string responsible, EStatus status, string description)
    {
        Responsible = responsible;
        Status = status;
        Description = description;

        Validate();
    }

    public void Validate()
    {
        if((int)Status < 1) throw new ArgumentException("O Status é obrigatório", nameof(Status));

        if (string.IsNullOrEmpty(Responsible)) throw new ArgumentException("O Responsável é obrigatório", nameof(Responsible));

        if(string.IsNullOrEmpty(Description)) throw new ArgumentException("A descrição da tarefa é obrigatória.", nameof(Description));
    }
}