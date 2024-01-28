using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using XMP.Domain.Entities;
using XMP.Domain.Repositories;

namespace XMP.Infrastructure.Repositories
{
    public class AxisBankTransactionRepository : IAxisBankTransactionRepository
    {
        private readonly IDbConnection _dbConnection;

        public AxisBankTransactionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<AxisBankTransaction>> GetAllAsync()
        {
            var query = "SELECT * FROM axisbank_transactions";
            return await _dbConnection.QueryAsync<AxisBankTransaction>(query);
        }

        public async Task<AxisBankTransaction> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM axisbank_transactions WHERE id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<AxisBankTransaction>(query, new { Id = id });
        }

        public async Task AddAsync(AxisBankTransaction transaction)
        {
            var query = @"INSERT INTO axisbank_transactions 
                        (transactiondate, valuedate, chequenumber, transactiondetails, amounttransferred, 
                         typeoftransaction, balanceamount, bankbranchname, bankname, companyname) 
                        VALUES (@TransactionDate, @ValueDate, @ChequeNumber, @TransactionDetails, @AmountTransferred, 
                                @TypeOfTransaction, @BalanceAmount, @BankBranchName, @BankName, @CompanyName)";
            await _dbConnection.ExecuteAsync(query, transaction);
        }

        public async Task UpdateAsync(AxisBankTransaction transaction)
        {
            var query = @"UPDATE axisbank_transactions SET 
                        transactiondate = @TransactionDate, valuedate = @ValueDate, chequenumber = @ChequeNumber, 
                        transactiondetails = @TransactionDetails, amounttransferred = @AmountTransferred, 
                        typeoftransaction = @TypeOfTransaction, balanceamount = @BalanceAmount, 
                        bankbranchname = @BankBranchName, bankname = @BankName, companyname = @CompanyName 
                        WHERE id = @Id";
            await _dbConnection.ExecuteAsync(query, transaction);
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM axisbank_transactions WHERE id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }
    }
}
