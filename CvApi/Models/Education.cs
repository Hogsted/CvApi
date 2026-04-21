namespace CvApi.Models
{
    public class Education
    {
        public int Id { get; set; }
        public string School { get; set; } = "";
        public string Degree { get; set; } = "";
        public string FieldOfStudy { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
