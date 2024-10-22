using System.Collections.Generic;
using System.Threading.Tasks;
using XMP.Domain.Entities;

namespace XMP.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync(int pageNumber, int pageSize);
        Task<Project> GetByIdAsync(long id);
        Task AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(long id);
    }
}
