using CvApi.Data;
using CvApi.Dtos;
using CvApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CvApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExperiencesController : ControllerBase
    {
        private readonly CvDbContext _context;

        public ExperiencesController(CvDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Experience>>> GetExperiences()
        {
            return await _context.Experiences.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Experience>> GetExperience(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);

            if (experience == null)
                return NotFound();

            return experience;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Experience>> CreateExperience(CreateExperienceDto dto)
        {
            var experience = new Experience
            {
                Company = dto.Company,
                Role = dto.Role,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };

            _context.Experiences.Add(experience);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExperience), new { id = experience.Id }, experience);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExperience(int id, UpdateExperienceDto dto)
        {
            var experience = await _context.Experiences.FindAsync(id);

            if (experience == null)
                return NotFound();

            experience.Company = dto.Company;
            experience.Role = dto.Role;
            experience.Description = dto.Description;
            experience.StartDate = dto.StartDate;
            experience.EndDate = dto.EndDate;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);

            if (experience == null)
                return NotFound();

            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}