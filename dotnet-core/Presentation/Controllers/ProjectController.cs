using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using XMP.Application.DTOs;
using XMP.Application.Interfaces;

namespace XMP.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return BadRequest("Invalid page number or page size.");
                }

                var projects = await _projectService.GetAllProjectsAsync(pageNumber, pageSize);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var project = await _projectService.GetProjectByIdAsync(id);
                if (project == null)
                {
                    return NotFound();
                }
                return Ok(project);
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectDto projectDto)
        {
            try
            {
                if (projectDto == null)
                {
                    return BadRequest("Project data is null.");
                }

                var createdProject = await _projectService.AddProjectAsync(projectDto);
                return CreatedAtAction(nameof(GetById), new { id = createdProject.Id }, createdProject);
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ProjectDto projectDto)
        {
            try
            {
                if (projectDto == null || id != projectDto.Id)
                {
                    return BadRequest("Project data is invalid.");
                }

                await _projectService.UpdateProjectAsync(projectDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _projectService.DeleteProjectAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
