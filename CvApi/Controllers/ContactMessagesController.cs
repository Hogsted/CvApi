using CvApi.Data;
using CvApi.Dtos;
using CvApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CvApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactMessagesController : ControllerBase
    {
        private readonly CvDbContext _context;

        public ContactMessagesController(CvDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactMessage>>> GetMessages()
        {
            return await _context.ContactMessages.OrderByDescending(m => m.SentAt).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ContactMessage>> SendMessage(CreateContactMessageDto dto)
        {
            var message = new ContactMessage
            {
                Name = dto.Name,
                Email = dto.Email,
                Subject = dto.Subject,
                Message = dto.Message
            };

            _context.ContactMessages.Add(message);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpPatch("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);

            if (message == null)
                return NotFound();

            message.IsRead = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);

            if (message == null)
                return NotFound();

            _context.ContactMessages.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
