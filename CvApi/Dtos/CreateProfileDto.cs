using System.ComponentModel.DataAnnotations;

namespace CvApi.Dtos
{
    public class CreateProfileDto
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = "";

        [Required]
        [StringLength(2000)]
        public string Bio { get; set; } = "";

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = "";

        [Url]
        [StringLength(300)]
        public string? GitHubUrl { get; set; }

        [Url]
        [StringLength(300)]
        public string? LinkedInUrl { get; set; }
    }
}
