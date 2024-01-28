using System.Collections.Generic;
using System.Threading.Tasks;
using XMP.Application.DTOs;


namespace XMP.Application.Interfaces
{
    public interface IAxisBankTransactionService
    {
        Task<IEnumerable<AxisBankTransactionDto>> GetAllTransactionsAsync();
        Task<AxisBankTransactionDto> GetTransactionByIdAsync(int id);
        Task AddTransactionAsync(AxisBankTransactionDto transactionDto);
        Task UpdateTransactionAsync(AxisBankTransactionDto transactionDto);
        Task DeleteTransactionAsync(int id);
    }
}
