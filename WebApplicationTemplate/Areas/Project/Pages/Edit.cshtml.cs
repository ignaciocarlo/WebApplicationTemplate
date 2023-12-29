using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationTemplate.Areas.Project.Models;
using WebApplicationTemplate.Features.Project.Commands;
using WebApplicationTemplate.Features.Project.Queries;

namespace WebApplicationTemplate.Areas.Project.Pages
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EditModel(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [BindProperty]
        public ProjectViewModel Project { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _mediator.Send(_mapper.Map<EditProjectCommand>(Project));
            if (result == null) { return NotFound(); }

            return RedirectToPage("./Index");
        }
    }
}
