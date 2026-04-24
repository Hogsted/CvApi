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
    public class SkillsController : ControllerBase
    {
        private readonly CvDbContext _context;

        public SkillsController(CvDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            return await _context.Skills.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
                return NotFound();

            return skill;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Skill>> CreateSkill(CreateSkillDto dto)
        {
            var skill = new Skill
            {
                Name = dto.Name,
                Level = dto.Level
            };

            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skill);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(int id, UpdateSkillDto dto)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
                return NotFound();

            skill.Name = dto.Name;
            skill.Level = dto.Level;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
                return NotFound();

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}