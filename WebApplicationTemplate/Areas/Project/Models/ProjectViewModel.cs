using System.ComponentModel.DataAnnotations;

namespace WebApplicationTemplate.Areas.Project.Models
{
    public class ProjectViewModel
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Display(Name = "Project Name")]
        [Required]
        public string ProjectName { get; set; } = string.Empty;

        [Display(Name = "Project Description")]
        [Required]
        public string ProjectDescription { get; set; } = string.Empty;
    }
}
