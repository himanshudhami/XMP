using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using XMP.Application.DTOs;
using XMP.Application.Interfaces;
using XMP.Domain.Entities;
using XMP.Domain.Repositories;

namespace XMP.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync(int pageNumber, int pageSize)
        {
            var projects = await _projectRepository.GetAllAsync(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto> GetProjectByIdAsync(long id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectDto> AddProjectAsync(ProjectDto projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);
            await _projectRepository.AddAsync(project);
            return _mapper.Map<ProjectDto>(project); // Return the created DTO
        }

        public async Task UpdateProjectAsync(ProjectDto projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);
            await _projectRepository.UpdateAsync(project);
        }

        public async Task DeleteProjectAsync(long id)
        {
            await _projectRepository.DeleteAsync(id);
        }
    }
}
