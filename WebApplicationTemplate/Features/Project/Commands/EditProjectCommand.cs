using AutoMapper;
using MediatR;
using WebApplicationTemplate.Areas.Project.Models;
using WebApplicationTemplate.Models;

namespace WebApplicationTemplate.Features.Project.Commands
{
    public class EditProjectCommand : ProjectViewModel, IRequest<ProjectState>
    {
        public class EditProjectCommandHandler : IRequestHandler<EditProjectCommand, ProjectState> 
        {
            private readonly ApplicationContext _applicationContext;
            private readonly IMapper _mapper;
            public EditProjectCommandHandler(ApplicationContext applicationContext, IMapper mapper)
            {
                _applicationContext = applicationContext;
                _mapper = mapper;
            }
            public async Task<ProjectState> Handle(EditProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await _applicationContext.Project.FindAsync(request.Id, cancellationToken);
                project = _mapper.Map(request, project);
                _applicationContext.Project.Update(project);
                await _applicationContext.SaveChangesAsync();
                return project;
            }
        }
    }
}
