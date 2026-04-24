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
    public class ProfileController : ControllerBase
    {
        private readonly CvDbContext _context;

        public ProfileController(CvDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Profile>> GetProfile()
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync();

            if (profile == null)
                return NotFound();

            return profile;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Profile>> CreateProfile(CreateProfileDto dto)
        {
            if (await _context.Profiles.AnyAsync())
                return Conflict("A profile already exists. Use PUT to update it.");

            var profile = new Profile
            {
                FullName = dto.FullName,
                Title = dto.Title,
                Bio = dto.Bio,
                Email = dto.Email,
                GitHubUrl = dto.GitHubUrl ?? "",
                LinkedInUrl = dto.LinkedInUrl ?? ""
            };

            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProfile), profile);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(int id, UpdateProfileDto dto)
        {
            var profile = await _context.Profiles.FindAsync(id);

            if (profile == null)
                return NotFound();

            profile.FullName = dto.FullName;
            profile.Title = dto.Title;
            profile.Bio = dto.Bio;
            profile.Email = dto.Email;
            profile.GitHubUrl = dto.GitHubUrl ?? "";
            profile.LinkedInUrl = dto.LinkedInUrl ?? "";

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
