using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using XMP.Application.DTOs;
using XMP.Application.Interfaces;
using XMP.Domain.Entities;
using XMP.Domain.Repositories;

namespace XMP.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(int pageNumber, int pageSize)
        {
            var companies = await _companyRepository.GetAllAsync(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        public async Task<CompanyDto> GetCompanyByIdAsync(long id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<CompanyDto> AddCompanyAsync(CompanyDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);
            await _companyRepository.AddAsync(company);
            return _mapper.Map<CompanyDto>(company); // Return the created DTO
        }

        public async Task UpdateCompanyAsync(CompanyDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);
            await _companyRepository.UpdateAsync(company);
        }

        public async Task DeleteCompanyAsync(long id)
        {
            await _companyRepository.DeleteAsync(id);
        }
    }
}
