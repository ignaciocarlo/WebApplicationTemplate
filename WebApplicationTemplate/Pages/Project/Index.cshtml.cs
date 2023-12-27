using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplicationTemplate.Models;

namespace WebApplicationTemplate.Pages.Project
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext _context;

        public IndexModel(ApplicationContext context)
        {
            _context = context;
        }

        public IList<ProjectState> Project { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var project = from p in _context.Project
                          select p;

            if(!SearchString.IsNullOrEmpty())
            {
                project = project.Where(p => p.ProjectName.Contains(SearchString!));
            }
            Project = await project.ToListAsync();
        }
    }
}
