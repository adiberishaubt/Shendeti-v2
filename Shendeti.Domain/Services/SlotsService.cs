using AutoMapper;
using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Exceptions;
using Shendeti.Infrastructure.Interfaces;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Domain.Services;

public class SlotsService : ISlotsService
{
    private readonly ISlotsRepository _slotsRepository;
    private readonly IScheduleService _scheduleService;
    private readonly IMapper _mapper;

    public SlotsService(ISlotsRepository slotsRepository, IScheduleService scheduleService, IMapper mapper)
    {
        _slotsRepository = slotsRepository;
        _scheduleService = scheduleService;
        _mapper = mapper;
    }

    public async Task CreateAsync(SlotRequest entity, string doctorId)
    {
        var schedule = await _scheduleService.ReadAsync(entity.ScheduleId, true);
        if (schedule.Doctor.UserId != doctorId) throw new ValidationException($"Schedule does not belong to doctor with id {doctorId} ");
        
        var slotEntity = _mapper.Map<Slot>(entity);
        slotEntity.Schedule = schedule;
        
        schedule.Slots.Add(slotEntity);
        schedule.Slots.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));

        for (var i = 1; i < schedule.Slots.Count; i++)
        {
            Validators.IsValidSlot(schedule.Slots[i-1], schedule.Slots[i]);
        }

        await _slotsRepository.CreateAsync(slotEntity);
    }

    public async Task<Slot> ReadAsync(int id)
    {
        return await _slotsRepository.ReadAsync(id);
    }

    public async Task<List<Slot>> ReadAllAsync()
    {
        return await _slotsRepository.ReadAllAsync();
    }

    public async Task UpdateAsync(int id, UpdateSlotRequest updatedEntity, string doctorId)
    {
        var slot = await _slotsRepository.ReadAsync(id);
        var schedule = await _scheduleService.ReadAsync(slot.ScheduleId, true);
        
        if (schedule.Doctor.UserId != doctorId) throw new ValidationException($"Schedule does not belong to doctor with id {doctorId} ");

        schedule.Slots.Remove(slot);
        
        slot.StartTime = updatedEntity.StartTime;
        slot.EndTime = updatedEntity.EndTime;

        schedule.Slots.Add(slot);
        schedule.Slots.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));

        for (var i = 1; i < schedule.Slots.Count; i++)
        {
            Validators.IsValidSlot(schedule.Slots[i-1], schedule.Slots[i]);
        }

        await _slotsRepository.UpdateAsync(slot);
    }

    public async Task DeleteAsync(int id, string doctorId)
    {
        var slot = await _slotsRepository.ReadAsync(id);
        var schedule = await _scheduleService.ReadAsync(slot.ScheduleId, true);
        
        if (schedule.Doctor.UserId != doctorId) throw new ValidationException($"Schedule does not belong to doctor with id {doctorId} ");
        
        await _slotsRepository.DeleteAsync(slot);
    }

    public async Task<List<Slot>> ReadDoctorSlots(string id)
    {
        return await _slotsRepository.ReadDoctorSlots(id);
    }
}