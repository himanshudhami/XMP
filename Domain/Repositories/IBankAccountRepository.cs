using System.Collections.Generic;
using System.Threading.Tasks;
using XMP.Domain.Entities;

namespace XMP.Domain.Repositories
{
    public interface IAxisBankTransactionRepository
    {
        Task<IEnumerable<AxisBankTransaction>> GetAllAsync();
        Task<AxisBankTransaction> GetByIdAsync(int id);
        Task AddAsync(AxisBankTransaction transaction);
        Task UpdateAsync(AxisBankTransaction transaction);
        Task DeleteAsync(int id);
    }
}
