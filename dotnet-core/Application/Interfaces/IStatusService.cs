using System.Collections.Generic;
using System.Threading.Tasks;
using XMP.Application.DTOs;

namespace XMP.Application.Interfaces
{
    public interface IStatusService
    {
        Task<IEnumerable<StatusDto>> GetAllStatusesAsync(int pageNumber, int pageSize);
        Task<StatusDto> GetStatusByIdAsync(long id);
        Task<StatusDto> AddStatusAsync(StatusDto statusDto);
        Task UpdateStatusAsync(StatusDto statusDto);
        Task DeleteStatusAsync(long id);
    }
}
