using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using XMP.Application.DTOs;
using XMP.Application.Interfaces;

namespace XMP.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
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

                var statuses = await _statusService.GetAllStatusesAsync(pageNumber, pageSize);
                return Ok(statuses);
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
                var status = await _statusService.GetStatusByIdAsync(id);
                if (status == null)
                {
                    return NotFound();
                }
                return Ok(status);
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StatusDto statusDto)
        {
            try
            {
                if (statusDto == null)
                {
                    return BadRequest("Status data is null.");
                }

                var createdStatus = await _statusService.AddStatusAsync(statusDto);
                return CreatedAtAction(nameof(GetById), new { id = createdStatus.Id }, createdStatus);
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] StatusDto statusDto)
        {
            try
            {
                if (statusDto == null || id != statusDto.Id)
                {
                    return BadRequest("Status data is invalid.");
                }

                await _statusService.UpdateStatusAsync(statusDto);
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
                await _statusService.DeleteStatusAsync(id);
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
