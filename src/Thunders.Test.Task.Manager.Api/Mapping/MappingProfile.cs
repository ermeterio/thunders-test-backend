using AutoMapper;
using Thunders.Test.Task.Manager.Application.DataTransferObject;
using Thunders.Test.Task.Manager.Domain.DataTransferObjects;

namespace Thunders.Test.Task.Manager.Application.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskCreateDto, Domain.Entity.Task>();
        CreateMap<TaskCreateDto, TaskSearchDto>();
        CreateMap<Domain.Entity.Task, TaskSearchDto>();
    }
}