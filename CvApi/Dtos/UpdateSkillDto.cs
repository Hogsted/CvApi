using System.ComponentModel.DataAnnotations;

namespace CvApi.Dtos
{
    public class UpdateSkillDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = "";

        [Required]
        [StringLength(50)]
        public string Level { get; set; } = "";
        public bool IsFeatured { get; set; } = false;
    }
}