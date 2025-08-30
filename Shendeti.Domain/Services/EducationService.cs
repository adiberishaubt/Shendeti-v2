using AutoMapper;
using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.Data;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Exceptions;
using Shendeti.Infrastructure.Extensions;
using Shendeti.Infrastructure.Interfaces;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Domain.Services;

public class EducationService : IEducationService
{
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;

    public EducationService(IEducationRepository educationRepository, DataContext dataContext, IMapper mapper)
    {
        _educationRepository = educationRepository;
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task CreateAsync(EducationRequest entity, string doctorId)
    {
        var doctor = await _dataContext.Doctors.FirstOrThrowAsync(d => d.UserId == doctorId);
        
        Validators.IsFieldOfLength<Education>(entity.Name, nameof(entity.Name), 3);

        var educationEntity = _mapper.Map<Education>(entity);
        educationEntity.Doctor = doctor;
        
        await _educationRepository.CreateAsync(educationEntity);
    }

    public async Task<Education> ReadAsync(int id)
    {
        return await _educationRepository.ReadAsync(id);
    }

    public async Task<List<Education>> ReadAllAsync()
    {
        return await _educationRepository.ReadAllAsync();
    }

    public async Task UpdateAsync(int id, EducationRequest updatedEntity, string doctorId)
    {
        var education = await _educationRepository.ReadAsync(id);
        if (education.Doctor.UserId != doctorId) throw new ValidationException($"Schedule does not belong to doctor with id {doctorId} ");
        
        Validators.IsFieldOfLength<Education>(updatedEntity.Name, nameof(updatedEntity.Name), 3);
        education.Name = updatedEntity.Name;

        await _educationRepository.UpdateAsync(education);
    }

    public async Task DeleteAsync(int id, string doctorId)
    {
        var education = await _educationRepository.ReadAsync(id);
        if (education.Doctor.UserId != doctorId) throw new ValidationException($"Schedule does not belong to doctor with id {doctorId} ");
        await _educationRepository.DeleteAsync(education);
    }

    public async Task<List<Education>> ReadMyEducationsAsync(string doctorId)
    {
        return await _educationRepository.ReadMyEducationsAsync(doctorId);
    }
}