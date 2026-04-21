using System.ComponentModel.DataAnnotations;

namespace CvApi.Dtos
{
    public class CreateEducationDto
    {
        [Required]
        [StringLength(200)]
        public string School { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string Degree { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string FieldOfStudy { get; set; } = "";

        [StringLength(1000)]
        public string Description { get; set; } = "";

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
