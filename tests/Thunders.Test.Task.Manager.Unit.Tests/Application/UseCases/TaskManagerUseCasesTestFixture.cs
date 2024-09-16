using Thunders.Test.Task.Manager.Domain.Enum;
using Thunders.Test.Task.Manager.Unit.Tests.Domain.Entity.Task;

namespace Thunders.Test.Task.Manager.Unit.Tests.Application.UseCases;
public class TaskManagerUseCasesTestFixture
{
    public static IEnumerable<Manager.Domain.Entity.Task> GetValidTaskList()
        => new List<Manager.Domain.Entity.Task> { TaskTestsFixture.GetValidInstance() };

    public static Manager.Domain.DataTransferObjects.TaskSearchDto GetValidInstance()
        => new()
        {
            Description = Faker.TextFaker.Sentence(),
            Id = 1,
            Responsible = Faker.NameFaker.FemaleName(),
            Status = EStatus.Todo
        };
}