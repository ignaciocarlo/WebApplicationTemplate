using System.ComponentModel.DataAnnotations;

namespace WebApplicationTemplate.Models
{
    public class ProjectState : BaseEntity
    {
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; } = string.Empty;
        [Display(Name = "Project Description")]
        public string ProjectDescription { get; set; } = string.Empty;
    }
}
