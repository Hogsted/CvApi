using CvApi.Data;
using CvApi.Dtos;
using CvApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CvApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly CvDbContext _context;

        public EducationController(CvDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Education>>> GetEducations()
        {
            return await _context.Educations.OrderByDescending(e => e.StartDate).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Education>> GetEducation(int id)
        {
            var education = await _context.Educations.FindAsync(id);

            if (education == null)
                return NotFound();

            return education;
        }

        [HttpPost]
        public async Task<ActionResult<Education>> CreateEducation(CreateEducationDto dto)
        {
            var education = new Education
            {
                School = dto.School,
                Degree = dto.Degree,
                FieldOfStudy = dto.FieldOfStudy,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };

            _context.Educations.Add(education);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEducation), new { id = education.Id }, education);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEducation(int id, UpdateEducationDto dto)
        {
            var education = await _context.Educations.FindAsync(id);

            if (education == null)
                return NotFound();

            education.School = dto.School;
            education.Degree = dto.Degree;
            education.FieldOfStudy = dto.FieldOfStudy;
            education.Description = dto.Description;
            education.StartDate = dto.StartDate;
            education.EndDate = dto.EndDate;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            var education = await _context.Educations.FindAsync(id);

            if (education == null)
                return NotFound();

            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
