using System.ComponentModel.DataAnnotations;

namespace CvApi.Dtos
{
    public class CreateProjectDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = "";

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = "";

        [StringLength(300)]
        public string? GitHubUrl { get; set; }

        [StringLength(300)]
        public string? LiveUrl { get; set; }
    }
}
