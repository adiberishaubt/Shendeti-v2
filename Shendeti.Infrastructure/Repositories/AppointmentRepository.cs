using Microsoft.EntityFrameworkCore;
using Shendeti.Infrastructure.Data;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Extensions;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly DataContext _dataContext;

    public AppointmentRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task CreateAsync(Appointment entity)
    {
        await _dataContext.Appointments.AddAsync(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<Appointment> ReadAsync(int id)
    {
        return await _dataContext.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.Slot)
            .FirstOrThrowAsync(id);
    }

    public async Task<List<Appointment>> ReadAllAsync()
    {
        return await _dataContext.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.Slot)
            .ToListAsync();
    }

    public async Task UpdateAsync(Appointment updatedEntity)
    {
        _dataContext.Appointments.Update(updatedEntity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Appointment appointment)
    {
        _dataContext.Appointments.Remove(appointment);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<List<Appointment>> ReadDoctorAppointmentsAsync(string id)
    {
        return await _dataContext.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Slot)
            .Where(a => a.Doctor.UserId == id).ToListAsync();
    }

    public async Task<List<Appointment>> ReadPatientAppointmentsAsync(string id)
    {
        return await _dataContext.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Slot)
            .Where(a => a.Patient.UserId == id).ToListAsync();
    }
}