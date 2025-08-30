using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shendeti.Domain.Interfaces;
using Shendeti.Extensions;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Controllers;

[ApiController]
[Route("[controller]")]
public class SlotsController : ControllerBase
{
    private readonly ISlotsService _slotsService;

    public SlotsController(ISlotsService slotsService)
    {
        _slotsService = slotsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Slot>>> ReadAllAsync()
    {
        return await _slotsService.ReadAllAsync();
    }

    [Authorize(Roles = "Doctor")]
    [HttpGet("mine")]
    public async Task<ActionResult<List<Slot>>> ReadDoctorSlots()
    {
        return await _slotsService.ReadDoctorSlots(User.GetId());
    }
    
    [HttpGet("doctorId")]
    public async Task<ActionResult<List<Slot>>> ReadDoctorSlots(string doctorId)
    {
        return await _slotsService.ReadDoctorSlots(doctorId);
    }
    
    [HttpGet("id")]
    public async Task<ActionResult<Slot>> ReadAsync(int id)
    {
        return await _slotsService.ReadAsync(id);
    }

    [Authorize(Roles = "Doctor")]
    [HttpPut("id")]
    public async Task<ActionResult> UpdateAsync(int id, UpdateSlotRequest slotRequest)
    {
        await _slotsService.UpdateAsync(id, slotRequest, User.GetId());
        return Ok();
    }

    [Authorize(Roles = "Doctor")]
    [HttpPost]
    public async Task<ActionResult> CreateAsync(SlotRequest slotRequest)
    {
        await _slotsService.CreateAsync(slotRequest, User.GetId());
        return Ok();
    }
    
    [Authorize(Roles = "Doctor")]
    [HttpDelete("id")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _slotsService.DeleteAsync(id, User.GetId());
        return Ok();
    }
}