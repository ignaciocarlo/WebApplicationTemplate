using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTemplate.Models;

namespace WebApplicationTemplate.Features.Project.Commands
{
    public class DeleteProjectCommand : IRequest<ProjectState>
    {
        public string Id { get; set; }
        public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, ProjectState>
        {
            private readonly ApplicationContext _applicationContext;
            public DeleteProjectCommandHandler(ApplicationContext applicationContext) 
            {
                _applicationContext = applicationContext;
            }

            public async Task<ProjectState> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await _applicationContext.Project.FindAsync(request.Id, cancellationToken);
                _applicationContext.Project.Remove(project);
                await _applicationContext.SaveChangesAsync();
                return project;
            }
        }
    }
}
