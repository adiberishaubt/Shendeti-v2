using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.Data;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Exceptions;
using Shendeti.Infrastructure.Extensions;
using Shendeti.Infrastructure.Interfaces;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Domain.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly ISlotsRepository _slotsRepository;
    private readonly ISlotsService _slotsService;
    private readonly DataContext _dataContext;
    private readonly IMeetingService _meetingService;

    public AppointmentService(IAppointmentRepository appointmentRepository, DataContext dataContext, ISlotsRepository slotsRepository, IMeetingService meetingService, ISlotsService slotsService)
    {
        _appointmentRepository = appointmentRepository;
        _dataContext = dataContext;
        _slotsRepository = slotsRepository;
        _meetingService = meetingService;
        _slotsService = slotsService;
    }

    public async Task CreateAsync(AppointmentRequest appointmentRequest, string patientId)
    {
        var doctor = await _dataContext.Doctors.FirstOrThrowAsync(d => d.UserId == appointmentRequest.DoctorId);
        var patient = await _dataContext.Patients.FirstOrThrowAsync(p => p.UserId == patientId);
        var slot = await _slotsService.ReadAsync(appointmentRequest.SlotId);
        
        var slots = await _slotsRepository.ReadDoctorSlots(doctor.UserId);

        if (!slots.Contains(slot)) throw new ValidationException("Slot doesn't exist / is already appointed");

        var entity = new Appointment
        {
            Doctor = doctor,
            Patient = patient,
            AppointmentDate = DateTime.Today.Date,
            Slot = slot,
            MeetingType = appointmentRequest.MeetingType
        };

        if (appointmentRequest.MeetingType == MeetingType.ONLINE)
            entity.MeetLink = await _meetingService.CreateMeeting();
        else
            entity.MeetingPlace = doctor.Office;

        await _appointmentRepository.CreateAsync(entity);
    }

    public async Task<Appointment> ReadAsync(int id)
    {
        return await _appointmentRepository.ReadAsync(id);
    }

    public async Task<List<Appointment>> ReadAllAsync()
    {
        return await _appointmentRepository.ReadAllAsync();
    }

    public async Task UpdateAsync(int id, AppointmentRequest entity)
    {
        /*await _appointmentRepository.UpdateAsync(entity);*/
    }

    public async Task DeleteAsync(int id)
    {
        var appointment = await _appointmentRepository.ReadAsync(id);
        await _appointmentRepository.DeleteAsync(appointment);
    }
    
    public async Task<List<Appointment>> ReadDoctorAppointmentsAsync(string id)
    {
        return await _appointmentRepository.ReadDoctorAppointmentsAsync(id);
    }

    public async Task<List<Appointment>> ReadPatientAppointmentsAsync(string id)
    {
        return await _appointmentRepository.ReadPatientAppointmentsAsync(id);
    }
}