namespace Thunders.Test.Task.Manager.Unit.Tests.Domain.Entity.Task;
public class TaskTests
{
    #region Success
    [Fact(DisplayName = nameof(ShouldBeCreateValidInstance))]
    [Trait("Domain", "Task")]
    public void ShouldBeCreateValidInstance()
    {
        //arrange 
        var task = TaskTestsFixture.GetValidInstance();

        //act
        var exception = Record.Exception(task.Validate);

        //assert
        Assert.Null(exception);
    }
    #endregion

    #region Error   

    [Fact(DisplayName = nameof(ShouldBeCreateInvalidInstanceWithInvalidResponsible))]
    [Trait("Domain", "Task")]
    public void ShouldBeCreateInvalidInstanceWithInvalidResponsible()
    {
        //arrange
        var task = TaskTestsFixture.InvalidInstanceWithInvalidResponsible();

        //assert
        Assert.Throws<ArgumentException>(task.Validate);
    }    

    [Fact(DisplayName = nameof(ShouldBeCreateInvalidInstanceWithInvalidDescription))]
    [Trait("Domain", "Task")]
    public void ShouldBeCreateInvalidInstanceWithInvalidDescription()
    {
        //arrange
        var task = TaskTestsFixture.InvalidInstanceWithInvalidDescription();

        //assert
        Assert.Throws<ArgumentException>(task.Validate);
    }

    [Fact(DisplayName = nameof(ShouldBeCreateInvalidInstanceWithInvalidStatus))]
    [Trait("Domain", "Task")]
    public void ShouldBeCreateInvalidInstanceWithInvalidStatus()
    {
        //arrange
        var task = TaskTestsFixture.InvalidInstanceWithInvalidStatus();

        //assert
        Assert.Throws<ArgumentException>(task.Validate);
    }
    #endregion
}