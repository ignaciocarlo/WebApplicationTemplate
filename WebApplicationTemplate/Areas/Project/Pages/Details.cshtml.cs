using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationTemplate.Areas.Project.Models;
using WebApplicationTemplate.Features.Project.Queries;

namespace WebApplicationTemplate.Areas.Project.Pages
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DetailsModel(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public ProjectViewModel Project { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var project = await _mediator.Send(new GetProjectByIdQuery() { Id = id });
            if (project == null)
            {
                return NotFound();
            }
            else
            {
                Project = _mapper.Map(project, Project);
            }
            return Page();
        }
    }
}
