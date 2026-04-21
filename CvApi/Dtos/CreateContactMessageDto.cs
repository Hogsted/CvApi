using System.ComponentModel.DataAnnotations;

namespace CvApi.Dtos
{
    public class CreateContactMessageDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = "";

        [Required]
        [StringLength(200)]
        public string Subject { get; set; } = "";

        [Required]
        [StringLength(2000)]
        public string Message { get; set; } = "";
    }
}
