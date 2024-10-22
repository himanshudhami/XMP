using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using XMP.Domain.Entities;
using XMP.Domain.Repositories;
using XMP.Infrastructure.DbContext;

namespace XMP.Infrastructure.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly DapperDbContext _dbContext;

        public StatusRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Status>> GetAllAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("PageNumber and PageSize must be positive integers.");
            }

            using var connection = _dbContext.GetConnection();
            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;
            var query = $"SELECT * FROM statuses ORDER BY id OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY";
            return await connection.QueryAsync<Status>(query);
        }

        public async Task<Status> GetByIdAsync(long id)
        {
            using var connection = _dbContext.GetConnection();
            var query = "SELECT * FROM statuses WHERE id = @Id";
            return await connection.QuerySingleOrDefaultAsync<Status>(query, new { Id = id });
        }

        public async Task AddAsync(Status status)
        {
            using var connection = _dbContext.GetConnection();
            var query = @"INSERT INTO statuses 
                        (status, created_at, updated_at) 
                        VALUES 
                        (@StatusValue, @CreatedAt, @UpdatedAt)";
            await connection.ExecuteAsync(query, status);
        }

        public async Task UpdateAsync(Status status)
        {
            using var connection = _dbContext.GetConnection();
            var query = @"UPDATE statuses SET 
                        status = @StatusValue, 
                        updated_at = @UpdatedAt 
                        WHERE id = @Id";
            await connection.ExecuteAsync(query, status);
        }

        public async Task DeleteAsync(long id)
        {
            using var connection = _dbContext.GetConnection();
            var query = "DELETE FROM statuses WHERE id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
