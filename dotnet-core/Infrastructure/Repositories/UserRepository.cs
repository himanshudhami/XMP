using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using XMP.Domain.Entities;
using XMP.Domain.Repositories;
using XMP.Infrastructure.DbContext;

namespace XMP.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperDbContext _dbContext;

        public UserRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<User>> GetAllAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("PageNumber and PageSize must be positive integers.");
            }

            using var connection = _dbContext.GetConnection();
            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;
            var query = $"SELECT * FROM users ORDER BY id OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY";
            return await connection.QueryAsync<User>(query);
        }

        public async Task<User> GetByIdAsync(long id)
        {
            using var connection = _dbContext.GetConnection();
            var query = "SELECT * FROM users WHERE id = @Id";
            return await connection.QuerySingleOrDefaultAsync<User>(query, new { Id = id });
        }

        public async Task AddAsync(User user)
        {
            using var connection = _dbContext.GetConnection();
            var query = @"INSERT INTO users 
                        (email, encrypted_password, reset_password_token, reset_password_sent_at, 
                         remember_created_at, created_at, updated_at, avatar, username, 
                         name, first_name, last_name, gender, mobile_number, role, is_active) 
                         VALUES 
                        (@Email, @EncryptedPassword, @ResetPasswordToken, @ResetPasswordSentAt, 
                         @RememberCreatedAt, @CreatedAt, @UpdatedAt, @Avatar, @Username, 
                         @Name, @FirstName, @LastName, @Gender, @MobileNumber, @Role, @IsActive)";
            await connection.ExecuteAsync(query, user);
        }

        public async Task UpdateAsync(User user)
        {
            using var connection = _dbContext.GetConnection();
            var query = @"UPDATE users SET 
                        email = @Email, 
                        encrypted_password = @EncryptedPassword, 
                        reset_password_token = @ResetPasswordToken, 
                        reset_password_sent_at = @ResetPasswordSentAt, 
                        remember_created_at = @RememberCreatedAt, 
                        updated_at = @UpdatedAt, 
                        avatar = @Avatar, 
                        username = @Username, 
                        name = @Name, 
                        first_name = @FirstName, 
                        last_name = @LastName, 
                        gender = @Gender, 
                        mobile_number = @MobileNumber, 
                        role = @Role, 
                        is_active = @IsActive 
                        WHERE id = @Id";
            await connection.ExecuteAsync(query, user);
        }

        public async Task DeleteAsync(long id)
        {
            using var connection = _dbContext.GetConnection();
            var query = "DELETE FROM users WHERE id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
