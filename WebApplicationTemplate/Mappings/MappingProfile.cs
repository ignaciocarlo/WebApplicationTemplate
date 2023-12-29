using AutoMapper;
using WebApplicationTemplate.Areas.Project.Models;
using WebApplicationTemplate.Features.Project.Commands;
using WebApplicationTemplate.Models;

namespace WebApplicationTemplate.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<ProjectViewModel, CreateProjectCommand>();
            CreateMap<ProjectViewModel, DeleteProjectCommand>();
            CreateMap<ProjectViewModel, EditProjectCommand>();
            CreateMap<ProjectViewModel, ProjectState>();
            CreateMap<ProjectViewModel, ProjectState>().ReverseMap();
        }
    }
}
