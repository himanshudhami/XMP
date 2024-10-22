using System.Collections.Generic;
using System.Threading.Tasks;
using XMP.Domain.Entities;

namespace XMP.Domain.Repositories
{
    public interface IStatusRepository
    {
        Task<IEnumerable<Status>> GetAllAsync(int pageNumber, int pageSize);
        Task<Status> GetByIdAsync(long id);
        Task AddAsync(Status status);
        Task UpdateAsync(Status status);
        Task DeleteAsync(long id);
    }
}
