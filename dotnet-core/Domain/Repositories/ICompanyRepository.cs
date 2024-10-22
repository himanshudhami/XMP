using System.Collections.Generic;
using System.Threading.Tasks;
using XMP.Domain.Entities;

namespace XMP.Domain.Repositories
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync(int pageNumber, int pageSize);
        Task<Company> GetByIdAsync(long id);
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task DeleteAsync(long id);
    }
}
