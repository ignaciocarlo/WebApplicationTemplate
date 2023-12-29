using System.ComponentModel.DataAnnotations;

namespace WebApplicationTemplate.Models
{
    public class ProjectState : BaseEntity
    {
        public string ProjectName { get; init; } = string.Empty;
        public string ProjectDescription { get; init; } = string.Empty;
    }
}
