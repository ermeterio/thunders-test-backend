using AutoMapper;
using Moq;
using Thunders.Test.Task.Manager.Application.UseCases;
using Thunders.Test.Task.Manager.Domain.DataTransferObjects;
using Thunders.Test.Task.Manager.Domain.Repository;
using Thunders.Test.Task.Manager.Unit.Tests.Domain.Entity.Task;

namespace Thunders.Test.Task.Manager.Unit.Tests.Application.UseCases;
public class TaskManagerUseCasesTest
{
    #region Success 
    [Fact(DisplayName = nameof(ShouldBeReturnTasksWithFilters))]
    [Trait("UseCases", "TaskManagerUseCases")]
    public async System.Threading.Tasks.Task ShouldBeReturnTasksWithFilters()
    {
        //arrange 
        var task = TaskManagerUseCasesTestFixture.GetValidInstance();
        var repository = new Mock<ITaskManagerRepository>();
        repository.Setup(r => r.GetByFiltersAsync(It.IsAny<TaskSearchDto>())).ReturnsAsync(TaskManagerUseCasesTestFixture.GetValidTaskList());

        var taskManagerUseCases = new TaskManagerUseCases(repository.Object, new Mock<IMapper>().Object);

        //act
        var data = await taskManagerUseCases.GetTasksByFilterAsync(task);
        Assert.NotNull(data);
        Assert.True(data.Any());
    }

    [Fact(DisplayName = nameof(ShouldBeCreateValidInstance))]
    [Trait("UseCases", "TaskManagerUseCases")]
    public async System.Threading.Tasks.Task ShouldBeCreateValidInstance()
    {
        //arrange 
        var task = TaskTestsFixture.GetValidInstance();
        var taskCreateDto = TaskTestsFixture.GetValidInstanceCreateDto();
        var taskSearchDto = TaskTestsFixture.GetValidInstanceSearchDto();
        var repository = new Mock<ITaskManagerRepository>();
        repository.Setup(r => r.GetByFiltersAsync(It.IsAny<TaskSearchDto>())).ReturnsAsync(Enumerable.Empty<Manager.Domain.Entity.Task>);
        repository.Setup(r => r.CreateAsync(It.IsAny<Manager.Domain.Entity.Task>())).ReturnsAsync(1);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<TaskSearchDto>(It.IsAny<object>())).Returns(taskSearchDto);
        mapperMock.Setup(m => m.Map<Manager.Domain.Entity.Task>(It.IsAny<object>())).Returns(task);

        var taskManagerUseCases = new TaskManagerUseCases(repository.Object, mapperMock.Object);

