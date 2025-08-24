using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioAPI.Models;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly PortfolioDbContext _context;

        public ProjectsController(PortfolioDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            try
            {
                var projects = await _context.Projects
                    .OrderByDescending(p => p.CreatedAt)
                    .ToListAsync();
                
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving projects", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            try
            {
                var project = await _context.Projects.FindAsync(id);
                
                if (project == null)
                    return NotFound(new { message = $"Project with ID {id} not found" });
                
                return Ok(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving project", error = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Project>> CreateProject(Project project)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                project.CreatedAt = DateTime.UtcNow;
                project.UpdatedAt = DateTime.UtcNow;
                
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                
                return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating project", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Project>> UpdateProject(int id, Project project)
        {
            try
            {
                if (id != project.Id)
                    return BadRequest(new { message = "Project ID mismatch" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingProject = await _context.Projects.FindAsync(id);
                if (existingProject == null)
                    return NotFound(new { message = $"Project with ID {id} not found" });

                existingProject.Title = project.Title;
                existingProject.Description = project.Description;
                existingProject.Image = project.Image;
                existingProject.Technologies = project.Technologies;
                existingProject.GitHubUrl = project.GitHubUrl;
                existingProject.LiveUrl = project.LiveUrl;
                existingProject.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                
                return Ok(existingProject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating project", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteProject(int id)
        {
            try
            {
                var project = await _context.Projects.FindAsync(id);
                if (project == null)
                    return NotFound(new { message = $"Project with ID {id} not found" });

                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
                
                return Ok(new { message = "Project deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting project", error = ex.Message });
            }
        }
    }
}
