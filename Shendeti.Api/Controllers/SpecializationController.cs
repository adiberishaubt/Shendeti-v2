using Microsoft.AspNetCore.Mvc;
using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Controllers;

[ApiController]
[Route("[controller]")]
public class SpecializationController : ControllerBase
{
    private readonly ISpecializationService _specializationService;

    public SpecializationController(ISpecializationService specializationService)
    {
        _specializationService = specializationService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<Specialization>>> ReadAllAsync(int pageIndex = 1, int pageSize = 10)
    {
        return await _specializationService.ReadAllAsync(pageIndex, pageSize);
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<Specialization>>> ReadAllAsync()
    {
        return await _specializationService.ReadAllAsync();
    }

    [HttpGet("id")]
    public async Task<ActionResult<Specialization>> ReadAsync(int id)
    {
        return await _specializationService.ReadAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(SpecializationRequest specializationRequest)
    {
        await _specializationService.CreateAsync(specializationRequest);
        return Ok();
    }

    [HttpPut("id")]
    public async Task<ActionResult> UpdateAsync(int id, SpecializationRequest specializationRequest)
    {
        await _specializationService.UpdateAsync(id, specializationRequest);
        return Ok();
    }

    [HttpDelete("id")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _specializationService.DeleteAsync(id);
        return Ok();
    }
    
    [HttpGet("id/services")]
    public async Task<ActionResult<List<Service>>> ReadServicesAsync(int id)
    {
        return await _specializationService.ReadServicesAsync(id);
    }
}