        //act
        var data = await taskManagerUseCases.CreateTaskAsync(taskCreateDto);
        Assert.True(data > 0);
    }

    [Fact(DisplayName = nameof(ShouldBeUpdateValidInstance))]
    [Trait("UseCases", "TaskManagerUseCases")]
    public async System.Threading.Tasks.Task ShouldBeUpdateValidInstance()
    {
        //arrange 
        var task = TaskTestsFixture.GetValidInstance();
        var taskSearchDto = TaskTestsFixture.GetValidInstanceSearchDto();
        var repository = new Mock<ITaskManagerRepository>();
        repository.Setup(r => r.GetByFiltersAsync(It.IsAny<TaskSearchDto>())).ReturnsAsync(Enumerable.Empty<Manager.Domain.Entity.Task>);
        repository.Setup(r => r.UpdateAsync(It.IsAny<Manager.Domain.Entity.Task>())).Returns(System.Threading.Tasks.Task.CompletedTask);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<TaskSearchDto>(It.IsAny<object>())).Returns(taskSearchDto);

        var taskManagerUseCases = new TaskManagerUseCases(repository.Object, mapperMock.Object);

        //act
        var exception = await Record.ExceptionAsync(() => taskManagerUseCases.UpdateTaskAsync(task));
        Assert.Null(exception);
    }

    [Fact(DisplayName = nameof(ShouldBeDeleteValidInstance))]
    [Trait("UseCases", "TaskManagerUseCases")]
    public async System.Threading.Tasks.Task ShouldBeDeleteValidInstance()
    {
        //arrange 
        var repository = new Mock<ITaskManagerRepository>();
        var tasksReturn = new List<Manager.Domain.Entity.Task> { TaskTestsFixture.GetValidInstance() };
        repository.Setup(r => r.GetByFiltersAsync(It.IsAny<TaskSearchDto>())).ReturnsAsync(tasksReturn);
        repository.Setup(r => r.DeleteAsync(It.IsAny<int>())).Returns(System.Threading.Tasks.Task.CompletedTask);

        var taskManagerUseCases = new TaskManagerUseCases(repository.Object, new Mock<IMapper>().Object);

        //act
        var exception = await Record.ExceptionAsync(() => taskManagerUseCases.DeleteTaskAsync(1));
        Assert.Null(exception);
    }
    #endregion

    #region Error
    [Fact(DisplayName = nameof(ShouldBeReturnTasksWithIncorrenctFilters))]
    [Trait("UseCases", "TaskManagerUseCases")]
    public async System.Threading.Tasks.Task ShouldBeReturnTasksWithIncorrenctFilters()
    {
        //arrange 
        var task = TaskManagerUseCasesTestFixture.GetValidInstance();
        var repository = new Mock<ITaskManagerRepository>();
        repository.Setup(r => r.GetByFiltersAsync(It.IsAny<TaskSearchDto>())).ThrowsAsync(new ArgumentException());

        var taskManagerUseCases = new TaskManagerUseCases(repository.Object, new Mock<IMapper>().Object);

        //act
        var exception = await Record.ExceptionAsync(() => taskManagerUseCases.GetTasksByFilterAsync(task));
        Assert.NotNull(exception);
    }

    [Fact(DisplayName = nameof(ShouldBeCreateValidInstanceButRepositoryError))]
    [Trait("UseCases", "TaskManagerUseCases")]
    public async System.Threading.Tasks.Task ShouldBeCreateValidInstanceButRepositoryError()
    {
        //arrange 
        var task = TaskTestsFixture.GetValidInstance();
        var taskDto = TaskTestsFixture.GetInvalidInstanceCreateDto();
        var repository = new Mock<ITaskManagerRepository>();
        repository.Setup(r => r.CreateAsync(It.IsAny<Manager.Domain.Entity.Task>())).ThrowsAsync(new ArgumentException());

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<Manager.Domain.Entity.Task>(It.IsAny<object>())).Returns(task);

        var taskManagerUseCases = new TaskManagerUseCases(repository.Object, mapperMock.Object);

        //act
        var exception = await Record.ExceptionAsync(() => taskManagerUseCases.CreateTaskAsync(taskDto));
        Assert.NotNull(exception);
    }

    [Fact(DisplayName = nameof(ShouldBeUpdateInvalidInstanceWithRepositoryError))]
    [Trait("UseCases", "TaskManagerUseCases")]
    public async System.Threading.Tasks.Task ShouldBeUpdateInvalidInstanceWithRepositoryError()
    {
        //arrange 
        var task = TaskTestsFixture.GetValidInstance();
        var repository = new Mock<ITaskManagerRepository>();
        repository.Setup(r => r.UpdateAsync(It.IsAny<Manager.Domain.Entity.Task>())).ThrowsAsync(new Exception());

        var taskManagerUseCases = new TaskManagerUseCases(repository.Object, new Mock<IMapper>().Object);

        //act
        var exception = await Record.ExceptionAsync(() => taskManagerUseCases.UpdateTaskAsync(task));
        Assert.NotNull(exception);
    }

    [Fact(DisplayName = nameof(ShouldBeUpdateInvalidConflicExistInstance))]
    [Trait("UseCases", "TaskManagerUseCases")]
    public async System.Threading.Tasks.Task ShouldBeUpdateInvalidConflicExistInstance()
    {
        //arrange 
        var task = TaskTestsFixture.GetValidInstance();
        var otherTaskInDb = TaskTestsFixture.GetValidInstance();
        otherTaskInDb.Id = 2;
        var tasks = new List<Manager.Domain.Entity.Task> { otherTaskInDb };
        var repository = new Mock<ITaskManagerRepository>();
        repository.Setup(r => r.GetByFiltersAsync(It.IsAny<TaskSearchDto>())).ReturnsAsync(tasks);

        var taskManagerUseCases = new TaskManagerUseCases(repository.Object, new Mock<IMapper>().Object);

        //act
        var exception = await Record.ExceptionAsync(() => taskManagerUseCases.UpdateTaskAsync(task));
        Assert.NotNull(exception);
    }

    [Fact(DisplayName = nameof(ShouldBeCreateInvalidConflicExistInstance))]
    [Trait("UseCases", "TaskManagerUseCases")]
    public async System.Threading.Tasks.Task ShouldBeCreateInvalidConflicExistInstance()
    {
        //arrange 
        var task = TaskTestsFixture.GetInvalidInstanceCreateDto();
        var otherTaskInDb = TaskTestsFixture.GetValidInstance();
        otherTaskInDb.Id = 2;
        var tasks = new List<Manager.Domain.Entity.Task> { otherTaskInDb };
        var repository = new Mock<ITaskManagerRepository>();
        repository.Setup(r => r.GetByFiltersAsync(It.IsAny<TaskSearchDto>())).ReturnsAsync(tasks);

        var taskManagerUseCases = new TaskManagerUseCases(repository.Object, new Mock<IMapper>().Object);

        //act
        var exception = await Record.ExceptionAsync(() => taskManagerUseCases.CreateTaskAsync(task));
        Assert.NotNull(exception);
    }

    [Fact(DisplayName = nameof(ShouldBeDeleteInvalidInstance))]
    [Trait("UseCases", "TaskManagerUseCases")]
    public async System.Threading.Tasks.Task ShouldBeDeleteInvalidInstance()
    {
        //arrange 
        var repository = new Mock<ITaskManagerRepository>();
        repository.Setup(r => r.DeleteAsync(It.IsAny<int>())).ThrowsAsync(new ArgumentException());

        var taskManagerUseCases = new TaskManagerUseCases(repository.Object, new Mock<IMapper>().Object);

        //act
        var exception = await Record.ExceptionAsync(() => taskManagerUseCases.DeleteTaskAsync(1));
        Assert.NotNull(exception);
    }

    [Fact(DisplayName = nameof(ShouldBeDeleteNotExistingInstanceId))]
    [Trait("UseCases", "TaskManagerUseCases")]
    public async System.Threading.Tasks.Task ShouldBeDeleteNotExistingInstanceId()
    {
        //arrange 
        var repository = new Mock<ITaskManagerRepository>();
        repository.Setup(r => r.GetByFiltersAsync(It.IsAny<TaskSearchDto>())).ReturnsAsync(Enumerable.Empty<Manager.Domain.Entity.Task>);

        var taskManagerUseCases = new TaskManagerUseCases(repository.Object, new Mock<IMapper>().Object);

        //act
        var exception = await Record.ExceptionAsync(() => taskManagerUseCases.DeleteTaskAsync(1));
        Assert.NotNull(exception);
    }
    #endregion
}