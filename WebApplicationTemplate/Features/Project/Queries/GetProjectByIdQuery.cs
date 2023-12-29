using MediatR;
using WebApplicationTemplate.Models;

namespace WebApplicationTemplate.Features.Project.Queries
{
    public class GetProjectByIdQuery : IRequest<ProjectState>
    {
        public string Id { get; set; }
        public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectState>
        {
            private readonly ApplicationContext _applicationContext;
            public GetProjectByIdQueryHandler(ApplicationContext applicationContext)
            {
                _applicationContext = applicationContext;
            }

            public async Task<ProjectState> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
            {
                var project = await _applicationContext.Project.FindAsync(request.Id);
                return project;
            }
        }
    }
}
