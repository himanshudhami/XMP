using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using XMP.Domain.Entities;
using XMP.Domain.Repositories;
using XMP.Infrastructure.DbContext;

namespace XMP.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DapperDbContext _dbContext;

        public ProjectRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Project>> GetAllAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("PageNumber and PageSize must be positive integers.");
            }

            using var connection = _dbContext.GetConnection();
            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;
            var query = $"SELECT * FROM projects ORDER BY id OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY";
            return await connection.QueryAsync<Project>(query);
        }

        public async Task<Project> GetByIdAsync(long id)
        {
            using var connection = _dbContext.GetConnection();
            var query = "SELECT * FROM projects WHERE id = @Id";
            return await connection.QuerySingleOrDefaultAsync<Project>(query, new { Id = id });
        }

        public async Task AddAsync(Project project)
        {
            using var connection = _dbContext.GetConnection();
            var query = @"INSERT INTO projects 
                        (name, created_at, updated_at, key_name) 
                        VALUES 
                        (@Name, @CreatedAt, @UpdatedAt, @KeyName)";
            await connection.ExecuteAsync(query, project);
        }

        public async Task UpdateAsync(Project project)
        {
            using var connection = _dbContext.GetConnection();
            var query = @"UPDATE projects SET 
                        name = @Name, 
                        updated_at = @UpdatedAt, 
                        key_name = @KeyName 
                        WHERE id = @Id";
            await connection.ExecuteAsync(query, project);
        }

        public async Task DeleteAsync(long id)
        {
            using var connection = _dbContext.GetConnection();
            var query = "DELETE FROM projects WHERE id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
