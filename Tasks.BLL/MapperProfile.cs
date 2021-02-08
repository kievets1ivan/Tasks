using AutoMapper;
using Tasks.BLL.DTOs;
using Tasks.DAL.Entities;

namespace Tasks.BLL
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<AdditionalTask, AdditionalTaskDTO>().ReverseMap();
        }
    }
}
