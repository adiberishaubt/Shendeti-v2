using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.Data;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Exceptions;
using Shendeti.Infrastructure.Extensions;
using Shendeti.Infrastructure.Interfaces;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Domain.Services;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public ScheduleService(IScheduleRepository scheduleRepository, DataContext dataContext, IMapper mapper)
    {
        _scheduleRepository = scheduleRepository;
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task CreateAsync(ScheduleRequest entity, string doctorId)
    {
        var doctor = await _dataContext.Doctors.FirstOrThrowAsync(d => d.UserId == doctorId);
        var schedules = await _scheduleRepository.ReadByDoctor(doctorId);
        if (schedules.Any(s => s.Day == entity.Day)) throw new ValidationException($"Schedule for day {entity.Day} already exists");
        Validators.IsValidDay(entity.Day);

        var slots = entity.Slots.Select(s => _mapper.Map<Slot>(s)).ToList();
        var schedule = _mapper.Map<Schedule>(entity);
        
        schedule.Doctor = doctor;
        schedule.Slots = slots;
        
        for (var i = 1; i < schedule.Slots.Count; i++)
        {
            Validators.IsValidSlot(schedule.Slots[i - 1], schedule.Slots[i]);
        }
        
        await _scheduleRepository.CreateAsync(schedule);
    }

    public async Task<Schedule> ReadAsync(int id, bool includeDoctor)
    {
        return await _scheduleRepository.ReadAsync(id, includeDoctor);
    }

    public async Task<List<Schedule>> ReadAllAsync(bool includeDoctor)
    {
        return await _scheduleRepository.ReadAllAsync(includeDoctor);
    }

    public async Task UpdateAsync(int id, UpdateScheduleRequest updatedEntity, string doctorId)
    {
        var schedule = await _scheduleRepository.ReadAsync(id, true);

        if (schedule.Doctor.UserId != doctorId) throw new ValidationException($"The schedule does not belong to doctor with id {doctorId}");
        
        var schedules = await _scheduleRepository.ReadByDoctor(doctorId);
        
        if (updatedEntity.Day.CompareTo(schedule.Day) == 0) throw new ValidationException($"This schedule is already for {updatedEntity.Day}");
        if (schedules.Any(s => s.Day == updatedEntity.Day)) throw new ValidationException($"Schedule for day {updatedEntity.Day} already exists");
        Validators.IsValidDay(updatedEntity.Day);

        schedule.Day = updatedEntity.Day;
        
        await _scheduleRepository.UpdateAsync(schedule);
    }

    public async Task DeleteAsync(int id, string doctorId)
    {
        var schedule = await _scheduleRepository.ReadAsync(id, true);
        
        if (schedule.Doctor.UserId != doctorId) throw new ValidationException($"The schedule does not belong to doctor with id {doctorId}");
        
        await _scheduleRepository.DeleteAsync(schedule);
    }

    public async Task<List<Schedule>> ReadByDoctor(string doctorId)
    {
        return await _scheduleRepository.ReadByDoctor(doctorId);
    }
}