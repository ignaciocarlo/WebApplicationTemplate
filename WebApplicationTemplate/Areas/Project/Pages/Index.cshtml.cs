using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationTemplate.Features.Project.Queries;
using WebApplicationTemplate.Models;

namespace WebApplicationTemplate.Areas.Project.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IList<ProjectState> Project { get; set; } = default!;

        public IActionResult OnGetAsync()
        {

            return Page();
        }

        public async Task<IActionResult> OnPostListAllAsync()
        {
            var projects = await _mediator.Send(new GetAllProjectsQuery());
            var dataTableResponse = new DataTableResponse
            {
                RecordsTotal = projects.Count(),
                RecordsFiltered = 10, // You may need to adjust this based on your filtering logic
                Data = projects.ToArray()
            };
            return new JsonResult(new
            {
                recordsTotal = dataTableResponse.RecordsTotal,
                recordsFiltered = dataTableResponse.RecordsFiltered,
                data = dataTableResponse.Data
            });
        }
    }
}
