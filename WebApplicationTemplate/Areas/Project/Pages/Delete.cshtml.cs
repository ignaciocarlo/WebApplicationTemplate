using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplicationTemplate.Areas.Project.Models;
using WebApplicationTemplate.Features.Project.Commands;
using WebApplicationTemplate.Features.Project.Queries;
using WebApplicationTemplate.Models;

namespace WebApplicationTemplate.Areas.Project.Pages
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DeleteModel(IMediator mediator, IMapper mapper)
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = await _mediator.Send(_mapper.Map<DeleteProjectCommand>(Project));
            if(result == null) { return NotFound(); }
            return RedirectToPage("./Index");
        }
    }
}
