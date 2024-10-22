using System.Collections.Generic;
using System.Threading.Tasks;
using XMP.Domain.Entities;

namespace XMP.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync(int pageNumber, int pageSize);
        Task<User> GetByIdAsync(long id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(long id);
    }
}

