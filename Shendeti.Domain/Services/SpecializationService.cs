using AutoMapper;
using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Interfaces;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Domain.Services;

public class SpecializationService : ISpecializationService
{
    private readonly ISpecializationRepository _specializationRepository;
    private readonly IMapper _mapper;

    public SpecializationService(ISpecializationRepository specializationRepository, IMapper mapper)
    {
        _specializationRepository = specializationRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(SpecializationRequest entity)
    {
        Validators.IsFieldOfLength<Specialization>(entity.Name, nameof(entity.Name), 3);
        await _specializationRepository.CreateAsync(_mapper.Map<Specialization>(entity));
    }

    public async Task<Specialization> ReadAsync(int id)
    {
        return await _specializationRepository.ReadAsync(id);
    }
    
    public async Task<List<Specialization>> ReadAllAsync()
    {
        return await _specializationRepository.ReadAllAsync();
    }

    public async Task<PaginatedList<Specialization>> ReadAllAsync(int pageIndex, int pageSize)
    {
        return await _specializationRepository.ReadAllAsync(pageIndex, pageSize);
    }

    public async Task UpdateAsync(int id, SpecializationRequest updatedEntity)
    {
        var specialization = await _specializationRepository.ReadAsync(id);
        Validators.IsFieldOfLength<Specialization>(updatedEntity.Name, nameof(updatedEntity.Name), 3);

        specialization.Name = updatedEntity.Name;
        await _specializationRepository.UpdateAsync(specialization);
    }

    public async Task DeleteAsync(int id)
    {
        var specialization = await _specializationRepository.ReadAsync(id);
        await _specializationRepository.DeleteAsync(specialization);
    }

    public async Task<List<Service>> ReadServicesAsync(int id)
    {
        await _specializationRepository.ReadAsync(id);
        return await _specializationRepository.ReadServicesAsync(id);
    }
}