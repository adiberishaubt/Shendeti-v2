using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shendeti.Domain.Interfaces;
using Shendeti.Extensions;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Schedule>>> ReadAllAsync()
    {
        return await _scheduleService.ReadAllAsync(false);
    }
    
    [HttpGet("id")]
    public async Task<ActionResult<Schedule>> ReadAsync(int id)
    {
        return await _scheduleService.ReadAsync(id, false);
    }
    
    [Authorize(Roles = "Doctor")]
    [HttpGet("mine")]
    public async Task<ActionResult<List<Schedule>>> ReadByDoctorAsync()
    {
        return await _scheduleService.ReadByDoctor(User.GetId());
    }
    
    [HttpGet("doctorId")]
    public async Task<ActionResult<List<Schedule>>> ReadByDoctorAsync(string doctorId)
    {
        return await _scheduleService.ReadByDoctor(doctorId);
    }

    [Authorize(Roles = "Doctor")]
    [HttpPost]
    public async Task<ActionResult> CreateAsync(ScheduleRequest scheduleRequest)
    {
        await _scheduleService.CreateAsync(scheduleRequest, User.GetId());
        return Ok();
    }

    [Authorize(Roles = "Doctor")]
    [HttpPut("id")]
    public async Task<ActionResult> UpdateAsync(int id, UpdateScheduleRequest scheduleRequest)
    {
        await _scheduleService.UpdateAsync(id, scheduleRequest, User.GetId());
        return Ok();
    }

    [Authorize(Roles = "Doctor")]
    [HttpDelete("id")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _scheduleService.DeleteAsync(id, User.GetId());
        return Ok();
    }
}