using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplicationTemplate.Models;

namespace WebApplicationTemplate.Areas.Project.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext _context;

        public IndexModel(ApplicationContext context)
        {
            _context = context;
        }

        public IList<ProjectState> Project { get; set; } = default!;

        public IActionResult OnGetAsync()
        {

            return Page();
        }

        public async Task<IActionResult> OnPostListAllAsync()
        {
            var projects = await _context.Project.ToListAsync();

            var dataTableResponse = new DataTableResponse
            {
                RecordsTotal = projects.Count,
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
