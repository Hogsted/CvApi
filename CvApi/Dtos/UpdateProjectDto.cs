using System.ComponentModel.DataAnnotations;

namespace CvApi.Dtos
{
    public class UpdateProjectDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = "";

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = "";

        public string GitHubUrl { get; set; } = "";
        public string LiveUrl { get; set; } = "";
    }
}