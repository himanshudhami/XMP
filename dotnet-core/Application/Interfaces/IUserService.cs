using System.Collections.Generic;
using System.Threading.Tasks;
using XMP.Application.DTOs;

namespace XMP.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync(int pageNumber, int pageSize);
        Task<UserDto> GetUserByIdAsync(long id);
        Task<UserDto> AddUserAsync(UserDto userDto);
        Task UpdateUserAsync(UserDto userDto);
        Task DeleteUserAsync(long id);
    }
}

