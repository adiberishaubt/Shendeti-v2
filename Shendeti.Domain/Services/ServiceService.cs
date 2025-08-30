using AutoMapper;
using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Interfaces;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Domain.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly ISpecializationService _specializationService;
    private readonly IMapper _mapper;

    public ServiceService(IServiceRepository serviceRepository, ISpecializationService specializationService, IMapper mapper)
    {
        _serviceRepository = serviceRepository;
        _specializationService = specializationService;
        _mapper = mapper;
    }

    public async Task CreateAsync(ServiceRequest entity)
    {
        var specialization = await _specializationService.ReadAsync(entity.SpecializationId);
        
        Validators.IsFieldOfLength<Service>(entity.Name, nameof(entity.Name), 3); 
        
        var serviceEntity = _mapper.Map<Service>(entity);
        serviceEntity.Specialization = specialization;
        
        await _serviceRepository.CreateAsync(serviceEntity);
    }

    public async Task<Service> ReadAsync(int id)
    {
        return await _serviceRepository.ReadAsync(id);
    }

    public async Task<PaginatedList<Service>> ReadAllAsync(int pageIndex, int pageSize)
    {
        return await _serviceRepository.ReadAllAsync(pageIndex, pageSize);
    }

    public async Task UpdateAsync(int id, ServiceRequest updatedEntity)
    {
        var service = await _serviceRepository.ReadAsync(id);
        var specialization = await _specializationService.ReadAsync(updatedEntity.SpecializationId);
        
        Validators.IsFieldOfLength<Service>(updatedEntity.Name, nameof(updatedEntity.Name), 3);

        service.Name = updatedEntity.Name;
        service.Specialization = specialization;

        await _serviceRepository.UpdateAsync(service);
    }

    public async Task DeleteAsync(int id)
    {
        var service = await _serviceRepository.ReadAsync(id);
        await _serviceRepository.DeleteAsync(service);
    }
}