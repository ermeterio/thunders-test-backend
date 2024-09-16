using Thunders.Test.Task.Manager.Application.DataTransferObject;
using Thunders.Test.Task.Manager.Domain.DataTransferObjects;
using Thunders.Test.Task.Manager.Domain.Enum;

namespace Thunders.Test.Task.Manager.Unit.Tests.Domain.Entity.Task;
public class TaskTestsFixture
{
    public static Manager.Domain.Entity.Task GetValidInstance()
        => new()
        {
            Description = Faker.TextFaker.Sentence(),
            Id = 1,
            Responsible = Faker.NameFaker.FemaleName(),
            Status = EStatus.Todo
        };

    public static TaskCreateDto GetValidInstanceCreateDto()
        => new()
        {
            Description = Faker.TextFaker.Sentence(),
            Responsible = Faker.NameFaker.FemaleName(),
            Status = EStatus.Todo
        };

    public static TaskSearchDto GetValidInstanceSearchDto()
        => new()
        {
            Description = Faker.TextFaker.Sentence(),
            Responsible = Faker.NameFaker.FemaleName(),
            Status = EStatus.Todo
        };

    public static TaskCreateDto GetInvalidInstanceCreateDto()
        => new()
        {
            Description = string.Empty,
            Responsible = Faker.NameFaker.FemaleName(),
            Status = EStatus.Todo
        };

    public static Manager.Domain.Entity.Task InvalidInstanceWithInvalidResponsible()
        => new()
        {
            Description = Faker.TextFaker.Sentence(),
            Id = 1,
            Responsible = string.Empty,
            Status = EStatus.Working
        };

    public static Manager.Domain.Entity.Task InvalidInstanceWithInvalidDescription()
        => new()
        {
            Description = string.Empty,
            Id = 1,
            Responsible = Faker.NameFaker.FemaleName(),
            Status = EStatus.Working
        };

    public static Manager.Domain.Entity.Task InvalidInstanceWithInvalidStatus()
       => new()
       {
           Description = string.Empty,
           Id = 1,
           Responsible = Faker.NameFaker.FemaleName(),
           Status = 0
       };
}