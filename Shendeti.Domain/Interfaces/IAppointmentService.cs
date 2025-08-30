using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Domain.Interfaces;

public interface IAppointmentService
{
    Task CreateAsync(AppointmentRequest appointmentRequest, string patientId);
    Task<Appointment> ReadAsync(int id);
    Task<List<Appointment>> ReadAllAsync();
    Task UpdateAsync(int id, AppointmentRequest entity);
    Task DeleteAsync(int id);
    Task<List<Appointment>> ReadDoctorAppointmentsAsync(string id);
    Task<List<Appointment>> ReadPatientAppointmentsAsync(string id);
}