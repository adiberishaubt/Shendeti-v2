using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Infrastructure.Interfaces;

public interface IAppointmentRepository
{
    Task CreateAsync(Appointment entity);
    Task<Appointment> ReadAsync(int id);
    Task<List<Appointment>> ReadAllAsync();
    Task UpdateAsync(Appointment updatedEntity);
    Task DeleteAsync(Appointment appointment);
    Task<List<Appointment>> ReadDoctorAppointmentsAsync(string id);
    Task<List<Appointment>> ReadPatientAppointmentsAsync(string id);
}