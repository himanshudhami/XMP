using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using XMP.Domain.Entities;
using XMP.Domain.Repositories;
using XMP.Infrastructure.DbContext;

namespace XMP.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperDbContext _dbContext;

        public CompanyRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Company>> GetAllAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("PageNumber and PageSize must be positive integers.");
            }

            using var connection = _dbContext.GetConnection();
            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;
            var query = $"SELECT * FROM companies ORDER BY id OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY";
            return await connection.QueryAsync<Company>(query);
        }

        public async Task<Company> GetByIdAsync(long id)
        {
            using var connection = _dbContext.GetConnection();
            var query = "SELECT * FROM companies WHERE id = @Id";
            return await connection.QuerySingleOrDefaultAsync<Company>(query, new { Id = id });
        }

        public async Task AddAsync(Company company)
        {
            using var connection = _dbContext.GetConnection();
            var query = @"INSERT INTO companies 
                        (name, description, created_at, updated_at) 
                        VALUES 
                        (@Name, @Description, @CreatedAt, @UpdatedAt)";
            await connection.ExecuteAsync(query, company);
        }

        public async Task UpdateAsync(Company company)
        {
            using var connection = _dbContext.GetConnection();
            var query = @"UPDATE companies SET 
                        name = @Name, 
                        description = @Description, 
                        created_at = @CreatedAt, 
                        updated_at = @UpdatedAt 
                        WHERE id = @Id";
            await connection.ExecuteAsync(query, company);
        }

        public async Task DeleteAsync(long id)
        {
            using var connection = _dbContext.GetConnection();
            var query = "DELETE FROM companies WHERE id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
