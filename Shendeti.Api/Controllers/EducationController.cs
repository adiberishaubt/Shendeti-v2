using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shendeti.Domain.Interfaces;
using Shendeti.Extensions;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Controllers;

[ApiController]
[Route("[controller]")]
public class EducationController : ControllerBase
{
    private readonly IEducationService _educationService;

    public EducationController(IEducationService educationService)
    {
        _educationService = educationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Education>>> ReadAllAsync()
    {
        return await _educationService.ReadAllAsync();
    }

    [HttpGet("id")]
    public async Task<ActionResult<Education>> ReadAsync(int id)
    {
        return await _educationService.ReadAsync(id);
    }

    [Authorize(Roles = "Doctor")]
    [HttpGet("mine/id")]
    public async Task<ActionResult<List<Education>>> ReadMyEducationsAsync()
    {
        return await _educationService.ReadMyEducationsAsync(User.GetId());
    }

    [Authorize(Roles = "Doctor")]
    [HttpPost]
    public async Task<ActionResult> CreateAsync(EducationRequest educationRequest)
    {
        await _educationService.CreateAsync(educationRequest, User.GetId());
        return Ok();
    }

    [Authorize(Roles = "Doctor")]
    [HttpPut("id")]
    public async Task<ActionResult> UpdateAsync(int id, EducationRequest educationRequest)
    {
        await _educationService.UpdateAsync(id, educationRequest, User.GetId());
        return Ok();
    }

    [Authorize(Roles = "Doctor")]
    [HttpDelete("id")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _educationService.DeleteAsync(id, User.GetId());
        return Ok();
    }
}