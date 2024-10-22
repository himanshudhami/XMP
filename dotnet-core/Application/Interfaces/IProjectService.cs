using System.Collections.Generic;
using System.Threading.Tasks;
using XMP.Application.DTOs;

namespace XMP.Application.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync(int pageNumber, int pageSize);
        Task<ProjectDto> GetProjectByIdAsync(long id);
        Task<ProjectDto> AddProjectAsync(ProjectDto projectDto);
        Task UpdateProjectAsync(ProjectDto projectDto);
        Task DeleteProjectAsync(long id);
    }
}
