using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shendeti.Domain.Interfaces;
using Shendeti.Extensions;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Controllers;

[ApiController]
[Route("[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Appointment>>> ReadAllAsync()
    {
        return await _appointmentService.ReadAllAsync();
    }

    [HttpGet("id")]
    public async Task<ActionResult<Appointment>> ReadAsync(int id)
    {
        return await _appointmentService.ReadAsync(id);
    }
    
    [Authorize(Roles = "Doctor")]
    [HttpGet("doctor")]
    public async Task<ActionResult<List<Appointment>>> ReadDoctorAppointmentsAsync()
    {
        return await _appointmentService.ReadDoctorAppointmentsAsync(User.GetId());
    }
    
    [Authorize(Roles = "Patient")]
    [HttpGet("patient")]
    public async Task<ActionResult<List<Appointment>>> ReadPatientAppointmentsAsync()
    {
        return await _appointmentService.ReadPatientAppointmentsAsync(User.GetId());
    }
    
    [Authorize(Roles = "Patient")]
    [HttpPost]
    public async Task<ActionResult> CreateAsync(AppointmentRequest appointmentRequest)
    {
        await _appointmentService.CreateAsync(appointmentRequest, User.GetId());
        return Ok();
    }
}