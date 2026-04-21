using System.ComponentModel.DataAnnotations;

namespace CvApi.Dtos
{
    public class CreateExperienceDto
    {
        [Required]
        [StringLength(100)]
        public string Company { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string Role { get; set; } = "";

        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = "";

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}