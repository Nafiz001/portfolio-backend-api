using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioAPI.Models;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly PortfolioDbContext _context;

        public ContactController(PortfolioDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage(ContactMessage message)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                message.CreatedAt = DateTime.UtcNow;
                message.IsRead = false;
                
                _context.ContactMessages.Add(message);
                await _context.SaveChangesAsync();
                
                return Ok(new { message = "Message sent successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error sending message", error = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ContactMessage>>> GetMessages()
        {
            try
            {
                var messages = await _context.ContactMessages
                    .OrderByDescending(m => m.CreatedAt)
                    .ToListAsync();
                
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving messages", error = ex.Message });
            }
        }

        [HttpPut("{id}/mark-read")]
        [Authorize]
        public async Task<ActionResult> MarkAsRead(int id)
        {
            try
            {
                var message = await _context.ContactMessages.FindAsync(id);
                if (message == null)
                    return NotFound(new { message = $"Message with ID {id} not found" });

                message.IsRead = true;
                await _context.SaveChangesAsync();
                
                return Ok(new { message = "Message marked as read" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating message", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            try
            {
                var message = await _context.ContactMessages.FindAsync(id);
                if (message == null)
                    return NotFound(new { message = $"Message with ID {id} not found" });

                _context.ContactMessages.Remove(message);
                await _context.SaveChangesAsync();
                
                return Ok(new { message = "Message deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting message", error = ex.Message });
            }
        }
    }
}
