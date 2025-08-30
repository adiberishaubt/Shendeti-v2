using Microsoft.EntityFrameworkCore;
using Shendeti.Infrastructure.Data;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Extensions;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Repositories;

public class SlotsRepository : ISlotsRepository
{
    private readonly DataContext _dataContext;

    public SlotsRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task CreateAsync(Slot entity)
    {
        await _dataContext.Slots.AddAsync(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<Slot> ReadAsync(int id)
    {
        return await _dataContext.Slots.FirstOrThrowAsync(s => s.Id == id);
    }

    public async Task<List<Slot>> ReadAllAsync()
    {
        return await _dataContext.Slots.ToListAsync();
    }

    public async Task UpdateAsync(Slot updatedEntity)
    {
        _dataContext.Slots.Update(updatedEntity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Slot entity)
    {
        _dataContext.Slots.Remove(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<List<Slot>> ReadDoctorSlots(string id)
    {
        var date = DateTime.Today.AddDays(-(DateTime.Today.DayOfWeek-DayOfWeek.Monday));
        
        var appointments = await _dataContext.Appointments
            .Include(a => a.Slot)
            .Where(a => a.AppointmentDate >= date && a.Doctor.UserId == id)
            .ToListAsync();

        var appointedSlotIds  = appointments.Select(a => a.Slot.Id).ToList();

        var slots = await _dataContext.Slots.Where(s => !appointedSlotIds.Contains(s.Id) && s.Schedule.Doctor.UserId == id).ToListAsync();
            
        return slots;
    }
}