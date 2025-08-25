using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioAPI.Models;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly PortfolioDbContext _context;

        public EducationController(PortfolioDbContext context)
        {
            _context = context;
        }

        // GET: api/education
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Education>>> GetEducation()
        {
            try
            {
                var education = await _context.Education
                    .OrderBy(e => e.CreatedAt)
                    .ToListAsync();
                return Ok(education);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving education", error = ex.Message });
            }
        }

        // GET: api/education/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Education>> GetEducation(int id)
        {
            try
            {
                var education = await _context.Education.FindAsync(id);

                if (education == null)
                {
                    return NotFound(new { message = "Education not found" });
                }

                return Ok(education);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving education", error = ex.Message });
            }
        }

        // POST: api/education
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Education>> CreateEducation(Education education)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                education.CreatedAt = DateTime.UtcNow;
                education.UpdatedAt = DateTime.UtcNow;

                _context.Education.Add(education);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEducation), new { id = education.Id }, education);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating education", error = ex.Message });
            }
        }

        // PUT: api/education/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateEducation(int id, Education education)
        {
            if (id != education.Id)
            {
                return BadRequest(new { message = "ID mismatch" });
            }

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingEducation = await _context.Education.FindAsync(id);
                if (existingEducation == null)
                {
                    return NotFound(new { message = "Education not found" });
                }

                existingEducation.Degree = education.Degree;
                existingEducation.Institution = education.Institution;
                existingEducation.Duration = education.Duration;
                existingEducation.Description = education.Description;
                existingEducation.Grade = education.Grade;
                existingEducation.Location = education.Location;
                existingEducation.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return Ok(existingEducation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating education", error = ex.Message });
            }
        }

        // DELETE: api/education/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            try
            {
                var education = await _context.Education.FindAsync(id);
                if (education == null)
                {
                    return NotFound(new { message = "Education not found" });
                }

                _context.Education.Remove(education);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Education deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting education", error = ex.Message });
            }
        }
    }
}
