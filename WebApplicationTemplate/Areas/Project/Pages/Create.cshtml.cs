using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationTemplate.Areas.Project.Models;
using WebApplicationTemplate.Features.Project.Commands;

namespace WebApplicationTemplate.Areas.Project.Pages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateModel(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ProjectViewModel Project { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        { 
            if(!ModelState.IsValid)
            {
                return Page();
            }
            var result = await _mediator.Send(_mapper.Map<CreateProjectCommand>(Project));
            if (result == null) { return Page(); }
            return RedirectToPage("./Index");
        }
    }
}
