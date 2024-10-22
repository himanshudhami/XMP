using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using XMP.Application.DTOs;
using XMP.Application.Interfaces;

namespace XMP.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
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

                var companies = await _companyService.GetAllCompaniesAsync(pageNumber, pageSize);
                return Ok(companies);
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
                var company = await _companyService.GetCompanyByIdAsync(id);
                if (company == null)
                {
                    return NotFound();
                }
                return Ok(company);
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompanyDto companyDto)
        {
            try
            {
                if (companyDto == null)
                {
                    return BadRequest("Company data is null.");
                }

                var createdCompany = await _companyService.AddCompanyAsync(companyDto);
                return CreatedAtAction(nameof(GetById), new { id = createdCompany.Id }, createdCompany);
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] CompanyDto companyDto)
        {
            try
            {
                if (companyDto == null || id != companyDto.Id)
                {
                    return BadRequest("Company data is invalid.");
                }

                await _companyService.UpdateCompanyAsync(companyDto);
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
                await _companyService.DeleteCompanyAsync(id);
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
