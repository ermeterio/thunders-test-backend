using Microsoft.Extensions.DependencyInjection;
using Thunders.Test.Task.Manager.Application.Interfaces;
using Thunders.Test.Task.Manager.Application.UseCases;
using Thunders.Test.Task.Manager.Domain.Repository;
using Thunders.Test.Task.Manager.Infra.EF.Repositories;

namespace Thunders.Test.TaskManager.Infra.IoC.IoC;
public static class DependenceInjection
{
    public static void InjectDependences(IServiceCollection collection)
    {
        InjectUseCases(collection);
        InjectRespositories(collection);
    }

    public static void InjectUseCases(IServiceCollection collection)
    {
        collection.AddScoped<ITaskManagerUseCases, TaskManagerUseCases>();
    }

    public static void InjectRespositories(IServiceCollection collection)
    {
        collection.AddScoped<ITaskManagerRepository, TaskManagerRepository>();
    }
}