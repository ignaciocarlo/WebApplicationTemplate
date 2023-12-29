using AutoMapper;
using MediatR;
using WebApplicationTemplate.Areas.Project.Models;
using WebApplicationTemplate.Models;

namespace WebApplicationTemplate.Features.Project.Commands
{
    public class CreateProjectCommand : ProjectViewModel, IRequest<ProjectState>
    {
        public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectState>
        {
            private readonly ApplicationContext _applicationContext;
            private readonly IMapper _mapper;
            public CreateProjectCommandHandler(ApplicationContext applicationContext, IMapper mapper)
            {
                _applicationContext = applicationContext;
                _mapper = mapper;
            }

            public async Task<ProjectState> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
            {

                var project = _mapper.Map<ProjectState>(request);
                _applicationContext.Add(project);
                await _applicationContext.SaveChangesAsync();
                return project;
            }
        }
    }
}
