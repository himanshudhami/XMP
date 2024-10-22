using System.Collections.Generic;
using System.Threading.Tasks;
using XMP.Application.DTOs;

namespace XMP.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(int pageNumber, int pageSize);
        Task<CompanyDto> GetCompanyByIdAsync(long id);
        Task<CompanyDto> AddCompanyAsync(CompanyDto companyDto);
        Task UpdateCompanyAsync(CompanyDto companyDto);
        Task DeleteCompanyAsync(long id);
    }
}
