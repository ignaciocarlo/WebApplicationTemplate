using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplicationTemplate.Models;

namespace WebApplicationTemplate.Features.Project.Queries
{
    public class GetAllProjectsQuery : IRequest<IEnumerable<ProjectState>>
    {
        public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectState>>
        {
            private readonly ApplicationContext _applicationContext;
            public GetAllProjectsQueryHandler(ApplicationContext applicationContext)
            {
                _applicationContext = applicationContext;
            }

            public async Task<IEnumerable<ProjectState>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
            {
                var projects = await _applicationContext.Project.ToListAsync();
                return projects;
            }
        }
    }
}
