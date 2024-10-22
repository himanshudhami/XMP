using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using XMP.Application.DTOs;
using XMP.Application.Interfaces;
using XMP.Domain.Entities;
using XMP.Domain.Repositories;

namespace XMP.Application.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public StatusService(IStatusRepository statusRepository, IMapper mapper)
        {
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StatusDto>> GetAllStatusesAsync(int pageNumber, int pageSize)
        {
            var statuses = await _statusRepository.GetAllAsync(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<StatusDto>>(statuses);
        }

        public async Task<StatusDto> GetStatusByIdAsync(long id)
        {
            var status = await _statusRepository.GetByIdAsync(id);
            return _mapper.Map<StatusDto>(status);
        }

        public async Task<StatusDto> AddStatusAsync(StatusDto statusDto)
        {
            var status = _mapper.Map<Status>(statusDto);
            await _statusRepository.AddAsync(status);
            return _mapper.Map<StatusDto>(status); // Return the created DTO
        }

        public async Task UpdateStatusAsync(StatusDto statusDto)
        {
            var status = _mapper.Map<Status>(statusDto);
            await _statusRepository.UpdateAsync(status);
        }

        public async Task DeleteStatusAsync(long id)
        {
            await _statusRepository.DeleteAsync(id);
        }
    }
}
